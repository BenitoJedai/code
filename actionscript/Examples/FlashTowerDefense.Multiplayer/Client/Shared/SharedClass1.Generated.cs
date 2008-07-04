using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashTowerDefense.Shared
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
            TeleportTo,
            UserTeleportTo,
            WalkTo,
            UserWalkTo,
            CancelServerRandomNumbers,
            ReadyForServerRandomNumbers,
            TakeBox,
            UserTakeBox,
            FiredWeapon,
            UserFiredWeapon,
            ServerRandomNumbers,
            ServerMessage,
            UserEnterMachineGun,
            UserExitMachineGun,
            UserStartMachineGun,
            UserStopMachineGun,
            EnterMachineGun,
            ExitMachineGun,
            StartMachineGun,
            StopMachineGun,
            Ping,
            AddDamage,
            UserAddDamage,
            AddDamageFromDirection,
            UserAddDamageFromDirection,
            ShowBulletsFlying,
            UserShowBulletsFlying,
            ServerPlayerHello,
            ServerPlayerJoined,
            ServerPlayerLeft,
            PlayerAdvertise,
            PlayerResurrect,
            UserPlayerResurrect,
            ServerPlayerAdvertise,
            UndeployExplosiveBarrel,
            UserUndeployExplosiveBarrel,
            DeployExplosiveBarrel,
            UserDeployExplosiveBarrel,
        }
        #endregion

        #region RemoteMessages
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IMessages
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
            public void TeleportTo(int x, int y)
            {
                Send(new SendArguments { i = Messages.TeleportTo, args = new object[] { x, y } });
            }
            public void UserTeleportTo(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserTeleportTo, args = new object[] { user, x, y } });
            }
            public void WalkTo(int x, int y)
            {
                Send(new SendArguments { i = Messages.WalkTo, args = new object[] { x, y } });
            }
            public void UserWalkTo(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserWalkTo, args = new object[] { user, x, y } });
            }
            public void CancelServerRandomNumbers()
            {
                Send(new SendArguments { i = Messages.CancelServerRandomNumbers, args = new object[] {  } });
            }
            public void ReadyForServerRandomNumbers()
            {
                Send(new SendArguments { i = Messages.ReadyForServerRandomNumbers, args = new object[] {  } });
            }
            public void TakeBox(int box)
            {
                Send(new SendArguments { i = Messages.TakeBox, args = new object[] { box } });
            }
            public void UserTakeBox(int user, int box)
            {
                Send(new SendArguments { i = Messages.UserTakeBox, args = new object[] { user, box } });
            }
            public void FiredWeapon(int weapon)
            {
                Send(new SendArguments { i = Messages.FiredWeapon, args = new object[] { weapon } });
            }
            public void UserFiredWeapon(int user, int weapon)
            {
                Send(new SendArguments { i = Messages.UserFiredWeapon, args = new object[] { user, weapon } });
            }
            public void ServerRandomNumbers(double[] e)
            {
                var args = new object[e.Length];
                Array.Copy(e, args, e.Length);
                Send(new SendArguments { i = Messages.ServerRandomNumbers, args = args });
            }
            public void ServerMessage(string text)
            {
                Send(new SendArguments { i = Messages.ServerMessage, args = new object[] { text } });
            }
            public void UserEnterMachineGun(int user)
            {
                Send(new SendArguments { i = Messages.UserEnterMachineGun, args = new object[] { user } });
            }
            public void UserExitMachineGun(int user)
            {
                Send(new SendArguments { i = Messages.UserExitMachineGun, args = new object[] { user } });
            }
            public void UserStartMachineGun(int user)
            {
                Send(new SendArguments { i = Messages.UserStartMachineGun, args = new object[] { user } });
            }
            public void UserStopMachineGun(int user)
            {
                Send(new SendArguments { i = Messages.UserStopMachineGun, args = new object[] { user } });
            }
            public void EnterMachineGun()
            {
                Send(new SendArguments { i = Messages.EnterMachineGun, args = new object[] {  } });
            }
            public void ExitMachineGun()
            {
                Send(new SendArguments { i = Messages.ExitMachineGun, args = new object[] {  } });
            }
            public void StartMachineGun()
            {
                Send(new SendArguments { i = Messages.StartMachineGun, args = new object[] {  } });
            }
            public void StopMachineGun()
            {
                Send(new SendArguments { i = Messages.StopMachineGun, args = new object[] {  } });
            }
            public void Ping()
            {
                Send(new SendArguments { i = Messages.Ping, args = new object[] {  } });
            }
            public void AddDamage(int target, int damage)
            {
                Send(new SendArguments { i = Messages.AddDamage, args = new object[] { target, damage } });
            }
            public void UserAddDamage(int user, int target, int damage)
            {
                Send(new SendArguments { i = Messages.UserAddDamage, args = new object[] { user, target, damage } });
            }
            public void AddDamageFromDirection(int target, int damage, int arc)
            {
                Send(new SendArguments { i = Messages.AddDamageFromDirection, args = new object[] { target, damage, arc } });
            }
            public void UserAddDamageFromDirection(int user, int target, int damage, int arc)
            {
                Send(new SendArguments { i = Messages.UserAddDamageFromDirection, args = new object[] { user, target, damage, arc } });
            }
            public void ShowBulletsFlying(int x, int y, int arc, int weaponType)
            {
                Send(new SendArguments { i = Messages.ShowBulletsFlying, args = new object[] { x, y, arc, weaponType } });
            }
            public void UserShowBulletsFlying(int user, int x, int y, int arc, int weaponType)
            {
                Send(new SendArguments { i = Messages.UserShowBulletsFlying, args = new object[] { user, x, y, arc, weaponType } });
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
            public void PlayerAdvertise(int ego)
            {
                Send(new SendArguments { i = Messages.PlayerAdvertise, args = new object[] { ego } });
            }
            public void PlayerResurrect()
            {
                Send(new SendArguments { i = Messages.PlayerResurrect, args = new object[] {  } });
            }
            public void UserPlayerResurrect(int user)
            {
                Send(new SendArguments { i = Messages.UserPlayerResurrect, args = new object[] { user } });
            }
            public void ServerPlayerAdvertise(int user, string name, int ego)
            {
                Send(new SendArguments { i = Messages.ServerPlayerAdvertise, args = new object[] { user, name, ego } });
            }
            public void UndeployExplosiveBarrel(int barrel)
            {
                Send(new SendArguments { i = Messages.UndeployExplosiveBarrel, args = new object[] { barrel } });
            }
            public void UserUndeployExplosiveBarrel(int user, int barrel)
            {
                Send(new SendArguments { i = Messages.UserUndeployExplosiveBarrel, args = new object[] { user, barrel } });
            }
            public void DeployExplosiveBarrel(int weapon, int barrel, int x, int y)
            {
                Send(new SendArguments { i = Messages.DeployExplosiveBarrel, args = new object[] { weapon, barrel, x, y } });
            }
            public void UserDeployExplosiveBarrel(int user, int weapon, int barrel, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserDeployExplosiveBarrel, args = new object[] { user, weapon, barrel, x, y } });
            }
        }
        #endregion

        #region RemoteEvents
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public sealed partial class RemoteEvents
        {
            private readonly Dictionary<Messages, Action<DispatchHelper>> DispatchTable;
            private readonly Dictionary<Messages, Converter<object, Delegate>> DispatchTableDelegates;
            #region DispatchHelper
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public partial class DispatchHelper
            {
                public Converter<uint, int> GetInt32;
                public Converter<uint, double> GetDouble;
                public Converter<uint, string> GetString;
                public Converter<uint, int[]> GetInt32Array;
                public Converter<uint, double[]> GetDoubleArray;
                public Converter<uint, string[]> GetStringArray;
                public Converter<uint, object[]> GetArray;
            }
            #endregion
            public bool Dispatch(Messages e, DispatchHelper h)
            {
                if (!DispatchTableDelegates.ContainsKey(e)) return false;
                if (DispatchTableDelegates[e](null) == null) return false;
                if (!DispatchTable.ContainsKey(e)) return false;
                DispatchTable[e](h);
                return true;
            }
            #region TeleportToArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class TeleportToArguments
            {
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y)(" }").ToString();
                }
            }
            #endregion
            public event Action<TeleportToArguments> TeleportTo;
            #region UserTeleportToArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserTeleportToArguments
            {
                public int user;
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserTeleportToArguments> UserTeleportTo;
            #region WalkToArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class WalkToArguments
            {
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y)(" }").ToString();
                }
            }
            #endregion
            public event Action<WalkToArguments> WalkTo;
            #region UserWalkToArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserWalkToArguments
            {
                public int user;
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserWalkToArguments> UserWalkTo;
            #region CancelServerRandomNumbersArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class CancelServerRandomNumbersArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<CancelServerRandomNumbersArguments> CancelServerRandomNumbers;
            #region ReadyForServerRandomNumbersArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ReadyForServerRandomNumbersArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<ReadyForServerRandomNumbersArguments> ReadyForServerRandomNumbers;
            #region TakeBoxArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class TakeBoxArguments
            {
                public int box;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ box = ").Append(this.box)(" }").ToString();
                }
            }
            #endregion
            public event Action<TakeBoxArguments> TakeBox;
            #region UserTakeBoxArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserTakeBoxArguments
            {
                public int user;
                public int box;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", box = ").Append(this.box)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserTakeBoxArguments> UserTakeBox;
            #region FiredWeaponArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class FiredWeaponArguments
            {
                public int weapon;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ weapon = ").Append(this.weapon)(" }").ToString();
                }
            }
            #endregion
            public event Action<FiredWeaponArguments> FiredWeapon;
            #region UserFiredWeaponArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserFiredWeaponArguments
            {
                public int user;
                public int weapon;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", weapon = ").Append(this.weapon)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserFiredWeaponArguments> UserFiredWeapon;
            #region ServerRandomNumbersArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerRandomNumbersArguments
            {
                public double[] e;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ e = ").Append(this.e)(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerRandomNumbersArguments> ServerRandomNumbers;
            #region ServerMessageArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerMessageArguments
            {
                public string text;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ text = ").Append(this.text)(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerMessageArguments> ServerMessage;
            #region UserEnterMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserEnterMachineGunArguments
            {
                public int user;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserEnterMachineGunArguments> UserEnterMachineGun;
            #region UserExitMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserExitMachineGunArguments
            {
                public int user;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserExitMachineGunArguments> UserExitMachineGun;
            #region UserStartMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserStartMachineGunArguments
            {
                public int user;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserStartMachineGunArguments> UserStartMachineGun;
            #region UserStopMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserStopMachineGunArguments
            {
                public int user;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserStopMachineGunArguments> UserStopMachineGun;
            #region EnterMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class EnterMachineGunArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<EnterMachineGunArguments> EnterMachineGun;
            #region ExitMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ExitMachineGunArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<ExitMachineGunArguments> ExitMachineGun;
            #region StartMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class StartMachineGunArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<StartMachineGunArguments> StartMachineGun;
            #region StopMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class StopMachineGunArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<StopMachineGunArguments> StopMachineGun;
            #region PingArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class PingArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<PingArguments> Ping;
            #region AddDamageArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class AddDamageArguments
            {
                public int target;
                public int damage;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ target = ").Append(this.target).Append(", damage = ").Append(this.damage)(" }").ToString();
                }
            }
            #endregion
            public event Action<AddDamageArguments> AddDamage;
            #region UserAddDamageArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserAddDamageArguments
            {
                public int user;
                public int target;
                public int damage;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", target = ").Append(this.target).Append(", damage = ").Append(this.damage)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserAddDamageArguments> UserAddDamage;
            #region AddDamageFromDirectionArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class AddDamageFromDirectionArguments
            {
                public int target;
                public int damage;
                public int arc;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(", arc = ").Append(this.arc)(" }").ToString();
                }
            }
            #endregion
            public event Action<AddDamageFromDirectionArguments> AddDamageFromDirection;
            #region UserAddDamageFromDirectionArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserAddDamageFromDirectionArguments
            {
                public int user;
                public int target;
                public int damage;
                public int arc;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(", arc = ").Append(this.arc)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserAddDamageFromDirectionArguments> UserAddDamageFromDirection;
            #region ShowBulletsFlyingArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ShowBulletsFlyingArguments
            {
                public int x;
                public int y;
                public int arc;
                public int weaponType;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y).Append(", arc = ").Append(this.arc).Append(", weaponType = ").Append(this.weaponType)(" }").ToString();
                }
            }
            #endregion
            public event Action<ShowBulletsFlyingArguments> ShowBulletsFlying;
            #region UserShowBulletsFlyingArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserShowBulletsFlyingArguments
            {
                public int user;
                public int x;
                public int y;
                public int arc;
                public int weaponType;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y).Append(", arc = ").Append(this.arc).Append(", weaponType = ").Append(this.weaponType)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserShowBulletsFlyingArguments> UserShowBulletsFlying;
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
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name)(" }").ToString();
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
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name)(" }").ToString();
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
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name)(" }").ToString();
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
                public int ego;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ ego = ").Append(this.ego)(" }").ToString();
                }
            }
            #endregion
            public event Action<PlayerAdvertiseArguments> PlayerAdvertise;
            #region PlayerResurrectArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class PlayerResurrectArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().ToString();
                }
            }
            #endregion
            public event Action<PlayerResurrectArguments> PlayerResurrect;
            #region UserPlayerResurrectArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserPlayerResurrectArguments
            {
                public int user;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserPlayerResurrectArguments> UserPlayerResurrect;
            #region ServerPlayerAdvertiseArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerPlayerAdvertiseArguments
            {
                public int user;
                public string name;
                public int ego;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(", ego = ").Append(this.ego)(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerPlayerAdvertiseArguments> ServerPlayerAdvertise;
            #region UndeployExplosiveBarrelArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UndeployExplosiveBarrelArguments
            {
                public int barrel;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ barrel = ").Append(this.barrel)(" }").ToString();
                }
            }
            #endregion
            public event Action<UndeployExplosiveBarrelArguments> UndeployExplosiveBarrel;
            #region UserUndeployExplosiveBarrelArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserUndeployExplosiveBarrelArguments
            {
                public int user;
                public int barrel;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", barrel = ").Append(this.barrel)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserUndeployExplosiveBarrelArguments> UserUndeployExplosiveBarrel;
            #region DeployExplosiveBarrelArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class DeployExplosiveBarrelArguments
            {
                public int weapon;
                public int barrel;
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ weapon = ").Append(this.weapon).Append(", barrel = ").Append(this.barrel).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y)(" }").ToString();
                }
            }
            #endregion
            public event Action<DeployExplosiveBarrelArguments> DeployExplosiveBarrel;
            #region UserDeployExplosiveBarrelArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserDeployExplosiveBarrelArguments
            {
                public int user;
                public int weapon;
                public int barrel;
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", weapon = ").Append(this.weapon).Append(", barrel = ").Append(this.barrel).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y)(" }").ToString();
                }
            }
            #endregion
            public event Action<UserDeployExplosiveBarrelArguments> UserDeployExplosiveBarrel;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<DispatchHelper>>
                        {
                            { Messages.TeleportTo, e => { TeleportTo(new TeleportToArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserTeleportTo, e => { UserTeleportTo(new UserTeleportToArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.WalkTo, e => { WalkTo(new WalkToArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                            { Messages.UserWalkTo, e => { UserWalkTo(new UserWalkToArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                            { Messages.CancelServerRandomNumbers, e => { CancelServerRandomNumbers(new CancelServerRandomNumbersArguments {  }); } },
                            { Messages.ReadyForServerRandomNumbers, e => { ReadyForServerRandomNumbers(new ReadyForServerRandomNumbersArguments {  }); } },
                            { Messages.TakeBox, e => { TakeBox(new TakeBoxArguments { box = e.GetInt32(0) }); } },
                            { Messages.UserTakeBox, e => { UserTakeBox(new UserTakeBoxArguments { user = e.GetInt32(0), box = e.GetInt32(1) }); } },
                            { Messages.FiredWeapon, e => { FiredWeapon(new FiredWeaponArguments { weapon = e.GetInt32(0) }); } },
                            { Messages.UserFiredWeapon, e => { UserFiredWeapon(new UserFiredWeaponArguments { user = e.GetInt32(0), weapon = e.GetInt32(1) }); } },
                            { Messages.ServerRandomNumbers, e => { ServerRandomNumbers(new ServerRandomNumbersArguments { e = e.GetDoubleArray(0) }); } },
                            { Messages.ServerMessage, e => { ServerMessage(new ServerMessageArguments { text = e.GetString(0) }); } },
                            { Messages.UserEnterMachineGun, e => { UserEnterMachineGun(new UserEnterMachineGunArguments { user = e.GetInt32(0) }); } },
                            { Messages.UserExitMachineGun, e => { UserExitMachineGun(new UserExitMachineGunArguments { user = e.GetInt32(0) }); } },
                            { Messages.UserStartMachineGun, e => { UserStartMachineGun(new UserStartMachineGunArguments { user = e.GetInt32(0) }); } },
                            { Messages.UserStopMachineGun, e => { UserStopMachineGun(new UserStopMachineGunArguments { user = e.GetInt32(0) }); } },
                            { Messages.EnterMachineGun, e => { EnterMachineGun(new EnterMachineGunArguments {  }); } },
                            { Messages.ExitMachineGun, e => { ExitMachineGun(new ExitMachineGunArguments {  }); } },
                            { Messages.StartMachineGun, e => { StartMachineGun(new StartMachineGunArguments {  }); } },
                            { Messages.StopMachineGun, e => { StopMachineGun(new StopMachineGunArguments {  }); } },
                            { Messages.Ping, e => { Ping(new PingArguments {  }); } },
                            { Messages.AddDamage, e => { AddDamage(new AddDamageArguments { target = e.GetInt32(0), damage = e.GetInt32(1) }); } },
                            { Messages.UserAddDamage, e => { UserAddDamage(new UserAddDamageArguments { user = e.GetInt32(0), target = e.GetInt32(1), damage = e.GetInt32(2) }); } },
                            { Messages.AddDamageFromDirection, e => { AddDamageFromDirection(new AddDamageFromDirectionArguments { target = e.GetInt32(0), damage = e.GetInt32(1), arc = e.GetInt32(2) }); } },
                            { Messages.UserAddDamageFromDirection, e => { UserAddDamageFromDirection(new UserAddDamageFromDirectionArguments { user = e.GetInt32(0), target = e.GetInt32(1), damage = e.GetInt32(2), arc = e.GetInt32(3) }); } },
                            { Messages.ShowBulletsFlying, e => { ShowBulletsFlying(new ShowBulletsFlyingArguments { x = e.GetInt32(0), y = e.GetInt32(1), arc = e.GetInt32(2), weaponType = e.GetInt32(3) }); } },
                            { Messages.UserShowBulletsFlying, e => { UserShowBulletsFlying(new UserShowBulletsFlyingArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2), arc = e.GetInt32(3), weaponType = e.GetInt32(4) }); } },
                            { Messages.ServerPlayerHello, e => { ServerPlayerHello(new ServerPlayerHelloArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerJoined, e => { ServerPlayerJoined(new ServerPlayerJoinedArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.ServerPlayerLeft, e => { ServerPlayerLeft(new ServerPlayerLeftArguments { user = e.GetInt32(0), name = e.GetString(1) }); } },
                            { Messages.PlayerAdvertise, e => { PlayerAdvertise(new PlayerAdvertiseArguments { ego = e.GetInt32(0) }); } },
                            { Messages.PlayerResurrect, e => { PlayerResurrect(new PlayerResurrectArguments {  }); } },
                            { Messages.UserPlayerResurrect, e => { UserPlayerResurrect(new UserPlayerResurrectArguments { user = e.GetInt32(0) }); } },
                            { Messages.ServerPlayerAdvertise, e => { ServerPlayerAdvertise(new ServerPlayerAdvertiseArguments { user = e.GetInt32(0), name = e.GetString(1), ego = e.GetInt32(2) }); } },
                            { Messages.UndeployExplosiveBarrel, e => { UndeployExplosiveBarrel(new UndeployExplosiveBarrelArguments { barrel = e.GetInt32(0) }); } },
                            { Messages.UserUndeployExplosiveBarrel, e => { UserUndeployExplosiveBarrel(new UserUndeployExplosiveBarrelArguments { user = e.GetInt32(0), barrel = e.GetInt32(1) }); } },
                            { Messages.DeployExplosiveBarrel, e => { DeployExplosiveBarrel(new DeployExplosiveBarrelArguments { weapon = e.GetInt32(0), barrel = e.GetInt32(1), x = e.GetInt32(2), y = e.GetInt32(3) }); } },
                            { Messages.UserDeployExplosiveBarrel, e => { UserDeployExplosiveBarrel(new UserDeployExplosiveBarrelArguments { user = e.GetInt32(0), weapon = e.GetInt32(1), barrel = e.GetInt32(2), x = e.GetInt32(3), y = e.GetInt32(4) }); } },
                        }
                ;
                DispatchTableDelegates = new Dictionary<Messages, Converter<object, Delegate>>
                        {
                            { Messages.TeleportTo, e => TeleportTo },
                            { Messages.UserTeleportTo, e => UserTeleportTo },
                            { Messages.WalkTo, e => WalkTo },
                            { Messages.UserWalkTo, e => UserWalkTo },
                            { Messages.CancelServerRandomNumbers, e => CancelServerRandomNumbers },
                            { Messages.ReadyForServerRandomNumbers, e => ReadyForServerRandomNumbers },
                            { Messages.TakeBox, e => TakeBox },
                            { Messages.UserTakeBox, e => UserTakeBox },
                            { Messages.FiredWeapon, e => FiredWeapon },
                            { Messages.UserFiredWeapon, e => UserFiredWeapon },
                            { Messages.ServerRandomNumbers, e => ServerRandomNumbers },
                            { Messages.ServerMessage, e => ServerMessage },
                            { Messages.UserEnterMachineGun, e => UserEnterMachineGun },
                            { Messages.UserExitMachineGun, e => UserExitMachineGun },
                            { Messages.UserStartMachineGun, e => UserStartMachineGun },
                            { Messages.UserStopMachineGun, e => UserStopMachineGun },
                            { Messages.EnterMachineGun, e => EnterMachineGun },
                            { Messages.ExitMachineGun, e => ExitMachineGun },
                            { Messages.StartMachineGun, e => StartMachineGun },
                            { Messages.StopMachineGun, e => StopMachineGun },
                            { Messages.Ping, e => Ping },
                            { Messages.AddDamage, e => AddDamage },
                            { Messages.UserAddDamage, e => UserAddDamage },
                            { Messages.AddDamageFromDirection, e => AddDamageFromDirection },
                            { Messages.UserAddDamageFromDirection, e => UserAddDamageFromDirection },
                            { Messages.ShowBulletsFlying, e => ShowBulletsFlying },
                            { Messages.UserShowBulletsFlying, e => UserShowBulletsFlying },
                            { Messages.ServerPlayerHello, e => ServerPlayerHello },
                            { Messages.ServerPlayerJoined, e => ServerPlayerJoined },
                            { Messages.ServerPlayerLeft, e => ServerPlayerLeft },
                            { Messages.PlayerAdvertise, e => PlayerAdvertise },
                            { Messages.PlayerResurrect, e => PlayerResurrect },
                            { Messages.UserPlayerResurrect, e => UserPlayerResurrect },
                            { Messages.ServerPlayerAdvertise, e => ServerPlayerAdvertise },
                            { Messages.UndeployExplosiveBarrel, e => UndeployExplosiveBarrel },
                            { Messages.UserUndeployExplosiveBarrel, e => UserUndeployExplosiveBarrel },
                            { Messages.DeployExplosiveBarrel, e => DeployExplosiveBarrel },
                            { Messages.UserDeployExplosiveBarrel, e => UserDeployExplosiveBarrel },
                        }
                ;
            }
        }
        #endregion
    }
    #endregion
}
// 4.07.2008 10:57:32
