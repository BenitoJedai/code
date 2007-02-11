using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace cnc.source.shared
{
    using Serializable = System.SerializableAttribute;

    public partial class Message
    {


        #region CreateExplosionByServer
        [Script, Serializable]
        public class _CreateExplosionByServer
        {
            public int x;
            public int y;
            public string text;

        }

        partial class ServerToClient
        {
            public void CreateExplosionByServer(int x, int y, string text, EventHandler done)
            {
                var CreateExplosionByServer = new _CreateExplosionByServer { x, y, text };
                var m = new Message { CreateExplosionByServer };
                this.Send(m, h => done());
            }
        }

        public _CreateExplosionByServer CreateExplosionByServer;
        #endregion


        #region DisplayNotification
        [Script, Serializable]
        public class _DisplayNotification
        {
            public string text;
            public int color;
        }

        partial class ServerToClient
        {
            public void DisplayNotification(string text, int color)
            {
                var DisplayNotification = new _DisplayNotification { text, color };
                var m = new Message { DisplayNotification };

                this.Send(m, null);
            }
        }

        public _DisplayNotification DisplayNotification;
        #endregion


    }
}