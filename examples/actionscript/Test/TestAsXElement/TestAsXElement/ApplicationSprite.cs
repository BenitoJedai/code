using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;
using System.Xml.Linq;

namespace TestAsXElement
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
        }

        // jsc, why not allow static methods
        public void Foo(XElement PageContainer, Action<XElement> y)
        {
            PageContainer.Elements().WithEach(
                e =>
                {
                    e.Elements().WithEach(
                        span =>
                        {
                            // what about xmlns like facebook?
                            span.Add(new XElement("code", " (" + span.Name.LocalName + ")"));
                        }
                    );

                    e.Add(new XElement("code", " (element of PageContainer)"));
                }
            );


            PageContainer.Elements().Elements("span").WithEach(
                span =>
                {
                    span.Add(
                        new XAttribute("style", "color: red;")
                    );

                }
            );

            y(PageContainer);
        }
    }
}
