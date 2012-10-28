using java.applet;
using System;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;

namespace TestAsXElementByApplet
{
    public sealed class ApplicationApplet : Applet
    {
        public override void init()
        {
            base.resize(400, 300);
        }

        public void Foo(XElement PageContainer, Action<XElement> y)
        {
            try
            {
                //{ Message = , StackTrace = java.lang.RuntimeException
                //    at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__List_1.GetEnumerator(__List_1.java:126)
                //    at ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__List_1.System_Collections_Generic_IEnumerable_1_GetEnumerator(__List_1.java:216)
                //    at ScriptCoreLib.Extensions.LinqExtensions.InternalWithEach(LinqExtensions.java:209)
                //    at ScriptCoreLib.Extensions.LinqExtensions.WithEach(LinqExtensions.java:39)
                //    at TestAsXElementByApplet.ApplicationApplet.Foo(ApplicationApplet.java:127)


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
            catch (Exception ex)
            {
                Console.WriteLine(new { ex.Message, ex.StackTrace });
            }
        }
    }
}
