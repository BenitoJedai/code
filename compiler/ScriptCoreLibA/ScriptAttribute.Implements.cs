using System.Runtime.CompilerServices;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib.CSharp.Extensions;

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
            get
            {
                if (InternalImplements == null)
                    if (!string.IsNullOrEmpty(this.ImplementsViaAssemblyQualifiedName))
                    {
                        // yay.
                        this.InternalImplements = Type.GetType(this.ImplementsViaAssemblyQualifiedName);

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
