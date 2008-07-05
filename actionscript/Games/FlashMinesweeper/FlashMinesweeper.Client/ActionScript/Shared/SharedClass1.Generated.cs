using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashMinesweeper.ActionScript.Shared
{
    #region SharedClass1
    public partial class SharedClass1
    {
        #region Messages
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public enum Messages
        {
            None = 100,
            ServerPlayerHello,
            ServerPlayerJoined,
            ServerPlayerLeft,
            PlayerAdvertise,
            UserPlayerAdvertise,
            MouseMove,
            UserMouseMove,
            MouseOut,
            UserMouseOut,
        }
        #endregion

        #region IMessages
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IMessages
        {
        }
        #endregion
        #region IEvents
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IEvents
        {
            event Action<RemoteEvents.ServerPlayerHelloArguments> ServerPlayerHello;
            event Action<RemoteEvents.ServerPlayerJoinedArguments> ServerPlayerJoined;
            event Action<RemoteEvents.ServerPlayerLeftArguments> ServerPlayerLeft;
            event Action<RemoteEvents.PlayerAdvertiseArguments> PlayerAdvertise;
            event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            event Action<RemoteEvents.MouseOutArguments> MouseOut;
            event Action<RemoteEvents.UserMouseOutArguments> UserMouseOut;
        }
        #endregion
        #region IPairedEventsWithoutUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedEventsWithoutUser
        {
            event Action<RemoteEvents.PlayerAdvertiseArguments> PlayerAdvertise;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            event Action<RemoteEvents.MouseOutArguments> MouseOut;
        }
        #endregion
        #region IPairedEventsWithUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedEventsWithUser
        {
            event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            event Action<RemoteEvents.UserMouseOutArguments> UserMouseOut;
        }
        #endregion
        #region IPairedMessagesWithUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedMessagesWithUser
        {
            void UserPlayerAdvertise(int user, string name);
            void UserMouseMove(int user, int x, int y, int color);
            void UserMouseOut(int user, int color);
        }
        #endregion
        #region IPairedMessagesWithoutUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedMessagesWithoutUser
        {
            void PlayerAdvertise(string name);
            void MouseMove(int x, int y, int color);
            void MouseOut(int color);
        }
        #endregion

        #region RemoteMessages
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IMessages, IPairedMessagesWithoutUser, IPairedMessagesWithUser
        {
            public Action<SendArguments> Send;
            #region SendArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class SendArguments
            {
                public Messages i;
                public object[] args;
            }
            #endregion
            public void ServerPlayerHello(int user, string name)
            {
                Send(new SendArguments { i = Messages.ServerPlayerHello, args = new object[] { user, name } });
            }
            public void ServerPlayerJoined(int user, string name)
            {
                Send(new SendArguments { i = Messages.ServerPlayerJoined, args = new object[] { user, name } });
            }
            public void ServerPlayerLeft(int user, string name)
            {
                Send(new SendArguments { i = Messages.ServerPlayerLeft, args = new object[] { user, name } });
            }
            public void PlayerAdvertise(string name)
            {
                Send(new SendArguments { i = Messages.PlayerAdvertise, args = new object[] { name } });
            }
            public void UserPlayerAdvertise(int user, string name)
            {
                Send(new SendArguments { i = Messages.UserPlayerAdvertise, args = new object[] { user, name } });
            }
            public void MouseMove(int x, int y, int color)
            {
                Send(new SendArguments { i = Messages.MouseMove, args = new object[] { x, y, color } });
            }
            public void UserMouseMove(int user, int x, int y, int color)
            {
                Send(new SendArguments { i = Messages.UserMouseMove, args = new object[] { user, x, y, color } });
            }
            public void MouseOut(int color)
            {
                Send(new SendArguments { i = Messages.MouseOut, args = new object[] { color } });
            }
            public void UserMouseOut(int user, int color)
            {
                Send(new SendArguments { i = Messages.UserMouseOut, args = new object[] { user, color } });
            }
        }
        #endregion

        #region RemoteEvents
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public sealed partial class RemoteEvents : IEvents, IPairedEventsWithoutUser, IPairedEventsWithUser
        {
            private readonly Dictionary<Messages, Action<IDispatchHelper>> DispatchTable;
            private readonly Dictionary<Messages, Converter<object, Delegate>> DispatchTableDelegates;
            [AccessedThroughProperty("Router")]
            private WithUserArgumentsRouter _Router;
            #region DispatchHelper
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public partial class DispatchHelper
            {
                public Converter<uint, int> GetInt32 { get; set; }
                public Converter<uint, double> GetDouble { get; set; }
                public Converter<uint, string> GetString { get; set; }
                public Converter<uint, int[]> GetInt32Array { get; set; }
                public Converter<uint, double[]> GetDoubleArray { get; set; }
                public Converter<uint, string[]> GetStringArray { get; set; }
                public Converter<uint, object[]> GetArray { get; set; }
            }
            #endregion
            public bool Dispatch(Messages e, IDispatchHelper h)
            {
                if (!DispatchTableDelegates.ContainsKey(e)) return false;
                if (DispatchTableDelegates[e](null) == null) return false;
                if (!DispatchTable.ContainsKey(e)) return false;
                DispatchTable[e](h);
                return true;
            }
            #region WithUserArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public abstract partial class WithUserArguments
            {
                public int user;
            }
            #endregion
            #region WithUserArgumentsRouter
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter : WithUserArguments
            {
                public IMessages Target;
                #region Routing
                public void UserPlayerAdvertise(PlayerAdvertiseArguments e)
                {
                    Target.UserPlayerAdvertise(this.user, e.name);
                }
                public void UserMouseMove(MouseMoveArguments e)
                {
                    Target.UserMouseMove(this.user, e.x, e.y, e.color);
                }
                public void UserMouseOut(MouseOutArguments e)
                {
                    Target.UserMouseOut(this.user, e.color);
                }
                #endregion
            }
            #endregion
            #region ServerPlayerHelloArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerPlayerHelloArguments
            {
                public int user;
                public string name;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerPlayerHelloArguments> ServerPlayerHello;
            #region ServerPlayerJoinedArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerPlayerJoinedArguments
            {
                public int user;
                public string name;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerPlayerJoinedArguments> ServerPlayerJoined;
            #region ServerPlayerLeftArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerPlayerLeftArguments
            {
                public int user;
                public string name;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerPlayerLeftArguments> ServerPlayerLeft;
            #region PlayerAdvertiseArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class PlayerAdvertiseArguments
            {
                public string name;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ name = ").Append(this.name).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<PlayerAdvertiseArguments> PlayerAdvertise;
            #region UserPlayerAdvertiseArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserPlayerAdvertiseArguments : WithUserArguments
            {
                public string name;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            #region MouseMoveArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class MouseMoveArguments
            {
                public int x;
                public int y;
                public int color;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y).Append(", color = ").Append(this.color).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<MouseMoveArguments> MouseMove;
            #region UserMouseMoveArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserMouseMoveArguments : WithUserArguments
            {
                public int x;
                public int y;
                public int color;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y).Append(", color = ").Append(this.color).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserMouseMoveArguments> UserMouseMove;
            #region MouseOutArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class MouseOutArguments
            {
                public int color;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ color = ").Append(this.color).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<MouseOutArguments> MouseOut;
            #region UserMouseOutArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserMouseOutArguments : WithUserArguments
            {
                public int color;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", color = ").Append(this.color).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserMouseOutArguments> UserMouseOut;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<IDispatchHelper>>
                        {
                            { Messages.ServerPlayerHello, e => { ServerPlayerHello(new ServerPlayerHelloArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerJoined, e => { ServerPlayerJoined(new ServerPlayerJoinedArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerLeft, e => { ServerPlayerLeft(new ServerPlayerLeftArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.PlayerAdvertise, e => { PlayerAdvertise(new PlayerAdvertiseArguments { name = e.GetString(0) }); } },
                            { Messages.UserPlayerAdvertise, e => { UserPlayerAdvertise(new UserPlayerAdvertiseArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.MouseMove, e => { MouseMove(new MouseMoveArguments { x = e.GetInt32(0), y = e.GetInt32(1), color = e.GetInt32(2) }); } },
                            { Messages.UserMouseMove, e => { UserMouseMove(new UserMouseMoveArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2), color = e.GetInt32(3) }); } },
                            { Messages.MouseOut, e => { MouseOut(new MouseOutArguments { color = e.GetInt32(0) }); } },
                            { Messages.UserMouseOut, e => { UserMouseOut(new UserMouseOutArguments { user = e.GetInt32(0), color = e.GetInt32(1) }); } },
                        }
                ;
                DispatchTableDelegates = new Dictionary<Messages, Converter<object, Delegate>>
                        {
                            { Messages.ServerPlayerHello, e => ServerPlayerHello },
                            { Messages.ServerPlayerJoined, e => ServerPlayerJoined },
                            { Messages.ServerPlayerLeft, e => ServerPlayerLeft },
                            { Messages.PlayerAdvertise, e => PlayerAdvertise },
                            { Messages.UserPlayerAdvertise, e => UserPlayerAdvertise },
                            { Messages.MouseMove, e => MouseMove },
                            { Messages.UserMouseMove, e => UserMouseMove },
                            { Messages.MouseOut, e => MouseOut },
                            { Messages.UserMouseOut, e => UserMouseOut },
                        }
                ;
            }
            public WithUserArgumentsRouter Router
            {
                [DebuggerNonUserCode]
                get
                {
                    return this._Router;
                }
                [DebuggerNonUserCode]
                [MethodImpl(MethodImplOptions.Synchronized)]
                set
                {
                    if(_Router != null)
                    {
                        this.PlayerAdvertise -= _Router.UserPlayerAdvertise;
                        this.MouseMove -= _Router.UserMouseMove;
                        this.MouseOut -= _Router.UserMouseOut;
                    }
                    _Router = value;
                    if(_Router != null)
                    {
                        this.PlayerAdvertise += _Router.UserPlayerAdvertise;
                        this.MouseMove += _Router.UserMouseMove;
                        this.MouseOut += _Router.UserMouseOut;
                    }
                }
            }
        }
        #endregion
        #region Bridge
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial class Bridge : IEvents, IPairedEventsWithoutUser, IPairedEventsWithUser, IMessages, IPairedMessagesWithoutUser, IPairedMessagesWithUser
        {
            public event Action<RemoteEvents.ServerPlayerHelloArguments> ServerPlayerHello;
            void IMessages.ServerPlayerHello(int user, string name)
            {
                if(ServerPlayerHello == null) return;
                ServerPlayerHello(new RemoteEvents.ServerPlayerHelloArguments { user = user, name = name });
            }

            public event Action<RemoteEvents.ServerPlayerJoinedArguments> ServerPlayerJoined;
            void IMessages.ServerPlayerJoined(int user, string name)
            {
                if(ServerPlayerJoined == null) return;
                ServerPlayerJoined(new RemoteEvents.ServerPlayerJoinedArguments { user = user, name = name });
            }

            public event Action<RemoteEvents.ServerPlayerLeftArguments> ServerPlayerLeft;
            void IMessages.ServerPlayerLeft(int user, string name)
            {
                if(ServerPlayerLeft == null) return;
                ServerPlayerLeft(new RemoteEvents.ServerPlayerLeftArguments { user = user, name = name });
            }

            public event Action<RemoteEvents.PlayerAdvertiseArguments> PlayerAdvertise;
            void IMessages.PlayerAdvertise(string name)
            {
                if(PlayerAdvertise == null) return;
                PlayerAdvertise(new RemoteEvents.PlayerAdvertiseArguments { name = name });
            }
            void IPairedMessagesWithoutUser.PlayerAdvertise(string name)
            {
                ((IMessages)this).PlayerAdvertise(name);
            }

            public event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            void IMessages.UserPlayerAdvertise(int user, string name)
            {
                if(UserPlayerAdvertise == null) return;
                UserPlayerAdvertise(new RemoteEvents.UserPlayerAdvertiseArguments { user = user, name = name });
            }
            void IPairedMessagesWithUser.UserPlayerAdvertise(int user, string name)
            {
                ((IMessages)this).UserPlayerAdvertise(user, name);
            }

            public event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            void IMessages.MouseMove(int x, int y, int color)
            {
                if(MouseMove == null) return;
                MouseMove(new RemoteEvents.MouseMoveArguments { x = x, y = y, color = color });
            }
            void IPairedMessagesWithoutUser.MouseMove(int x, int y, int color)
            {
                ((IMessages)this).MouseMove(x, y, color);
            }

            public event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            void IMessages.UserMouseMove(int user, int x, int y, int color)
            {
                if(UserMouseMove == null) return;
                UserMouseMove(new RemoteEvents.UserMouseMoveArguments { user = user, x = x, y = y, color = color });
            }
            void IPairedMessagesWithUser.UserMouseMove(int user, int x, int y, int color)
            {
                ((IMessages)this).UserMouseMove(user, x, y, color);
            }

            public event Action<RemoteEvents.MouseOutArguments> MouseOut;
            void IMessages.MouseOut(int color)
            {
                if(MouseOut == null) return;
                MouseOut(new RemoteEvents.MouseOutArguments { color = color });
            }
            void IPairedMessagesWithoutUser.MouseOut(int color)
            {
                ((IMessages)this).MouseOut(color);
            }

            public event Action<RemoteEvents.UserMouseOutArguments> UserMouseOut;
            void IMessages.UserMouseOut(int user, int color)
            {
                if(UserMouseOut == null) return;
                UserMouseOut(new RemoteEvents.UserMouseOutArguments { user = user, color = color });
            }
            void IPairedMessagesWithUser.UserMouseOut(int user, int color)
            {
                ((IMessages)this).UserMouseOut(user, color);
            }

        }
        #endregion
    }
    #endregion
}
// 6.07.2008 0:47:29
