using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestQuerySelectorFromServer
{
    // query element
    // XHTMLElement
    class QElement
    {
        public ApplicationWebService Context;


        public string selectorText;


        public class PStyle
        {
            public QElement Context;

            public string selectorTextSuffix = "";

            public PStyle this[string selectorTextSuffix]
            {
                get
                {
                    return new PStyle { Context = Context, selectorTextSuffix = this.selectorTextSuffix + selectorTextSuffix };
                }
            }

            public PStyle hover
            {
                get
                {

                    return this[":hover"];

                }
            }


            public PStyle after
            {
                get
                {

                    return this[":after"];

                }
            }

            public string color
            {
                set { this.setProperty("color", value); }
            }

            public void setProperty(
                string styleName,
                string styleValue
                )
            {
                Context.Context.query_setStyle(
                    Context.selectorText + selectorTextSuffix,
                    styleName,
                    styleValue
                );
            }
        }

        public PStyle css
        {
            get { return new PStyle { Context = this }; }
        }

        public XElement InternalElement = new XElement("q");
        public QMethod InternalElementConstuctor;

        public string innerText
        {
            get
            {
                return InternalElement.Value;
            }
            set
            {
                InternalElement.Value = value;

                if (InternalElementConstuctor == null)
                {
                    Context.query_setInnerText(selectorText, value);
                }
                else
                {
                    //Context.query_setInternalElement(selectorText, InternalElement);
                }
            }
        }



        [Obsolete("by implementing anonymous callbacks, we can also do Task<>")]
        public event Action<QElement> onclick
        {
            add
            {
                if (!value.Method.IsStatic)
                {
                    // do we know how to serialize scope closure?
                    Debugger.Break();
                }

                Context.query_onclick(
                    this.selectorText,

                    // will this work for js?
                    // or will this stay on server for now?
                    new QMethod { MetadataToken = value.Method.MetadataToken }

                );
            }
            remove
            {
            }

        }
    }

    public class QMethod
    {
        public int MetadataToken;

        public int TamperSignature = 666;
    }


    public delegate void __query_setInternalElement(string selectorText, XElement InternalElement);
    public delegate void __query_onclick(string selectorText, QMethod qmethod);



    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // next step is interface or event
        // or actually just allow to move this into a separate type
        // what about chrome webservice and web workers?
        // what about other examples like this?
        // what about code sent in as a handler which can also run on the server?
        // what about code sent to the client which can also run on the client?
        // how did we do interface support for flash?
        // class __handlers
        // {
        public __query_setInternalElement query_setInternalElement;
        public Action<string, string> query_setInnerText;
        public Action<string, string, string> query_setStyle;

        public Action __document_location_reload;
        public __query_onclick query_onclick;
        // }



        //        ILStringConversion Prepare System.Action`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //ILStringConversion Prepare System.Action`3[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //ILStringConversion Prepare System.Action`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[TestQuerySelectorFromServer.QMethod, TestQuerySelectorFromServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
        //2448:01:01 RewriteToAssembly error: System.ArgumentException: Duplicate type name within an assembly.
        //   at System.Reflection.Emit.TypeBuilder.DefineType(RuntimeModule module, String fullname, Int32 tkParent, TypeAttributes attributes, Int32 tkEnclosingType, Int32[] interfaceTokens)

        public Task __QMethod_Invoke(QMethod qmethod, string selectorText, XElement InternalElement)
        {
            // how will jsc know all the possible inline calls?
            // do we have to do a trace of which methods are callable?
            // basically whoever calls onclick
            // shall be made callable as it calls query_onclick
            // which means it goes to the client
            // its like java throws.
            // this time areound its more [resumable]
            var m = typeof(ApplicationWebService).Assembly.ManifestModule.ResolveMethod(qmethod.MetadataToken);


            var before = InternalElement.ToString();

            var x = new QElement
            {
                selectorText = selectorText,
                Context = this,
                InternalElement = InternalElement,
                InternalElementConstuctor = qmethod
            };



            if (m.IsStatic)
            {
                m.Invoke(null, new[] { x });
            }

            var after = InternalElement.ToString();

            if (before != after)
            {
                this.query_setInternalElement(selectorText, InternalElement);
            }

            var xx = new TaskCompletionSource<string>();
            xx.SetResult("");
            return xx.Task;
        }

        public Task Fill()
        {
            // see also
            // X:\jsc.svn\examples\javascript\css\CSSContentDataSource\CSSContentDataSource\Application.cs


            {
                var selector = "[data-column='Column 1']";

                query_setInnerText(
                    selector,
                    new { selector, foo = "bar" }.ToString()
                );
            }

            {
                var x = new QElement { selectorText = "[data-column='Column 2']", Context = this };



                x.innerText = new { x.selectorText, foo = "bar 2" }.ToString();
                x.css.color = "blue";
                x.css.setProperty("text-decoration", "underline");
                x.css.hover.color = "red";

                x.css["[special]"].after.setProperty("content", "' !!! after a secondary click this text will appear by a css rule. the next click will reload the page via the server call'");

                x.onclick +=
                    e =>
                    {
                        if (e.InternalElement.Attribute("special") == null)
                        {
                            if (e.innerText == "you clicked me!")
                            {
                                e.innerText = "you double clicked me!";
                                e.css.color = "purple";

                                e.css.setProperty("font-weight", "bold");

                                // Additional information: Duplicate attribute.

                                e.InternalElement.Attribute("special").With(
                                    a => a.Remove()
                                    );


                                e.InternalElement.Add(
                                    new XAttribute("special", "333")
                                    );
                            }
                            else
                            {
                                e.innerText = "you clicked me!";
                                e.css.color = "green";
                            }


                            return;
                        }


                        // reload after doubleclick
                        e.Context.__document_location_reload();
                    };
            }

            var xx = new TaskCompletionSource<string>();
            xx.SetResult("");
            return xx.Task;

        }

    }
}
