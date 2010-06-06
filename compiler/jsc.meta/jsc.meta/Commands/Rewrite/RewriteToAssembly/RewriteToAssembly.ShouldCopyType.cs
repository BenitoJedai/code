using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;
using jsc;

namespace jsc.meta.Commands.Rewrite
{
    partial class RewriteToAssembly
    {
        public event Action<AtShouldCopyTypeTuple> AtShouldCopyType;

        private bool ShouldCopyAssembly(Assembly ContextAssembly)
        {
            return PrimaryTypes.Any(k => k.Assembly == ContextAssembly)
                ||
                this.merge.Any(k => k.name == ContextAssembly.GetName().Name);
        }

        private bool ShouldCopyType(Type ContextType)
        {
            if (ContextType.IsGenericParameter)
                return false;

            if (ContextType.IsGenericType)
                if (!ContextType.IsGenericTypeDefinition)
                {
                    if (ShouldCopyType(ContextType.GetGenericTypeDefinition()))
                    {
                        return true;
                    }
                    else
                    {
                        // the type itself is not copied. what about arguments passed in?

                        if (ContextType.GetGenericArguments().Any(ShouldCopyType))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }


                }

            var t = new AtShouldCopyTypeTuple { ContextType = ContextType };

            if (AtShouldCopyType != null)
                AtShouldCopyType(t);

            if (t.DisableCopyType)
                return false;

            return ShouldCopyAssembly(ContextType.Assembly)
                || (!DisableIsMarkedForMerge && IsMarkedForMerge(ContextType));
        }



    }
}
