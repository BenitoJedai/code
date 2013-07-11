using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Friendface
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        public void AddContact(Action<string> y)
        {
            y("John Smith");
            y("Glen Beck");
            y("Jim Stone");
            y("Alex Jones");
            y("Brad Pitt");
        }

        public void AddTimelineUnit(Action<string> y)
        {
            y("Brad Pitt bough a house on moon.");
        }

        // can we do enums already?
        enum dataside { l, r }

        //public void AddTimelinePictureUnit(Action<string, dataside> y)
        public void AddTimelinePictureUnit(Action<string, string> y)
        {
            y(Data.Images.robot_invasion.GetImageAssetPath(), "l");
            y(Data.Images.robot_invasion.GetImageAssetPath(), "r");
        }

    }
}
