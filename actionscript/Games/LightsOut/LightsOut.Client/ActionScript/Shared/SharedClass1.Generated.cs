using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
#if !NoAttributes
using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba;
#endif
namespace LightsOut.ActionScript.Shared
{
    #region SharedClass1
#if !NoAttributes
    [Script]
#endif
    [CompilerGenerated]
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
            ServerSendMap,
            SendMap,
            UserSendMap,
            Click,
            UserClick,
            MouseMove,
            UserMouseMove,
            AddScore,
            AwardCompletedThree,
            AwardCompletedTen,
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
            event Action<RemoteEvents.ServerSendMapArguments> ServerSendMap;
            event Action<RemoteEvents.SendMapArguments> SendMap;
            event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            event Action<RemoteEvents.ClickArguments> Click;
            event Action<RemoteEvents.UserClickArguments> UserClick;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            event Action<RemoteEvents.AddScoreArguments> AddScore;
            event Action<RemoteEvents.AwardCompletedThreeArguments> AwardCompletedThree;
            event Action<RemoteEvents.AwardCompletedTenArguments> AwardCompletedTen;
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
            event Action<RemoteEvents.SendMapArguments> SendMap;
            event Action<RemoteEvents.ClickArguments> Click;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
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
            event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            event Action<RemoteEvents.UserClickArguments> UserClick;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
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
            void UserSendMap(int user, int[] data);
            void UserClick(int user, int x, int y);
            void UserMouseMove(int user, int x, int y, int color);
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
            void SendMap(int[] data);
            void Click(int x, int y);
            void MouseMove(int x, int y, int color);
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
            public void ServerSendMap()
            {
                Send(new SendArguments { i = Messages.ServerSendMap, args = new object[] {  } });
            }
            public void SendMap(int[] data)
            {
                var args = new object[data.Length];
                Array.Copy(data, args, data.Length);
                Send(new SendArguments { i = Messages.SendMap, args = args });
            }
            public void UserSendMap(int user, int[] data)
            {
                var args = new object[data.Length];
                Array.Copy(data, args, data.Length);
                Send(new SendArguments { i = Messages.UserSendMap, args = args });
            }
            public void Click(int x, int y)
            {
                Send(new SendArguments { i = Messages.Click, args = new object[] { x, y } });
            }
            public void UserClick(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserClick, args = new object[] { user, x, y } });
            }
            public void MouseMove(int x, int y, int color)
            {
                Send(new SendArguments { i = Messages.MouseMove, args = new object[] { x, y, color } });
            }
            public void UserMouseMove(int user, int x, int y, int color)
            {
                Send(new SendArguments { i = Messages.UserMouseMove, args = new object[] { user, x, y, color } });
            }
            public void AddScore(int score)
            {
                Send(new SendArguments { i = Messages.AddScore, args = new object[] { score } });
            }
            public void AwardCompletedThree()
            {
                Send(new SendArguments { i = Messages.AwardCompletedThree, args = new object[] {  } });
            }
            public void AwardCompletedTen()
            {
                Send(new SendArguments { i = Messages.AwardCompletedTen, args = new object[] {  } });
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
                var hx = DispatchTableDelegates[e];

                if (hx == null)
                    throw new Exception("Dispatch handler null for " + e);

                if (hx(null) == null) return false;
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
                public void UserSendMap(SendMapArguments e)
                {
                    Target.UserSendMap(this.user, e.data);
                }
                public void UserClick(ClickArguments e)
                {
                    Target.UserClick(this.user, e.x, e.y);
                }
                public void UserMouseMove(MouseMoveArguments e)
                {
                    Target.UserMouseMove(this.user, e.x, e.y, e.color);
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
            #region ServerSendMapArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerSendMapArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<ServerSendMapArguments> ServerSendMap;
            #region SendMapArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class SendMapArguments
            {
                public int[] data;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ data = ").Append(this.data).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SendMapArguments> SendMap;
            #region UserSendMapArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserSendMapArguments : WithUserArguments
            {
                public int[] data;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", data = ").Append(this.data).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSendMapArguments> UserSendMap;
            #region ClickArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ClickArguments
            {
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ClickArguments> Click;
            #region UserClickArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserClickArguments : WithUserArguments
            {
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserClickArguments> UserClick;
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
            #region AddScoreArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class AddScoreArguments
            {
                public int score;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ score = ").Append(this.score).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<AddScoreArguments> AddScore;
            #region AwardCompletedThreeArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class AwardCompletedThreeArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<AwardCompletedThreeArguments> AwardCompletedThree;
            #region AwardCompletedTenArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class AwardCompletedTenArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<AwardCompletedTenArguments> AwardCompletedTen;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<IDispatchHelper>>
                        {
                            { Messages.ServerPlayerHello, e => { ServerPlayerHello(new ServerPlayerHelloArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerJoined, e => { ServerPlayerJoined(new ServerPlayerJoinedArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerLeft, e => { ServerPlayerLeft(new ServerPlayerLeftArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.PlayerAdvertise, e => { PlayerAdvertise(new PlayerAdvertiseArguments { name = e.GetString(0) }); } },
                            { Messages.UserPlayerAdvertise, e => { UserPlayerAdvertise(new UserPlayerAdvertiseArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerSendMap, e => { ServerSendMap(new ServerSendMapArguments {  }); } },
                            { Messages.SendMap, e => { SendMap(new SendMapArguments { data = e.GetInt32Array(0) }); } },
                            { Messages.UserSendMap, e => { UserSendMap(new UserSendMapArguments { user = e.GetInt32(0), data = e.GetInt32Array(1) }); } },
                            { Messages.Click, e => { Click(new ClickArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserClick, e => { UserClick(new UserClickArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.MouseMove, e => { MouseMove(new MouseMoveArguments { x = e.GetInt32(0), y = e.GetInt32(1), color = e.GetInt32(2) }); } },
                            { Messages.UserMouseMove, e => { UserMouseMove(new UserMouseMoveArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2), color = e.GetInt32(3) }); } },
                            { Messages.AddScore, e => { AddScore(new AddScoreArguments { score = e.GetInt32(0) }); } },
                            { Messages.AwardCompletedThree, e => { AwardCompletedThree(new AwardCompletedThreeArguments {  }); } },
                            { Messages.AwardCompletedTen, e => { AwardCompletedTen(new AwardCompletedTenArguments {  }); } },
                        }
                ;
                DispatchTableDelegates = new Dictionary<Messages, Converter<object, Delegate>>
                        {
                            { Messages.ServerPlayerHello, e => ServerPlayerHello },
                            { Messages.ServerPlayerJoined, e => ServerPlayerJoined },
                            { Messages.ServerPlayerLeft, e => ServerPlayerLeft },
                            { Messages.PlayerAdvertise, e => PlayerAdvertise },
                            { Messages.UserPlayerAdvertise, e => UserPlayerAdvertise },
                            { Messages.ServerSendMap, e => ServerSendMap },
                            { Messages.SendMap, e => SendMap },
                            { Messages.UserSendMap, e => UserSendMap },
                            { Messages.Click, e => Click },
                            { Messages.UserClick, e => UserClick },
                            { Messages.MouseMove, e => MouseMove },
                            { Messages.UserMouseMove, e => UserMouseMove },
                            { Messages.AddScore, e => AddScore },
                            { Messages.AwardCompletedThree, e => AwardCompletedThree },
                            { Messages.AwardCompletedTen, e => AwardCompletedTen },
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
                        this.SendMap -= _Router.UserSendMap;
                        this.Click -= _Router.UserClick;
                        this.MouseMove -= _Router.UserMouseMove;
                    }
                    _Router = value;
                    if(_Router != null)
                    {
                        this.PlayerAdvertise += _Router.UserPlayerAdvertise;
                        this.SendMap += _Router.UserSendMap;
                        this.Click += _Router.UserClick;
                        this.MouseMove += _Router.UserMouseMove;
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

            public event Action<RemoteEvents.ServerSendMapArguments> ServerSendMap;
            void IMessages.ServerSendMap()
            {
                if(ServerSendMap == null) return;
                ServerSendMap(new RemoteEvents.ServerSendMapArguments {  });
            }

            public event Action<RemoteEvents.SendMapArguments> SendMap;
            void IMessages.SendMap(int[] data)
            {
                if(SendMap == null) return;
                SendMap(new RemoteEvents.SendMapArguments { data = data });
            }
            void IPairedMessagesWithoutUser.SendMap(int[] data)
            {
                ((IMessages)this).SendMap(data);
            }

            public event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            void IMessages.UserSendMap(int user, int[] data)
            {
                if(UserSendMap == null) return;
                UserSendMap(new RemoteEvents.UserSendMapArguments { user = user, data = data });
            }
            void IPairedMessagesWithUser.UserSendMap(int user, int[] data)
            {
                ((IMessages)this).UserSendMap(user, data);
            }

            public event Action<RemoteEvents.ClickArguments> Click;
            void IMessages.Click(int x, int y)
            {
                if(Click == null) return;
                Click(new RemoteEvents.ClickArguments { x = x, y = y });
            }
            void IPairedMessagesWithoutUser.Click(int x, int y)
            {
                ((IMessages)this).Click(x, y);
            }

            public event Action<RemoteEvents.UserClickArguments> UserClick;
            void IMessages.UserClick(int user, int x, int y)
            {
                if(UserClick == null) return;
                UserClick(new RemoteEvents.UserClickArguments { user = user, x = x, y = y });
            }
            void IPairedMessagesWithUser.UserClick(int user, int x, int y)
            {
                ((IMessages)this).UserClick(user, x, y);
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

            public event Action<RemoteEvents.AddScoreArguments> AddScore;
            void IMessages.AddScore(int score)
            {
                if(AddScore == null) return;
                AddScore(new RemoteEvents.AddScoreArguments { score = score });
            }

            public event Action<RemoteEvents.AwardCompletedThreeArguments> AwardCompletedThree;
            void IMessages.AwardCompletedThree()
            {
                if(AwardCompletedThree == null) return;
                AwardCompletedThree(new RemoteEvents.AwardCompletedThreeArguments {  });
            }

            public event Action<RemoteEvents.AwardCompletedTenArguments> AwardCompletedTen;
            void IMessages.AwardCompletedTen()
            {
                if(AwardCompletedTen == null) return;
                AwardCompletedTen(new RemoteEvents.AwardCompletedTenArguments {  });
            }

        }
        #endregion
    }
    #endregion
}
// 14.07.2008 15:45:36
