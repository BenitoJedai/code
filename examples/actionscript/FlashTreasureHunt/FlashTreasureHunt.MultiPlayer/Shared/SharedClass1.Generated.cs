using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ScriptCoreLib.Shared.Nonoba;
using ScriptCoreLib;

namespace FlashTreasureHunt.Shared
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
            ServerSendMap,
            SendMap,
            UserSendMap,
            TakeAmmo,
            UserTakeAmmo,
            TakeGold,
            UserTakeGold,
            AddDamageToCoPlayer,
            UserAddDamageToCoPlayer,
            WalkTo,
            UserWalkTo,
            LookAt,
            UserLookAt,
            FireWeapon,
            UserFireWeapon,
            EnterEndLevelMode,
            UserEnterEndLevelMode,
            GuardWalkTo,
            UserGuardWalkTo,
            GuardLookAt,
            UserGuardLookAt,
            GuardAddDamage,
            UserGuardAddDamage,
            ReportScore,
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
            event Action<RemoteEvents.ServerSendMapArguments> ServerSendMap;
            event Action<RemoteEvents.SendMapArguments> SendMap;
            event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            event Action<RemoteEvents.TakeAmmoArguments> TakeAmmo;
            event Action<RemoteEvents.UserTakeAmmoArguments> UserTakeAmmo;
            event Action<RemoteEvents.TakeGoldArguments> TakeGold;
            event Action<RemoteEvents.UserTakeGoldArguments> UserTakeGold;
            event Action<RemoteEvents.AddDamageToCoPlayerArguments> AddDamageToCoPlayer;
            event Action<RemoteEvents.UserAddDamageToCoPlayerArguments> UserAddDamageToCoPlayer;
            event Action<RemoteEvents.WalkToArguments> WalkTo;
            event Action<RemoteEvents.UserWalkToArguments> UserWalkTo;
            event Action<RemoteEvents.LookAtArguments> LookAt;
            event Action<RemoteEvents.UserLookAtArguments> UserLookAt;
            event Action<RemoteEvents.FireWeaponArguments> FireWeapon;
            event Action<RemoteEvents.UserFireWeaponArguments> UserFireWeapon;
            event Action<RemoteEvents.EnterEndLevelModeArguments> EnterEndLevelMode;
            event Action<RemoteEvents.UserEnterEndLevelModeArguments> UserEnterEndLevelMode;
            event Action<RemoteEvents.GuardWalkToArguments> GuardWalkTo;
            event Action<RemoteEvents.UserGuardWalkToArguments> UserGuardWalkTo;
            event Action<RemoteEvents.GuardLookAtArguments> GuardLookAt;
            event Action<RemoteEvents.UserGuardLookAtArguments> UserGuardLookAt;
            event Action<RemoteEvents.GuardAddDamageArguments> GuardAddDamage;
            event Action<RemoteEvents.UserGuardAddDamageArguments> UserGuardAddDamage;
            event Action<RemoteEvents.ReportScoreArguments> ReportScore;
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
            public void ServerPlayerHello(int user, string name, int user_with_map, int[] handshake)
            {
                if (this.Send != null)
                {
                    var args = new object[handshake.Length + 3];
                    args[0] = user;
                    args[1] = name;
                    args[2] = user_with_map;
                    Array.Copy(handshake, 0, args, 3, handshake.Length);
                    Send(new SendArguments { i = Messages.ServerPlayerHello, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerPlayerHello(user, name, user_with_map, handshake);
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
            public void PlayerAdvertise(string name, int[] vector)
            {
                if (this.Send != null)
                {
                    var args = new object[vector.Length + 1];
                    args[0] = name;
                    Array.Copy(vector, 0, args, 1, vector.Length);
                    Send(new SendArguments { i = Messages.PlayerAdvertise, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.PlayerAdvertise(name, vector);
                    }
                }
            }
            public void UserPlayerAdvertise(int user, string name, int[] vector)
            {
                if (this.Send != null)
                {
                    var args = new object[vector.Length + 2];
                    args[0] = user;
                    args[1] = name;
                    Array.Copy(vector, 0, args, 2, vector.Length);
                    Send(new SendArguments { i = Messages.UserPlayerAdvertise, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserPlayerAdvertise(user, name, vector);
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
            public void SendMap(int[] bytestream)
            {
                if (this.Send != null)
                {
                    var args = new object[bytestream.Length + 0];
                    Array.Copy(bytestream, 0, args, 0, bytestream.Length);
                    Send(new SendArguments { i = Messages.SendMap, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.SendMap(bytestream);
                    }
                }
            }
            public void UserSendMap(int user, int[] bytestream)
            {
                if (this.Send != null)
                {
                    var args = new object[bytestream.Length + 1];
                    args[0] = user;
                    Array.Copy(bytestream, 0, args, 1, bytestream.Length);
                    Send(new SendArguments { i = Messages.UserSendMap, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserSendMap(user, bytestream);
                    }
                }
            }
            public void TakeAmmo(int index)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.TakeAmmo, args = new object[] { index } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.TakeAmmo(index);
                    }
                }
            }
            public void UserTakeAmmo(int user, int index)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserTakeAmmo, args = new object[] { user, index } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserTakeAmmo(user, index);
                    }
                }
            }
            public void TakeGold(int index)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.TakeGold, args = new object[] { index } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.TakeGold(index);
                    }
                }
            }
            public void UserTakeGold(int user, int index)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserTakeGold, args = new object[] { user, index } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserTakeGold(user, index);
                    }
                }
            }
            public void AddDamageToCoPlayer(int target, double damage)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.AddDamageToCoPlayer, args = new object[] { target, damage } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.AddDamageToCoPlayer(target, damage);
                    }
                }
            }
            public void UserAddDamageToCoPlayer(int user, int target, double damage)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserAddDamageToCoPlayer, args = new object[] { user, target, damage } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserAddDamageToCoPlayer(user, target, damage);
                    }
                }
            }
            public void WalkTo(int[] bytestream)
            {
                if (this.Send != null)
                {
                    var args = new object[bytestream.Length + 0];
                    Array.Copy(bytestream, 0, args, 0, bytestream.Length);
                    Send(new SendArguments { i = Messages.WalkTo, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.WalkTo(bytestream);
                    }
                }
            }
            public void UserWalkTo(int user, int[] bytestream)
            {
                if (this.Send != null)
                {
                    var args = new object[bytestream.Length + 1];
                    args[0] = user;
                    Array.Copy(bytestream, 0, args, 1, bytestream.Length);
                    Send(new SendArguments { i = Messages.UserWalkTo, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserWalkTo(user, bytestream);
                    }
                }
            }
            public void LookAt(double arc)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.LookAt, args = new object[] { arc } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.LookAt(arc);
                    }
                }
            }
            public void UserLookAt(int user, double arc)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserLookAt, args = new object[] { user, arc } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserLookAt(user, arc);
                    }
                }
            }
            public void FireWeapon()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.FireWeapon, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.FireWeapon();
                    }
                }
            }
            public void UserFireWeapon(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserFireWeapon, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserFireWeapon(user);
                    }
                }
            }
            public void EnterEndLevelMode()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.EnterEndLevelMode, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.EnterEndLevelMode();
                    }
                }
            }
            public void UserEnterEndLevelMode(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserEnterEndLevelMode, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserEnterEndLevelMode(user);
                    }
                }
            }
            public void GuardWalkTo(int[] bytestream)
            {
                if (this.Send != null)
                {
                    var args = new object[bytestream.Length + 0];
                    Array.Copy(bytestream, 0, args, 0, bytestream.Length);
                    Send(new SendArguments { i = Messages.GuardWalkTo, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.GuardWalkTo(bytestream);
                    }
                }
            }
            public void UserGuardWalkTo(int user, int[] bytestream)
            {
                if (this.Send != null)
                {
                    var args = new object[bytestream.Length + 1];
                    args[0] = user;
                    Array.Copy(bytestream, 0, args, 1, bytestream.Length);
                    Send(new SendArguments { i = Messages.UserGuardWalkTo, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserGuardWalkTo(user, bytestream);
                    }
                }
            }
            public void GuardLookAt(int index, double arc)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.GuardLookAt, args = new object[] { index, arc } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.GuardLookAt(index, arc);
                    }
                }
            }
            public void UserGuardLookAt(int user, int index, double arc)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserGuardLookAt, args = new object[] { user, index, arc } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserGuardLookAt(user, index, arc);
                    }
                }
            }
            public void GuardAddDamage(int index, double damage)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.GuardAddDamage, args = new object[] { index, damage } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.GuardAddDamage(index, damage);
                    }
                }
            }
            public void UserGuardAddDamage(int user, int index, double damage)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserGuardAddDamage, args = new object[] { user, index, damage } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserGuardAddDamage(user, index, damage);
                    }
                }
            }
            public void ReportScore(int level, int score, int kills, int teleports, int fps)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ReportScore, args = new object[] { level, score, kills, teleports, fps } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ReportScore(level, score, kills, teleports, fps);
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
                    value.SendMap += this.UserSendMap;
                    value.TakeAmmo += this.UserTakeAmmo;
                    value.TakeGold += this.UserTakeGold;
                    value.AddDamageToCoPlayer += this.UserAddDamageToCoPlayer;
                    value.WalkTo += this.UserWalkTo;
                    value.LookAt += this.UserLookAt;
                    value.FireWeapon += this.UserFireWeapon;
                    value.EnterEndLevelMode += this.UserEnterEndLevelMode;
                    value.GuardWalkTo += this.UserGuardWalkTo;
                    value.GuardLookAt += this.UserGuardLookAt;
                    value.GuardAddDamage += this.UserGuardAddDamage;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.PlayerAdvertise -= this.UserPlayerAdvertise;
                    value.SendMap -= this.UserSendMap;
                    value.TakeAmmo -= this.UserTakeAmmo;
                    value.TakeGold -= this.UserTakeGold;
                    value.AddDamageToCoPlayer -= this.UserAddDamageToCoPlayer;
                    value.WalkTo -= this.UserWalkTo;
                    value.LookAt -= this.UserLookAt;
                    value.FireWeapon -= this.UserFireWeapon;
                    value.EnterEndLevelMode -= this.UserEnterEndLevelMode;
                    value.GuardWalkTo -= this.UserGuardWalkTo;
                    value.GuardLookAt -= this.UserGuardLookAt;
                    value.GuardAddDamage -= this.UserGuardAddDamage;
                }
                #endregion

                #region Routing
                public void UserPlayerAdvertise(PlayerAdvertiseArguments e)
                {
                    Target.UserPlayerAdvertise(this.user, e.name, e.vector);
                }
                public void UserSendMap(SendMapArguments e)
                {
                    Target.UserSendMap(this.user, e.bytestream);
                }
                public void UserTakeAmmo(TakeAmmoArguments e)
                {
                    Target.UserTakeAmmo(this.user, e.index);
                }
                public void UserTakeGold(TakeGoldArguments e)
                {
                    Target.UserTakeGold(this.user, e.index);
                }
                public void UserAddDamageToCoPlayer(AddDamageToCoPlayerArguments e)
                {
                    Target.UserAddDamageToCoPlayer(this.user, e.target, e.damage);
                }
                public void UserWalkTo(WalkToArguments e)
                {
                    Target.UserWalkTo(this.user, e.bytestream);
                }
                public void UserLookAt(LookAtArguments e)
                {
                    Target.UserLookAt(this.user, e.arc);
                }
                public void UserFireWeapon(FireWeaponArguments e)
                {
                    Target.UserFireWeapon(this.user);
                }
                public void UserEnterEndLevelMode(EnterEndLevelModeArguments e)
                {
                    Target.UserEnterEndLevelMode(this.user);
                }
                public void UserGuardWalkTo(GuardWalkToArguments e)
                {
                    Target.UserGuardWalkTo(this.user, e.bytestream);
                }
                public void UserGuardLookAt(GuardLookAtArguments e)
                {
                    Target.UserGuardLookAt(this.user, e.index, e.arc);
                }
                public void UserGuardAddDamage(GuardAddDamageArguments e)
                {
                    Target.UserGuardAddDamage(this.user, e.index, e.damage);
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
                public void UserPlayerAdvertise(string name, int[] vector)
                {
                    this.Target.UserPlayerAdvertise(this.user, name, vector);
                }
                public void UserPlayerAdvertise(UserPlayerAdvertiseArguments e)
                {
                    this.Target.UserPlayerAdvertise(this.user, e.name, e.vector);
                }
                public void UserSendMap(int[] bytestream)
                {
                    this.Target.UserSendMap(this.user, bytestream);
                }
                public void UserSendMap(UserSendMapArguments e)
                {
                    this.Target.UserSendMap(this.user, e.bytestream);
                }
                public void UserTakeAmmo(int index)
                {
                    this.Target.UserTakeAmmo(this.user, index);
                }
                public void UserTakeAmmo(UserTakeAmmoArguments e)
                {
                    this.Target.UserTakeAmmo(this.user, e.index);
                }
                public void UserTakeGold(int index)
                {
                    this.Target.UserTakeGold(this.user, index);
                }
                public void UserTakeGold(UserTakeGoldArguments e)
                {
                    this.Target.UserTakeGold(this.user, e.index);
                }
                public void UserAddDamageToCoPlayer(int target, double damage)
                {
                    this.Target.UserAddDamageToCoPlayer(this.user, target, damage);
                }
                public void UserAddDamageToCoPlayer(UserAddDamageToCoPlayerArguments e)
                {
                    this.Target.UserAddDamageToCoPlayer(this.user, e.target, e.damage);
                }
                public void UserWalkTo(int[] bytestream)
                {
                    this.Target.UserWalkTo(this.user, bytestream);
                }
                public void UserWalkTo(UserWalkToArguments e)
                {
                    this.Target.UserWalkTo(this.user, e.bytestream);
                }
                public void UserLookAt(double arc)
                {
                    this.Target.UserLookAt(this.user, arc);
                }
                public void UserLookAt(UserLookAtArguments e)
                {
                    this.Target.UserLookAt(this.user, e.arc);
                }
                public void UserFireWeapon()
                {
                    this.Target.UserFireWeapon(this.user);
                }
                public void UserFireWeapon(UserFireWeaponArguments e)
                {
                    this.Target.UserFireWeapon(this.user);
                }
                public void UserEnterEndLevelMode()
                {
                    this.Target.UserEnterEndLevelMode(this.user);
                }
                public void UserEnterEndLevelMode(UserEnterEndLevelModeArguments e)
                {
                    this.Target.UserEnterEndLevelMode(this.user);
                }
                public void UserGuardWalkTo(int[] bytestream)
                {
                    this.Target.UserGuardWalkTo(this.user, bytestream);
                }
                public void UserGuardWalkTo(UserGuardWalkToArguments e)
                {
                    this.Target.UserGuardWalkTo(this.user, e.bytestream);
                }
                public void UserGuardLookAt(int index, double arc)
                {
                    this.Target.UserGuardLookAt(this.user, index, arc);
                }
                public void UserGuardLookAt(UserGuardLookAtArguments e)
                {
                    this.Target.UserGuardLookAt(this.user, e.index, e.arc);
                }
                public void UserGuardAddDamage(int index, double damage)
                {
                    this.Target.UserGuardAddDamage(this.user, index, damage);
                }
                public void UserGuardAddDamage(UserGuardAddDamageArguments e)
                {
                    this.Target.UserGuardAddDamage(this.user, e.index, e.damage);
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
                    value.UserSendMap += this.UserSendMap;
                    value.UserTakeAmmo += this.UserTakeAmmo;
                    value.UserTakeGold += this.UserTakeGold;
                    value.UserAddDamageToCoPlayer += this.UserAddDamageToCoPlayer;
                    value.UserWalkTo += this.UserWalkTo;
                    value.UserLookAt += this.UserLookAt;
                    value.UserFireWeapon += this.UserFireWeapon;
                    value.UserEnterEndLevelMode += this.UserEnterEndLevelMode;
                    value.UserGuardWalkTo += this.UserGuardWalkTo;
                    value.UserGuardLookAt += this.UserGuardLookAt;
                    value.UserGuardAddDamage += this.UserGuardAddDamage;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.UserPlayerAdvertise -= this.UserPlayerAdvertise;
                    value.UserSendMap -= this.UserSendMap;
                    value.UserTakeAmmo -= this.UserTakeAmmo;
                    value.UserTakeGold -= this.UserTakeGold;
                    value.UserAddDamageToCoPlayer -= this.UserAddDamageToCoPlayer;
                    value.UserWalkTo -= this.UserWalkTo;
                    value.UserLookAt -= this.UserLookAt;
                    value.UserFireWeapon -= this.UserFireWeapon;
                    value.UserEnterEndLevelMode -= this.UserEnterEndLevelMode;
                    value.UserGuardWalkTo -= this.UserGuardWalkTo;
                    value.UserGuardLookAt -= this.UserGuardLookAt;
                    value.UserGuardAddDamage -= this.UserGuardAddDamage;
                }
                #endregion

                #region Routing
                public void UserPlayerAdvertise(UserPlayerAdvertiseArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserPlayerAdvertise(this.user, e.name, e.vector);
                }
                public void UserSendMap(UserSendMapArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserSendMap(this.user, e.bytestream);
                }
                public void UserTakeAmmo(UserTakeAmmoArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserTakeAmmo(this.user, e.index);
                }
                public void UserTakeGold(UserTakeGoldArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserTakeGold(this.user, e.index);
                }
                public void UserAddDamageToCoPlayer(UserAddDamageToCoPlayerArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserAddDamageToCoPlayer(this.user, e.target, e.damage);
                }
                public void UserWalkTo(UserWalkToArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserWalkTo(this.user, e.bytestream);
                }
                public void UserLookAt(UserLookAtArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserLookAt(this.user, e.arc);
                }
                public void UserFireWeapon(UserFireWeaponArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserFireWeapon(this.user);
                }
                public void UserEnterEndLevelMode(UserEnterEndLevelModeArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserEnterEndLevelMode(this.user);
                }
                public void UserGuardWalkTo(UserGuardWalkToArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserGuardWalkTo(this.user, e.bytestream);
                }
                public void UserGuardLookAt(UserGuardLookAtArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserGuardLookAt(this.user, e.index, e.arc);
                }
                public void UserGuardAddDamage(UserGuardAddDamageArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserGuardAddDamage(this.user, e.index, e.damage);
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
                public int user_with_map;
                public int[] handshake;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(", user_with_map = ").Append(this.user_with_map).Append(", handshake = ").Append(this.handshake).Append(" }").ToString();
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
                public int[] vector;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ name = ").Append(this.name).Append(", vector = ").Append(this.vector).Append(" }").ToString();
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
                public int[] vector;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(", vector = ").Append(this.vector).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserPlayerAdvertiseArguments> UserPlayerAdvertise;
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
                public int[] bytestream;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ bytestream = ").Append(this.bytestream).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<SendMapArguments> SendMap;
            #region UserSendMapArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserSendMapArguments : WithUserArguments
            {
                public int[] bytestream;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", bytestream = ").Append(this.bytestream).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserSendMapArguments> UserSendMap;
            #region TakeAmmoArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class TakeAmmoArguments
            {
                public int index;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ index = ").Append(this.index).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<TakeAmmoArguments> TakeAmmo;
            #region UserTakeAmmoArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserTakeAmmoArguments : WithUserArguments
            {
                public int index;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", index = ").Append(this.index).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserTakeAmmoArguments> UserTakeAmmo;
            #region TakeGoldArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class TakeGoldArguments
            {
                public int index;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ index = ").Append(this.index).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<TakeGoldArguments> TakeGold;
            #region UserTakeGoldArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserTakeGoldArguments : WithUserArguments
            {
                public int index;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", index = ").Append(this.index).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserTakeGoldArguments> UserTakeGold;
            #region AddDamageToCoPlayerArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AddDamageToCoPlayerArguments
            {
                public int target;
                public double damage;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<AddDamageToCoPlayerArguments> AddDamageToCoPlayer;
            #region UserAddDamageToCoPlayerArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserAddDamageToCoPlayerArguments : WithUserArguments
            {
                public int target;
                public double damage;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserAddDamageToCoPlayerArguments> UserAddDamageToCoPlayer;
            #region WalkToArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class WalkToArguments
            {
                public int[] bytestream;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ bytestream = ").Append(this.bytestream).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<WalkToArguments> WalkTo;
            #region UserWalkToArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserWalkToArguments : WithUserArguments
            {
                public int[] bytestream;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", bytestream = ").Append(this.bytestream).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserWalkToArguments> UserWalkTo;
            #region LookAtArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class LookAtArguments
            {
                public double arc;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ arc = ").Append(this.arc).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<LookAtArguments> LookAt;
            #region UserLookAtArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserLookAtArguments : WithUserArguments
            {
                public double arc;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", arc = ").Append(this.arc).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserLookAtArguments> UserLookAt;
            #region FireWeaponArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class FireWeaponArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<FireWeaponArguments> FireWeapon;
            #region UserFireWeaponArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserFireWeaponArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserFireWeaponArguments> UserFireWeapon;
            #region EnterEndLevelModeArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class EnterEndLevelModeArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<EnterEndLevelModeArguments> EnterEndLevelMode;
            #region UserEnterEndLevelModeArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserEnterEndLevelModeArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserEnterEndLevelModeArguments> UserEnterEndLevelMode;
            #region GuardWalkToArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class GuardWalkToArguments
            {
                public int[] bytestream;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ bytestream = ").Append(this.bytestream).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<GuardWalkToArguments> GuardWalkTo;
            #region UserGuardWalkToArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserGuardWalkToArguments : WithUserArguments
            {
                public int[] bytestream;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", bytestream = ").Append(this.bytestream).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserGuardWalkToArguments> UserGuardWalkTo;
            #region GuardLookAtArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class GuardLookAtArguments
            {
                public int index;
                public double arc;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ index = ").Append(this.index).Append(", arc = ").Append(this.arc).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<GuardLookAtArguments> GuardLookAt;
            #region UserGuardLookAtArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserGuardLookAtArguments : WithUserArguments
            {
                public int index;
                public double arc;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", index = ").Append(this.index).Append(", arc = ").Append(this.arc).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserGuardLookAtArguments> UserGuardLookAt;
            #region GuardAddDamageArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class GuardAddDamageArguments
            {
                public int index;
                public double damage;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ index = ").Append(this.index).Append(", damage = ").Append(this.damage).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<GuardAddDamageArguments> GuardAddDamage;
            #region UserGuardAddDamageArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class UserGuardAddDamageArguments : WithUserArguments
            {
                public int index;
                public double damage;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", index = ").Append(this.index).Append(", damage = ").Append(this.damage).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserGuardAddDamageArguments> UserGuardAddDamage;
            #region ReportScoreArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class ReportScoreArguments
            {
                public int level;
                public int score;
                public int kills;
                public int teleports;
                public int fps;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ level = ").Append(this.level).Append(", score = ").Append(this.score).Append(", kills = ").Append(this.kills).Append(", teleports = ").Append(this.teleports).Append(", fps = ").Append(this.fps).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ReportScoreArguments> ReportScore;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<IDispatchHelper>>
                        {
                            { Messages.ServerPlayerHello, e => { ServerPlayerHello(new ServerPlayerHelloArguments { user = e.GetInt32(0), name = e.GetString(1), user_with_map = e.GetInt32(2), handshake = e.GetInt32Array(3) }); } },
                            { Messages.ServerPlayerJoined, e => { ServerPlayerJoined(new ServerPlayerJoinedArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerLeft, e => { ServerPlayerLeft(new ServerPlayerLeftArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.PlayerAdvertise, e => { PlayerAdvertise(new PlayerAdvertiseArguments { name = e.GetString(0), vector = e.GetInt32Array(1) }); } },
                            { Messages.UserPlayerAdvertise, e => { UserPlayerAdvertise(new UserPlayerAdvertiseArguments { user = e.GetInt32(0), name = e.GetString(1), vector = e.GetInt32Array(2) }); } },
                            { Messages.ServerSendMap, e => { ServerSendMap(new ServerSendMapArguments {  }); } },
                            { Messages.SendMap, e => { SendMap(new SendMapArguments { bytestream = e.GetInt32Array(0) }); } },
                            { Messages.UserSendMap, e => { UserSendMap(new UserSendMapArguments { user = e.GetInt32(0), bytestream = e.GetInt32Array(1) }); } },
                            { Messages.TakeAmmo, e => { TakeAmmo(new TakeAmmoArguments { index = e.GetInt32(0) }); } },
                            { Messages.UserTakeAmmo, e => { UserTakeAmmo(new UserTakeAmmoArguments { user = e.GetInt32(0), index = e.GetInt32(1) }); } },
                            { Messages.TakeGold, e => { TakeGold(new TakeGoldArguments { index = e.GetInt32(0) }); } },
                            { Messages.UserTakeGold, e => { UserTakeGold(new UserTakeGoldArguments { user = e.GetInt32(0), index = e.GetInt32(1) }); } },
                            { Messages.AddDamageToCoPlayer, e => { AddDamageToCoPlayer(new AddDamageToCoPlayerArguments { target = e.GetInt32(0), damage = e.GetDouble(1) }); } },
                            { Messages.UserAddDamageToCoPlayer, e => { UserAddDamageToCoPlayer(new UserAddDamageToCoPlayerArguments { user = e.GetInt32(0), target = e.GetInt32(1), damage = e.GetDouble(2) }); } },
                            { Messages.WalkTo, e => { WalkTo(new WalkToArguments { bytestream = e.GetInt32Array(0) }); } },
                            { Messages.UserWalkTo, e => { UserWalkTo(new UserWalkToArguments { user = e.GetInt32(0), bytestream = e.GetInt32Array(1) }); } },
                            { Messages.LookAt, e => { LookAt(new LookAtArguments { arc = e.GetDouble(0) }); } },
                            { Messages.UserLookAt, e => { UserLookAt(new UserLookAtArguments { user = e.GetInt32(0), arc = e.GetDouble(1) }); } },
                            { Messages.FireWeapon, e => { FireWeapon(new FireWeaponArguments {  }); } },
                            { Messages.UserFireWeapon, e => { UserFireWeapon(new UserFireWeaponArguments { user = e.GetInt32(0) }); } },
                            { Messages.EnterEndLevelMode, e => { EnterEndLevelMode(new EnterEndLevelModeArguments {  }); } },
                            { Messages.UserEnterEndLevelMode, e => { UserEnterEndLevelMode(new UserEnterEndLevelModeArguments { user = e.GetInt32(0) }); } },
                            { Messages.GuardWalkTo, e => { GuardWalkTo(new GuardWalkToArguments { bytestream = e.GetInt32Array(0) }); } },
                            { Messages.UserGuardWalkTo, e => { UserGuardWalkTo(new UserGuardWalkToArguments { user = e.GetInt32(0), bytestream = e.GetInt32Array(1) }); } },
                            { Messages.GuardLookAt, e => { GuardLookAt(new GuardLookAtArguments { index = e.GetInt32(0), arc = e.GetDouble(1) }); } },
                            { Messages.UserGuardLookAt, e => { UserGuardLookAt(new UserGuardLookAtArguments { user = e.GetInt32(0), index = e.GetInt32(1), arc = e.GetDouble(2) }); } },
                            { Messages.GuardAddDamage, e => { GuardAddDamage(new GuardAddDamageArguments { index = e.GetInt32(0), damage = e.GetDouble(1) }); } },
                            { Messages.UserGuardAddDamage, e => { UserGuardAddDamage(new UserGuardAddDamageArguments { user = e.GetInt32(0), index = e.GetInt32(1), damage = e.GetDouble(2) }); } },
                            { Messages.ReportScore, e => { ReportScore(new ReportScoreArguments { level = e.GetInt32(0), score = e.GetInt32(1), kills = e.GetInt32(2), teleports = e.GetInt32(3), fps = e.GetInt32(4) }); } },
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
                            { Messages.TakeAmmo, e => TakeAmmo },
                            { Messages.UserTakeAmmo, e => UserTakeAmmo },
                            { Messages.TakeGold, e => TakeGold },
                            { Messages.UserTakeGold, e => UserTakeGold },
                            { Messages.AddDamageToCoPlayer, e => AddDamageToCoPlayer },
                            { Messages.UserAddDamageToCoPlayer, e => UserAddDamageToCoPlayer },
                            { Messages.WalkTo, e => WalkTo },
                            { Messages.UserWalkTo, e => UserWalkTo },
                            { Messages.LookAt, e => LookAt },
                            { Messages.UserLookAt, e => UserLookAt },
                            { Messages.FireWeapon, e => FireWeapon },
                            { Messages.UserFireWeapon, e => UserFireWeapon },
                            { Messages.EnterEndLevelMode, e => EnterEndLevelMode },
                            { Messages.UserEnterEndLevelMode, e => UserEnterEndLevelMode },
                            { Messages.GuardWalkTo, e => GuardWalkTo },
                            { Messages.UserGuardWalkTo, e => UserGuardWalkTo },
                            { Messages.GuardLookAt, e => GuardLookAt },
                            { Messages.UserGuardLookAt, e => UserGuardLookAt },
                            { Messages.GuardAddDamage, e => GuardAddDamage },
                            { Messages.UserGuardAddDamage, e => UserGuardAddDamage },
                            { Messages.ReportScore, e => ReportScore },
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
            void IMessages.ServerPlayerHello(int user, string name, int user_with_map, int[] handshake)
            {
                if(ServerPlayerHello == null) return;
                var v = new RemoteEvents.ServerPlayerHelloArguments { user = user, name = name, user_with_map = user_with_map, handshake = handshake };
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
            void IMessages.PlayerAdvertise(string name, int[] vector)
            {
                if(PlayerAdvertise == null) return;
                var v = new RemoteEvents.PlayerAdvertiseArguments { name = name, vector = vector };
                this.VirtualLatency(() => this.PlayerAdvertise(v));
            }

            public event Action<RemoteEvents.UserPlayerAdvertiseArguments> UserPlayerAdvertise;
            void IMessages.UserPlayerAdvertise(int user, string name, int[] vector)
            {
                if(UserPlayerAdvertise == null) return;
                var v = new RemoteEvents.UserPlayerAdvertiseArguments { user = user, name = name, vector = vector };
                this.VirtualLatency(() => this.UserPlayerAdvertise(v));
            }

            public event Action<RemoteEvents.ServerSendMapArguments> ServerSendMap;
            void IMessages.ServerSendMap()
            {
                if(ServerSendMap == null) return;
                var v = new RemoteEvents.ServerSendMapArguments {  };
                this.VirtualLatency(() => this.ServerSendMap(v));
            }

            public event Action<RemoteEvents.SendMapArguments> SendMap;
            void IMessages.SendMap(int[] bytestream)
            {
                if(SendMap == null) return;
                var v = new RemoteEvents.SendMapArguments { bytestream = bytestream };
                this.VirtualLatency(() => this.SendMap(v));
            }

            public event Action<RemoteEvents.UserSendMapArguments> UserSendMap;
            void IMessages.UserSendMap(int user, int[] bytestream)
            {
                if(UserSendMap == null) return;
                var v = new RemoteEvents.UserSendMapArguments { user = user, bytestream = bytestream };
                this.VirtualLatency(() => this.UserSendMap(v));
            }

            public event Action<RemoteEvents.TakeAmmoArguments> TakeAmmo;
            void IMessages.TakeAmmo(int index)
            {
                if(TakeAmmo == null) return;
                var v = new RemoteEvents.TakeAmmoArguments { index = index };
                this.VirtualLatency(() => this.TakeAmmo(v));
            }

            public event Action<RemoteEvents.UserTakeAmmoArguments> UserTakeAmmo;
            void IMessages.UserTakeAmmo(int user, int index)
            {
                if(UserTakeAmmo == null) return;
                var v = new RemoteEvents.UserTakeAmmoArguments { user = user, index = index };
                this.VirtualLatency(() => this.UserTakeAmmo(v));
            }

            public event Action<RemoteEvents.TakeGoldArguments> TakeGold;
            void IMessages.TakeGold(int index)
            {
                if(TakeGold == null) return;
                var v = new RemoteEvents.TakeGoldArguments { index = index };
                this.VirtualLatency(() => this.TakeGold(v));
            }

            public event Action<RemoteEvents.UserTakeGoldArguments> UserTakeGold;
            void IMessages.UserTakeGold(int user, int index)
            {
                if(UserTakeGold == null) return;
                var v = new RemoteEvents.UserTakeGoldArguments { user = user, index = index };
                this.VirtualLatency(() => this.UserTakeGold(v));
            }

            public event Action<RemoteEvents.AddDamageToCoPlayerArguments> AddDamageToCoPlayer;
            void IMessages.AddDamageToCoPlayer(int target, double damage)
            {
                if(AddDamageToCoPlayer == null) return;
                var v = new RemoteEvents.AddDamageToCoPlayerArguments { target = target, damage = damage };
                this.VirtualLatency(() => this.AddDamageToCoPlayer(v));
            }

            public event Action<RemoteEvents.UserAddDamageToCoPlayerArguments> UserAddDamageToCoPlayer;
            void IMessages.UserAddDamageToCoPlayer(int user, int target, double damage)
            {
                if(UserAddDamageToCoPlayer == null) return;
                var v = new RemoteEvents.UserAddDamageToCoPlayerArguments { user = user, target = target, damage = damage };
                this.VirtualLatency(() => this.UserAddDamageToCoPlayer(v));
            }

            public event Action<RemoteEvents.WalkToArguments> WalkTo;
            void IMessages.WalkTo(int[] bytestream)
            {
                if(WalkTo == null) return;
                var v = new RemoteEvents.WalkToArguments { bytestream = bytestream };
                this.VirtualLatency(() => this.WalkTo(v));
            }

            public event Action<RemoteEvents.UserWalkToArguments> UserWalkTo;
            void IMessages.UserWalkTo(int user, int[] bytestream)
            {
                if(UserWalkTo == null) return;
                var v = new RemoteEvents.UserWalkToArguments { user = user, bytestream = bytestream };
                this.VirtualLatency(() => this.UserWalkTo(v));
            }

            public event Action<RemoteEvents.LookAtArguments> LookAt;
            void IMessages.LookAt(double arc)
            {
                if(LookAt == null) return;
                var v = new RemoteEvents.LookAtArguments { arc = arc };
                this.VirtualLatency(() => this.LookAt(v));
            }

            public event Action<RemoteEvents.UserLookAtArguments> UserLookAt;
            void IMessages.UserLookAt(int user, double arc)
            {
                if(UserLookAt == null) return;
                var v = new RemoteEvents.UserLookAtArguments { user = user, arc = arc };
                this.VirtualLatency(() => this.UserLookAt(v));
            }

            public event Action<RemoteEvents.FireWeaponArguments> FireWeapon;
            void IMessages.FireWeapon()
            {
                if(FireWeapon == null) return;
                var v = new RemoteEvents.FireWeaponArguments {  };
                this.VirtualLatency(() => this.FireWeapon(v));
            }

            public event Action<RemoteEvents.UserFireWeaponArguments> UserFireWeapon;
            void IMessages.UserFireWeapon(int user)
            {
                if(UserFireWeapon == null) return;
                var v = new RemoteEvents.UserFireWeaponArguments { user = user };
                this.VirtualLatency(() => this.UserFireWeapon(v));
            }

            public event Action<RemoteEvents.EnterEndLevelModeArguments> EnterEndLevelMode;
            void IMessages.EnterEndLevelMode()
            {
                if(EnterEndLevelMode == null) return;
                var v = new RemoteEvents.EnterEndLevelModeArguments {  };
                this.VirtualLatency(() => this.EnterEndLevelMode(v));
            }

            public event Action<RemoteEvents.UserEnterEndLevelModeArguments> UserEnterEndLevelMode;
            void IMessages.UserEnterEndLevelMode(int user)
            {
                if(UserEnterEndLevelMode == null) return;
                var v = new RemoteEvents.UserEnterEndLevelModeArguments { user = user };
                this.VirtualLatency(() => this.UserEnterEndLevelMode(v));
            }

            public event Action<RemoteEvents.GuardWalkToArguments> GuardWalkTo;
            void IMessages.GuardWalkTo(int[] bytestream)
            {
                if(GuardWalkTo == null) return;
                var v = new RemoteEvents.GuardWalkToArguments { bytestream = bytestream };
                this.VirtualLatency(() => this.GuardWalkTo(v));
            }

            public event Action<RemoteEvents.UserGuardWalkToArguments> UserGuardWalkTo;
            void IMessages.UserGuardWalkTo(int user, int[] bytestream)
            {
                if(UserGuardWalkTo == null) return;
                var v = new RemoteEvents.UserGuardWalkToArguments { user = user, bytestream = bytestream };
                this.VirtualLatency(() => this.UserGuardWalkTo(v));
            }

            public event Action<RemoteEvents.GuardLookAtArguments> GuardLookAt;
            void IMessages.GuardLookAt(int index, double arc)
            {
                if(GuardLookAt == null) return;
                var v = new RemoteEvents.GuardLookAtArguments { index = index, arc = arc };
                this.VirtualLatency(() => this.GuardLookAt(v));
            }

            public event Action<RemoteEvents.UserGuardLookAtArguments> UserGuardLookAt;
            void IMessages.UserGuardLookAt(int user, int index, double arc)
            {
                if(UserGuardLookAt == null) return;
                var v = new RemoteEvents.UserGuardLookAtArguments { user = user, index = index, arc = arc };
                this.VirtualLatency(() => this.UserGuardLookAt(v));
            }

            public event Action<RemoteEvents.GuardAddDamageArguments> GuardAddDamage;
            void IMessages.GuardAddDamage(int index, double damage)
            {
                if(GuardAddDamage == null) return;
                var v = new RemoteEvents.GuardAddDamageArguments { index = index, damage = damage };
                this.VirtualLatency(() => this.GuardAddDamage(v));
            }

            public event Action<RemoteEvents.UserGuardAddDamageArguments> UserGuardAddDamage;
            void IMessages.UserGuardAddDamage(int user, int index, double damage)
            {
                if(UserGuardAddDamage == null) return;
                var v = new RemoteEvents.UserGuardAddDamageArguments { user = user, index = index, damage = damage };
                this.VirtualLatency(() => this.UserGuardAddDamage(v));
            }

            public event Action<RemoteEvents.ReportScoreArguments> ReportScore;
            void IMessages.ReportScore(int level, int score, int kills, int teleports, int fps)
            {
                if(ReportScore == null) return;
                var v = new RemoteEvents.ReportScoreArguments { level = level, score = score, kills = kills, teleports = teleports, fps = fps };
                this.VirtualLatency(() => this.ReportScore(v));
            }

        }
        #endregion
    }
    #endregion
}
// 25.09.2008 15:19:24
