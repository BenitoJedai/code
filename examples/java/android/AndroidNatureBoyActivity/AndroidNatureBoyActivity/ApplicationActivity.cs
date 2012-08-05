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

namespace AndroidNatureBoyActivity.Activities
{
    public class ApplicationActivity : xavalon.net.WebServiceServerActivity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            this.ApplicationFile = "";

            base.onCreate(savedInstanceState);

            
        }


    }


}
