using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Net;


namespace jsXMLhttpRequest
{
    [Script, ScriptApplicationEntryPoint]
    public class WebApplication
    {
        [Script(OptimizedCode= @"
            var a = '<b><code>' + (typeof (e)) + '</code></b><br />';



            var z = function (m) 
            {
                var style='';

                try
                {
                    var isok = false;

                    try { isok = (m in e); } catch (exc) { isok = false; }

                    var xtype = typeof void(0);

                    if (isok)
                    {
                        try { xtype = typeof e[m]; } catch (exc) { isok = false; }
                    }

                    
                    
                    style += 'color: darkcyan;';
                    style += 'padding-left: 16px;';
                    style += 'padding-right: 3px;';

                    
                    
                    if (xtype == 'function') 
                        style += 'background: url(gfx/method.gif) no-repeat;';

                    if (xtype == 'string'
                        || xtype == 'object'
                        || xtype == 'boolean'
                        || xtype == 'number') 
                        style += 'background: url(gfx/field.gif) no-repeat;';
                }
                catch (exc)
                {
                    isok = false; 

                    a += 'error';
                }                
                
                var codestyle = '';

                codestyle = isok ? 'color: darkgreen;' : 'color: red;';

                a += '(<code style=""' + style + '"">' + xtype + '</code>) <code style=""' + codestyle + '"">' + m + '</code>;<br />'; 
            };

            
            

    
            try
            {
                for (var i in m)
                    z(m[i]);

                for (var i in e)
                    z(i);

                
            }
            catch (exc)
            {
                a += 'wont enum members';
            }

            return a;

        ")]
        static string TypeDebug(object e, params string[] m)
        {
            return default(string);
        }

        public static string asx;

        static void TypeDebugAppend(object x, params string[] m)
        {
            IHTMLDiv td = new IHTMLDiv();

            
            
            
            Native.Document.body.appendChild(td);

            td.innerHTML = TypeDebug(x, m);
        }

        static void FlashElement(IStyle x, Color a, Color b)
        {

            Timer t = null;

            t = new Timer(
                delegate
                {
                    x.color = ((t.Counter % 2) == 0) ? a : b;

                });
            
            t.StartInterval(30, 10);
        }

        static WebApplication()
        {
            Native.Document.write("hello world");

            Native.Window.onload +=
                delegate
                {
                    IHTMLElement colorbtn = new IHTMLButton( "Color");

                    colorbtn.onclick += colorbtn_onclick;

                    Native.Document.body.appendChild(colorbtn);

                    IHTMLDiv x = new IHTMLDiv("this is a hover text");

                    x.style.cursor = IStyle.CursorEnum.pointer;

                    x.onmouseover +=
                        delegate
                        {
                            x.style.color = Color.Red;
                        };

                    x.onmouseout +=
                       delegate
                       {

                           FlashElement(x.style, Color.Blue, Color.Red);

                       };

                    Native.Document.body.appendChild(x);

                    IXMLHttpRequest r = new IXMLHttpRequest();

                    r.open(HTTPMethodEnum.GET, "data.xml");
                    r.send();

                    Timer t = null;

                    t = new Timer(
                        delegate
                        {
                            if (r.complete)
                            {
                                // http://www.xml.com/pub/a/2003/03/19/dive-into-xml.html
                                Native.Document.body.appendChild(new ITextNode(r.responseXML == null ? "fail" : "ok"));
                                Native.Document.body.appendChild(new ITextNode(r.responseText));

                                t.Stop();

                                if (r.responseXML != null)
                                {
                                    TypeDebugAppend(r.responseXML, 
                                        "createElement",
                                        "selectNodes",
                                        "childNodes");

                                }

                                
                            }
                            else
                            {
                                Native.Document.body.appendChild(new ITextNode("."));
                            }
                        }
                        );
                    
                    t.StartInterval(300);
                };
        }

        static void colorbtn_onclick(IEvent e)
        {
            IHTMLButton btn = (IHTMLButton)(e.Element);

            btn.disabled = true;

            ColorDialogBeta cd = new ColorDialogBeta();

            cd.Show();

            cd.DialogClosed += delegate { btn.disabled = false; };
            
        }
    }


}
