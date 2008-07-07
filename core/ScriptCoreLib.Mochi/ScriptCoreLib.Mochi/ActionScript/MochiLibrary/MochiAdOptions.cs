using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.MochiLibrary.Ad;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.MochiLibrary
{
    [Script]
    public sealed class MochiAdOptions
    {
        readonly DynamicContainer Data;

        public MochiAdOptions()
        {
            Data = new DynamicContainer { Subject = new object() };

            ad_finished = delegate { };
            ad_started = delegate { };
        }

        public string id
        {
            set { Data["id"] = value; }
        }

        public string res
        {
            set { Data["res"] = value; }
        }

        public Action ad_started
        {
            set { Data["ad_started"] = value.ToFunction(); }
        }

        public Action ad_finished
        {
            set { Data["ad_finished"] = value.ToFunction(); }
        }

        public object clip
        {
            set { Data["clip"] = value; }
        }

        public void showPreGameAd()
        {

            MochiAd.showPreGameAd(Data.Subject);
        }
    }


}
