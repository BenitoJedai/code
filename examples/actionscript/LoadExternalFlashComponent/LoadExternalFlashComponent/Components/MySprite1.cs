using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.system;
using System.Xml.Linq;
using System.Linq;
using System;

namespace LoadExternalFlashComponent.Components
{
    internal sealed class MySprite1 : Sprite
    {
        public event Action<XElement> Inspecting;

        public event Action Ready;

        public MySprite1()
        {
            // http://www.adobe.com/livedocs/flash/9.0/ActionScriptLangRefV3/flash/display/Loader.html

            Security.allowDomain("*");
            Security.allowInsecureDomain("*");

            var ldr = new Loader();
            var url = "http://sketch.odopod.com/flash/OdoSketch.swf?sketchURL=/sketches/231498.xml&userURL=/users/21416&bgURL=/images/bigbg.jpg&mode=embed";
            var urlReq = new URLRequest(url);

            var ctx_app = ApplicationDomain.currentDomain;
            var ctx_sec = SecurityDomain.currentDomain;
            
            var ctx = new LoaderContext(true, ctx_app, ctx_sec);

            ldr.contentLoaderInfo.complete +=
                    delegate
                    {
                        if (Ready != null)
                            Ready();

                     
                    };

            ldr.load(urlReq, ctx);

        
            DoClean =
                delegate
                {

                    ldr.content.GetChildren().Where(k => k.GetType().Name == "InfoPanel").ToArray().WithEach(
                        k => k.Orphanize()
                    );
                        
                };

            addChild(ldr);

            var Inspect = default(Action<DisplayObject, XElement>);

            Inspect = (Target, Journal) =>
            {
                var SourceType = Target.GetType();


                var n = new XElement(SourceType.Name);

                n.Add(new XAttribute("Namespace", SourceType.Namespace));

                SourceType.BaseType.With(
                    BaseType =>
                        n.Add(new XAttribute("BaseType", BaseType.FullName))
                );


                Journal.Add(n);

                (Target as DisplayObjectContainer).With(
                    Container =>
                    {
                        for (int i = 0; i < Container.numChildren; i++)
                        {
                            Inspect(Container.getChildAt(i), n);
                        }
                    }
                );
            };

            DoInspect = 
                delegate
                {
                    var doc = new XElement("mxml");

                    // SecurityError: Error #2121: Security sandbox violation: Loader.content: http://localhost:26925/assets/LoadExternalFlashComponent.Application/LoadExternalFlashComponent.Components.MySprite1.swf cannot access http://sketch.odopod.com/flash/OdoSketch.swf?sketchURL=/sketches/231498.xml&userURL=/users/21416&bgURL=/images/bigbg.jpg&mode=embed. This may be worked around by calling Security.allowDomain.
                    //	at flash.display::Loader/get content()


                    Inspect(ldr.content, doc);

                    if (Inspecting != null)
                        Inspecting(doc);
                };



                
        }

        Action DoInspect;
        public void Inspect()
        {
            DoInspect();
        }

        Action DoClean;
        public void Clean()
        {
            DoClean();
        }
    }
}
