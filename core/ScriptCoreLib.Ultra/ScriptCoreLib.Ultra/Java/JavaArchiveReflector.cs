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
using ScriptCoreLib.Java.Extensions;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using java.lang;

namespace ScriptCoreLib.Java
{
    public class InternalURLClassLoader : URLClassLoader
    {
        // one of the jobs of any class loader is to protect the system name space.
        // The normal ClassLoader heirarchy does address this by always delegating to the parent ClassLoader first,
        // thus ensuring that duplicate classes are not loaded

        // If you use custom ClassLoader, do not give the system one a chance to come into play.
        // http://blog.cyberborean.org/2007/07/04/custom-classloaders-the-black-art-of-java

        public InternalURLClassLoader(URL[] u, ClassLoader parent)
            : base(u, parent)
        {

        }


        protected override Class loadClass(string name, bool resolve)
        {
            //Console.WriteLine(".loadClass: " + name);

            var c = default(Class);



            try
            {
                if (ClassCache.ContainsKey(name))
                {
                    var LoadedClass = (Class)ClassCache[name];

                    c = LoadedClass;
                }
                else if (ClassBytes.ContainsKey(name))
                {
                    var Bytes = (sbyte[])ClassBytes[name];

                    var DefineName = name;

                    var LoadedClass = this.defineClass(DefineName, Bytes, 0, Bytes.Length);

                    ClassCache[name] = LoadedClass;

                    c = LoadedClass;
                }
                else
                {
                    c = base.loadClass(name, resolve);
                }
            }
            catch
            {
                throw;
            }

            //Console.WriteLine(".loadClass: " + c.GetDeclaringFile());

            return c;
        }



        #region findClass
        protected override Class findClass(string name)
        {
            //Console.WriteLine("findClass: " + name);

            var c = default(Class);

            try
            {
                c = base.findClass(name);
            }
            catch (csharp.ThrowableException ex)
            {
                //Console.WriteLine("error: " + ex);

                c = findClass_catch(name);
            }

            return c;
        }

        public ScriptCoreLib.Java.JavaArchiveReflector.JavaArchiveResolveHandler Resolve;

        private Class findClass_catch(string name)
        {
            var f = Resolve(name);
            var c = default(Class);

            //if (name == "com.amazonaws.AmazonWebServiceRequest")
            if (f == null)
                throw new InvalidOperationException();


            //var x = @"C:\util\aws-android-sdk-0.2.0\lib\aws-android-sdk-0.2.0-core.jar";
            var x = f.FullName;

            //Console.WriteLine("will look into: " + x);

            try
            {
                var url = new java.io.File(x).toURL();
                this.addURL(url);
            }
            catch
            {
                throw new InvalidOperationException();
            }

            c = __findClass(name);


            return c;
        }

        protected Class __findClass(string name)
        {
            //Console.WriteLine("__findClass: " + name);

            var c = default(Class);

            try
            {
                c = base.findClass(name);
            }
            catch (csharp.ThrowableException ex)
            {

                Console.WriteLine("__findClass error: " + ex);
                throw new InvalidOperationException();
            }

            return c;
        }
        #endregion


        public Hashtable ClassCache = new Hashtable();
        public Hashtable ClassBytes = new Hashtable();

        public void CacheClassBytes(FileInfo jar)
        {
            // experimental..
            try
            {
                Console.WriteLine(".cache: " + jar.FullName);

                var zip = default(ZipInputStream);


                zip = new ZipInputStream(new FileInputStream(jar.FullName));


                var Current = zip.getNextEntry();

                while (Current != null)
                {
                    var Name = Current.getName();

                    if (Name.EndsWith(".class"))
                    {
                        var TypeName = Name.Substring(0, Name.Length - ".class".Length).Replace("/", ".");

                        if (TypeName.StartsWith("java."))
                        {
                            // we cannot use ClassLoader to load such class anyhow..
                            // what we probably would need is a JVM parser now...
                        }
                        else
                        {
                            var Memory = zip.ReadToMemoryStream();

                            this.ClassBytes[TypeName] = Memory.ToArray();
                        }
                    }

                    Current = zip.getNextEntry();
                }

            }
            catch (csharp.ThrowableException cc)
            {
                Console.WriteLine("error @CacheClassBytes: " + cc);

                throw new InvalidOperationException();
            }
        }
    }

    public partial class JavaArchiveReflector : IJavaArchiveReflector
    {
        public delegate Type GetType();


        public delegate DynamicEnumerator GetDynamicEnumeratorFunc();
        GetDynamicEnumeratorFunc GetDynamicEnumerator;

        public delegate Entry GetNextEntry();

        public delegate FileInfo JavaArchiveResolveHandler(string name);


        #region DynamicEnumerator
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
        #endregion

        public event JavaArchiveResolveHandler JavaArchiveResolve;


        // readonly private ? :) javac would fail.. could jsc do the smart thing here?
        public InternalURLClassLoader clazzLoader;

        public JavaArchiveReflector(FileInfo jar)
        {
            this.FileName = jar;

            #region clazzLoader
            this.clazzLoader = default(InternalURLClassLoader);

            try
            {
                //var filePath = "jar:file://" + jar.FullName + "!/";

                // http://download.oracle.com/javase/1.4.2/docs/api/java/io/File.html
                // file:///C:/util/aws-android-sdk-0.2.0/lib/aws-android-sdk-0.2.0-ec2.jar

                // error @URLClassLoader: java.net.MalformedURLException: unknown protocol: c
                var url = new java.io.File(jar.FullName).toURL();

                // http://www.javakb.com/Uwe/Forum.aspx/java-programmer/34778/URLClassLoader-ClassNotFoundException
                // http://www.chinaup.org/docs/reference/java/net/URLClassLoader.html
                // http://www.docjar.com/html/api/sun/applet/AppletClassLoader.java.html


                // http://www.docjar.com/html/api/java/net/URLClassLoader.java.html
                // http://www.docjar.com/html/api/java/security/SecureClassLoader.java.html

                clazzLoader = new InternalURLClassLoader(new URL[] { url }, null);

            }
            catch (csharp.ThrowableException ex)
            {
                Console.WriteLine("error @URLClassLoader: " + ex);
                throw new InvalidOperationException();
            }
            #endregion


            //clazzLoader.CacheClassBytes(jar);

            #region Resolve
            clazzLoader.Resolve =
                name =>
                {
                    var f = default(FileInfo);

                    if (this.JavaArchiveResolve != null)
                    {
                        f = this.JavaArchiveResolve(name);
                        //clazzLoader.CacheClassBytes(f);
                    }

                    return f;
                };
            #endregion




            #region GetDynamicEnumerator
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
                    throw new InvalidOperationException();
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
                                        //Console.WriteLine(".deprecated loadClass: " + n.TypeFullName);

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
            #endregion

        }

        public void LoadTypes()
        {
            // http://www.artima.com/insidejvm/ed2/linkmod5.html
            // http://www.artima.com/javaseminars/modules/DynaExt/index.html
            // http://www.javaworld.com/javaworld/jw-10-1996/jw-10-indepth.html
            // http://www.javaworld.com/javaworld/jw-10-1996/indepth/indepth.src.html

        }

        //public IEnumerator GetEnumerator()
        //{
        //    return GetDynamicEnumerator();
        //}

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

                    var e = this.GetDynamicEnumerator();

                    while (e.MoveNext())
                    {
                        var item = (Entry)e.Current;
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
