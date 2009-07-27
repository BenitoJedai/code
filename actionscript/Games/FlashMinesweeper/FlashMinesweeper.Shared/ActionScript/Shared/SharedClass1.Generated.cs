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

        #region RemoteMessages
        [Script]
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IMessages
        {
            public Action<SendArguments> Send;
            public Func<IEnumerable<IMessages>> VirtualTargets;
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
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ServerPlayerHello, args = new object[] { user, name } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerPlayerHello(user, name);
                    }
                }
            }
            public void ServerPlayerJoined(int user, string name)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ServerPlayerJoined, args = new object[] { user, name } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerPlayerJoined(user, name);
                    }
                }
            }
            public void ServerPlayerLeft(int user, string name)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ServerPlayerLeft, args = new object[] { user, name } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerPlayerLeft(user, name);
                    }
                }
            }
            public void PlayerAdvertise(string name)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.PlayerAdvertise, args = new object[] { name } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.PlayerAdvertise(name);
                    }
                }
            }
            public void UserPlayerAdvertise(int user, string name)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserPlayerAdvertise, args = new object[] { user, name } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserPlayerAdvertise(user, name);
                    }
                }
            }
            public void MouseMove(int x, int y, int color)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.MouseMove, args = new object[] { x, y, color } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.MouseMove(x, y, color);
                    }
                }
            }
            public void UserMouseMove(int user, int x, int y, int color)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserMouseMove, args = new object[] { user, x, y, color } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserMouseMove(user, x, y, color);
                    }
                }
            }
            public void MouseOut(int color)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.MouseOut, args = new object[] { color } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.MouseOut(color);
                    }
                }
            }
            public void UserMouseOut(int user, int color)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserMouseOut, args = new object[] { user, color } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserMouseOut(user, color);
                    }
                }
            }
            public void ServerSendMap()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ServerSendMap, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerSendMap();
                    }
                }
            }
            public void SendMap(int[] buttons)
            {
                if (this.Send != null)
                {
                    var args = new object[buttons.Length + 0];
                    Array.Copy(buttons, 0, args, 0, buttons.Length);
                    Send(new SendArguments { i = Messages.SendMap, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SendMap(buttons);
                    }
                }
            }
            public void UserSendMap(int user, int[] buttons)
            {
                if (this.Send != null)
                {
                    var args = new object[buttons.Length + 1];
                    args[0] = user;
                    Array.Copy(buttons, 0, args, 1, buttons.Length);
                    Send(new SendArguments { i = Messages.UserSendMap, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSendMap(user, buttons);
                    }
                }
            }
            public void SendMapLater()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SendMapLater, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SendMapLater();
                    }
                }
            }
            public void UserSendMapLater(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSendMapLater, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSendMapLater(user);
                    }
                }
            }
            public void SetFlag(int button, int value)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SetFlag, args = new object[] { button, value } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SetFlag(button, value);
                    }
                }
            }
            public void UserSetFlag(int user, int button, int value)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserSetFlag, args = new object[] { user, button, value } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSetFlag(user, button, value);
                    }
                }
            }
            public void Reveal(int button)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.Reveal, args = new object[] { button } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.Reveal(button);
                    }
                }
            }
            public void UserReveal(int user, int button)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserReveal, args = new object[] { user, button } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserReveal(user, button);
                    }
                }
            }
            public void AddScore(int score)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.AddScore, args = new object[] { score } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.AddScore(score);
                    }
                }
            }
            public void AwardAchievementFirstMinefieldComplete()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.AwardAchievementFirstMinefieldComplete, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.AwardAchievementFirstMinefieldComplete();
                    }
                }
            }
            public void SendPassword(string password)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.SendPassword, args = new object[] { password } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SendPassword(password);
                    }
                }
            }
            public void ServerPasswordStatus(int status)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ServerPasswordStatus, args = new object[] { status } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerPasswordStatus(status);
                    }
                }
            }
            public void LockGame()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.LockGame, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.LockGame();
                    }
                }
            }
            public void UnlockGame()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UnlockGame, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UnlockGame();
                    }
                }
            }
        }
        #endregion

        #region RemoteEvents
        [Script]
        [CompilerGenerated]
        public sealed partial class RemoteEvents : IEvents
        {
            private readonly Dictionary<Messages, Action<IDispatchHelper>> DispatchTable;
            private readonly Dictionary<Messages, Converter<object, Delegate>> DispatchTableDelegates;
            [AccessedThroughProperty("BroadcastRouter")]
            private WithUserArgumentsRouter_Broadcast _BroadcastRouter;
            [AccessedThroughProperty("SinglecastRouter")]
            private WithUserArgumentsRouter_Singlecast _SinglecastRouter;
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
                public Converter<uint, byte[]> GetMemoryStream { get; set; }
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
            #region WithUserArgumentsRouter_Broadcast
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter_Broadcast : WithUserArguments
            {
                public IMessages Target;

                #region Automatic Event Routing
                public void CombineDelegates(IEvents value)
                {
                    value.PlayerAdvertise += this.UserPlayerAdvertise;
                    value.MouseMove += this.UserMouseMove;
                    value.MouseOut += this.UserMouseOut;
                    value.SendMap += this.UserSendMap;
                    value.SendMapLater += this.UserSendMapLater;
                    value.SetFlag += this.UserSetFlag;
                    value.Reveal += this.UserReveal;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.PlayerAdvertise -= this.UserPlayerAdvertise;
                    value.MouseMove -= this.UserMouseMove;
                    value.MouseOut -= this.UserMouseOut;
                    value.SendMap -= this.UserSendMap;
                    value.SendMapLater -= this.UserSendMapLater;
                    value.SetFlag -= this.UserSetFlag;
                    value.Reveal -= this.UserReveal;
                }
                #endregion

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
            #region WithUserArgumentsRouter_SinglecastView
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter_SinglecastView : WithUserArguments
            {
                public IMessages Target;
                #region Routing
                public void UserPlayerAdvertise(string name)
                {
                    this.Target.UserPlayerAdvertise(this.user, name);
                }
                public void UserPlayerAdvertise(UserPlayerAdvertiseArguments e)
                {
                    this.Target.UserPlayerAdvertise(this.user, e.name);
                }
                public void UserMouseMove(int x, int y, int color)
                {
                    this.Target.UserMouseMove(this.user, x, y, color);
                }
                public void UserMouseMove(UserMouseMoveArguments e)
                {
                    this.Target.UserMouseMove(this.user, e.x, e.y, e.color);
                }
                public void UserMouseOut(int color)
                {
                    this.Target.UserMouseOut(this.user, color);
                }
                public void UserMouseOut(UserMouseOutArguments e)
                {
                    this.Target.UserMouseOut(this.user, e.color);
                }
                public void UserSendMap(int[] buttons)
                {
                    this.Target.UserSendMap(this.user, buttons);
                }
                public void UserSendMap(UserSendMapArguments e)
                {
                    this.Target.UserSendMap(this.user, e.buttons);
                }
                public void UserSendMapLater()
                {
                    this.Target.UserSendMapLater(this.user);
                }
                public void UserSendMapLater(UserSendMapLaterArguments e)
                {
                    this.Target.UserSendMapLater(this.user);
                }
                public void UserSetFlag(int button, int value)
                {
                    this.Target.UserSetFlag(this.user, button, value);
                }
                public void UserSetFlag(UserSetFlagArguments e)
                {
                    this.Target.UserSetFlag(this.user, e.button, e.value);
                }
                public void UserReveal(int button)
                {
                    this.Target.UserReveal(this.user, button);
                }
                public void UserReveal(UserRevealArguments e)
                {
                    this.Target.UserReveal(this.user, e.button);
                }
                #endregion
            }
            #endregion
            #region WithUserArgumentsRouter_Singlecast
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter_Singlecast : WithUserArguments
            {
                public System.Converter<int, IMessages> Target;

                #region Automatic Event Routing
                public void CombineDelegates(IEvents value)
                {
                    value.UserPlayerAdvertise += this.UserPlayerAdvertise;
                    value.UserMouseMove += this.UserMouseMove;
                    value.UserMouseOut += this.UserMouseOut;
                    value.UserSendMap += this.UserSendMap;
                    value.UserSendMapLater += this.UserSendMapLater;
                    value.UserSetFlag += this.UserSetFlag;
                    value.UserReveal += this.UserReveal;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.UserPlayerAdvertise -= this.UserPlayerAdvertise;
                    value.UserMouseMove -= this.UserMouseMove;
                    value.UserMouseOut -= this.UserMouseOut;
                    value.UserSendMap -= this.UserSendMap;
                    value.UserSendMapLater -= this.UserSendMapLater;
                    value.UserSetFlag -= this.UserSetFlag;
                    value.UserReveal -= this.UserReveal;
                }
                #endregion

                #region Routing
                public void UserPlayerAdvertise(UserPlayerAdvertiseArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserPlayerAdvertise(this.user, e.name);
                }
                public void UserMouseMove(UserMouseMoveArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMouseMove(this.user, e.x, e.y, e.color);
                }
                public void UserMouseOut(UserMouseOutArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMouseOut(this.user, e.color);
                }
                public void UserSendMap(UserSendMapArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSendMap(this.user, e.buttons);
                }
                public void UserSendMapLater(UserSendMapLaterArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSendMapLater(this.user);
                }
                public void UserSetFlag(UserSetFlagArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSetFlag(this.user, e.button, e.value);
                }
                public void UserReveal(UserRevealArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserReveal(this.user, e.button);
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
            public WithUserArgumentsRouter_Broadcast BroadcastRouter
            {
                [DebuggerNonUserCode]
                get
                {
                    return this._BroadcastRouter;
                }
                [DebuggerNonUserCode]
                [MethodImpl(MethodImplOptions.Synchronized)]
                set
                {
                    if(_BroadcastRouter != null)
                    {
                        _BroadcastRouter.RemoveDelegates(this);
                    }
                    _BroadcastRouter = value;
                    if(_BroadcastRouter != null)
                    {
                        _BroadcastRouter.CombineDelegates(this);
                    }
                }
            }
            public WithUserArgumentsRouter_Singlecast SinglecastRouter
            {
                [DebuggerNonUserCode]
                get
                {
                    return this._SinglecastRouter;
                }
                [DebuggerNonUserCode]
                [MethodImpl(MethodImplOptions.Synchronized)]
                set
                {
                    if(_SinglecastRouter != null)
                    {
                        _SinglecastRouter.RemoveDelegates(this);
                    }
                    _SinglecastRouter = value;
                    if(_SinglecastRouter != null)
                    {
                        _SinglecastRouter.CombineDelegates(this);
                    }
                }
            }
        }
        #endregion
        #region Bridge
        [Script]
        [CompilerGenerated]
        public partial class Bridge : IEvents, IMessages
        {
            public Action<Action> VirtualLatency;
            public Bridge()
            {
                this.VirtualLatency = VirtualLatencyDefaultImplemenetation;
            }
            public void VirtualLatencyDefaultImplemenetation(Action e)
            {
                e();
            }
            public event Action<RemoteEvents.ServerPlayerHelloArguments> ServerPlayerHello;
            void IMessages.ServerPlayerHello(int user, string name)
            {
                if(ServerPlayerHello == null) return;
                var v = new RemoteEvents.ServerPlayerHelloArguments { user = user, name = name };
                this.VirtualLatency(() => this.ServerPlayerHello(v));
            }

            public event Action<RemoteEvents.ServerPlayerJoinedArguments> ServerPlayerJoined;
            void IMessages.ServerPlayerJoined(int user, string name)
            {
                if(ServerPlayerJoined == null) return;
                var v = new RemoteEvents.ServerPlayerJoinedArguments { user = user, name = name };
                this.VirtualLatency(() => this.ServerPlayerJoined(v));
            }

            public event Action<RemoteEvents.ServerPlayerLeftArguments> ServerPlayerLeft;
            void IMessages.ServerPlayerLeft(int user, string name)
            {
                if(ServerPlayerLeft == null) return;
                var v = new RemoteEvents.ServerPlayerLeftArguments { user = user, name = name };
                this.VirtualLatency(() => this.ServerPlayerLeft(v));
            }

            public event Action<RemoteEvents.PlayerAdvertiseArguments> PlayerAdvertise;
            void IMessages.PlayerAdvertise(string name)
            {
                if(PlayerAdvertise == null) return;
                var v = new RemoteEvents.PlayerAdvertiseArguments { name = name };
                this.VirtualLatency(() => this.PlayerAdvertise(v));
            }

            public event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            void IMessages.UserPlayerAdvertise(int user, string name)
            {
                if(UserPlayerAdvertise == null) return;
                var v = new RemoteEvents.UserPlayerAdvertiseArguments { user = user, name = name };
                this.VirtualLatency(() => this.UserPlayerAdvertise(v));
            }

            public event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            void IMessages.MouseMove(int x, int y, int color)
            {
                if(MouseMove == null) return;
                var v = new RemoteEvents.MouseMoveArguments { x = x, y = y, color = color };
                this.VirtualLatency(() => this.MouseMove(v));
            }

            public event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            void IMessages.UserMouseMove(int user, int x, int y, int color)
            {
                if(UserMouseMove == null) return;
                var v = new RemoteEvents.UserMouseMoveArguments { user = user, x = x, y = y, color = color };
                this.VirtualLatency(() => this.UserMouseMove(v));
            }

            public event Action<RemoteEvents.MouseOutArguments> MouseOut;
            void IMessages.MouseOut(int color)
            {
                if(MouseOut == null) return;
                var v = new RemoteEvents.MouseOutArguments { color = color };
                this.VirtualLatency(() => this.MouseOut(v));
            }

            public event Action<RemoteEvents.UserMouseOutArguments> UserMouseOut;
            void IMessages.UserMouseOut(int user, int color)
            {
                if(UserMouseOut == null) return;
                var v = new RemoteEvents.UserMouseOutArguments { user = user, color = color };
                this.VirtualLatency(() => this.UserMouseOut(v));
            }

            public event Action<RemoteEvents.ServerSendMapArguments> ServerSendMap;
            void IMessages.ServerSendMap()
            {
                if(ServerSendMap == null) return;
                var v = new RemoteEvents.ServerSendMapArguments {  };
                this.VirtualLatency(() => this.ServerSendMap(v));
            }

            public event Action<RemoteEvents.SendMapArguments> SendMap;
            void IMessages.SendMap(int[] buttons)
            {
                if(SendMap == null) return;
                var v = new RemoteEvents.SendMapArguments { buttons = buttons };
                this.VirtualLatency(() => this.SendMap(v));
            }

            public event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            void IMessages.UserSendMap(int user, int[] buttons)
            {
                if(UserSendMap == null) return;
                var v = new RemoteEvents.UserSendMapArguments { user = user, buttons = buttons };
                this.VirtualLatency(() => this.UserSendMap(v));
            }

            public event Action<RemoteEvents.SendMapLaterArguments> SendMapLater;
            void IMessages.SendMapLater()
            {
                if(SendMapLater == null) return;
                var v = new RemoteEvents.SendMapLaterArguments {  };
                this.VirtualLatency(() => this.SendMapLater(v));
            }

            public event Action<RemoteEvents.UserSendMapLaterArguments> UserSendMapLater;
            void IMessages.UserSendMapLater(int user)
            {
                if(UserSendMapLater == null) return;
                var v = new RemoteEvents.UserSendMapLaterArguments { user = user };
                this.VirtualLatency(() => this.UserSendMapLater(v));
            }

            public event Action<RemoteEvents.SetFlagArguments> SetFlag;
            void IMessages.SetFlag(int button, int value)
            {
                if(SetFlag == null) return;
                var v = new RemoteEvents.SetFlagArguments { button = button, value = value };
                this.VirtualLatency(() => this.SetFlag(v));
            }

            public event Action<RemoteEvents.UserSetFlagArguments> UserSetFlag;
            void IMessages.UserSetFlag(int user, int button, int value)
            {
                if(UserSetFlag == null) return;
                var v = new RemoteEvents.UserSetFlagArguments { user = user, button = button, value = value };
                this.VirtualLatency(() => this.UserSetFlag(v));
            }

            public event Action<RemoteEvents.RevealArguments> Reveal;
            void IMessages.Reveal(int button)
            {
                if(Reveal == null) return;
                var v = new RemoteEvents.RevealArguments { button = button };
                this.VirtualLatency(() => this.Reveal(v));
            }

            public event Action<RemoteEvents.UserRevealArguments> UserReveal;
            void IMessages.UserReveal(int user, int button)
            {
                if(UserReveal == null) return;
                var v = new RemoteEvents.UserRevealArguments { user = user, button = button };
                this.VirtualLatency(() => this.UserReveal(v));
            }

            public event Action<RemoteEvents.AddScoreArguments> AddScore;
            void IMessages.AddScore(int score)
            {
                if(AddScore == null) return;
                var v = new RemoteEvents.AddScoreArguments { score = score };
                this.VirtualLatency(() => this.AddScore(v));
            }

            public event Action<RemoteEvents.AwardAchievementFirstMinefieldCompleteArguments> AwardAchievementFirstMinefieldComplete;
            void IMessages.AwardAchievementFirstMinefieldComplete()
            {
                if(AwardAchievementFirstMinefieldComplete == null) return;
                var v = new RemoteEvents.AwardAchievementFirstMinefieldCompleteArguments {  };
                this.VirtualLatency(() => this.AwardAchievementFirstMinefieldComplete(v));
            }

            public event Action<RemoteEvents.SendPasswordArguments> SendPassword;
            void IMessages.SendPassword(string password)
            {
                if(SendPassword == null) return;
                var v = new RemoteEvents.SendPasswordArguments { password = password };
                this.VirtualLatency(() => this.SendPassword(v));
            }

            public event Action<RemoteEvents.ServerPasswordStatusArguments> ServerPasswordStatus;
            void IMessages.ServerPasswordStatus(int status)
            {
                if(ServerPasswordStatus == null) return;
                var v = new RemoteEvents.ServerPasswordStatusArguments { status = status };
                this.VirtualLatency(() => this.ServerPasswordStatus(v));
            }

            public event Action<RemoteEvents.LockGameArguments> LockGame;
            void IMessages.LockGame()
            {
                if(LockGame == null) return;
                var v = new RemoteEvents.LockGameArguments {  };
                this.VirtualLatency(() => this.LockGame(v));
            }

            public event Action<RemoteEvents.UnlockGameArguments> UnlockGame;
            void IMessages.UnlockGame()
            {
                if(UnlockGame == null) return;
                var v = new RemoteEvents.UnlockGameArguments {  };
                this.VirtualLatency(() => this.UnlockGame(v));
            }

        }
        #endregion
    }
    #endregion
}
// 27.07.2009 9:06:47
