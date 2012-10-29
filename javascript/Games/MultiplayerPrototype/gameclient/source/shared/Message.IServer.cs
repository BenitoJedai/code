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
            public void MethodA(string A, string B, System.Action<string> done)
            {
                var MethodA = new _MethodA { A = A, B = B };
                var m = new Message { MethodA = MethodA };

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
            public void CreateExplosionAt(int x, int y, System.Action done)
            {
                var CreateExplosionAt = new _CreateExplosionAt { x = x, y = y };
                var m = new Message { CreateExplosionAt = CreateExplosionAt };
                this.Send(m, h => done());
            }
        }

        public _CreateExplosionAt CreateExplosionAt;
        #endregion


        #region TalkToOthers
        [Script, Serializable]
        public class _IServer_TalkToOthers
        {
            public string text;
        }

        partial class ClientToServer
        {
            public void IServer_TalkToOthers(string text)
            {
                var IServer_TalkToOthers = new _IServer_TalkToOthers { text = text };
                var m = new Message { IServer_TalkToOthers = IServer_TalkToOthers };

                this.Send(m, null);
            }
        }

        public _IServer_TalkToOthers IServer_TalkToOthers;
        #endregion


        #region EnterLobby
        [Script, Serializable]
        public class _IServer_EnterLobby
        {
            public string ReturnValue;
        }

        partial class ClientToServer
        {
            public void IServer_EnterLobby(System.Action<string> done)
            {
                var IServer_EnterLobby = new _IServer_EnterLobby { };
                var m = new Message { IServer_EnterLobby = IServer_EnterLobby };

                this.Send(m, x => done(x.IServer_EnterLobby.ReturnValue));
            }
        }

        public _IServer_EnterLobby IServer_EnterLobby;
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
                var IServer_DrawRectangle = new _IServer_DrawRectangle { rect = rect, color = color };
                var m = new Message { IServer_DrawRectangle = IServer_DrawRectangle };

                this.Send(m, null);
            }
        }

        public _IServer_DrawRectangle IServer_DrawRectangle;
        #endregion


        #region SpawnHarvester
        [Script, Serializable]
        public class _IServer_SpawnHarvester
        {
            public Point Location;
            public int Direction;

            //public string ReturnValue;
        }

        partial class ClientToServer
        {
            public void IServer_SpawnHarvester( Point Location, int Direction)
            {
                var IServer_SpawnHarvester = new _IServer_SpawnHarvester { Location = Location, Direction = Direction };
                var m = new Message { IServer_SpawnHarvester = IServer_SpawnHarvester };

                this.Send(m, null);
            }
        }

        public _IServer_SpawnHarvester IServer_SpawnHarvester;
        #endregion
    }
}