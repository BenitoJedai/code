using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;

namespace org.elasticdroid
{
    public class ApplicationActivity : org.elasticdroid.LoginView
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        class local__EC2_START_INSTANCE : __EC2_START_INSTANCE
        {
            public Action AtInvoke;

            public override void Invoke()
            {
                AtInvoke();
            }
        }

        public override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            __EC2_START_INSTANCE.Handler = new local__EC2_START_INSTANCE
            {
                AtInvoke = delegate
                {
                    Toast.makeText(
                         this,
                         "! __EC2_START_INSTANCE",
                         Toast.LENGTH_LONG
                     ).show();
                }
            };

            Toast.makeText(
                 this,
                 "http://jsc-solutions.net",
                 Toast.LENGTH_LONG
             ).show();
        }


    }
}
