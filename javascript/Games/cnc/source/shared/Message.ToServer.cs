using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace cnc.source.shared
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

        #region EnterLobby
        [Script, Serializable]
        public class _EnterLobby
        {
            public string ReturnValue;
        }

        partial class ClientToServer
        {
            public void EnterLobby(EventHandler<string> done)
            {
                var EnterLobby = new _EnterLobby { };
                var m = new Message { EnterLobby };

                this.Send(m, x => done(x.EnterLobby.ReturnValue));
            }
        }

        public _EnterLobby EnterLobby;
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
    
    }
}