using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using java.lang;

namespace TestCharSequenceAsStringAndConst.Activities
{
    class XFoo
    {
        public static readonly int uu = 55;

        // jsc does not generate consts?
        public const int u = 5;

        public XFoo()
        {

        }

        public XFoo(CharSequence e)
        {

        }

        public static void invoke(CharSequence e)
        {

        }
    }

    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        XFoo foo;
        Native.Foo nfoo;

        protected override void onCreate(Bundle savedInstanceState)
        {
            //            Error	1	The expression being assigned to 'TestCharSequenceAsStringAndConst.Activities.ApplicationActivity.uuu' must be constant	y:\jsc.svn\examples\java\Test\TestCharSequenceAsStringAndConst\TestCharSequenceAsStringAndConst\ApplicationActivity.cs	40	32	TestCharSequenceAsStringAndConst
            //Error	3	Argument 1: cannot convert from 'string' to 'java.lang.CharSequence'	y:\jsc.svn\examples\java\Test\TestCharSequenceAsStringAndConst\TestCharSequenceAsStringAndConst\ApplicationActivity.cs	37	31	TestCharSequenceAsStringAndConst


            Native.Foo.invoke("hello");
        }

        public const int uuu = Native.Foo.uu;

    }


}
