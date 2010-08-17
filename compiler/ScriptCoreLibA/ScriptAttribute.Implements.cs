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
                    }

                return InternalImplements;
            }
            set
            {
                InternalImplements = value;
            }
        }

        /// <summary>
        /// Supports redirecting BCLImplementation type while the target is visible.
        /// </summary>
        public string ImplementsViaAssemblyQualifiedName { get; set; }

    }



}
