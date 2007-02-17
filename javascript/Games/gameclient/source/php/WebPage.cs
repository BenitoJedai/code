using ScriptCoreLib;

using ScriptCoreLib.PHP;

namespace gameclient.source.php
{
    [Script]
    static class WebPage
    {
        public const string Entrypoint = "WebPageEntry";
        public const string Filename = "MyWebPage.php";

        /// <summary>
        /// php script will invoke this method
        /// </summary>
        [Script(NoDecoration = true)]
        public static void WebPageEntry()
        {
            Native.echo("hello world (php)");
            Native.Link("see html for javascript DemoControl", js.Controls.DemoControl.Alias + ".htm");
        }
    }
}
