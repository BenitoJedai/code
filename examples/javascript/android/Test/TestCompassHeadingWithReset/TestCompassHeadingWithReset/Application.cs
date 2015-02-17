using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestCompassHeadingWithReset;
using TestCompassHeadingWithReset.Design;
using TestCompassHeadingWithReset.HTML.Pages;

namespace TestCompassHeadingWithReset
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var compassHeadingOffset = 0.0;
            var compassHeadingInitialized = 0;
            var lon1 = 0.0;

            Native.body.Clear();

            var div0 = new IHTMLDiv { }.AttachToDocument();

            Native.document.onclick +=
                delegate
                {
                    div0 = new IHTMLDiv { }.AttachToDocument();
                    compassHeadingInitialized = 0;
                };


            #region compassHeading
            Native.window.ondeviceorientation +=
              dataValues =>
              {
                  // Convert degrees to radians
                  var alphaRad = dataValues.alpha * (Math.PI / 180);
                  var betaRad = dataValues.beta * (Math.PI / 180);
                  var gammaRad = dataValues.gamma * (Math.PI / 180);

                  // Calculate equation components
                  var cA = Math.Cos(alphaRad);
                  var sA = Math.Sin(alphaRad);
                  var cB = Math.Cos(betaRad);
                  var sB = Math.Sin(betaRad);
                  var cG = Math.Cos(gammaRad);
                  var sG = Math.Sin(gammaRad);

                  // Calculate A, B, C rotation components
                  var rA = -cA * sG - sA * sB * cG;
                  var rB = -sA * sG + cA * sB * cG;
                  var rC = -cB * cG;

                  // Calculate compass heading
                  var compassHeading = Math.Atan(rA / rB);

                  // Convert from half unit circle to whole unit circle
                  if (rB < 0)
                  {
                      compassHeading += Math.PI;
                  }
                  else if (rA < 0)
                  {
                      compassHeading += 2 * Math.PI;
                  }

                  /*
                  Alternative calculation (replacing lines 99-107 above):

                    var compassHeading = Math.atan2(rA, rB);

                    if(rA < 0) {
                      compassHeading += 2 * Math.PI;
                    }
                  */

                  // Convert radians to degrees
                  compassHeading *= 180 / Math.PI;

                  // Compass heading can only be derived if returned values are 'absolute'

                  if (compassHeadingInitialized > 0)
                  {
                      lon1 = compassHeading - compassHeadingOffset;
                  }
                  else
                  {
                      compassHeadingOffset = compassHeading;
                      compassHeadingInitialized++;
                  }

                  div0.innerText = new { lon1, compassHeading, compassHeadingOffset }.ToString();
              };
            #endregion
        }

    }
}
