using System.Runtime.CompilerServices;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib.CSharp.Extensions;
using System.Diagnostics;

namespace ScriptCoreLib
{
    partial class ScriptAttribute
    {
        internal Type InternalImplements;



        /// <summary>
        /// Supports redirecting BCLImplementation.
        /// </summary>
        public Type Implements
        {
            // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

            get
            {
                if (InternalImplements == null)
                    if (!string.IsNullOrEmpty(this.ImplementsViaAssemblyQualifiedName))
                    {
                        // yay.
                        // shall we inspect current context?

                        var Candidates = AppDomain.CurrentDomain.GetAssemblies()
                            .Select(x => x.GetType(this.ImplementsViaAssemblyQualifiedName))
                            .Where(x => x != null)
                            .OrderBy(x => x.Assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.MSIL)
                            .ToArray();

                        if (Candidates.Length > 1)
                        {
                            if (Debugger.IsAttached)
                                Debugger.Break();
                        }

                        //this.InternalImplements = Type.GetType(this.ImplementsViaAssemblyQualifiedName);
                        this.InternalImplements = Candidates.FirstOrDefault();

                        if (this.InternalImplements != null)
                            this.ImplementsViaAssemblyQualifiedName = null;
                        // cannot be both!
                    }

                return InternalImplements;
            }
            set
            {
                InternalImplements = value;
            }
        }

        // Supports redirecting BCLImplementation type while the target is visible.
        // 
        // Example for F# interactive: typeof[System.Tuple].AssemblyQualifiedName;;
        // x:\jsc.svn\examples\javascript\Test\TestGetAwaiter\TestGetAwaiter\Class1.cs
        public string ImplementsViaAssemblyQualifiedName { get; set; }

    }



}
