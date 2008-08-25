using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Runtime;
using Console = System.Console;

namespace gameclient.source.js
{
    using shared;

    [Script]
    public partial class ClientSession : ClientToServerBase, Message.IClient
    {

        public Controls.DemoControl Control;

        public void CreateExplosionByServer(int x, int y, string text)
        {
            Control.DrawExplosion(x, y);

            Console.WriteLine("create the damn explosion at " + x + ", " + y);
        }

        public void IClient_DisplayNotification(string text, int color)
        {
            Control.DisplayNotification(text, (Color)color);
        }

        public void ForceReload()
        {
            Console.WriteLine("server told us to reload!");

            Native.Document.location.reload();
        }


        public void IClient_DrawRectangle(RectangleInfo rect, int color)
        {

        }


        #region IClient Members


        public void IClient_SpawnHarvester(Point Location, int Direction)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
