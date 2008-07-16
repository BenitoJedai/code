using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ScriptCoreLib.Shared.Nonoba;
using ScriptCoreLib;
namespace FlashMinesweeper.ActionScript.Shared
{
    #region SharedClass1
    [Script]
    [CompilerGenerated]
    public partial class SharedClass1
    {
        #region Messages
        [Script]
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
            ServerSendMap,
            SendMap,
            UserSendMap,
            SendMapLater,
            UserSendMapLater,
            SetFlag,
            UserSetFlag,
            Reveal,
            UserReveal,
            AddScore,
            AwardAchievementFirstMinefieldComplete,
            SendPassword,
            ServerPasswordStatus,
            LockGame,
            UnlockGame,
        }
        #endregion

        #region IMessages
        [Script]
        [CompilerGenerated]
        public partial interface IMessages
        {
        }
        #endregion
        #region IEvents
        [Script]
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
            event Action<RemoteEvents.ServerSendMapArguments> ServerSendMap;
            event Action<RemoteEvents.SendMapArguments> SendMap;
            event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            event Action<RemoteEvents.SendMapLaterArguments> SendMapLater;
            event Action<RemoteEvents.UserSendMapLaterArguments> UserSendMapLater;
            event Action<RemoteEvents.SetFlagArguments> SetFlag;
            event Action<RemoteEvents.UserSetFlagArguments> UserSetFlag;
            event Action<RemoteEvents.RevealArguments> Reveal;
            event Action<RemoteEvents.UserRevealArguments> UserReveal;
            event Action<RemoteEvents.AddScoreArguments> AddScore;
            event Action<RemoteEvents.AwardAchievementFirstMinefieldCompleteArguments> AwardAchievementFirstMinefieldComplete;
            event Action<RemoteEvents.SendPasswordArguments> SendPassword;
            event Action<RemoteEvents.ServerPasswordStatusArguments> ServerPasswordStatus;
            event Action<RemoteEvents.LockGameArguments> LockGame;
            event Action<RemoteEvents.UnlockGameArguments> UnlockGame;
        }
        #endregion
        #region IPairedEventsWithoutUser
        [Script]
        [CompilerGenerated]
        public partial interface IPairedEventsWithoutUser
        {
            event Action<RemoteEvents.PlayerAdvertiseArguments> PlayerAdvertise;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            event Action<RemoteEvents.MouseOutArguments> MouseOut;
            event Action<RemoteEvents.SendMapArguments> SendMap;
            event Action<RemoteEvents.SendMapLaterArguments> SendMapLater;
            event Action<RemoteEvents.SetFlagArguments> SetFlag;
            event Action<RemoteEvents.RevealArguments> Reveal;
        }
        #endregion
        #region IPairedEventsWithUser
        [Script]
        [CompilerGenerated]
        public partial interface IPairedEventsWithUser
        {
            event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            event Action<RemoteEvents.UserMouseOutArguments> UserMouseOut;
            event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            event Action<RemoteEvents.UserSendMapLaterArguments> UserSendMapLater;
            event Action<RemoteEvents.UserSetFlagArguments> UserSetFlag;
            event Action<RemoteEvents.UserRevealArguments> UserReveal;
        }
        #endregion
        #region IPairedMessagesWithUser
        [Script]
        [CompilerGenerated]
        public partial interface IPairedMessagesWithUser
        {
            void UserPlayerAdvertise(int user, string name);
            void UserMouseMove(int user, int x, int y, int color);
            void UserMouseOut(int user, int color);
            void UserSendMap(int user, int[] buttons);
            void UserSendMapLater(int user);
            void UserSetFlag(int user, int button, int value);
            void UserReveal(int user, int button);
        }
        #endregion
        #region IPairedMessagesWithoutUser
        [Script]
        [CompilerGenerated]
        public partial interface IPairedMessagesWithoutUser
        {
            void PlayerAdvertise(string name);
            void MouseMove(int x, int y, int color);
            void MouseOut(int color);
            void SendMap(int[] buttons);
            void SendMapLater();
            void SetFlag(int button, int value);
            void Reveal(int button);
        }
        #endregion

        #region RemoteMessages
        [Script]
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IMessages, IPairedMessagesWithoutUser, IPairedMessagesWithUser
        {
            public Action<SendArguments> Send;
            #region SendArguments
            [Script]
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
            public void ServerSendMap()
            {
                Send(new SendArguments { i = Messages.ServerSendMap, args = new object[] {  } });
            }
            public void SendMap(int[] buttons)
            {
                var args = new object[buttons.Length];
                Array.Copy(buttons, args, buttons.Length);
                Send(new SendArguments { i = Messages.SendMap, args = args });
            }
            public void UserSendMap(int user, int[] buttons)
            {
                var args = new object[buttons.Length];
                Array.Copy(buttons, args, buttons.Length);
                Send(new SendArguments { i = Messages.UserSendMap, args = args });
            }
            public void SendMapLater()
            {
                Send(new SendArguments { i = Messages.SendMapLater, args = new object[] {  } });
            }
            public void UserSendMapLater(int user)
            {
                Send(new SendArguments { i = Messages.UserSendMapLater, args = new object[] { user } });
            }
            public void SetFlag(int button, int value)
            {
                Send(new SendArguments { i = Messages.SetFlag, args = new object[] { button, value } });
            }
            public void UserSetFlag(int user, int button, int value)
            {
                Send(new SendArguments { i = Messages.UserSetFlag, args = new object[] { user, button, value } });
            }
            public void Reveal(int button)
            {
                Send(new SendArguments { i = Messages.Reveal, args = new object[] { button } });
            }
            public void UserReveal(int user, int button)
            {
                Send(new SendArguments { i = Messages.UserReveal, args = new object[] { user, button } });
            }
            public void AddScore(int score)
            {
                Send(new SendArguments { i = Messages.AddScore, args = new object[] { score } });
            }
            public void AwardAchievementFirstMinefieldComplete()
            {
                Send(new SendArguments { i = Messages.AwardAchievementFirstMinefieldComplete, args = new object[] {  } });
            }
            public void SendPassword(string password)
            {
                Send(new SendArguments { i = Messages.SendPassword, args = new object[] { password } });
            }
            public void ServerPasswordStatus(int status)
            {
                Send(new SendArguments { i = Messages.ServerPasswordStatus, args = new object[] { status } });
            }
            public void LockGame()
            {
                Send(new SendArguments { i = Messages.LockGame, args = new object[] {  } });
            }
            public void UnlockGame()
            {
                Send(new SendArguments { i = Messages.UnlockGame, args = new object[] {  } });
            }
        }
        #endregion

        #region RemoteEvents
        [Script]
        [CompilerGenerated]
        public sealed partial class RemoteEvents : IEvents, IPairedEventsWithoutUser, IPairedEventsWithUser
        {
            private readonly Dictionary<Messages, Action<IDispatchHelper>> DispatchTable;
            private readonly Dictionary<Messages, Converter<object, Delegate>> DispatchTableDelegates;
            [AccessedThroughProperty("Router")]
            private WithUserArgumentsRouter _Router;
            #region DispatchHelper
            [Script]
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
            [Script]
            [CompilerGenerated]
            public abstract partial class WithUserArguments
            {
                public int user;
            }
            #endregion
            #region WithUserArgumentsRouter
            [Script]
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
                public void UserSendMap(SendMapArguments e)
                {
                    Target.UserSendMap(this.user, e.buttons);
                }
                public void UserSendMapLater(SendMapLaterArguments e)
                {
                    Target.UserSendMapLater(this.user);
                }
                public void UserSetFlag(SetFlagArguments e)
                {
                    Target.UserSetFlag(this.user, e.button, e.value);
                }
                public void UserReveal(RevealArguments e)
                {
                    Target.UserReveal(this.user, e.button);
                }
                #endregion
            }
            #endregion
            #region ServerPlayerHelloArguments
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            #region ServerSendMapArguments
            [Script]
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
            [Script]
            [CompilerGenerated]
            public sealed partial class SendMapArguments
            {
                public int[] buttons;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ buttons = ").Append(this.buttons).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SendMapArguments> SendMap;
            #region UserSendMapArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSendMapArguments : WithUserArguments
            {
                public int[] buttons;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", buttons = ").Append(this.buttons).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSendMapArguments> UserSendMap;
            #region SendMapLaterArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SendMapLaterArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<SendMapLaterArguments> SendMapLater;
            #region UserSendMapLaterArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSendMapLaterArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSendMapLaterArguments> UserSendMapLater;
            #region SetFlagArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SetFlagArguments
            {
                public int button;
                public int value;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ button = ").Append(this.button).Append(", value = ").Append(this.value).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SetFlagArguments> SetFlag;
            #region UserSetFlagArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSetFlagArguments : WithUserArguments
            {
                public int button;
                public int value;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", button = ").Append(this.button).Append(", value = ").Append(this.value).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSetFlagArguments> UserSetFlag;
            #region RevealArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class RevealArguments
            {
                public int button;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ button = ").Append(this.button).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<RevealArguments> Reveal;
            #region UserRevealArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserRevealArguments : WithUserArguments
            {
                public int button;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", button = ").Append(this.button).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserRevealArguments> UserReveal;
            #region AddScoreArguments
            [Script]
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
            #region AwardAchievementFirstMinefieldCompleteArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AwardAchievementFirstMinefieldCompleteArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<AwardAchievementFirstMinefieldCompleteArguments> AwardAchievementFirstMinefieldComplete;
            #region SendPasswordArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SendPasswordArguments
            {
                public string password;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ password = ").Append(this.password).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SendPasswordArguments> SendPassword;
            #region ServerPasswordStatusArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class ServerPasswordStatusArguments
            {
                public int status;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ status = ").Append(this.status).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerPasswordStatusArguments> ServerPasswordStatus;
            #region LockGameArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class LockGameArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<LockGameArguments> LockGame;
            #region UnlockGameArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UnlockGameArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<UnlockGameArguments> UnlockGame;
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
                            { Messages.ServerSendMap, e => { ServerSendMap(new ServerSendMapArguments {  }); } },
                            { Messages.SendMap, e => { SendMap(new SendMapArguments { buttons = e.GetInt32Array(0) }); } },
                            { Messages.UserSendMap, e => { UserSendMap(new UserSendMapArguments { user = e.GetInt32(0), buttons = e.GetInt32Array(1) }); } },
                            { Messages.SendMapLater, e => { SendMapLater(new SendMapLaterArguments {  }); } },
                            { Messages.UserSendMapLater, e => { UserSendMapLater(new UserSendMapLaterArguments { user = e.GetInt32(0) }); } },
                            { Messages.SetFlag, e => { SetFlag(new SetFlagArguments { button = e.GetInt32(0), value = e.GetInt32(1) }); } },
                            { Messages.UserSetFlag, e => { UserSetFlag(new UserSetFlagArguments { user = e.GetInt32(0), button = e.GetInt32(1), value = e.GetInt32(2) }); } },
                            { Messages.Reveal, e => { Reveal(new RevealArguments { button = e.GetInt32(0) }); } },
                            { Messages.UserReveal, e => { UserReveal(new UserRevealArguments { user = e.GetInt32(0), button = e.GetInt32(1) }); } },
                            { Messages.AddScore, e => { AddScore(new AddScoreArguments { score = e.GetInt32(0) }); } },
                            { Messages.AwardAchievementFirstMinefieldComplete, e => { AwardAchievementFirstMinefieldComplete(new AwardAchievementFirstMinefieldCompleteArguments {  }); } },
                            { Messages.SendPassword, e => { SendPassword(new SendPasswordArguments { password = e.GetString(0) }); } },
                            { Messages.ServerPasswordStatus, e => { ServerPasswordStatus(new ServerPasswordStatusArguments { status = e.GetInt32(0) }); } },
                            { Messages.LockGame, e => { LockGame(new LockGameArguments {  }); } },
                            { Messages.UnlockGame, e => { UnlockGame(new UnlockGameArguments {  }); } },
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
                            { Messages.ServerSendMap, e => ServerSendMap },
                            { Messages.SendMap, e => SendMap },
                            { Messages.UserSendMap, e => UserSendMap },
                            { Messages.SendMapLater, e => SendMapLater },
                            { Messages.UserSendMapLater, e => UserSendMapLater },
                            { Messages.SetFlag, e => SetFlag },
                            { Messages.UserSetFlag, e => UserSetFlag },
                            { Messages.Reveal, e => Reveal },
                            { Messages.UserReveal, e => UserReveal },
                            { Messages.AddScore, e => AddScore },
                            { Messages.AwardAchievementFirstMinefieldComplete, e => AwardAchievementFirstMinefieldComplete },
                            { Messages.SendPassword, e => SendPassword },
                            { Messages.ServerPasswordStatus, e => ServerPasswordStatus },
                            { Messages.LockGame, e => LockGame },
                            { Messages.UnlockGame, e => UnlockGame },
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
                        this.SendMap -= _Router.UserSendMap;
                        this.SendMapLater -= _Router.UserSendMapLater;
                        this.SetFlag -= _Router.UserSetFlag;
                        this.Reveal -= _Router.UserReveal;
                    }
                    _Router = value;
                    if(_Router != null)
                    {
                        this.PlayerAdvertise += _Router.UserPlayerAdvertise;
                        this.MouseMove += _Router.UserMouseMove;
                        this.MouseOut += _Router.UserMouseOut;
                        this.SendMap += _Router.UserSendMap;
                        this.SendMapLater += _Router.UserSendMapLater;
                        this.SetFlag += _Router.UserSetFlag;
                        this.Reveal += _Router.UserReveal;
                    }
                }
            }
        }
        #endregion
        #region Bridge
        [Script]
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

            public event Action<RemoteEvents.ServerSendMapArguments> ServerSendMap;
            void IMessages.ServerSendMap()
            {
                if(ServerSendMap == null) return;
                ServerSendMap(new RemoteEvents.ServerSendMapArguments {  });
            }

            public event Action<RemoteEvents.SendMapArguments> SendMap;
            void IMessages.SendMap(int[] buttons)
            {
                if(SendMap == null) return;
                SendMap(new RemoteEvents.SendMapArguments { buttons = buttons });
            }
            void IPairedMessagesWithoutUser.SendMap(int[] buttons)
            {
                ((IMessages)this).SendMap(buttons);
            }

            public event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            void IMessages.UserSendMap(int user, int[] buttons)
            {
                if(UserSendMap == null) return;
                UserSendMap(new RemoteEvents.UserSendMapArguments { user = user, buttons = buttons });
            }
            void IPairedMessagesWithUser.UserSendMap(int user, int[] buttons)
            {
                ((IMessages)this).UserSendMap(user, buttons);
            }

            public event Action<RemoteEvents.SendMapLaterArguments> SendMapLater;
            void IMessages.SendMapLater()
            {
                if(SendMapLater == null) return;
                SendMapLater(new RemoteEvents.SendMapLaterArguments {  });
            }
            void IPairedMessagesWithoutUser.SendMapLater()
            {
                ((IMessages)this).SendMapLater();
            }

            public event Action<RemoteEvents.UserSendMapLaterArguments> UserSendMapLater;
            void IMessages.UserSendMapLater(int user)
            {
                if(UserSendMapLater == null) return;
                UserSendMapLater(new RemoteEvents.UserSendMapLaterArguments { user = user });
            }
            void IPairedMessagesWithUser.UserSendMapLater(int user)
            {
                ((IMessages)this).UserSendMapLater(user);
            }

            public event Action<RemoteEvents.SetFlagArguments> SetFlag;
            void IMessages.SetFlag(int button, int value)
            {
                if(SetFlag == null) return;
                SetFlag(new RemoteEvents.SetFlagArguments { button = button, value = value });
            }
            void IPairedMessagesWithoutUser.SetFlag(int button, int value)
            {
                ((IMessages)this).SetFlag(button, value);
            }

            public event Action<RemoteEvents.UserSetFlagArguments> UserSetFlag;
            void IMessages.UserSetFlag(int user, int button, int value)
            {
                if(UserSetFlag == null) return;
                UserSetFlag(new RemoteEvents.UserSetFlagArguments { user = user, button = button, value = value });
            }
            void IPairedMessagesWithUser.UserSetFlag(int user, int button, int value)
            {
                ((IMessages)this).UserSetFlag(user, button, value);
            }

            public event Action<RemoteEvents.RevealArguments> Reveal;
            void IMessages.Reveal(int button)
            {
                if(Reveal == null) return;
                Reveal(new RemoteEvents.RevealArguments { button = button });
            }
            void IPairedMessagesWithoutUser.Reveal(int button)
            {
                ((IMessages)this).Reveal(button);
            }

            public event Action<RemoteEvents.UserRevealArguments> UserReveal;
            void IMessages.UserReveal(int user, int button)
            {
                if(UserReveal == null) return;
                UserReveal(new RemoteEvents.UserRevealArguments { user = user, button = button });
            }
            void IPairedMessagesWithUser.UserReveal(int user, int button)
            {
                ((IMessages)this).UserReveal(user, button);
            }

            public event Action<RemoteEvents.AddScoreArguments> AddScore;
            void IMessages.AddScore(int score)
            {
                if(AddScore == null) return;
                AddScore(new RemoteEvents.AddScoreArguments { score = score });
            }

            public event Action<RemoteEvents.AwardAchievementFirstMinefieldCompleteArguments> AwardAchievementFirstMinefieldComplete;
            void IMessages.AwardAchievementFirstMinefieldComplete()
            {
                if(AwardAchievementFirstMinefieldComplete == null) return;
                AwardAchievementFirstMinefieldComplete(new RemoteEvents.AwardAchievementFirstMinefieldCompleteArguments {  });
            }

            public event Action<RemoteEvents.SendPasswordArguments> SendPassword;
            void IMessages.SendPassword(string password)
            {
                if(SendPassword == null) return;
                SendPassword(new RemoteEvents.SendPasswordArguments { password = password });
            }

            public event Action<RemoteEvents.ServerPasswordStatusArguments> ServerPasswordStatus;
            void IMessages.ServerPasswordStatus(int status)
            {
                if(ServerPasswordStatus == null) return;
                ServerPasswordStatus(new RemoteEvents.ServerPasswordStatusArguments { status = status });
            }

            public event Action<RemoteEvents.LockGameArguments> LockGame;
            void IMessages.LockGame()
            {
                if(LockGame == null) return;
                LockGame(new RemoteEvents.LockGameArguments {  });
            }

            public event Action<RemoteEvents.UnlockGameArguments> UnlockGame;
            void IMessages.UnlockGame()
            {
                if(UnlockGame == null) return;
                UnlockGame(new RemoteEvents.UnlockGameArguments {  });
            }

        }
        #endregion
    }
    #endregion
}
// 16.07.2008 10:32:37
