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
            ServerPlayerHandshake,
            ServerPlayerHello,
            ServerPlayerJoined,
            ServerPlayerLeft,
            PlayerAdvertise,
            UserPlayerAdvertise,
            MouseMove,
            UserMouseMove,
            MouseOut,
            UserMouseOut,
            VectorChanged,
            UserVectorChanged,
            FireBullet,
            UserFireBullet,
            AddDamage,
            UserAddDamage,
            RestoreStarship,
            UserRestoreStarship,
            TeleportTo,
            UserTeleportTo,
            EatApple,
            UserEatApple,
            EatThisWormBegin,
            UserEatThisWormBegin,
            EatThisWormEnd,
            UserEatThisWormEnd,
            LevelHasEnded,
            UserLevelHasEnded,
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
            KillAllInvaders,
            UserKillAllInvaders,
            AwardAchievementFirst,
            AwardAchievementFiver,
            AwardAchievementUFOKill,
            AwardAchievementMaxGun,
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
            event Action<RemoteEvents.ServerPlayerHandshakeArguments> ServerPlayerHandshake;
            event Action<RemoteEvents.ServerPlayerHelloArguments> ServerPlayerHello;
            event Action<RemoteEvents.ServerPlayerJoinedArguments> ServerPlayerJoined;
            event Action<RemoteEvents.ServerPlayerLeftArguments> ServerPlayerLeft;
            event Action<RemoteEvents.PlayerAdvertiseArguments> PlayerAdvertise;
            event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            event Action<RemoteEvents.MouseMoveArguments> MouseMove;
            event Action<RemoteEvents.UserMouseMoveArguments> UserMouseMove;
            event Action<RemoteEvents.MouseOutArguments> MouseOut;
            event Action<RemoteEvents.UserMouseOutArguments> UserMouseOut;
            event Action<RemoteEvents.VectorChangedArguments> VectorChanged;
            event Action<RemoteEvents.UserVectorChangedArguments> UserVectorChanged;
            event Action<RemoteEvents.FireBulletArguments> FireBullet;
            event Action<RemoteEvents.UserFireBulletArguments> UserFireBullet;
            event Action<RemoteEvents.AddDamageArguments> AddDamage;
            event Action<RemoteEvents.UserAddDamageArguments> UserAddDamage;
            event Action<RemoteEvents.RestoreStarshipArguments> RestoreStarship;
            event Action<RemoteEvents.UserRestoreStarshipArguments> UserRestoreStarship;
            event Action<RemoteEvents.TeleportToArguments> TeleportTo;
            event Action<RemoteEvents.UserTeleportToArguments> UserTeleportTo;
            event Action<RemoteEvents.EatAppleArguments> EatApple;
            event Action<RemoteEvents.UserEatAppleArguments> UserEatApple;
            event Action<RemoteEvents.EatThisWormBeginArguments> EatThisWormBegin;
            event Action<RemoteEvents.UserEatThisWormBeginArguments> UserEatThisWormBegin;
            event Action<RemoteEvents.EatThisWormEndArguments> EatThisWormEnd;
            event Action<RemoteEvents.UserEatThisWormEndArguments> UserEatThisWormEnd;
            event Action<RemoteEvents.LevelHasEndedArguments> LevelHasEnded;
            event Action<RemoteEvents.UserLevelHasEndedArguments> UserLevelHasEnded;
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
            event Action<RemoteEvents.KillAllInvadersArguments> KillAllInvaders;
            event Action<RemoteEvents.UserKillAllInvadersArguments> UserKillAllInvaders;
            event Action<RemoteEvents.AwardAchievementFirstArguments> AwardAchievementFirst;
            event Action<RemoteEvents.AwardAchievementFiverArguments> AwardAchievementFiver;
            event Action<RemoteEvents.AwardAchievementUFOKillArguments> AwardAchievementUFOKill;
            event Action<RemoteEvents.AwardAchievementMaxGunArguments> AwardAchievementMaxGun;
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
            #region SendArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class SendArguments
            {
                public Messages i;
                public object[] args;
            }
            #endregion
            public void ServerPlayerHandshake(int[] version)
            {
                var args = new object[version.Length + 0];
                Array.Copy(version, 0, args, 0, version.Length);
                Send(new SendArguments { i = Messages.ServerPlayerHandshake, args = args });
            }
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
            public void VectorChanged(int x, int y)
            {
                Send(new SendArguments { i = Messages.VectorChanged, args = new object[] { x, y } });
            }
            public void UserVectorChanged(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserVectorChanged, args = new object[] { user, x, y } });
            }
            public void FireBullet(int starship, int multiplier, int from_x, int from_y, int to_x, int to_y, int limit)
            {
                Send(new SendArguments { i = Messages.FireBullet, args = new object[] { starship, multiplier, from_x, from_y, to_x, to_y, limit } });
            }
            public void UserFireBullet(int user, int starship, int multiplier, int from_x, int from_y, int to_x, int to_y, int limit)
            {
                Send(new SendArguments { i = Messages.UserFireBullet, args = new object[] { user, starship, multiplier, from_x, from_y, to_x, to_y, limit } });
            }
            public void AddDamage(int target, double damage, int shooter)
            {
                Send(new SendArguments { i = Messages.AddDamage, args = new object[] { target, damage, shooter } });
            }
            public void UserAddDamage(int user, int target, double damage, int shooter)
            {
                Send(new SendArguments { i = Messages.UserAddDamage, args = new object[] { user, target, damage, shooter } });
            }
            public void RestoreStarship(int starship)
            {
                Send(new SendArguments { i = Messages.RestoreStarship, args = new object[] { starship } });
            }
            public void UserRestoreStarship(int user, int starship)
            {
                Send(new SendArguments { i = Messages.UserRestoreStarship, args = new object[] { user, starship } });
            }
            public void TeleportTo(int x, int y)
            {
                Send(new SendArguments { i = Messages.TeleportTo, args = new object[] { x, y } });
            }
            public void UserTeleportTo(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserTeleportTo, args = new object[] { user, x, y } });
            }
            public void EatApple(int x, int y)
            {
                Send(new SendArguments { i = Messages.EatApple, args = new object[] { x, y } });
            }
            public void UserEatApple(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserEatApple, args = new object[] { user, x, y } });
            }
            public void EatThisWormBegin(int food)
            {
                Send(new SendArguments { i = Messages.EatThisWormBegin, args = new object[] { food } });
            }
            public void UserEatThisWormBegin(int user, int food)
            {
                Send(new SendArguments { i = Messages.UserEatThisWormBegin, args = new object[] { user, food } });
            }
            public void EatThisWormEnd(int food)
            {
                Send(new SendArguments { i = Messages.EatThisWormEnd, args = new object[] { food } });
            }
            public void UserEatThisWormEnd(int user, int food)
            {
                Send(new SendArguments { i = Messages.UserEatThisWormEnd, args = new object[] { user, food } });
            }
            public void LevelHasEnded()
            {
                Send(new SendArguments { i = Messages.LevelHasEnded, args = new object[] {  } });
            }
            public void UserLevelHasEnded(int user)
            {
                Send(new SendArguments { i = Messages.UserLevelHasEnded, args = new object[] { user } });
            }
            public void ServerSendMap()
            {
                Send(new SendArguments { i = Messages.ServerSendMap, args = new object[] {  } });
            }
            public void SendMap(int[] buttons)
            {
                var args = new object[buttons.Length + 0];
                Array.Copy(buttons, 0, args, 0, buttons.Length);
                Send(new SendArguments { i = Messages.SendMap, args = args });
            }
            public void UserSendMap(int user, int[] buttons)
            {
                var args = new object[buttons.Length + 1];
                args[0] = user;
                Array.Copy(buttons, 0, args, 1, buttons.Length);
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
            public void KillAllInvaders()
            {
                Send(new SendArguments { i = Messages.KillAllInvaders, args = new object[] {  } });
            }
            public void UserKillAllInvaders(int user)
            {
                Send(new SendArguments { i = Messages.UserKillAllInvaders, args = new object[] { user } });
            }
            public void AwardAchievementFirst()
            {
                Send(new SendArguments { i = Messages.AwardAchievementFirst, args = new object[] {  } });
            }
            public void AwardAchievementFiver()
            {
                Send(new SendArguments { i = Messages.AwardAchievementFiver, args = new object[] {  } });
            }
            public void AwardAchievementUFOKill()
            {
                Send(new SendArguments { i = Messages.AwardAchievementUFOKill, args = new object[] {  } });
            }
            public void AwardAchievementMaxGun()
            {
                Send(new SendArguments { i = Messages.AwardAchievementMaxGun, args = new object[] {  } });
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
        public sealed partial class RemoteEvents : IEvents
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
            #region WithUserArgumentsRouter
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter : WithUserArguments
            {
                public IMessages Target;

                #region Automatic Event Routing
                public void CombineDelegates(IEvents value)
                {
                    value.PlayerAdvertise += this.UserPlayerAdvertise;
                    value.MouseMove += this.UserMouseMove;
                    value.MouseOut += this.UserMouseOut;
                    value.VectorChanged += this.UserVectorChanged;
                    value.FireBullet += this.UserFireBullet;
                    value.AddDamage += this.UserAddDamage;
                    value.RestoreStarship += this.UserRestoreStarship;
                    value.TeleportTo += this.UserTeleportTo;
                    value.EatApple += this.UserEatApple;
                    value.EatThisWormBegin += this.UserEatThisWormBegin;
                    value.EatThisWormEnd += this.UserEatThisWormEnd;
                    value.LevelHasEnded += this.UserLevelHasEnded;
                    value.SendMap += this.UserSendMap;
                    value.SendMapLater += this.UserSendMapLater;
                    value.SetFlag += this.UserSetFlag;
                    value.Reveal += this.UserReveal;
                    value.KillAllInvaders += this.UserKillAllInvaders;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.PlayerAdvertise -= this.UserPlayerAdvertise;
                    value.MouseMove -= this.UserMouseMove;
                    value.MouseOut -= this.UserMouseOut;
                    value.VectorChanged -= this.UserVectorChanged;
                    value.FireBullet -= this.UserFireBullet;
                    value.AddDamage -= this.UserAddDamage;
                    value.RestoreStarship -= this.UserRestoreStarship;
                    value.TeleportTo -= this.UserTeleportTo;
                    value.EatApple -= this.UserEatApple;
                    value.EatThisWormBegin -= this.UserEatThisWormBegin;
                    value.EatThisWormEnd -= this.UserEatThisWormEnd;
                    value.LevelHasEnded -= this.UserLevelHasEnded;
                    value.SendMap -= this.UserSendMap;
                    value.SendMapLater -= this.UserSendMapLater;
                    value.SetFlag -= this.UserSetFlag;
                    value.Reveal -= this.UserReveal;
                    value.KillAllInvaders -= this.UserKillAllInvaders;
                }
                #endregion

                #region Routing
                public void UserPlayerAdvertise(PlayerAdvertiseArguments e)
                {
                    Target.UserPlayerAdvertise(this.user, e.name);
                }
                public void UserMouseMove(MouseMoveArguments e)
                {
                    Target.UserMouseMove(this.user, e.x, e.y);
                }
                public void UserMouseOut(MouseOutArguments e)
                {
                    Target.UserMouseOut(this.user, e.color);
                }
                public void UserVectorChanged(VectorChangedArguments e)
                {
                    Target.UserVectorChanged(this.user, e.x, e.y);
                }
                public void UserFireBullet(FireBulletArguments e)
                {
                    Target.UserFireBullet(this.user, e.starship, e.multiplier, e.from_x, e.from_y, e.to_x, e.to_y, e.limit);
                }
                public void UserAddDamage(AddDamageArguments e)
                {
                    Target.UserAddDamage(this.user, e.target, e.damage, e.shooter);
                }
                public void UserRestoreStarship(RestoreStarshipArguments e)
                {
                    Target.UserRestoreStarship(this.user, e.starship);
                }
                public void UserTeleportTo(TeleportToArguments e)
                {
                    Target.UserTeleportTo(this.user, e.x, e.y);
                }
                public void UserEatApple(EatAppleArguments e)
                {
                    Target.UserEatApple(this.user, e.x, e.y);
                }
                public void UserEatThisWormBegin(EatThisWormBeginArguments e)
                {
                    Target.UserEatThisWormBegin(this.user, e.food);
                }
                public void UserEatThisWormEnd(EatThisWormEndArguments e)
                {
                    Target.UserEatThisWormEnd(this.user, e.food);
                }
                public void UserLevelHasEnded(LevelHasEndedArguments e)
                {
                    Target.UserLevelHasEnded(this.user);
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
                public void UserKillAllInvaders(KillAllInvadersArguments e)
                {
                    Target.UserKillAllInvaders(this.user);
                }
                #endregion
            }
            #endregion
            #region ServerPlayerHandshakeArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class ServerPlayerHandshakeArguments
            {
                public int[] version;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ version = ").Append(this.version).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerPlayerHandshakeArguments> ServerPlayerHandshake;
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
            #region VectorChangedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class VectorChangedArguments
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
            public event Action<VectorChangedArguments> VectorChanged;
            #region UserVectorChangedArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserVectorChangedArguments : WithUserArguments
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
            public event Action<UserVectorChangedArguments> UserVectorChanged;
            #region FireBulletArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class FireBulletArguments
            {
                public int starship;
                public int multiplier;
                public int from_x;
                public int from_y;
                public int to_x;
                public int to_y;
                public int limit;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ starship = ").Append(this.starship).Append(", multiplier = ").Append(this.multiplier).Append(", from_x = ").Append(this.from_x).Append(", from_y = ").Append(this.from_y).Append(", to_x = ").Append(this.to_x).Append(", to_y = ").Append(this.to_y).Append(", limit = ").Append(this.limit).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<FireBulletArguments> FireBullet;
            #region UserFireBulletArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserFireBulletArguments : WithUserArguments
            {
                public int starship;
                public int multiplier;
                public int from_x;
                public int from_y;
                public int to_x;
                public int to_y;
                public int limit;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", starship = ").Append(this.starship).Append(", multiplier = ").Append(this.multiplier).Append(", from_x = ").Append(this.from_x).Append(", from_y = ").Append(this.from_y).Append(", to_x = ").Append(this.to_x).Append(", to_y = ").Append(this.to_y).Append(", limit = ").Append(this.limit).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserFireBulletArguments> UserFireBullet;
            #region AddDamageArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AddDamageArguments
            {
                public int target;
                public double damage;
                public int shooter;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(", shooter = ").Append(this.shooter).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<AddDamageArguments> AddDamage;
            #region UserAddDamageArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserAddDamageArguments : WithUserArguments
            {
                public int target;
                public double damage;
                public int shooter;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(", shooter = ").Append(this.shooter).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserAddDamageArguments> UserAddDamage;
            #region RestoreStarshipArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class RestoreStarshipArguments
            {
                public int starship;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ starship = ").Append(this.starship).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<RestoreStarshipArguments> RestoreStarship;
            #region UserRestoreStarshipArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserRestoreStarshipArguments : WithUserArguments
            {
                public int starship;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", starship = ").Append(this.starship).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserRestoreStarshipArguments> UserRestoreStarship;
            #region TeleportToArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class TeleportToArguments
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
            public event Action<TeleportToArguments> TeleportTo;
            #region UserTeleportToArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserTeleportToArguments : WithUserArguments
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
            public event Action<UserTeleportToArguments> UserTeleportTo;
            #region EatAppleArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class EatAppleArguments
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
            public event Action<EatAppleArguments> EatApple;
            #region UserEatAppleArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserEatAppleArguments : WithUserArguments
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
            public event Action<UserEatAppleArguments> UserEatApple;
            #region EatThisWormBeginArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class EatThisWormBeginArguments
            {
                public int food;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ food = ").Append(this.food).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<EatThisWormBeginArguments> EatThisWormBegin;
            #region UserEatThisWormBeginArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserEatThisWormBeginArguments : WithUserArguments
            {
                public int food;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", food = ").Append(this.food).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserEatThisWormBeginArguments> UserEatThisWormBegin;
            #region EatThisWormEndArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class EatThisWormEndArguments
            {
                public int food;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ food = ").Append(this.food).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<EatThisWormEndArguments> EatThisWormEnd;
            #region UserEatThisWormEndArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserEatThisWormEndArguments : WithUserArguments
            {
                public int food;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", food = ").Append(this.food).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserEatThisWormEndArguments> UserEatThisWormEnd;
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
            #region KillAllInvadersArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class KillAllInvadersArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<KillAllInvadersArguments> KillAllInvaders;
            #region UserKillAllInvadersArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserKillAllInvadersArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserKillAllInvadersArguments> UserKillAllInvaders;
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
            #region AwardAchievementFiverArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AwardAchievementFiverArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<AwardAchievementFiverArguments> AwardAchievementFiver;
            #region AwardAchievementUFOKillArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AwardAchievementUFOKillArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<AwardAchievementUFOKillArguments> AwardAchievementUFOKill;
            #region AwardAchievementMaxGunArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AwardAchievementMaxGunArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<AwardAchievementMaxGunArguments> AwardAchievementMaxGun;
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
                            { Messages.ServerPlayerHandshake, e => { ServerPlayerHandshake(new ServerPlayerHandshakeArguments { version = e.GetInt32Array(0) }); } },
                            { Messages.ServerPlayerHello, e => { ServerPlayerHello(new ServerPlayerHelloArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerJoined, e => { ServerPlayerJoined(new ServerPlayerJoinedArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerLeft, e => { ServerPlayerLeft(new ServerPlayerLeftArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.PlayerAdvertise, e => { PlayerAdvertise(new PlayerAdvertiseArguments { name = e.GetString(0) }); } },
                            { Messages.UserPlayerAdvertise, e => { UserPlayerAdvertise(new UserPlayerAdvertiseArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.MouseMove, e => { MouseMove(new MouseMoveArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserMouseMove, e => { UserMouseMove(new UserMouseMoveArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.MouseOut, e => { MouseOut(new MouseOutArguments { color = e.GetInt32(0) }); } },
                            { Messages.UserMouseOut, e => { UserMouseOut(new UserMouseOutArguments { user = e.GetInt32(0), color = e.GetInt32(1) }); } },
                            { Messages.VectorChanged, e => { VectorChanged(new VectorChangedArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserVectorChanged, e => { UserVectorChanged(new UserVectorChangedArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.FireBullet, e => { FireBullet(new FireBulletArguments { starship = e.GetInt32(0), multiplier = e.GetInt32(1), from_x = e.GetInt32(2), from_y = e.GetInt32(3), to_x = e.GetInt32(4), to_y = e.GetInt32(5), limit = e.GetInt32(6) }); } },
                            { Messages.UserFireBullet, e => { UserFireBullet(new UserFireBulletArguments { user = e.GetInt32(0), starship = e.GetInt32(1), multiplier = e.GetInt32(2), from_x = e.GetInt32(3), from_y = e.GetInt32(4), to_x = e.GetInt32(5), to_y = e.GetInt32(6), limit = e.GetInt32(7) }); } },
                            { Messages.AddDamage, e => { AddDamage(new AddDamageArguments { target = e.GetInt32(0), damage = e.GetDouble(1), shooter = e.GetInt32(2) }); } },
                            { Messages.UserAddDamage, e => { UserAddDamage(new UserAddDamageArguments { user = e.GetInt32(0), target = e.GetInt32(1), damage = e.GetDouble(2), shooter = e.GetInt32(3) }); } },
                            { Messages.RestoreStarship, e => { RestoreStarship(new RestoreStarshipArguments { starship = e.GetInt32(0) }); } },
                            { Messages.UserRestoreStarship, e => { UserRestoreStarship(new UserRestoreStarshipArguments { user = e.GetInt32(0), starship = e.GetInt32(1) }); } },
                            { Messages.TeleportTo, e => { TeleportTo(new TeleportToArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserTeleportTo, e => { UserTeleportTo(new UserTeleportToArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.EatApple, e => { EatApple(new EatAppleArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserEatApple, e => { UserEatApple(new UserEatAppleArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.EatThisWormBegin, e => { EatThisWormBegin(new EatThisWormBeginArguments { food = e.GetInt32(0) }); } },
                            { Messages.UserEatThisWormBegin, e => { UserEatThisWormBegin(new UserEatThisWormBeginArguments { user = e.GetInt32(0), food = e.GetInt32(1) }); } },
                            { Messages.EatThisWormEnd, e => { EatThisWormEnd(new EatThisWormEndArguments { food = e.GetInt32(0) }); } },
                            { Messages.UserEatThisWormEnd, e => { UserEatThisWormEnd(new UserEatThisWormEndArguments { user = e.GetInt32(0), food = e.GetInt32(1) }); } },
                            { Messages.LevelHasEnded, e => { LevelHasEnded(new LevelHasEndedArguments {  }); } },
                            { Messages.UserLevelHasEnded, e => { UserLevelHasEnded(new UserLevelHasEndedArguments { user = e.GetInt32(0) }); } },
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
                            { Messages.KillAllInvaders, e => { KillAllInvaders(new KillAllInvadersArguments {  }); } },
                            { Messages.UserKillAllInvaders, e => { UserKillAllInvaders(new UserKillAllInvadersArguments { user = e.GetInt32(0) }); } },
                            { Messages.AwardAchievementFirst, e => { AwardAchievementFirst(new AwardAchievementFirstArguments {  }); } },
                            { Messages.AwardAchievementFiver, e => { AwardAchievementFiver(new AwardAchievementFiverArguments {  }); } },
                            { Messages.AwardAchievementUFOKill, e => { AwardAchievementUFOKill(new AwardAchievementUFOKillArguments {  }); } },
                            { Messages.AwardAchievementMaxGun, e => { AwardAchievementMaxGun(new AwardAchievementMaxGunArguments {  }); } },
                            { Messages.SendPassword, e => { SendPassword(new SendPasswordArguments { password = e.GetString(0) }); } },
                            { Messages.ServerPasswordStatus, e => { ServerPasswordStatus(new ServerPasswordStatusArguments { status = e.GetInt32(0) }); } },
                            { Messages.LockGame, e => { LockGame(new LockGameArguments {  }); } },
                            { Messages.UnlockGame, e => { UnlockGame(new UnlockGameArguments {  }); } },
                        }
                ;
                DispatchTableDelegates = new Dictionary<Messages, Converter<object, Delegate>>
                        {
                            { Messages.ServerPlayerHandshake, e => ServerPlayerHandshake },
                            { Messages.ServerPlayerHello, e => ServerPlayerHello },
                            { Messages.ServerPlayerJoined, e => ServerPlayerJoined },
                            { Messages.ServerPlayerLeft, e => ServerPlayerLeft },
                            { Messages.PlayerAdvertise, e => PlayerAdvertise },
                            { Messages.UserPlayerAdvertise, e => UserPlayerAdvertise },
                            { Messages.MouseMove, e => MouseMove },
                            { Messages.UserMouseMove, e => UserMouseMove },
                            { Messages.MouseOut, e => MouseOut },
                            { Messages.UserMouseOut, e => UserMouseOut },
                            { Messages.VectorChanged, e => VectorChanged },
                            { Messages.UserVectorChanged, e => UserVectorChanged },
                            { Messages.FireBullet, e => FireBullet },
                            { Messages.UserFireBullet, e => UserFireBullet },
                            { Messages.AddDamage, e => AddDamage },
                            { Messages.UserAddDamage, e => UserAddDamage },
                            { Messages.RestoreStarship, e => RestoreStarship },
                            { Messages.UserRestoreStarship, e => UserRestoreStarship },
                            { Messages.TeleportTo, e => TeleportTo },
                            { Messages.UserTeleportTo, e => UserTeleportTo },
                            { Messages.EatApple, e => EatApple },
                            { Messages.UserEatApple, e => UserEatApple },
                            { Messages.EatThisWormBegin, e => EatThisWormBegin },
                            { Messages.UserEatThisWormBegin, e => UserEatThisWormBegin },
                            { Messages.EatThisWormEnd, e => EatThisWormEnd },
                            { Messages.UserEatThisWormEnd, e => UserEatThisWormEnd },
                            { Messages.LevelHasEnded, e => LevelHasEnded },
                            { Messages.UserLevelHasEnded, e => UserLevelHasEnded },
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
                            { Messages.KillAllInvaders, e => KillAllInvaders },
                            { Messages.UserKillAllInvaders, e => UserKillAllInvaders },
                            { Messages.AwardAchievementFirst, e => AwardAchievementFirst },
                            { Messages.AwardAchievementFiver, e => AwardAchievementFiver },
                            { Messages.AwardAchievementUFOKill, e => AwardAchievementUFOKill },
                            { Messages.AwardAchievementMaxGun, e => AwardAchievementMaxGun },
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
                        _Router.RemoveDelegates(this);
                    }
                    _Router = value;
                    if(_Router != null)
                    {
                        _Router.CombineDelegates(this);
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
            public event Action<RemoteEvents.ServerPlayerHandshakeArguments> ServerPlayerHandshake;
            void IMessages.ServerPlayerHandshake(int[] version)
            {
                if(ServerPlayerHandshake == null) return;
                var v = new RemoteEvents.ServerPlayerHandshakeArguments { version = version };
                this.VirtualLatency(() => this.ServerPlayerHandshake(v));
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

            public event Action<RemoteEvents.VectorChangedArguments> VectorChanged;
            void IMessages.VectorChanged(int x, int y)
            {
                if(VectorChanged == null) return;
                var v = new RemoteEvents.VectorChangedArguments { x = x, y = y };
                this.VirtualLatency(() => this.VectorChanged(v));
            }

            public event Action<RemoteEvents.UserVectorChangedArguments> UserVectorChanged;
            void IMessages.UserVectorChanged(int user, int x, int y)
            {
                if(UserVectorChanged == null) return;
                var v = new RemoteEvents.UserVectorChangedArguments { user = user, x = x, y = y };
                this.VirtualLatency(() => this.UserVectorChanged(v));
            }

            public event Action<RemoteEvents.FireBulletArguments> FireBullet;
            void IMessages.FireBullet(int starship, int multiplier, int from_x, int from_y, int to_x, int to_y, int limit)
            {
                if(FireBullet == null) return;
                var v = new RemoteEvents.FireBulletArguments { starship = starship, multiplier = multiplier, from_x = from_x, from_y = from_y, to_x = to_x, to_y = to_y, limit = limit };
                this.VirtualLatency(() => this.FireBullet(v));
            }

            public event Action<RemoteEvents.UserFireBulletArguments> UserFireBullet;
            void IMessages.UserFireBullet(int user, int starship, int multiplier, int from_x, int from_y, int to_x, int to_y, int limit)
            {
                if(UserFireBullet == null) return;
                var v = new RemoteEvents.UserFireBulletArguments { user = user, starship = starship, multiplier = multiplier, from_x = from_x, from_y = from_y, to_x = to_x, to_y = to_y, limit = limit };
                this.VirtualLatency(() => this.UserFireBullet(v));
            }

            public event Action<RemoteEvents.AddDamageArguments> AddDamage;
            void IMessages.AddDamage(int target, double damage, int shooter)
            {
                if(AddDamage == null) return;
                var v = new RemoteEvents.AddDamageArguments { target = target, damage = damage, shooter = shooter };
                this.VirtualLatency(() => this.AddDamage(v));
            }

            public event Action<RemoteEvents.UserAddDamageArguments> UserAddDamage;
            void IMessages.UserAddDamage(int user, int target, double damage, int shooter)
            {
                if(UserAddDamage == null) return;
                var v = new RemoteEvents.UserAddDamageArguments { user = user, target = target, damage = damage, shooter = shooter };
                this.VirtualLatency(() => this.UserAddDamage(v));
            }

            public event Action<RemoteEvents.RestoreStarshipArguments> RestoreStarship;
            void IMessages.RestoreStarship(int starship)
            {
                if(RestoreStarship == null) return;
                var v = new RemoteEvents.RestoreStarshipArguments { starship = starship };
                this.VirtualLatency(() => this.RestoreStarship(v));
            }

            public event Action<RemoteEvents.UserRestoreStarshipArguments> UserRestoreStarship;
            void IMessages.UserRestoreStarship(int user, int starship)
            {
                if(UserRestoreStarship == null) return;
                var v = new RemoteEvents.UserRestoreStarshipArguments { user = user, starship = starship };
                this.VirtualLatency(() => this.UserRestoreStarship(v));
            }

            public event Action<RemoteEvents.TeleportToArguments> TeleportTo;
            void IMessages.TeleportTo(int x, int y)
            {
                if(TeleportTo == null) return;
                var v = new RemoteEvents.TeleportToArguments { x = x, y = y };
                this.VirtualLatency(() => this.TeleportTo(v));
            }

            public event Action<RemoteEvents.UserTeleportToArguments> UserTeleportTo;
            void IMessages.UserTeleportTo(int user, int x, int y)
            {
                if(UserTeleportTo == null) return;
                var v = new RemoteEvents.UserTeleportToArguments { user = user, x = x, y = y };
                this.VirtualLatency(() => this.UserTeleportTo(v));
            }

            public event Action<RemoteEvents.EatAppleArguments> EatApple;
            void IMessages.EatApple(int x, int y)
            {
                if(EatApple == null) return;
                var v = new RemoteEvents.EatAppleArguments { x = x, y = y };
                this.VirtualLatency(() => this.EatApple(v));
            }

            public event Action<RemoteEvents.UserEatAppleArguments> UserEatApple;
            void IMessages.UserEatApple(int user, int x, int y)
            {
                if(UserEatApple == null) return;
                var v = new RemoteEvents.UserEatAppleArguments { user = user, x = x, y = y };
                this.VirtualLatency(() => this.UserEatApple(v));
            }

            public event Action<RemoteEvents.EatThisWormBeginArguments> EatThisWormBegin;
            void IMessages.EatThisWormBegin(int food)
            {
                if(EatThisWormBegin == null) return;
                var v = new RemoteEvents.EatThisWormBeginArguments { food = food };
                this.VirtualLatency(() => this.EatThisWormBegin(v));
            }

            public event Action<RemoteEvents.UserEatThisWormBeginArguments> UserEatThisWormBegin;
            void IMessages.UserEatThisWormBegin(int user, int food)
            {
                if(UserEatThisWormBegin == null) return;
                var v = new RemoteEvents.UserEatThisWormBeginArguments { user = user, food = food };
                this.VirtualLatency(() => this.UserEatThisWormBegin(v));
            }

            public event Action<RemoteEvents.EatThisWormEndArguments> EatThisWormEnd;
            void IMessages.EatThisWormEnd(int food)
            {
                if(EatThisWormEnd == null) return;
                var v = new RemoteEvents.EatThisWormEndArguments { food = food };
                this.VirtualLatency(() => this.EatThisWormEnd(v));
            }

            public event Action<RemoteEvents.UserEatThisWormEndArguments> UserEatThisWormEnd;
            void IMessages.UserEatThisWormEnd(int user, int food)
            {
                if(UserEatThisWormEnd == null) return;
                var v = new RemoteEvents.UserEatThisWormEndArguments { user = user, food = food };
                this.VirtualLatency(() => this.UserEatThisWormEnd(v));
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

            public event Action<RemoteEvents.KillAllInvadersArguments> KillAllInvaders;
            void IMessages.KillAllInvaders()
            {
                if(KillAllInvaders == null) return;
                var v = new RemoteEvents.KillAllInvadersArguments {  };
                this.VirtualLatency(() => this.KillAllInvaders(v));
            }

            public event Action<RemoteEvents.UserKillAllInvadersArguments> UserKillAllInvaders;
            void IMessages.UserKillAllInvaders(int user)
            {
                if(UserKillAllInvaders == null) return;
                var v = new RemoteEvents.UserKillAllInvadersArguments { user = user };
                this.VirtualLatency(() => this.UserKillAllInvaders(v));
            }

            public event Action<RemoteEvents.AwardAchievementFirstArguments> AwardAchievementFirst;
            void IMessages.AwardAchievementFirst()
            {
                if(AwardAchievementFirst == null) return;
                var v = new RemoteEvents.AwardAchievementFirstArguments {  };
                this.VirtualLatency(() => this.AwardAchievementFirst(v));
            }

            public event Action<RemoteEvents.AwardAchievementFiverArguments> AwardAchievementFiver;
            void IMessages.AwardAchievementFiver()
            {
                if(AwardAchievementFiver == null) return;
                var v = new RemoteEvents.AwardAchievementFiverArguments {  };
                this.VirtualLatency(() => this.AwardAchievementFiver(v));
            }

            public event Action<RemoteEvents.AwardAchievementUFOKillArguments> AwardAchievementUFOKill;
            void IMessages.AwardAchievementUFOKill()
            {
                if(AwardAchievementUFOKill == null) return;
                var v = new RemoteEvents.AwardAchievementUFOKillArguments {  };
                this.VirtualLatency(() => this.AwardAchievementUFOKill(v));
            }

            public event Action<RemoteEvents.AwardAchievementMaxGunArguments> AwardAchievementMaxGun;
            void IMessages.AwardAchievementMaxGun()
            {
                if(AwardAchievementMaxGun == null) return;
                var v = new RemoteEvents.AwardAchievementMaxGunArguments {  };
                this.VirtualLatency(() => this.AwardAchievementMaxGun(v));
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
// 14.09.2008 20:23:15
