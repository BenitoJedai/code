using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibAppJet;
using ScriptCoreLibAppJet.JavaScript.Library;
using CakeCuttingProblemAppJet.Library;
using ScriptCoreLibAppJet.JavaScript;
using CakeCuttingProblem.Library;

namespace CakeCuttingProblemAppJet
{
    [Script]
    public static class Server
    {
        [Script, Serializable]
        public sealed class DataItem
        {
            public string id;

            public string Key;
            public string Value;
        }

        [Script, Serializable]
        public sealed class SmartClient
        {
            public string id;

            public string url;
            public string data;
        }


        public static void Render()
        {
            // /* appjet:version 0.1 */ 

            Native.page.setMode("plain");



            Func<string, Action<string>> WithColor =
                    color =>
                        text =>
                        {
                            ("<div style='color: " + color + "'>" + text + "</div>").ToConsole();


                            text.ToConsole();
                        };





            DemoSituation.Demo(
                WithColor("red"),
                WithColor("green"),
                WithColor("blue"),
                WithColor("yellow")
            );   




        }

        static Server()
        {
            Native.import("storage");
            Render();
        }
    }
}
