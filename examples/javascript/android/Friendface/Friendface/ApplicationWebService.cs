using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib.Shared.Lambda;

namespace Friendface
{
    using ystring = Action<string>;
    using ScriptCoreLib.Ultra.WebService;


    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {



        public void AddContact(Action<string> y)
        {
#if DEBUG
            y("John Smith");
            y("Glen Beck");
            y("Jim Stone");
            y("Alex Jones");
            y("Brad Pitt");
#else

            new AndroidContacts.ApplicationWebService().GetContacts("",
                (
                    string id,
                    string name,
                    string email,
                    string gravatar) =>
                {
                    y(name);
                }
            );

#endif
        }

        public void AddTimelineUnit(Action<string> y)
        {
            y("Brad Pitt bough a house on moon.");
        }

        // can we do enums already?
        enum dataside { l, r }

        //DCIMCameraAppWithThumbnails.ApplicationWebService dcim = new DCIMCameraAppWithThumbnails.ApplicationWebService();


        //public void AddTimelinePictureUnit(Action<string, dataside> y)
        public void AddTimelinePictureUnit(Action<string, string> y)
        {
            Console.WriteLine("AddTimelinePictureUnit");

            //            [javac] Compiling 454 source files to I:\bin\classes
            //[javac] I:\src\Friendface\ApplicationWebService.java:10: package ScriptCoreLib.Shared.Lambda does not exist
            //[javac] import ScriptCoreLib.Shared.Lambda.CyclicEnumeratorExtensions;
            //[javac]                                   ^
            //[javac] I:\src\Friendface\ApplicationWebService.java:64: cannot find symbol
            //[javac] symbol  : variable CyclicEnumeratorExtensions
            //[javac] location: class Friendface.ApplicationWebService
            //[javac]         class60.side = CyclicEnumeratorExtensions.<String>AsCyclicEnumerator(__SZArrayEnumerator_1.<String>Of(stringArray1));
            //[javac]                        ^
            //[javac] Note: Some input files use or override a deprecated API.

            // not supported for java just yet?
            //var side = new[] { "l", "r" }.AsCyclicEnumerator();

            var i = 0;

#if FDCIM
			dcim.File_list("",
                stake: "30",

                ydirectory: delegate { },
                yfile: p =>
                {
                    //if (p ends with jpg)
                    if (p.EndsWith(".jpg"))
                    {
                        i++;

                        if (i % 2 == 0)
                            y("/thumb/" + p, "l");
                        else
                            y("/thumb/" + p, "r");
                    }


                }
            );
#endif
		}

        public void Handler(WebServiceHandler h)
        {
            //dcim.Handler(h);
        }
    }
}
