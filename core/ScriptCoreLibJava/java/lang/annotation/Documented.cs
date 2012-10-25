using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace java.lang.annotation
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/annotation/Documented.html
    [Script(IsNative = true)]
    public interface Documented : Annotation
    {

    }

    [Script(IsNative = true)]
    public class DocumentedAttribute : Attribute
    {
    }
}

