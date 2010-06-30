using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace jsc.meta.Loader
{
    class LoaderStrategy
    {

        public static void Main(string[] args)
        {
            // if this assembly does not embed the referenced
            // assemblies we might need to tell where to look
            // for them.

            // jsc.meta.exe is at c:\util\bin
            // common libraries are at c:\util\lib

            // note that common libraries could be used in
            // script applications and be used in the compilers too
            // not all common libraries are designed for all platforms

            // new appdomain...
            var WorkerDomain = AppDomain.CreateDomain("Worker");

            // loaded at the moment
            //mscorlib.dll	C:\Windows\assembly\GAC_32\mscorlib\2.0.0.0__b77a5c561934e089\mscorlib.dll	Yes	No	Skipped loading symbols.		1	2.0.50727.4927 (NetFXspW7.050727-4900)	04/06/2009 08:26	71A70000-72568000	[4300] jsc.meta.exe: Managed (v2.0.50727)	
            //jsc.meta.exe	W:\jsc.svn\compiler\jsc.meta\jsc.meta\bin\Debug\jsc.meta.exe	No	Yes	Symbols loaded.	W:\jsc.svn\compiler\jsc.meta\jsc.meta\bin\Debug\jsc.meta.pdb	2	1.0.*	29/06/2010 09:44	00C30000-00D52000	[4300] jsc.meta.exe: Managed (v2.0.50727)	

            // http://it.toolbox.com/blogs/programming-life/enter-the-net-appdomain-5595

            var Worker = WorkerDomain.CreateInstanceAndUnwrap(Assembly.GetCallingAssembly().GetName().Name, typeof(Worker).FullName) as Worker;

            Action Delayed = delegate { };

            Worker.AtDelayedMoveFileAction = new DelayedMoveFileAction
            {
                AtDelayedMoveFileAction = (source, dest) => Delayed += delegate
                {
                    File.Copy(source, dest, true);
                    File.Delete(source);
                }
            };

            Worker.Invoke(args);


            AppDomain.Unload(WorkerDomain);

            Delayed();
        }




    }

    public class DelayedMoveFileAction : MarshalByRefObject
    {
        public Action<string, string> AtDelayedMoveFileAction;

        public void Invoke(string source, string dest)
        {
            AtDelayedMoveFileAction(source, dest);
        }
    }

    public class Worker : MarshalByRefObject
    {
        public DelayedMoveFileAction AtDelayedMoveFileAction;

        public void Invoke(string[] args)
        {
            LoaderStrategyImplementation.Initialize();

            Program.DelayedMoveFile = AtDelayedMoveFileAction.Invoke;
            Program.InternalMain(args);
        }
    }
}
