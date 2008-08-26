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
            public void ServerPlayerHello(int user, string name, int user_with_map, int[] handshake)
            {
                var args = new object[handshake.Length + 3];
                args[0] = user;
                args[1] = name;
                args[2] = user_with_map;
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
            public void PlayerAdvertise(string name, int[] vector)
            {
                var args = new object[vector.Length + 1];
                args[0] = name;
                Array.Copy(vector, 0, args, 1, vector.Length);
                Send(new SendArguments { i = Messages.PlayerAdvertise, args = args });
            }
            public void UserPlayerAdvertise(int user, string name, int[] vector)
            {
                var args = new object[vector.Length + 2];
                args[0] = user;
                args[1] = name;
                Array.Copy(vector, 0, args, 2, vector.Length);
                Send(new SendArguments { i = Messages.UserPlayerAdvertise, args = args });
            }
            public void ServerSendMap()
            {
                Send(new SendArguments { i = Messages.ServerSendMap, args = new object[] {  } });
            }
            public void SendMap(int[] bytestream)
            {
                var args = new object[bytestream.Length + 0];
                Array.Copy(bytestream, 0, args, 0, bytestream.Length);
                Send(new SendArguments { i = Messages.SendMap, args = args });
            }
            public void UserSendMap(int user, int[] bytestream)
            {
                var args = new object[bytestream.Length + 1];
                args[0] = user;
                Array.Copy(bytestream, 0, args, 1, bytestream.Length);
                Send(new SendArguments { i = Messages.UserSendMap, args = args });
            }
            public void TakeAmmo(int index)
            {
                Send(new SendArguments { i = Messages.TakeAmmo, args = new object[] { index } });
            }
            public void UserTakeAmmo(int user, int index)
            {
                Send(new SendArguments { i = Messages.UserTakeAmmo, args = new object[] { user, index } });
            }
            public void TakeGold(int index)
            {
                Send(new SendArguments { i = Messages.TakeGold, args = new object[] { index } });
            }
            public void UserTakeGold(int user, int index)
            {
                Send(new SendArguments { i = Messages.UserTakeGold, args = new object[] { user, index } });
            }
            public void AddDamageToCoPlayer(int target, double damage)
            {
                Send(new SendArguments { i = Messages.AddDamageToCoPlayer, args = new object[] { target, damage } });
            }
            public void UserAddDamageToCoPlayer(int user, int target, double damage)
            {
                Send(new SendArguments { i = Messages.UserAddDamageToCoPlayer, args = new object[] { user, target, damage } });
            }
            public void WalkTo(int[] bytestream)
            {
                var args = new object[bytestream.Length + 0];
                Array.Copy(bytestream, 0, args, 0, bytestream.Length);
                Send(new SendArguments { i = Messages.WalkTo, args = args });
            }
            public void UserWalkTo(int user, int[] bytestream)
            {
                var args = new object[bytestream.Length + 1];
                args[0] = user;
                Array.Copy(bytestream, 0, args, 1, bytestream.Length);
                Send(new SendArguments { i = Messages.UserWalkTo, args = args });
            }
            public void LookAt(double arc)
            {
                Send(new SendArguments { i = Messages.LookAt, args = new object[] { arc } });
            }
            public void UserLookAt(int user, double arc)
            {
                Send(new SendArguments { i = Messages.UserLookAt, args = new object[] { user, arc } });
            }
            public void FireWeapon()
            {
                Send(new SendArguments { i = Messages.FireWeapon, args = new object[] {  } });
            }
            public void UserFireWeapon(int user)
            {
                Send(new SendArguments { i = Messages.UserFireWeapon, args = new object[] { user } });
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
                    value.SendMap += this.UserSendMap;
                    value.TakeAmmo += this.UserTakeAmmo;
                    value.TakeGold += this.UserTakeGold;
                    value.AddDamageToCoPlayer += this.UserAddDamageToCoPlayer;
                    value.WalkTo += this.UserWalkTo;
                    value.LookAt += this.UserLookAt;
                    value.FireWeapon += this.UserFireWeapon;
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

        }
        #endregion
    }
    #endregion
}
// 26.08.2008 18:45:01
