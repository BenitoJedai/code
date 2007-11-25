using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace gameclient.source.shared
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


            void IServer_TalkToOthers(string text);

            string IServer_EnterLobby();

            void IServer_DrawRectangle(RectangleInfo rect, int color);

            void IServer_SpawnHarvester(Point Location, int Direction);
        }

        public int ToServerMessageId;

        [Script]
        public abstract partial class ServerToClient : AsyncProxy { }

        public partial interface IClient
        {
            [Script(NoDecoration = true)]
            void CreateExplosionByServer(int x, int y, string text);

            [Script(NoDecoration = true)]
            void IClient_DisplayNotification(string text, int color);

            [Script(NoDecoration = true)]
            void ForceReload();

            [Script(NoDecoration = true)]
            void IClient_DrawRectangle(RectangleInfo rect, int color);

            [Script(NoDecoration = true)]
            void IClient_SpawnHarvester(Point Location, int Direction);
        }

        public int ToClientMessageId;

    }
}