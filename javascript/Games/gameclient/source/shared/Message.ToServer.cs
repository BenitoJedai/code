using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace gameclient.source.shared
{
    using Serializable = System.SerializableAttribute;

    public partial class Message
    {
        #region MethodA
        [Script, Serializable]
        public class _MethodA
        {
            public string A;
            public string B;

            public string ReturnValue;
        }

        partial class ClientToServer
        {
            public void MethodA(string A, string B, EventHandler<string> done)
            {
                var MethodA = new _MethodA { A, B };
                var m = new Message { MethodA };

                this.Send(m, x => done(x.MethodA.ReturnValue));
            }
        }

        public _MethodA MethodA;
        #endregion

        #region CreateExplosionAt
        [Script, Serializable]
        public class _CreateExplosionAt
        {
            public int x;
            public int y;

        }

        partial class ClientToServer
        {
            public void CreateExplosionAt(int x, int y, EventHandler done)
            {
                var CreateExplosionAt = new _CreateExplosionAt { x, y };
                var m = new Message { CreateExplosionAt };
                this.Send(m, h => done());
            }
        }

        public _CreateExplosionAt CreateExplosionAt;
        #endregion


        #region TalkToOthers
        [Script, Serializable]
        public class _TalkToOthers
        {
            public string text;
        }

        partial class ClientToServer
        {
            public void TalkToOthers(string text)
            {
                var TalkToOthers = new _TalkToOthers { text };
                var m = new Message { TalkToOthers };

                this.Send(m, null);
            }
        }

        public _TalkToOthers TalkToOthers;
        #endregion


        #region EnterLobby
        [Script, Serializable]
        public class _ToServer_EnterLobby
        {
            public string ReturnValue;
        }

        partial class ClientToServer
        {
            public void ToServer_EnterLobby(EventHandler<string> done)
            {
                var ToServer_EnterLobby = new _ToServer_EnterLobby { };
                var m = new Message { ToServer_EnterLobby };

                this.Send(m, x => done(x.ToServer_EnterLobby.ReturnValue));
            }
        }

        public _ToServer_EnterLobby ToServer_EnterLobby;
        #endregion


        #region DrawRectangle
        [Script, Serializable]
        public class _IServer_DrawRectangle
        {
            public RectangleInfo rect;
            public int color;

            public string ReturnValue;
        }

        partial class ClientToServer
        {
            public void IServer_DrawRectangle( RectangleInfo rect, int color)
            {
                var IServer_DrawRectangle = new _IServer_DrawRectangle { rect , color};
                var m = new Message { IServer_DrawRectangle };

                this.Send(m, null);
            }
        }

        public _IServer_DrawRectangle IServer_DrawRectangle;
        #endregion
    }
}