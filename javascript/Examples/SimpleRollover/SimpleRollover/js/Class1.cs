//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Controls.Effects;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

//using global::System.Collections.Generic;



namespace SimpleRollover.js
{


    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";


        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            // wallpapers at http://labnol.blogspot.com/2006/11/download-windows-vista-wallpapers.html

            #region AnimateCharacterColors
            Func<string, INode> AnimateCharacterColors =
                (text) =>
                {
                    var s = new IHTMLSpan();

                    var l = new global::System.Collections.Generic.List<IHTMLSpan>();

                    foreach (char c in text)
                    {
                        var y = "" + c;
                        var x = new IHTMLSpan(y);

                        if (y == " ")
                        {
                            s.appendChild(" ");
                        }
                        else
                        {
                            l.Add(x);


                            s.appendChild(x);
                        }

                        
                    }



                    new Timer(
                        t =>
                        {
                            var len = l.Count + 40;

                            if (t.Counter % len < l.Count)
                            {
                                if (t.Counter % (len * 2) < l.Count)
                                {
                                    l[t.Counter % len].style.visibility = IStyle.VisibilityEnum.hidden;
                                }
                                else
                                {
                                    l[t.Counter % len].style.visibility = IStyle.VisibilityEnum.visible;
                                }
                            }


                                
                        }, 6000, 100);


                    return s;
                };
            #endregion

            var u = new IHTMLDiv();

            var ad = new IHTMLDiv(
                            new IHTMLSpan(
                                AnimateCharacterColors("this application was written in c# and then translated to javascript by jsc to run in your browser")
                            ),
                            new IHTMLAnchor("http://zproxy.wordpress.com", "visit blog"),
                            new IHTMLAnchor("http://jsc.sf.net", "get more examples")
                         )
                         {
                             className = "ad1"
                         };

            u.appendChild(ad);

            var sheet = new IStyleSheet();

            sheet.AddRule(".ad1",
                r =>
                {
                    r.style.marginTop = "1em";
                    r.style.color = Color.White;
                    r.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
                }
            );


            sheet.AddRule(".ad1 > *",
                r =>
                {
                    r.style.padding = "1em";
                    
                    r.style.marginTop = "1em";
                }
            );

            sheet.AddRule(".ad1 > span",
                r =>
                {
                    r.style.Float = IStyle.FloatEnum.right;
                }
            );

            sheet.AddRule(".ad1 > a",
                r =>
                {
                    r.style.Float = IStyle.FloatEnum.left;
                    r.style.color = Color.White;

                    r.style.textDecoration = "none";
                }
            );

            sheet.AddRule(".ad1 a:hover",
                r =>
                {
                    r.style.color = Color.Yellow;
                }
            );



            sheet.AddRule("body",
                r =>
                {
                    r.style.padding = "0";
                    r.style.margin = "0";
                    r.style.overflow = IStyle.OverflowEnum.auto;
                    r.style.backgroundImage = "url(assets/vista.jpg)";
                }
            );

            sheet.AddRule(".special1",
                r =>
                {
                    r.style.background = "none";
                    r.style.border = "0";
                    r.style.width = "100%";
                    r.style.marginTop = "4em";


                }
            );

            sheet.AddRule(".content1",
                r =>
                {
                    r.style.backgroundColor = Color.White;

                    r.style.padding = "1em";
                    r.style.marginLeft = "4em";
                    r.style.marginRight = "4em";
                    r.style.Opacity = 0.5;
                    r.style.border = "1px solid gray";
                }
            );

            sheet.AddRule(".special1 img", "border: 0", 0);
            sheet.AddRule(".special1:hover", "background: url(assets/Untitled-3.png) repeat-x", 1);

            sheet.AddRule(".special1 .hot").style.display = IStyle.DisplayEnum.none;
            sheet.AddRule(".special1:hover .hot").style.display = IStyle.DisplayEnum.inline;

            sheet.AddRule(".special1 .cold", "display: inline;", 1);
            sheet.AddRule(".special1:hover .cold", "display: none;", 1);


            var states = new[] { new { Show = default(Action), Hide = default(Action), Selected = false } }.Where(p => false);


            ActionParams<string> Spawn =
                i =>
                    ((IHTMLImage)i[0]).InvokeOnComplete(cold =>
                    ((IHTMLImage)i[1]).InvokeOnComplete(hot =>
                         {
                             cold.className = "cold";
                             hot.className = "hot";

                             var btn = new IHTMLButton()
                                 {
                                     className = "special1"
                                 };

                             btn.appendChild(cold, hot);

                             var content = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre);

                             content.innerHTML = "...";
                             content.className = "content1";

                             var tween = new TweenDataDouble();
                             var tween_max = 32;

                             tween.ValueChanged +=
                                 delegate
                                 {
                                     content.style.Opacity = tween.Value / tween_max;
                                     content.style.height = tween.Value + "em";

                                     content.style.overflow = IStyle.OverflowEnum.hidden;

                                 };

                             tween.Done += delegate
                             {
                                 if (tween.Value > 0)
                                     content.style.overflow = IStyle.OverflowEnum.auto;
                             };

                             tween.Value = 0;

                             var state = new
                                {
                                    Show = (Action)(() =>
                                                        {
                                                            tween.Value = tween_max;
                                                        }
                                    ),
                                    Hide = (Action)(() => tween.Value = 0),
                                    Selected = false
                                };

                             try
                             {
                                 new IXMLHttpRequest(HTTPMethodEnum.GET, i[2],
                                    request => content.innerHTML = request.responseText
                                 );
                             }
                             catch
                             {
                                 content.innerHTML = string.Format("navigate to <a href='{0}'>{0}</a> manually", i[2]);
                             }

                             states = states.Concat(new[] { state });

                             btn.onclick +=
                                 delegate
                                 {
                                     foreach (var v in states)
                                     {
                                         if (v == state)
                                         {

                                             v.Selected = !v.Selected;

                                             if (v.Selected)
                                             {
                                                 v.Show();
                                             }
                                             else
                                             {
                                                 v.Hide();
                                             }

                                         }
                                         else
                                         {
                                             v.Selected = false;
                                             v.Hide();
                                         }
                                     }
                                 };

                             u.appendChild(btn, content);
                         }
                    ));

            u.attachToDocument();

            Spawn("assets/Untitled-1_03.png", "assets/Untitled-2_03.png", "assets/cs.htm");
            Spawn("assets/Untitled-1_07.png", "assets/Untitled-2_07.png", "assets/js.htm");
        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
