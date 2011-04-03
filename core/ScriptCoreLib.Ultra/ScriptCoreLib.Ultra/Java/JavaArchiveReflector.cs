using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Java
{
    public partial class JavaArchiveReflector : IEnumerable, IJavaArchiveReflector
    {
        public delegate Type GetType();

   
        public delegate DynamicEnumerator GetDynamicEnumeratorFunc();
        GetDynamicEnumeratorFunc GetDynamicEnumerator;

        public delegate Entry GetNextEntry();



        public class DynamicEnumerator : IEnumerator
        {
            public object Current
            {
                get;
                set;
            }

            public GetNextEntry GetNextEntry;
            public static implicit operator DynamicEnumerator(GetNextEntry e)
            {
                return new DynamicEnumerator { GetNextEntry = e };
            }

            public bool MoveNext()
            {
                if (GetNextEntry == null)
                    return false;

                this.Current = GetNextEntry();

                if (this.Current == null)
                {
                    this.GetNextEntry = null;
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }


        public JavaArchiveReflector(FileInfo jar)
        {
            this.FileName = jar;

            var clazzLoader = default(URLClassLoader);

            try
            {
                //var filePath = "jar:file://" + jar.FullName + "!/";
                var filePath = jar.FullName;
                var url = new java.io.File(filePath).toURL();

                Console.WriteLine("JavaArchiveReflector url: " + url);

                clazzLoader = new URLClassLoader(new URL[] { url });
            }
            catch
            {
                Console.WriteLine("error @URLClassLoader");
            }



            this.GetDynamicEnumerator = () =>
            {

                var zip = default(ZipInputStream);

                try
                {
                    zip = new ZipInputStream(new FileInputStream(jar.FullName));
                }
                catch
                {
                    Console.WriteLine("error @ ZipInputStream");
                }

                return (GetNextEntry)
                    delegate
                    {
                        if (zip == null)
                            return null;

                        var e = default(ZipEntry);

                        try
                        {
                            e = zip.getNextEntry();
                        }
                        catch
                        {
                        }

                        if (e == null)
                            return null;



                        var n = new Entry { Name = e.getName() };

                        if (clazzLoader != null)
                            if (n.Name.EndsWith(".class"))
                            {

                                n.TypeFullName = n.Name.Substring(0, n.Name.Length - ".class".Length).Replace("/", ".");


                                n.InternalGetType = delegate
                                {

                                    var c = default(java.lang.Class);


                                    try
                                    {
                                        c = clazzLoader.loadClass(n.TypeFullName);
                                    }
                                    catch (csharp.ThrowableException cc)
                                    {
                                        Console.WriteLine("error @loadClass: " + n.TypeFullName + "; " + cc);

                                        throw new InvalidOperationException();
                                    }

                                    return c.ToType();
                                };
                            }

                        return n;
                    };
            };
        }

        public IEnumerator GetEnumerator()
        {
            return GetDynamicEnumerator();
        }

        public FileInfo FileName
        {
            get;
            private set;
        }

        public string FileNameString
        {
            get { return FileName.FullName; }
        }

        Entry[] InternalEntries;
        public Entry[] Entries
        {
            get
            {
                if (InternalEntries == null)
                {
                    var a = new ArrayList();

                    foreach (Entry item in this)
                    {
                        a.Add(item);
                    }

                    InternalEntries = (Entry[])a.ToArray(typeof(Entry));
                }

                return InternalEntries;
            }
        }

        public int Count
        {
            get
            {

                return Entries.Length;
            }
        }


        public string GetTypeFullName(int index)
        {
            return this.Entries[index].TypeFullName;
        }


        public Entry this[string TypeFullName]
        {
            get
            {
                var i = IndexOf(TypeFullName);

                EnsureElementWasFound(i);

                return this.Entries[i];
            }
        }

        private static void EnsureElementWasFound(int i)
        {
            if (i < 0)
                throw new InvalidOperationException();
        }

        public int IndexOf(string TypeFullName)
        {
            var i = -1;

            for (int j = 0; j < this.Entries.Length; j++)
            {
                // fixme: JSC should drop the use of string.equals
                if (TypeFullName == this.Entries[j].TypeFullName)
                {
                    i = j;
                    break;
                }
            }

            return i;
        }
    }

}
