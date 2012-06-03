using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using java.net;
using java.lang;
using java.util;
using java.util.zip;
using ScriptCoreLibJava.Extensions;

namespace TestGetFieldsForGAEStatement
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("this code is running inside JVM");
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/201205/20120528-gae-natives

            // c:\util\jsc\bin\jsc.internal.exe RewriteToJavaNatives /source:"C:\util\appengine-java-sdk-1.6.6\lib\impl\appengine-api.jar" /target:xappengine-api.dll /PrimaryType:com.google.cloud.sql.jdbc.Statement /FilterMembersTo:CLOSE_CURRENT_RESULT /DisableWorkerDomain 

            var jar = @"C:\util\appengine-java-sdk-1.6.6\lib\impl\appengine-api.jar";
            var TypeFullName = "com.google.cloud.sql.jdbc.Statement";

            // compiler how do we load a jar and get the class and inspect it as System.Type within JVM? :)
            var t = jar.ToType(TypeFullName);

            var a = t.GetFields();

            foreach (var item in a)
            {
                Console.WriteLine(item);

                //if (item.IsStatic)
                //    Console.Write("static ");

                //Console.WriteLine("field " + item.Name + " declared by " + item.DeclaringType.FullName);
            }
            //CLRProgram.CLRMain();
        }
    }

    #region class loader
    static class X
    {
        public static Type ToType(this string jar, string TypeFullName)
        {
            Type t = null;
            try
            {
                var url = new java.io.File(jar).toURL();
                var clazzLoader = new InternalURLClassLoader(new URL[] { url }, null);

                var c = clazzLoader.loadClass(TypeFullName);

                t = c.ToType();

            }
            catch
            {
            }
            return t;
        }
    }

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

                c = base.loadClass(name, resolve);
            }
            catch (csharp.ThrowableException cc)
            {
                Console.WriteLine("InternalURLClassLoader.loadClass error, name: " + name + "; " + cc + "; " + cc.Message);

                // what should we do with the missing types?
                // we can only return null as it is going to gail anyhow

                //throw new InvalidOperationException();
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

            }

            return c;
        }



        protected Class __findClass(string name, string x)
        {
            //Console.WriteLine("__findClass: " + name);

            var c = default(Class);

            try
            {
                c = base.findClass(name);
            }
            catch (csharp.ThrowableException ex)
            {

                Console.WriteLine("InternalURLClassLoader.__findClass error, name: " + name + ", x: " + x + "; " + ex);
                throw new InvalidOperationException();
            }

            return c;
        }
        #endregion

    }

    static class CharExtensions
    {
        public static bool StartsWithNumber(this string c)
        {
            if (c == null)
                return false;

            if (c.Length == 0)
                return false;

            return c[0].IsNumber();
        }

        public static bool IsNumber(this char c)
        {
            if (c >= '0')
                if (c <= '9') return true;

            return false;
        }
    }


    #endregion


    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.WriteLine(Path.GetFileName(Assembly.GetExecutingAssembly().Location));

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(Path.GetFullPath(item.Location) + " types: " + item.GetTypes().Length);
            }

            // if jsc supported pdb rewrite we could have a break point over here!

            MessageBox.Show(
                caption: "hello",
                text: new StackTrace(0, true).ToString(),
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Information
            );

            Console.WriteLine("done!");
        }
    }
}
