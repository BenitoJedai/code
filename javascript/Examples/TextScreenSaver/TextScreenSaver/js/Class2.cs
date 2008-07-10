using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;

namespace TextScreenSaver.js
{


    [Script/*, ScriptApplicationEntryPoint(IsClickOnce = true)*/]
    public class Class2
    {
        public static readonly Qoutes.DocumentList DefaultData =
            new Qoutes.DocumentList
                {
                    Documents = new[]
                    {
                        new Qoutes.DocumentRef {
                            Document = new Qoutes.Document
                            {
                                Topic = "Debug2",
                                Count = "10",
                                Style = new Qoutes.Style
                                {
                                    BackgroundColor = "black",
                                    Color = "white",
                                    HoverColor = "red"
                                },
                                Content = "Hello world1\nHello world2"
                            }
                        }
                    }
                };

        public readonly Qoutes.DocumentList Data;


        public Class2(Qoutes.DocumentList Data)
        {
            this.Data = Data;
        }



        static Class2()
        {
            typeof(Class2).SpawnTo<Qoutes.DocumentList>(Qoutes.Settings.KnownTypes, i => new Class2(i));
        }
    }
}
