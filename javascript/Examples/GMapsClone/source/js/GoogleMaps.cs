using ScriptCoreLib;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace GMapsClone.source.js
{

    [Script]
    class GoogleMaps
    {
        public string World;

        IHTMLImage UBase;

        int xWidth = 256;
        int xHeight = 256;

        int xWidth2 = 256 * 2;
        int xHeight2 = 256 * 2;

        int xOffset = 8;
        int yOffset = 32;

        IHTMLDiv A = new IHTMLDiv();
        IHTMLDiv B = new IHTMLDiv();
        IHTMLDiv C = new IHTMLDiv();
        IHTMLDiv D = new IHTMLDiv();



        //IHTMLDiv ULeft = new IHTMLDiv();
        
        // the big map on the right side
        IHTMLImage URight = new IHTMLImage();

        IHTMLButton Up = new IHTMLButton("Up ");
        IHTMLDiv Description = new IHTMLDiv();

        WorkPool DetailedImages = new WorkPool(1000);

        private string _location = "";

        public string Location
        {
            get 
            { 
                return _location; 
            }
            set 
            {
                _location = value;

                Up.innerHTML = "Up to level " + _location.Length;
                Up.disabled = _location.Length == 0;

                string src = World + value;

                Console.WriteLine(src + ", level=" + this.ZoomLevel);

                UBase.src = src;

                
                //ULeft.style.backgroundImage = "";
                //URight.style.backgroundImage = "";

                A.style.backgroundImage = "";
                B.style.backgroundImage = "";
                C.style.backgroundImage = "";
                D.style.backgroundImage = "";

                UBase.InvokeOnComplete(
                    delegate
                    {
                      


                         //ULeft.style.backgroundImage = "url(" + UBase.src + ")";
                         //URight.style.backgroundImage = "url(" + UBase.src + ")"; 
                        DetailedImages["details"]
                        =
                        delegate
                        {
                            A.style.backgroundImage = "url(" + UBase.src + "q)";
                            A.title = A.style.backgroundImage;

                            B.style.backgroundImage = "url(" + UBase.src + "r)";
                            B.title = B.style.backgroundImage;

                            C.style.backgroundImage = "url(" + UBase.src + "t)";
                            C.title = C.style.backgroundImage;

                            D.style.backgroundImage = "url(" + UBase.src + "s)";
                            D.title = D.style.backgroundImage;
                        };
                    }
                );
            }
        }

        public int BaseZoomLevel = 1;

        int ZoomLevel
        {
            get
            {
                return BaseZoomLevel + _location.Length;
            }
        }
        

        public GoogleMaps(string _world, IHTMLElement e)
        {
            this.World = _world;
            this.UBase = new IHTMLImage(World);

            Native.Document.body.DisableContextMenu();

            UBase.style.SetLocation(xOffset, yOffset, xWidth * 2, xHeight * 2);

            Description.innerHTML = "zoom with wheel, or click on a tile to zoom in. (<a href='http://jsc.svn.sourceforge.net/viewvc/jsc/javascript/Examples/GMapsClone/source/js/GoogleMaps.cs?view=markup'>sourcecode</a>)";
            Description.style.SetLocation(200, 8);

            
            Up.style.SetLocation(64, 6, 64, 20);

            
            
            A.style.SetLocation(xOffset, yOffset, xWidth, xHeight);
            B.style.SetLocation(xOffset + xWidth, yOffset, xWidth, xHeight);
            C.style.SetLocation(xOffset, yOffset + xHeight, xWidth, xHeight);
            D.style.SetLocation(xOffset + xWidth, yOffset + xHeight, xWidth, xHeight);



            //ULeft.style.SetLocation(xOffset - xWidth2, yOffset, xWidth2, xHeight2);
            //URight.style.SetLocation(xOffset + xWidth2, yOffset, xWidth2, xHeight2);
            //URight.style.backgroundColor = Color.Gray;

           
            
            Up.onclick += delegate { GoUp(); };



            BindWheel(A, delegate { Location += "q"; });
            BindWheel(B, delegate { Location += "r"; });
            BindWheel(C, delegate { Location += "t"; });
            BindWheel(D, delegate { Location += "s"; });

            A.onclick += delegate { Location += "q"; };
            B.onclick += delegate { Location += "r"; };
            C.onclick += delegate { Location += "t"; };
            D.onclick += delegate { Location += "s"; };


            Location = "";

            Native.Document.body.appendChild(UBase, A, B, C, D, Up, Description);

        }

        private void BindWheel(IHTMLDiv div, EventHandler GoDown)
        {
            div.onmousewheel += delegate (IEvent ev)
            {
                if (ev.WheelDirection > 0)
                    GoDown();
                else
                    GoUp();
            };

        }

        string GetUpperLocation(int z)
        {
                return Location.Substring(0, Location.Length - z);
        }

        private void GoUp()
        {
            Location = GetUpperLocation(1);
        }



        
    }
}
