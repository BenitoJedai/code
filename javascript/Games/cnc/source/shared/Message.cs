using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace cnc.source.shared
{
    using Serializable = System.SerializableAttribute;

    [Script]
    public abstract class AsyncProxy
    {
        public const string ReturnValue = "ReturnValue";

        [Script]
        public class Info
        {
            public Message m;
            public EventHandler<Message> done;

            public bool IsPending;
        }

        

        protected virtual void Send(Message m, EventHandler<Message> done)
        {

        }
    }


    /// <summary>
    /// this is the class that gets sent to the server back and forth
    /// </summary>
    [Script, Serializable]
    public partial class Message
    {
        /// <summary>
        /// defines proxy calls, so that parameters can be serialized
        /// </summary>
        [Script]
        public abstract partial class ClientToServer : AsyncProxy { }

        /// <summary>
        /// defines the methods, the server must implement
        /// </summary>
        [Script]
        public partial interface IServer 
        {
            string MethodA(string A, string B);

            void CreateExplosionAt(int x, int y);

            string EnterLobby();

            void TalkToOthers(string text);
        }
        public int ToServerMessageId;

        [Script]
        public abstract partial class ServerToClient : AsyncProxy { }

        public partial interface IClient
        {
            [Script(NoDecoration = true)]
            void CreateExplosionByServer(int x, int y, string text);

            [Script(NoDecoration = true)]
            void DisplayNotification(string text, int color);
        }

        public int ToClientMessageId;

    }
}