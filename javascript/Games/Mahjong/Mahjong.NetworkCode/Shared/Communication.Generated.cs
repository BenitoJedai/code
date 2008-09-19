using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ScriptCoreLib.Shared.Nonoba;
using ScriptCoreLib;

namespace Mahjong.NetworkCode.Shared
{
    #region Communication
    [Script]
    [CompilerGenerated]
    public partial class Communication
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
            UserPlayerAdvertise,
            UserMapRequest,
            UserMapResponse,
            MapReload,
            UserMapReload,
            MouseMove,
            UserMouseMove,
            MouseOut,
            UserMouseOut,
            LevelHasEnded,
            UserLevelHasEnded,
            SetFlag,
            UserSetFlag,
            Reveal,
            UserReveal,
            AddScore,
            AwardAchievementFirst,
            SendPassword,
            ServerPasswordStatus,
            LockGame,
            UnlockGame,
            UserSayLine,
            UserLockEnter,
            UserLockValidate,
            UserLockExit,
            RemovePair,
            UserRemovePair,
            GoBack,
            UserGoBack,
            GoForward,
            UserGoForward,
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
            event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            event Action<RemoteEvents.UserMapRequestArguments> UserMapRequest;
            event Action<RemoteEvents.UserMapResponseArguments> UserMapResponse;
            event Action<RemoteEvents.MapReloadArguments> MapReload;
            event Action<RemoteEvents.UserMapReloadArguments> UserMapReload;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            event Action<RemoteEvents.MouseOutArguments> MouseOut;
            event Action<RemoteEvents.UserMouseOutArguments> UserMouseOut;
            event Action<RemoteEvents.LevelHasEndedArguments> LevelHasEnded;
            event Action<RemoteEvents.UserLevelHasEndedArguments> UserLevelHasEnded;
            event Action<RemoteEvents.SetFlagArguments> SetFlag;
            event Action<RemoteEvents.UserSetFlagArguments> UserSetFlag;
            event Action<RemoteEvents.RevealArguments> Reveal;
            event Action<RemoteEvents.UserRevealArguments> UserReveal;
            event Action<RemoteEvents.AddScoreArguments> AddScore;
            event Action<RemoteEvents.AwardAchievementFirstArguments> AwardAchievementFirst;
            event Action<RemoteEvents.SendPasswordArguments> SendPassword;
            event Action<RemoteEvents.ServerPasswordStatusArguments> ServerPasswordStatus;
            event Action<RemoteEvents.LockGameArguments> LockGame;
            event Action<RemoteEvents.UnlockGameArguments> UnlockGame;
            event Action<RemoteEvents.UserSayLineArguments> UserSayLine;
            event Action<RemoteEvents.UserLockEnterArguments> UserLockEnter;
            event Action<RemoteEvents.UserLockValidateArguments> UserLockValidate;
            event Action<RemoteEvents.UserLockExitArguments> UserLockExit;
            event Action<RemoteEvents.RemovePairArguments> RemovePair;
            event Action<RemoteEvents.UserRemovePairArguments> UserRemovePair;
            event Action<RemoteEvents.GoBackArguments> GoBack;
            event Action<RemoteEvents.UserGoBackArguments> UserGoBack;
            event Action<RemoteEvents.GoForwardArguments> GoForward;
            event Action<RemoteEvents.UserGoForwardArguments> UserGoForward;
        }
        #endregion

        #region RemoteMessages
        [Script]
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IMessages
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
            public void ServerPlayerHello(int user, string name, int others, int[] handshake)
            {
                var args = new object[handshake.Length + 3];
                args[0] = user;
                args[1] = name;
                args[2] = others;
                Array.Copy(handshake, 0, args, 3, handshake.Length);
                Send(new SendArguments { i = Messages.ServerPlayerHello, args = args });
            }
            public void ServerPlayerJoined(int user, string name)
            {
                Send(new SendArguments { i = Messages.ServerPlayerJoined, args = new object[] { user, name } });
            }
            public void ServerPlayerLeft(int user, string name)
            {
                Send(new SendArguments { i = Messages.ServerPlayerLeft, args = new object[] { user, name } });
            }
            public void UserPlayerAdvertise(int user, string name)
            {
                Send(new SendArguments { i = Messages.UserPlayerAdvertise, args = new object[] { user, name } });
            }
            public void UserMapRequest(int user)
            {
                Send(new SendArguments { i = Messages.UserMapRequest, args = new object[] { user } });
            }
            public void UserMapResponse(int user, int[] bytes)
            {
                var args = new object[bytes.Length + 1];
                args[0] = user;
                Array.Copy(bytes, 0, args, 1, bytes.Length);
                Send(new SendArguments { i = Messages.UserMapResponse, args = args });
            }
            public void MapReload(int[] bytes)
            {
                var args = new object[bytes.Length + 0];
                Array.Copy(bytes, 0, args, 0, bytes.Length);
                Send(new SendArguments { i = Messages.MapReload, args = args });
            }
            public void UserMapReload(int user, int[] bytes)
            {
                var args = new object[bytes.Length + 1];
                args[0] = user;
                Array.Copy(bytes, 0, args, 1, bytes.Length);
                Send(new SendArguments { i = Messages.UserMapReload, args = args });
            }
            public void MouseMove(int x, int y)
            {
                Send(new SendArguments { i = Messages.MouseMove, args = new object[] { x, y } });
            }
            public void UserMouseMove(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserMouseMove, args = new object[] { user, x, y } });
            }
            public void MouseOut(int color)
            {
                Send(new SendArguments { i = Messages.MouseOut, args = new object[] { color } });
            }
            public void UserMouseOut(int user, int color)
            {
                Send(new SendArguments { i = Messages.UserMouseOut, args = new object[] { user, color } });
            }
            public void LevelHasEnded()
            {
                Send(new SendArguments { i = Messages.LevelHasEnded, args = new object[] {  } });
            }
            public void UserLevelHasEnded(int user)
            {
                Send(new SendArguments { i = Messages.UserLevelHasEnded, args = new object[] { user } });
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
            public void AwardAchievementFirst()
            {
                Send(new SendArguments { i = Messages.AwardAchievementFirst, args = new object[] {  } });
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
            public void UserSayLine(int user, string text)
            {
                Send(new SendArguments { i = Messages.UserSayLine, args = new object[] { user, text } });
            }
            public void UserLockEnter(int user, int id)
            {
                Send(new SendArguments { i = Messages.UserLockEnter, args = new object[] { user, id } });
            }
            public void UserLockValidate(int user, int id)
            {
                Send(new SendArguments { i = Messages.UserLockValidate, args = new object[] { user, id } });
            }
            public void UserLockExit(int user, int id)
            {
                Send(new SendArguments { i = Messages.UserLockExit, args = new object[] { user, id } });
            }
            public void RemovePair(int a, int b)
            {
                Send(new SendArguments { i = Messages.RemovePair, args = new object[] { a, b } });
            }
            public void UserRemovePair(int user, int a, int b)
            {
                Send(new SendArguments { i = Messages.UserRemovePair, args = new object[] { user, a, b } });
            }
            public void GoBack()
            {
                Send(new SendArguments { i = Messages.GoBack, args = new object[] {  } });
            }
            public void UserGoBack(int user)
            {
                Send(new SendArguments { i = Messages.UserGoBack, args = new object[] { user } });
            }
            public void GoForward()
            {
                Send(new SendArguments { i = Messages.GoForward, args = new object[] {  } });
            }
            public void UserGoForward(int user)
            {
                Send(new SendArguments { i = Messages.UserGoForward, args = new object[] { user } });
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
                    value.MapReload += this.UserMapReload;
                    value.MouseMove += this.UserMouseMove;
                    value.MouseOut += this.UserMouseOut;
                    value.LevelHasEnded += this.UserLevelHasEnded;
                    value.SetFlag += this.UserSetFlag;
                    value.Reveal += this.UserReveal;
                    value.RemovePair += this.UserRemovePair;
                    value.GoBack += this.UserGoBack;
                    value.GoForward += this.UserGoForward;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.MapReload -= this.UserMapReload;
                    value.MouseMove -= this.UserMouseMove;
                    value.MouseOut -= this.UserMouseOut;
                    value.LevelHasEnded -= this.UserLevelHasEnded;
                    value.SetFlag -= this.UserSetFlag;
                    value.Reveal -= this.UserReveal;
                    value.RemovePair -= this.UserRemovePair;
                    value.GoBack -= this.UserGoBack;
                    value.GoForward -= this.UserGoForward;
                }
                #endregion

                #region Routing
                public void UserMapReload(MapReloadArguments e)
                {
                    Target.UserMapReload(this.user, e.bytes);
                }
                public void UserMouseMove(MouseMoveArguments e)
                {
                    Target.UserMouseMove(this.user, e.x, e.y);
                }
                public void UserMouseOut(MouseOutArguments e)
                {
                    Target.UserMouseOut(this.user, e.color);
                }
                public void UserLevelHasEnded(LevelHasEndedArguments e)
                {
                    Target.UserLevelHasEnded(this.user);
                }
                public void UserSetFlag(SetFlagArguments e)
                {
                    Target.UserSetFlag(this.user, e.button, e.value);
                }
                public void UserReveal(RevealArguments e)
                {
                    Target.UserReveal(this.user, e.button);
                }
                public void UserRemovePair(RemovePairArguments e)
                {
                    Target.UserRemovePair(this.user, e.a, e.b);
                }
                public void UserGoBack(GoBackArguments e)
                {
                    Target.UserGoBack(this.user);
                }
                public void UserGoForward(GoForwardArguments e)
                {
                    Target.UserGoForward(this.user);
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
                public void UserMapRequest()
                {
                    this.Target.UserMapRequest(this.user);
                }
                public void UserMapRequest(UserMapRequestArguments e)
                {
                    this.Target.UserMapRequest(this.user);
                }
                public void UserMapResponse(int[] bytes)
                {
                    this.Target.UserMapResponse(this.user, bytes);
                }
                public void UserMapResponse(UserMapResponseArguments e)
                {
                    this.Target.UserMapResponse(this.user, e.bytes);
                }
                public void UserMapReload(int[] bytes)
                {
                    this.Target.UserMapReload(this.user, bytes);
                }
                public void UserMapReload(UserMapReloadArguments e)
                {
                    this.Target.UserMapReload(this.user, e.bytes);
                }
                public void UserMouseMove(int x, int y)
                {
                    this.Target.UserMouseMove(this.user, x, y);
                }
                public void UserMouseMove(UserMouseMoveArguments e)
                {
                    this.Target.UserMouseMove(this.user, e.x, e.y);
                }
                public void UserMouseOut(int color)
                {
                    this.Target.UserMouseOut(this.user, color);
                }
                public void UserMouseOut(UserMouseOutArguments e)
                {
                    this.Target.UserMouseOut(this.user, e.color);
                }
                public void UserLevelHasEnded()
                {
                    this.Target.UserLevelHasEnded(this.user);
                }
                public void UserLevelHasEnded(UserLevelHasEndedArguments e)
                {
                    this.Target.UserLevelHasEnded(this.user);
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
                public void UserSayLine(string text)
                {
                    this.Target.UserSayLine(this.user, text);
                }
                public void UserSayLine(UserSayLineArguments e)
                {
                    this.Target.UserSayLine(this.user, e.text);
                }
                public void UserLockEnter(int id)
                {
                    this.Target.UserLockEnter(this.user, id);
                }
                public void UserLockEnter(UserLockEnterArguments e)
                {
                    this.Target.UserLockEnter(this.user, e.id);
                }
                public void UserLockValidate(int id)
                {
                    this.Target.UserLockValidate(this.user, id);
                }
                public void UserLockValidate(UserLockValidateArguments e)
                {
                    this.Target.UserLockValidate(this.user, e.id);
                }
                public void UserLockExit(int id)
                {
                    this.Target.UserLockExit(this.user, id);
                }
                public void UserLockExit(UserLockExitArguments e)
                {
                    this.Target.UserLockExit(this.user, e.id);
                }
                public void UserRemovePair(int a, int b)
                {
                    this.Target.UserRemovePair(this.user, a, b);
                }
                public void UserRemovePair(UserRemovePairArguments e)
                {
                    this.Target.UserRemovePair(this.user, e.a, e.b);
                }
                public void UserGoBack()
                {
                    this.Target.UserGoBack(this.user);
                }
                public void UserGoBack(UserGoBackArguments e)
                {
                    this.Target.UserGoBack(this.user);
                }
                public void UserGoForward()
                {
                    this.Target.UserGoForward(this.user);
                }
                public void UserGoForward(UserGoForwardArguments e)
                {
                    this.Target.UserGoForward(this.user);
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
                    value.UserMapRequest += this.UserMapRequest;
                    value.UserMapResponse += this.UserMapResponse;
                    value.UserMapReload += this.UserMapReload;
                    value.UserMouseMove += this.UserMouseMove;
                    value.UserMouseOut += this.UserMouseOut;
                    value.UserLevelHasEnded += this.UserLevelHasEnded;
                    value.UserSetFlag += this.UserSetFlag;
                    value.UserReveal += this.UserReveal;
                    value.UserSayLine += this.UserSayLine;
                    value.UserLockEnter += this.UserLockEnter;
                    value.UserLockValidate += this.UserLockValidate;
                    value.UserLockExit += this.UserLockExit;
                    value.UserRemovePair += this.UserRemovePair;
                    value.UserGoBack += this.UserGoBack;
                    value.UserGoForward += this.UserGoForward;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.UserPlayerAdvertise -= this.UserPlayerAdvertise;
                    value.UserMapRequest -= this.UserMapRequest;
                    value.UserMapResponse -= this.UserMapResponse;
                    value.UserMapReload -= this.UserMapReload;
                    value.UserMouseMove -= this.UserMouseMove;
                    value.UserMouseOut -= this.UserMouseOut;
                    value.UserLevelHasEnded -= this.UserLevelHasEnded;
                    value.UserSetFlag -= this.UserSetFlag;
                    value.UserReveal -= this.UserReveal;
                    value.UserSayLine -= this.UserSayLine;
                    value.UserLockEnter -= this.UserLockEnter;
                    value.UserLockValidate -= this.UserLockValidate;
                    value.UserLockExit -= this.UserLockExit;
                    value.UserRemovePair -= this.UserRemovePair;
                    value.UserGoBack -= this.UserGoBack;
                    value.UserGoForward -= this.UserGoForward;
                }
                #endregion

                #region Routing
                public void UserPlayerAdvertise(UserPlayerAdvertiseArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserPlayerAdvertise(this.user, e.name);
                }
                public void UserMapRequest(UserMapRequestArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMapRequest(this.user);
                }
                public void UserMapResponse(UserMapResponseArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMapResponse(this.user, e.bytes);
                }
                public void UserMapReload(UserMapReloadArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMapReload(this.user, e.bytes);
                }
                public void UserMouseMove(UserMouseMoveArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMouseMove(this.user, e.x, e.y);
                }
                public void UserMouseOut(UserMouseOutArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserMouseOut(this.user, e.color);
                }
                public void UserLevelHasEnded(UserLevelHasEndedArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserLevelHasEnded(this.user);
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
                public void UserSayLine(UserSayLineArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSayLine(this.user, e.text);
                }
                public void UserLockEnter(UserLockEnterArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserLockEnter(this.user, e.id);
                }
                public void UserLockValidate(UserLockValidateArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserLockValidate(this.user, e.id);
                }
                public void UserLockExit(UserLockExitArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserLockExit(this.user, e.id);
                }
                public void UserRemovePair(UserRemovePairArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserRemovePair(this.user, e.a, e.b);
                }
                public void UserGoBack(UserGoBackArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserGoBack(this.user);
                }
                public void UserGoForward(UserGoForwardArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserGoForward(this.user);
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
                public int others;
                public int[] handshake;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(", others = ").Append(this.others).Append(", handshake = ").Append(this.handshake).Append(" }").ToString();
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
            #region UserMapRequestArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserMapRequestArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserMapRequestArguments> UserMapRequest;
            #region UserMapResponseArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserMapResponseArguments : WithUserArguments
            {
                public int[] bytes;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", bytes = ").Append(this.bytes).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserMapResponseArguments> UserMapResponse;
            #region MapReloadArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class MapReloadArguments
            {
                public int[] bytes;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ bytes = ").Append(this.bytes).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<MapReloadArguments> MapReload;
            #region UserMapReloadArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserMapReloadArguments : WithUserArguments
            {
                public int[] bytes;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", bytes = ").Append(this.bytes).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserMapReloadArguments> UserMapReload;
            #region MouseMoveArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class MouseMoveArguments
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
            public event Action<MouseMoveArguments> MouseMove;
            #region UserMouseMoveArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserMouseMoveArguments : WithUserArguments
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
            #region LevelHasEndedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class LevelHasEndedArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<LevelHasEndedArguments> LevelHasEnded;
            #region UserLevelHasEndedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserLevelHasEndedArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserLevelHasEndedArguments> UserLevelHasEnded;
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
            #region AwardAchievementFirstArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AwardAchievementFirstArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<AwardAchievementFirstArguments> AwardAchievementFirst;
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
            #region UserSayLineArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSayLineArguments : WithUserArguments
            {
                public string text;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", text = ").Append(this.text).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSayLineArguments> UserSayLine;
            #region UserLockEnterArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserLockEnterArguments : WithUserArguments
            {
                public int id;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", id = ").Append(this.id).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserLockEnterArguments> UserLockEnter;
            #region UserLockValidateArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserLockValidateArguments : WithUserArguments
            {
                public int id;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", id = ").Append(this.id).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserLockValidateArguments> UserLockValidate;
            #region UserLockExitArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserLockExitArguments : WithUserArguments
            {
                public int id;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", id = ").Append(this.id).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserLockExitArguments> UserLockExit;
            #region RemovePairArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class RemovePairArguments
            {
                public int a;
                public int b;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ a = ").Append(this.a).Append(", b = ").Append(this.b).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<RemovePairArguments> RemovePair;
            #region UserRemovePairArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserRemovePairArguments : WithUserArguments
            {
                public int a;
                public int b;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", a = ").Append(this.a).Append(", b = ").Append(this.b).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserRemovePairArguments> UserRemovePair;
            #region GoBackArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class GoBackArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<GoBackArguments> GoBack;
            #region UserGoBackArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserGoBackArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserGoBackArguments> UserGoBack;
            #region GoForwardArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class GoForwardArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<GoForwardArguments> GoForward;
            #region UserGoForwardArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserGoForwardArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserGoForwardArguments> UserGoForward;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<IDispatchHelper>>
                        {
                            { Messages.ServerPlayerHello, e => { ServerPlayerHello(new ServerPlayerHelloArguments { user = e.GetInt32(0), name = e.GetString(1), others = e.GetInt32(2), handshake = e.GetInt32Array(3) }); } },
                            { Messages.ServerPlayerJoined, e => { ServerPlayerJoined(new ServerPlayerJoinedArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerLeft, e => { ServerPlayerLeft(new ServerPlayerLeftArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.UserPlayerAdvertise, e => { UserPlayerAdvertise(new UserPlayerAdvertiseArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.UserMapRequest, e => { UserMapRequest(new UserMapRequestArguments { user = e.GetInt32(0) }); } },
                            { Messages.UserMapResponse, e => { UserMapResponse(new UserMapResponseArguments { user = e.GetInt32(0), bytes = e.GetInt32Array(1) }); } },
                            { Messages.MapReload, e => { MapReload(new MapReloadArguments { bytes = e.GetInt32Array(0) }); } },
                            { Messages.UserMapReload, e => { UserMapReload(new UserMapReloadArguments { user = e.GetInt32(0), bytes = e.GetInt32Array(1) }); } },
                            { Messages.MouseMove, e => { MouseMove(new MouseMoveArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserMouseMove, e => { UserMouseMove(new UserMouseMoveArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.MouseOut, e => { MouseOut(new MouseOutArguments { color = e.GetInt32(0) }); } },
                            { Messages.UserMouseOut, e => { UserMouseOut(new UserMouseOutArguments { user = e.GetInt32(0), color = e.GetInt32(1) }); } },
                            { Messages.LevelHasEnded, e => { LevelHasEnded(new LevelHasEndedArguments {  }); } },
                            { Messages.UserLevelHasEnded, e => { UserLevelHasEnded(new UserLevelHasEndedArguments { user = e.GetInt32(0) }); } },
                            { Messages.SetFlag, e => { SetFlag(new SetFlagArguments { button = e.GetInt32(0), value = e.GetInt32(1) }); } },
                            { Messages.UserSetFlag, e => { UserSetFlag(new UserSetFlagArguments { user = e.GetInt32(0), button = e.GetInt32(1), value = e.GetInt32(2) }); } },
                            { Messages.Reveal, e => { Reveal(new RevealArguments { button = e.GetInt32(0) }); } },
                            { Messages.UserReveal, e => { UserReveal(new UserRevealArguments { user = e.GetInt32(0), button = e.GetInt32(1) }); } },
                            { Messages.AddScore, e => { AddScore(new AddScoreArguments { score = e.GetInt32(0) }); } },
                            { Messages.AwardAchievementFirst, e => { AwardAchievementFirst(new AwardAchievementFirstArguments {  }); } },
                            { Messages.SendPassword, e => { SendPassword(new SendPasswordArguments { password = e.GetString(0) }); } },
                            { Messages.ServerPasswordStatus, e => { ServerPasswordStatus(new ServerPasswordStatusArguments { status = e.GetInt32(0) }); } },
                            { Messages.LockGame, e => { LockGame(new LockGameArguments {  }); } },
                            { Messages.UnlockGame, e => { UnlockGame(new UnlockGameArguments {  }); } },
                            { Messages.UserSayLine, e => { UserSayLine(new UserSayLineArguments { user = e.GetInt32(0), text = e.GetString(1) }); } },
                            { Messages.UserLockEnter, e => { UserLockEnter(new UserLockEnterArguments { user = e.GetInt32(0), id = e.GetInt32(1) }); } },
                            { Messages.UserLockValidate, e => { UserLockValidate(new UserLockValidateArguments { user = e.GetInt32(0), id = e.GetInt32(1) }); } },
                            { Messages.UserLockExit, e => { UserLockExit(new UserLockExitArguments { user = e.GetInt32(0), id = e.GetInt32(1) }); } },
                            { Messages.RemovePair, e => { RemovePair(new RemovePairArguments { a = e.GetInt32(0), b = e.GetInt32(1) }); } },
                            { Messages.UserRemovePair, e => { UserRemovePair(new UserRemovePairArguments { user = e.GetInt32(0), a = e.GetInt32(1), b = e.GetInt32(2) }); } },
                            { Messages.GoBack, e => { GoBack(new GoBackArguments {  }); } },
                            { Messages.UserGoBack, e => { UserGoBack(new UserGoBackArguments { user = e.GetInt32(0) }); } },
                            { Messages.GoForward, e => { GoForward(new GoForwardArguments {  }); } },
                            { Messages.UserGoForward, e => { UserGoForward(new UserGoForwardArguments { user = e.GetInt32(0) }); } },
                        }
                ;
                DispatchTableDelegates = new Dictionary<Messages, Converter<object, Delegate>>
                        {
                            { Messages.ServerPlayerHello, e => ServerPlayerHello },
                            { Messages.ServerPlayerJoined, e => ServerPlayerJoined },
                            { Messages.ServerPlayerLeft, e => ServerPlayerLeft },
                            { Messages.UserPlayerAdvertise, e => UserPlayerAdvertise },
                            { Messages.UserMapRequest, e => UserMapRequest },
                            { Messages.UserMapResponse, e => UserMapResponse },
                            { Messages.MapReload, e => MapReload },
                            { Messages.UserMapReload, e => UserMapReload },
                            { Messages.MouseMove, e => MouseMove },
                            { Messages.UserMouseMove, e => UserMouseMove },
                            { Messages.MouseOut, e => MouseOut },
                            { Messages.UserMouseOut, e => UserMouseOut },
                            { Messages.LevelHasEnded, e => LevelHasEnded },
                            { Messages.UserLevelHasEnded, e => UserLevelHasEnded },
                            { Messages.SetFlag, e => SetFlag },
                            { Messages.UserSetFlag, e => UserSetFlag },
                            { Messages.Reveal, e => Reveal },
                            { Messages.UserReveal, e => UserReveal },
                            { Messages.AddScore, e => AddScore },
                            { Messages.AwardAchievementFirst, e => AwardAchievementFirst },
                            { Messages.SendPassword, e => SendPassword },
                            { Messages.ServerPasswordStatus, e => ServerPasswordStatus },
                            { Messages.LockGame, e => LockGame },
                            { Messages.UnlockGame, e => UnlockGame },
                            { Messages.UserSayLine, e => UserSayLine },
                            { Messages.UserLockEnter, e => UserLockEnter },
                            { Messages.UserLockValidate, e => UserLockValidate },
                            { Messages.UserLockExit, e => UserLockExit },
                            { Messages.RemovePair, e => RemovePair },
                            { Messages.UserRemovePair, e => UserRemovePair },
                            { Messages.GoBack, e => GoBack },
                            { Messages.UserGoBack, e => UserGoBack },
                            { Messages.GoForward, e => GoForward },
                            { Messages.UserGoForward, e => UserGoForward },
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
            void IMessages.ServerPlayerHello(int user, string name, int others, int[] handshake)
            {
                if(ServerPlayerHello == null) return;
                var v = new RemoteEvents.ServerPlayerHelloArguments { user = user, name = name, others = others, handshake = handshake };
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

            public event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            void IMessages.UserPlayerAdvertise(int user, string name)
            {
                if(UserPlayerAdvertise == null) return;
                var v = new RemoteEvents.UserPlayerAdvertiseArguments { user = user, name = name };
                this.VirtualLatency(() => this.UserPlayerAdvertise(v));
            }

            public event Action<RemoteEvents.UserMapRequestArguments> UserMapRequest;
            void IMessages.UserMapRequest(int user)
            {
                if(UserMapRequest == null) return;
                var v = new RemoteEvents.UserMapRequestArguments { user = user };
                this.VirtualLatency(() => this.UserMapRequest(v));
            }

            public event Action<RemoteEvents.UserMapResponseArguments> UserMapResponse;
            void IMessages.UserMapResponse(int user, int[] bytes)
            {
                if(UserMapResponse == null) return;
                var v = new RemoteEvents.UserMapResponseArguments { user = user, bytes = bytes };
                this.VirtualLatency(() => this.UserMapResponse(v));
            }

            public event Action<RemoteEvents.MapReloadArguments> MapReload;
            void IMessages.MapReload(int[] bytes)
            {
                if(MapReload == null) return;
                var v = new RemoteEvents.MapReloadArguments { bytes = bytes };
                this.VirtualLatency(() => this.MapReload(v));
            }

            public event Action<RemoteEvents.UserMapReloadArguments> UserMapReload;
            void IMessages.UserMapReload(int user, int[] bytes)
            {
                if(UserMapReload == null) return;
                var v = new RemoteEvents.UserMapReloadArguments { user = user, bytes = bytes };
                this.VirtualLatency(() => this.UserMapReload(v));
            }

            public event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            void IMessages.MouseMove(int x, int y)
            {
                if(MouseMove == null) return;
                var v = new RemoteEvents.MouseMoveArguments { x = x, y = y };
                this.VirtualLatency(() => this.MouseMove(v));
            }

            public event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            void IMessages.UserMouseMove(int user, int x, int y)
            {
                if(UserMouseMove == null) return;
                var v = new RemoteEvents.UserMouseMoveArguments { user = user, x = x, y = y };
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

            public event Action<RemoteEvents.LevelHasEndedArguments> LevelHasEnded;
            void IMessages.LevelHasEnded()
            {
                if(LevelHasEnded == null) return;
                var v = new RemoteEvents.LevelHasEndedArguments {  };
                this.VirtualLatency(() => this.LevelHasEnded(v));
            }

            public event Action<RemoteEvents.UserLevelHasEndedArguments> UserLevelHasEnded;
            void IMessages.UserLevelHasEnded(int user)
            {
                if(UserLevelHasEnded == null) return;
                var v = new RemoteEvents.UserLevelHasEndedArguments { user = user };
                this.VirtualLatency(() => this.UserLevelHasEnded(v));
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

            public event Action<RemoteEvents.AwardAchievementFirstArguments> AwardAchievementFirst;
            void IMessages.AwardAchievementFirst()
            {
                if(AwardAchievementFirst == null) return;
                var v = new RemoteEvents.AwardAchievementFirstArguments {  };
                this.VirtualLatency(() => this.AwardAchievementFirst(v));
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

            public event Action<RemoteEvents.UserSayLineArguments> UserSayLine;
            void IMessages.UserSayLine(int user, string text)
            {
                if(UserSayLine == null) return;
                var v = new RemoteEvents.UserSayLineArguments { user = user, text = text };
                this.VirtualLatency(() => this.UserSayLine(v));
            }

            public event Action<RemoteEvents.UserLockEnterArguments> UserLockEnter;
            void IMessages.UserLockEnter(int user, int id)
            {
                if(UserLockEnter == null) return;
                var v = new RemoteEvents.UserLockEnterArguments { user = user, id = id };
                this.VirtualLatency(() => this.UserLockEnter(v));
            }

            public event Action<RemoteEvents.UserLockValidateArguments> UserLockValidate;
            void IMessages.UserLockValidate(int user, int id)
            {
                if(UserLockValidate == null) return;
                var v = new RemoteEvents.UserLockValidateArguments { user = user, id = id };
                this.VirtualLatency(() => this.UserLockValidate(v));
            }

            public event Action<RemoteEvents.UserLockExitArguments> UserLockExit;
            void IMessages.UserLockExit(int user, int id)
            {
                if(UserLockExit == null) return;
                var v = new RemoteEvents.UserLockExitArguments { user = user, id = id };
                this.VirtualLatency(() => this.UserLockExit(v));
            }

            public event Action<RemoteEvents.RemovePairArguments> RemovePair;
            void IMessages.RemovePair(int a, int b)
            {
                if(RemovePair == null) return;
                var v = new RemoteEvents.RemovePairArguments { a = a, b = b };
                this.VirtualLatency(() => this.RemovePair(v));
            }

            public event Action<RemoteEvents.UserRemovePairArguments> UserRemovePair;
            void IMessages.UserRemovePair(int user, int a, int b)
            {
                if(UserRemovePair == null) return;
                var v = new RemoteEvents.UserRemovePairArguments { user = user, a = a, b = b };
                this.VirtualLatency(() => this.UserRemovePair(v));
            }

            public event Action<RemoteEvents.GoBackArguments> GoBack;
            void IMessages.GoBack()
            {
                if(GoBack == null) return;
                var v = new RemoteEvents.GoBackArguments {  };
                this.VirtualLatency(() => this.GoBack(v));
            }

            public event Action<RemoteEvents.UserGoBackArguments> UserGoBack;
            void IMessages.UserGoBack(int user)
            {
                if(UserGoBack == null) return;
                var v = new RemoteEvents.UserGoBackArguments { user = user };
                this.VirtualLatency(() => this.UserGoBack(v));
            }

            public event Action<RemoteEvents.GoForwardArguments> GoForward;
            void IMessages.GoForward()
            {
                if(GoForward == null) return;
                var v = new RemoteEvents.GoForwardArguments {  };
                this.VirtualLatency(() => this.GoForward(v));
            }

            public event Action<RemoteEvents.UserGoForwardArguments> UserGoForward;
            void IMessages.UserGoForward(int user)
            {
                if(UserGoForward == null) return;
                var v = new RemoteEvents.UserGoForwardArguments { user = user };
                this.VirtualLatency(() => this.UserGoForward(v));
            }

        }
        #endregion
    }
    #endregion
}
// 19.09.2008 19:04:41
