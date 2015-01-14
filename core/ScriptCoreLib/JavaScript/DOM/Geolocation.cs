using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{

    // http://www.w3.org/TR/geolocation-API/
    // http://simpl.info/geolocation/
    // https://www.chromestatus.com/features/6348855016685568

    [Script(HasNoPrototype = true, ExternalTarget = "Geolocation")]
    public class Geolocation
    {
        // tested by
        // X:\jsc.svn\examples\javascript\Test\TestGeolocation\TestGeolocation\Application.cs


        // available from service worker?

        // <meta name="mobile-web-app-capable" content="yes">

        public void getCurrentPosition(Action<Position> successCallback)
        {

        }
    }

    [Script(HasNoPrototype = true, ExternalTarget = "Position")]
    public class Position
    {
        public readonly Coordinates coords;

    }

    [Script(HasNoPrototype = true, ExternalTarget = "Coordinates")]
    public class Coordinates
    {
        public readonly double latitude;
        public readonly double longitude;
    }

    [Script]
    public static class GeolocationExtensions
    {

        public static Task<Position> getCurrentPosition(this Geolocation g)
        {
            var x = new TaskCompletionSource<Position>();

            g.getCurrentPosition(x.SetResult);


            return x.Task;
        }

    }
}
