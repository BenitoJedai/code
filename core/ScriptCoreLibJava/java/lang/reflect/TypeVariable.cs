using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace java.lang.reflect
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/reflect/TypeVariable.html
    [Script(IsNative = true)]
    public interface TypeVariable<D> : Type
    {
        Type[] getBounds();

        D getGenericDeclaration();

        string getName();
    }
}
