using FakeWindowsLoginExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeWindowsLoginExperiment.Design
{
    sealed class FakeLoginScreen
    {
        public FakeLoginScreen(IFakeLoginScreen e)
        {
            e.Image.onclick +=
                delegate
                {
                    e.Image = new HTML.Images.FromAssets.DBeforeLogon { id = e.Image.id };

                    e.Image.onclick +=
                       delegate
                       {
                           e.Image = new HTML.Images.FromAssets.EBeforeUsername { id = e.Image.id };


                           e.Image.onclick +=
                              delegate
                              {
                                  e.Image = new HTML.Images.FromAssets.FBeforePassword { id = e.Image.id };

                                  e.Image.onclick +=
                                    delegate
                                    {
                                        e.Image.Orphanize();

                                        Native.Document.location.replace("/Application");
                                    };

                              };
                       };
                };
        }
    }

}
