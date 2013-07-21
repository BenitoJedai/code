using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Drawing;

namespace SpaceInvadersTemplate.Library
{
    using TweenDataDouble = ScriptCoreLib.JavaScript.Controls.Effects.TweenDataDouble;


    namespace Controls
    {
        /// <summary>
        /// makes the page look dark and displays another layer in the front
        /// </summary>
        public class Overlay
        {
            public readonly IHTMLDiv ControlInBack = new IHTMLDiv();
            public readonly IHTMLDiv ControlInFront = new IHTMLDiv();
            
            TweenDataDouble t = new TweenDataDouble();

            public Overlay()
            {
                t.ValueChanged +=
                    delegate
                    {
                        var z = t.Value / this.MaximumOpacity;

                        ControlInBack.style.Opacity = t.Value;
                        ControlInFront.style.Opacity = z;
                    };

                t.Done +=
                    delegate
                    {
                        if (!_Visible)
                        {
                            ControlInBack.Orphanize();
                            ControlInFront.Orphanize();
                        }
                    };

                Native.Window.onresize +=
                    delegate
                    {
                        if (_Visible)
                            UpdateLocation();
                    };

                Native.Window.onscroll +=
                    delegate
                    {
                        if (_Visible)
                            UpdateLocation();

                    };

                BackgroundColor = Color.Black;

                t.Value = 0;
            }

            public void UpdateLocation()
            {
                var w = Native.Window.Width;
                var h = Native.Window.Height;

                ControlInBack.style.SetLocation(Native.Document.body.scrollLeft,
                    Native.Document.body.scrollTop, w, h);

                ControlInFront.SetCenteredLocation(Native.Document.body.scrollLeft +w / 2, Native.Document.body.scrollTop + h / 2);

            }

            private Color _Color;

            public Color BackgroundColor
            {
                get { return _Color; }
                set { _Color = value; this.ControlInBack.style.backgroundColor = value; }
            }

            public double MaximumOpacity = 0.6;

            private bool _Visible;

            public bool Visible
            {
                get { return _Visible; }
                set
                {
                    _Visible = value;

                    if (value)
                    {
                        ControlInBack.style.position = IStyle.PositionEnum.absolute;
                        ControlInBack.AttachToDocument();
                        ControlInBack.style.SetLocation(0, 0);

                        ControlInFront.style.position = IStyle.PositionEnum.absolute;
                        ControlInFront.AttachToDocument();
                        ControlInFront.style.SetLocation(0, 0);

                        ControlInBack.style.backgroundColor = BackgroundColor;
                        ControlInBack.style.display = IStyle.DisplayEnum.block;
                        

                        UpdateLocation();
                        
                        t.Value = MaximumOpacity;
                    }
                    else
                    {
                        t.Value = 0.0;
                    }
                }
            }


        }




    }

}