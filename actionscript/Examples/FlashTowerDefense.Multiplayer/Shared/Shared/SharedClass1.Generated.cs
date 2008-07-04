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
            event Action<RemoteEvents.TeleportToArguments> TeleportTo;
            event Action<RemoteEvents.UserTeleportToArguments> UserTeleportTo;
            event Action<RemoteEvents.WalkToArguments> WalkTo;
            event Action<RemoteEvents.UserWalkToArguments> UserWalkTo;
            event Action<RemoteEvents.CancelServerRandomNumbersArguments> CancelServerRandomNumbers;
            event Action<RemoteEvents.ReadyForServerRandomNumbersArguments> ReadyForServerRandomNumbers;
            event Action<RemoteEvents.TakeBoxArguments> TakeBox;
            event Action<RemoteEvents.UserTakeBoxArguments> UserTakeBox;
            event Action<RemoteEvents.FiredWeaponArguments> FiredWeapon;
            event Action<RemoteEvents.UserFiredWeaponArguments> UserFiredWeapon;
            event Action<RemoteEvents.ServerRandomNumbersArguments> ServerRandomNumbers;
            event Action<RemoteEvents.ServerMessageArguments> ServerMessage;
            event Action<RemoteEvents.UserEnterMachineGunArguments> UserEnterMachineGun;
            event Action<RemoteEvents.UserExitMachineGunArguments> UserExitMachineGun;
            event Action<RemoteEvents.UserStartMachineGunArguments> UserStartMachineGun;
            event Action<RemoteEvents.UserStopMachineGunArguments> UserStopMachineGun;
            event Action<RemoteEvents.EnterMachineGunArguments> EnterMachineGun;
            event Action<RemoteEvents.ExitMachineGunArguments> ExitMachineGun;
            event Action<RemoteEvents.StartMachineGunArguments> StartMachineGun;
            event Action<RemoteEvents.StopMachineGunArguments> StopMachineGun;
            event Action<RemoteEvents.PingArguments> Ping;
            event Action<RemoteEvents.AddDamageArguments> AddDamage;
            event Action<RemoteEvents.UserAddDamageArguments> UserAddDamage;
            event Action<RemoteEvents.AddDamageFromDirectionArguments> AddDamageFromDirection;
            event Action<RemoteEvents.UserAddDamageFromDirectionArguments> UserAddDamageFromDirection;
            event Action<RemoteEvents.ShowBulletsFlyingArguments> ShowBulletsFlying;
            event Action<RemoteEvents.UserShowBulletsFlyingArguments> UserShowBulletsFlying;
            event Action<RemoteEvents.ServerPlayerHelloArguments> ServerPlayerHello;
            event Action<RemoteEvents.ServerPlayerJoinedArguments> ServerPlayerJoined;
            event Action<RemoteEvents.ServerPlayerLeftArguments> ServerPlayerLeft;
            event Action<RemoteEvents.PlayerAdvertiseArguments> PlayerAdvertise;
            event Action<RemoteEvents.PlayerResurrectArguments> PlayerResurrect;
            event Action<RemoteEvents.UserPlayerResurrectArguments> UserPlayerResurrect;
            event Action<RemoteEvents.ServerPlayerAdvertiseArguments> ServerPlayerAdvertise;
            event Action<RemoteEvents.UndeployExplosiveBarrelArguments> UndeployExplosiveBarrel;
            event Action<RemoteEvents.UserUndeployExplosiveBarrelArguments> UserUndeployExplosiveBarrel;
            event Action<RemoteEvents.DeployExplosiveBarrelArguments> DeployExplosiveBarrel;
            event Action<RemoteEvents.UserDeployExplosiveBarrelArguments> UserDeployExplosiveBarrel;
        }
        #endregion
        #region IPairedEventsWithoutUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedEventsWithoutUser
        {
            event Action<RemoteEvents.TeleportToArguments> TeleportTo;
            event Action<RemoteEvents.WalkToArguments> WalkTo;
            event Action<RemoteEvents.TakeBoxArguments> TakeBox;
            event Action<RemoteEvents.FiredWeaponArguments> FiredWeapon;
            event Action<RemoteEvents.EnterMachineGunArguments> EnterMachineGun;
            event Action<RemoteEvents.ExitMachineGunArguments> ExitMachineGun;
            event Action<RemoteEvents.StartMachineGunArguments> StartMachineGun;
            event Action<RemoteEvents.StopMachineGunArguments> StopMachineGun;
            event Action<RemoteEvents.AddDamageArguments> AddDamage;
            event Action<RemoteEvents.AddDamageFromDirectionArguments> AddDamageFromDirection;
            event Action<RemoteEvents.ShowBulletsFlyingArguments> ShowBulletsFlying;
            event Action<RemoteEvents.PlayerResurrectArguments> PlayerResurrect;
            event Action<RemoteEvents.UndeployExplosiveBarrelArguments> UndeployExplosiveBarrel;
            event Action<RemoteEvents.DeployExplosiveBarrelArguments> DeployExplosiveBarrel;
        }
        #endregion
        #region IPairedEventsWithUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedEventsWithUser
        {
            event Action<RemoteEvents.UserTeleportToArguments> UserTeleportTo;
            event Action<RemoteEvents.UserWalkToArguments> UserWalkTo;
            event Action<RemoteEvents.UserTakeBoxArguments> UserTakeBox;
            event Action<RemoteEvents.UserFiredWeaponArguments> UserFiredWeapon;
            event Action<RemoteEvents.UserEnterMachineGunArguments> UserEnterMachineGun;
            event Action<RemoteEvents.UserExitMachineGunArguments> UserExitMachineGun;
            event Action<RemoteEvents.UserStartMachineGunArguments> UserStartMachineGun;
            event Action<RemoteEvents.UserStopMachineGunArguments> UserStopMachineGun;
            event Action<RemoteEvents.UserAddDamageArguments> UserAddDamage;
            event Action<RemoteEvents.UserAddDamageFromDirectionArguments> UserAddDamageFromDirection;
            event Action<RemoteEvents.UserShowBulletsFlyingArguments> UserShowBulletsFlying;
            event Action<RemoteEvents.UserPlayerResurrectArguments> UserPlayerResurrect;
            event Action<RemoteEvents.UserUndeployExplosiveBarrelArguments> UserUndeployExplosiveBarrel;
            event Action<RemoteEvents.UserDeployExplosiveBarrelArguments> UserDeployExplosiveBarrel;
        }
        #endregion
        #region IPairedMessagesWithUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedMessagesWithUser
        {
            void UserTeleportTo(int user, int x, int y);
            void UserWalkTo(int user, int x, int y);
            void UserTakeBox(int user, int box);
            void UserFiredWeapon(int user, int weapon);
            void UserEnterMachineGun(int user);
            void UserExitMachineGun(int user);
            void UserStartMachineGun(int user);
            void UserStopMachineGun(int user);
            void UserAddDamage(int user, int target, int damage);
            void UserAddDamageFromDirection(int user, int target, int damage, int arc);
            void UserShowBulletsFlying(int user, int x, int y, int arc, int weaponType);
            void UserPlayerResurrect(int user);
            void UserUndeployExplosiveBarrel(int user, int barrel);
            void UserDeployExplosiveBarrel(int user, int weapon, int barrel, int x, int y);
        }
        #endregion
        #region IPairedMessagesWithoutUser
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial interface IPairedMessagesWithoutUser
        {
            void TeleportTo(int x, int y);
            void WalkTo(int x, int y);
            void TakeBox(int box);
            void FiredWeapon(int weapon);
            void EnterMachineGun();
            void ExitMachineGun();
            void StartMachineGun();
            void StopMachineGun();
            void AddDamage(int target, int damage);
            void AddDamageFromDirection(int target, int damage, int arc);
            void ShowBulletsFlying(int x, int y, int arc, int weaponType);
            void PlayerResurrect();
            void UndeployExplosiveBarrel(int barrel);
            void DeployExplosiveBarrel(int weapon, int barrel, int x, int y);
        }
        #endregion

        #region RemoteMessages
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IRemoteMessages<RemoteMessages.SendArguments>, IMessages, IPairedMessagesWithoutUser, IPairedMessagesWithUser
        {
            public Action<SendArguments> Send { get; set; }

            #region SendArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class SendArguments : ISendArguments
            {
                int ISendArguments.i { get { return (int)i; } }

                public Messages i { get; set; }
                public object[] args { get; set; }
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
        public sealed partial class RemoteEvents : IEvents, IPairedEventsWithoutUser, IPairedEventsWithUser
        {
            private readonly Dictionary<Messages, Action<DispatchHelper>> DispatchTable;
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
            
            public bool Dispatch(Messages e, DispatchHelper h)
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
            public sealed partial class WithUserArgumentsRouter : WithUserArguments, IWithUserArgumentsRouter<RemoteMessages>
            {
                int IWithUserArgumentsRouter<RemoteMessages>.user { set { this.user = value; } }
                RemoteMessages IWithUserArgumentsRouter<RemoteMessages>.Target { set { this.Target = value; } }

                public RemoteMessages Target;
                #region Routing
                public void UserTeleportTo(TeleportToArguments e)
                {
                    Target.UserTeleportTo(this.user, e.x, e.y);
                }
                public void UserWalkTo(WalkToArguments e)
                {
                    Target.UserWalkTo(this.user, e.x, e.y);
                }
                public void UserTakeBox(TakeBoxArguments e)
                {
                    Target.UserTakeBox(this.user, e.box);
                }
                public void UserFiredWeapon(FiredWeaponArguments e)
                {
                    Target.UserFiredWeapon(this.user, e.weapon);
                }
                public void UserEnterMachineGun(EnterMachineGunArguments e)
                {
                    Target.UserEnterMachineGun(this.user);
                }
                public void UserExitMachineGun(ExitMachineGunArguments e)
                {
                    Target.UserExitMachineGun(this.user);
                }
                public void UserStartMachineGun(StartMachineGunArguments e)
                {
                    Target.UserStartMachineGun(this.user);
                }
                public void UserStopMachineGun(StopMachineGunArguments e)
                {
                    Target.UserStopMachineGun(this.user);
                }
                public void UserAddDamage(AddDamageArguments e)
                {
                    Target.UserAddDamage(this.user, e.target, e.damage);
                }
                public void UserAddDamageFromDirection(AddDamageFromDirectionArguments e)
                {
                    Target.UserAddDamageFromDirection(this.user, e.target, e.damage, e.arc);
                }
                public void UserShowBulletsFlying(ShowBulletsFlyingArguments e)
                {
                    Target.UserShowBulletsFlying(this.user, e.x, e.y, e.arc, e.weaponType);
                }
                public void UserPlayerResurrect(PlayerResurrectArguments e)
                {
                    Target.UserPlayerResurrect(this.user);
                }
                public void UserUndeployExplosiveBarrel(UndeployExplosiveBarrelArguments e)
                {
                    Target.UserUndeployExplosiveBarrel(this.user, e.barrel);
                }
                public void UserDeployExplosiveBarrel(DeployExplosiveBarrelArguments e)
                {
                    Target.UserDeployExplosiveBarrel(this.user, e.weapon, e.barrel, e.x, e.y);
                }
                #endregion
            }
            #endregion
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
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<TeleportToArguments> TeleportTo;
            #region UserTeleportToArguments
#if !NoAttributes
            [Script]
#endif
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
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<WalkToArguments> WalkTo;
            #region UserWalkToArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserWalkToArguments : WithUserArguments
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
                    return new StringBuilder().Append("{ box = ").Append(this.box).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<TakeBoxArguments> TakeBox;
            #region UserTakeBoxArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserTakeBoxArguments : WithUserArguments
            {
                public int box;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", box = ").Append(this.box).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ weapon = ").Append(this.weapon).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<FiredWeaponArguments> FiredWeapon;
            #region UserFiredWeaponArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserFiredWeaponArguments : WithUserArguments
            {
                public int weapon;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", weapon = ").Append(this.weapon).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ e = ").Append(this.e).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ text = ").Append(this.text).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ServerMessageArguments> ServerMessage;
            #region UserEnterMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserEnterMachineGunArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserEnterMachineGunArguments> UserEnterMachineGun;
            #region UserExitMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserExitMachineGunArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserExitMachineGunArguments> UserExitMachineGun;
            #region UserStartMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserStartMachineGunArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UserStartMachineGunArguments> UserStartMachineGun;
            #region UserStopMachineGunArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserStopMachineGunArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<AddDamageArguments> AddDamage;
            #region UserAddDamageArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserAddDamageArguments : WithUserArguments
            {
                public int target;
                public int damage;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(", arc = ").Append(this.arc).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<AddDamageFromDirectionArguments> AddDamageFromDirection;
            #region UserAddDamageFromDirectionArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserAddDamageFromDirectionArguments : WithUserArguments
            {
                public int target;
                public int damage;
                public int arc;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", target = ").Append(this.target).Append(", damage = ").Append(this.damage).Append(", arc = ").Append(this.arc).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ x = ").Append(this.x).Append(", y = ").Append(this.y).Append(", arc = ").Append(this.arc).Append(", weaponType = ").Append(this.weaponType).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<ShowBulletsFlyingArguments> ShowBulletsFlying;
            #region UserShowBulletsFlyingArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserShowBulletsFlyingArguments : WithUserArguments
            {
                public int x;
                public int y;
                public int arc;
                public int weaponType;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y).Append(", arc = ").Append(this.arc).Append(", weaponType = ").Append(this.weaponType).Append(" }").ToString();
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
                public int ego;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ ego = ").Append(this.ego).Append(" }").ToString();
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
            public sealed partial class UserPlayerResurrectArguments : WithUserArguments
            {
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", name = ").Append(this.name).Append(", ego = ").Append(this.ego).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ barrel = ").Append(this.barrel).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<UndeployExplosiveBarrelArguments> UndeployExplosiveBarrel;
            #region UserUndeployExplosiveBarrelArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserUndeployExplosiveBarrelArguments : WithUserArguments
            {
                public int barrel;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", barrel = ").Append(this.barrel).Append(" }").ToString();
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
                    return new StringBuilder().Append("{ weapon = ").Append(this.weapon).Append(", barrel = ").Append(this.barrel).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<DeployExplosiveBarrelArguments> DeployExplosiveBarrel;
            #region UserDeployExplosiveBarrelArguments
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserDeployExplosiveBarrelArguments : WithUserArguments
            {
                public int weapon;
                public int barrel;
                public int x;
                public int y;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ user = ").Append(this.user).Append(", weapon = ").Append(this.weapon).Append(", barrel = ").Append(this.barrel).Append(", x = ").Append(this.x).Append(", y = ").Append(this.y).Append(" }").ToString();
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
                        this.TeleportTo -= _Router.UserTeleportTo;
                        this.WalkTo -= _Router.UserWalkTo;
                        this.TakeBox -= _Router.UserTakeBox;
                        this.FiredWeapon -= _Router.UserFiredWeapon;
                        this.EnterMachineGun -= _Router.UserEnterMachineGun;
                        this.ExitMachineGun -= _Router.UserExitMachineGun;
                        this.StartMachineGun -= _Router.UserStartMachineGun;
                        this.StopMachineGun -= _Router.UserStopMachineGun;
                        this.AddDamage -= _Router.UserAddDamage;
                        this.AddDamageFromDirection -= _Router.UserAddDamageFromDirection;
                        this.ShowBulletsFlying -= _Router.UserShowBulletsFlying;
                        this.PlayerResurrect -= _Router.UserPlayerResurrect;
                        this.UndeployExplosiveBarrel -= _Router.UserUndeployExplosiveBarrel;
                        this.DeployExplosiveBarrel -= _Router.UserDeployExplosiveBarrel;
                    }
                    _Router = value;
                    if(_Router != null)
                    {
                        this.TeleportTo += _Router.UserTeleportTo;
                        this.WalkTo += _Router.UserWalkTo;
                        this.TakeBox += _Router.UserTakeBox;
                        this.FiredWeapon += _Router.UserFiredWeapon;
                        this.EnterMachineGun += _Router.UserEnterMachineGun;
                        this.ExitMachineGun += _Router.UserExitMachineGun;
                        this.StartMachineGun += _Router.UserStartMachineGun;
                        this.StopMachineGun += _Router.UserStopMachineGun;
                        this.AddDamage += _Router.UserAddDamage;
                        this.AddDamageFromDirection += _Router.UserAddDamageFromDirection;
                        this.ShowBulletsFlying += _Router.UserShowBulletsFlying;
                        this.PlayerResurrect += _Router.UserPlayerResurrect;
                        this.UndeployExplosiveBarrel += _Router.UserUndeployExplosiveBarrel;
                        this.DeployExplosiveBarrel += _Router.UserDeployExplosiveBarrel;
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
            public event Action<RemoteEvents.TeleportToArguments> TeleportTo;
            void IMessages.TeleportTo(int x, int y)
            {
                if(TeleportTo == null) return;
                TeleportTo(new RemoteEvents.TeleportToArguments { x = x, y = y });
            }
            void IPairedMessagesWithoutUser.TeleportTo(int x, int y)
            {
                ((IMessages)this).TeleportTo(x, y);
            }

            public event Action<RemoteEvents.UserTeleportToArguments> UserTeleportTo;
            void IMessages.UserTeleportTo(int user, int x, int y)
            {
                if(UserTeleportTo == null) return;
                UserTeleportTo(new RemoteEvents.UserTeleportToArguments { user = user, x = x, y = y });
            }
            void IPairedMessagesWithUser.UserTeleportTo(int user, int x, int y)
            {
                ((IMessages)this).UserTeleportTo(user, x, y);
            }

            public event Action<RemoteEvents.WalkToArguments> WalkTo;
            void IMessages.WalkTo(int x, int y)
            {
                if(WalkTo == null) return;
                WalkTo(new RemoteEvents.WalkToArguments { x = x, y = y });
            }
            void IPairedMessagesWithoutUser.WalkTo(int x, int y)
            {
                ((IMessages)this).WalkTo(x, y);
            }

            public event Action<RemoteEvents.UserWalkToArguments> UserWalkTo;
            void IMessages.UserWalkTo(int user, int x, int y)
            {
                if(UserWalkTo == null) return;
                UserWalkTo(new RemoteEvents.UserWalkToArguments { user = user, x = x, y = y });
            }
            void IPairedMessagesWithUser.UserWalkTo(int user, int x, int y)
            {
                ((IMessages)this).UserWalkTo(user, x, y);
            }

            public event Action<RemoteEvents.CancelServerRandomNumbersArguments> CancelServerRandomNumbers;
            void IMessages.CancelServerRandomNumbers()
            {
                if(CancelServerRandomNumbers == null) return;
                CancelServerRandomNumbers(new RemoteEvents.CancelServerRandomNumbersArguments {  });
            }

            public event Action<RemoteEvents.ReadyForServerRandomNumbersArguments> ReadyForServerRandomNumbers;
            void IMessages.ReadyForServerRandomNumbers()
            {
                if(ReadyForServerRandomNumbers == null) return;
                ReadyForServerRandomNumbers(new RemoteEvents.ReadyForServerRandomNumbersArguments {  });
            }

            public event Action<RemoteEvents.TakeBoxArguments> TakeBox;
            void IMessages.TakeBox(int box)
            {
                if(TakeBox == null) return;
                TakeBox(new RemoteEvents.TakeBoxArguments { box = box });
            }
            void IPairedMessagesWithoutUser.TakeBox(int box)
            {
                ((IMessages)this).TakeBox(box);
            }

            public event Action<RemoteEvents.UserTakeBoxArguments> UserTakeBox;
            void IMessages.UserTakeBox(int user, int box)
            {
                if(UserTakeBox == null) return;
                UserTakeBox(new RemoteEvents.UserTakeBoxArguments { user = user, box = box });
            }
            void IPairedMessagesWithUser.UserTakeBox(int user, int box)
            {
                ((IMessages)this).UserTakeBox(user, box);
            }

            public event Action<RemoteEvents.FiredWeaponArguments> FiredWeapon;
            void IMessages.FiredWeapon(int weapon)
            {
                if(FiredWeapon == null) return;
                FiredWeapon(new RemoteEvents.FiredWeaponArguments { weapon = weapon });
            }
            void IPairedMessagesWithoutUser.FiredWeapon(int weapon)
            {
                ((IMessages)this).FiredWeapon(weapon);
            }

            public event Action<RemoteEvents.UserFiredWeaponArguments> UserFiredWeapon;
            void IMessages.UserFiredWeapon(int user, int weapon)
            {
                if(UserFiredWeapon == null) return;
                UserFiredWeapon(new RemoteEvents.UserFiredWeaponArguments { user = user, weapon = weapon });
            }
            void IPairedMessagesWithUser.UserFiredWeapon(int user, int weapon)
            {
                ((IMessages)this).UserFiredWeapon(user, weapon);
            }

            public event Action<RemoteEvents.ServerRandomNumbersArguments> ServerRandomNumbers;
            void IMessages.ServerRandomNumbers(double[] e)
            {
                if(ServerRandomNumbers == null) return;
                ServerRandomNumbers(new RemoteEvents.ServerRandomNumbersArguments { e = e });
            }

            public event Action<RemoteEvents.ServerMessageArguments> ServerMessage;
            void IMessages.ServerMessage(string text)
            {
                if(ServerMessage == null) return;
                ServerMessage(new RemoteEvents.ServerMessageArguments { text = text });
            }

            public event Action<RemoteEvents.UserEnterMachineGunArguments> UserEnterMachineGun;
            void IMessages.UserEnterMachineGun(int user)
            {
                if(UserEnterMachineGun == null) return;
                UserEnterMachineGun(new RemoteEvents.UserEnterMachineGunArguments { user = user });
            }
            void IPairedMessagesWithUser.UserEnterMachineGun(int user)
            {
                ((IMessages)this).UserEnterMachineGun(user);
            }

            public event Action<RemoteEvents.UserExitMachineGunArguments> UserExitMachineGun;
            void IMessages.UserExitMachineGun(int user)
            {
                if(UserExitMachineGun == null) return;
                UserExitMachineGun(new RemoteEvents.UserExitMachineGunArguments { user = user });
            }
            void IPairedMessagesWithUser.UserExitMachineGun(int user)
            {
                ((IMessages)this).UserExitMachineGun(user);
            }

            public event Action<RemoteEvents.UserStartMachineGunArguments> UserStartMachineGun;
            void IMessages.UserStartMachineGun(int user)
            {
                if(UserStartMachineGun == null) return;
                UserStartMachineGun(new RemoteEvents.UserStartMachineGunArguments { user = user });
            }
            void IPairedMessagesWithUser.UserStartMachineGun(int user)
            {
                ((IMessages)this).UserStartMachineGun(user);
            }

            public event Action<RemoteEvents.UserStopMachineGunArguments> UserStopMachineGun;
            void IMessages.UserStopMachineGun(int user)
            {
                if(UserStopMachineGun == null) return;
                UserStopMachineGun(new RemoteEvents.UserStopMachineGunArguments { user = user });
            }
            void IPairedMessagesWithUser.UserStopMachineGun(int user)
            {
                ((IMessages)this).UserStopMachineGun(user);
            }

            public event Action<RemoteEvents.EnterMachineGunArguments> EnterMachineGun;
            void IMessages.EnterMachineGun()
            {
                if(EnterMachineGun == null) return;
                EnterMachineGun(new RemoteEvents.EnterMachineGunArguments {  });
            }
            void IPairedMessagesWithoutUser.EnterMachineGun()
            {
                ((IMessages)this).EnterMachineGun();
            }

            public event Action<RemoteEvents.ExitMachineGunArguments> ExitMachineGun;
            void IMessages.ExitMachineGun()
            {
                if(ExitMachineGun == null) return;
                ExitMachineGun(new RemoteEvents.ExitMachineGunArguments {  });
            }
            void IPairedMessagesWithoutUser.ExitMachineGun()
            {
                ((IMessages)this).ExitMachineGun();
            }

            public event Action<RemoteEvents.StartMachineGunArguments> StartMachineGun;
            void IMessages.StartMachineGun()
            {
                if(StartMachineGun == null) return;
                StartMachineGun(new RemoteEvents.StartMachineGunArguments {  });
            }
            void IPairedMessagesWithoutUser.StartMachineGun()
            {
                ((IMessages)this).StartMachineGun();
            }

            public event Action<RemoteEvents.StopMachineGunArguments> StopMachineGun;
            void IMessages.StopMachineGun()
            {
                if(StopMachineGun == null) return;
                StopMachineGun(new RemoteEvents.StopMachineGunArguments {  });
            }
            void IPairedMessagesWithoutUser.StopMachineGun()
            {
                ((IMessages)this).StopMachineGun();
            }

            public event Action<RemoteEvents.PingArguments> Ping;
            void IMessages.Ping()
            {
                if(Ping == null) return;
                Ping(new RemoteEvents.PingArguments {  });
            }

            public event Action<RemoteEvents.AddDamageArguments> AddDamage;
            void IMessages.AddDamage(int target, int damage)
            {
                if(AddDamage == null) return;
                AddDamage(new RemoteEvents.AddDamageArguments { target = target, damage = damage });
            }
            void IPairedMessagesWithoutUser.AddDamage(int target, int damage)
            {
                ((IMessages)this).AddDamage(target, damage);
            }

            public event Action<RemoteEvents.UserAddDamageArguments> UserAddDamage;
            void IMessages.UserAddDamage(int user, int target, int damage)
            {
                if(UserAddDamage == null) return;
                UserAddDamage(new RemoteEvents.UserAddDamageArguments { user = user, target = target, damage = damage });
            }
            void IPairedMessagesWithUser.UserAddDamage(int user, int target, int damage)
            {
                ((IMessages)this).UserAddDamage(user, target, damage);
            }

            public event Action<RemoteEvents.AddDamageFromDirectionArguments> AddDamageFromDirection;
            void IMessages.AddDamageFromDirection(int target, int damage, int arc)
            {
                if(AddDamageFromDirection == null) return;
                AddDamageFromDirection(new RemoteEvents.AddDamageFromDirectionArguments { target = target, damage = damage, arc = arc });
            }
            void IPairedMessagesWithoutUser.AddDamageFromDirection(int target, int damage, int arc)
            {
                ((IMessages)this).AddDamageFromDirection(target, damage, arc);
            }

            public event Action<RemoteEvents.UserAddDamageFromDirectionArguments> UserAddDamageFromDirection;
            void IMessages.UserAddDamageFromDirection(int user, int target, int damage, int arc)
            {
                if(UserAddDamageFromDirection == null) return;
                UserAddDamageFromDirection(new RemoteEvents.UserAddDamageFromDirectionArguments { user = user, target = target, damage = damage, arc = arc });
            }
            void IPairedMessagesWithUser.UserAddDamageFromDirection(int user, int target, int damage, int arc)
            {
                ((IMessages)this).UserAddDamageFromDirection(user, target, damage, arc);
            }

            public event Action<RemoteEvents.ShowBulletsFlyingArguments> ShowBulletsFlying;
            void IMessages.ShowBulletsFlying(int x, int y, int arc, int weaponType)
            {
                if(ShowBulletsFlying == null) return;
                ShowBulletsFlying(new RemoteEvents.ShowBulletsFlyingArguments { x = x, y = y, arc = arc, weaponType = weaponType });
            }
            void IPairedMessagesWithoutUser.ShowBulletsFlying(int x, int y, int arc, int weaponType)
            {
                ((IMessages)this).ShowBulletsFlying(x, y, arc, weaponType);
            }

            public event Action<RemoteEvents.UserShowBulletsFlyingArguments> UserShowBulletsFlying;
            void IMessages.UserShowBulletsFlying(int user, int x, int y, int arc, int weaponType)
            {
                if(UserShowBulletsFlying == null) return;
                UserShowBulletsFlying(new RemoteEvents.UserShowBulletsFlyingArguments { user = user, x = x, y = y, arc = arc, weaponType = weaponType });
            }
            void IPairedMessagesWithUser.UserShowBulletsFlying(int user, int x, int y, int arc, int weaponType)
            {
                ((IMessages)this).UserShowBulletsFlying(user, x, y, arc, weaponType);
            }

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
            void IMessages.PlayerAdvertise(int ego)
            {
                if(PlayerAdvertise == null) return;
                PlayerAdvertise(new RemoteEvents.PlayerAdvertiseArguments { ego = ego });
            }

            public event Action<RemoteEvents.PlayerResurrectArguments> PlayerResurrect;
            void IMessages.PlayerResurrect()
            {
                if(PlayerResurrect == null) return;
                PlayerResurrect(new RemoteEvents.PlayerResurrectArguments {  });
            }
            void IPairedMessagesWithoutUser.PlayerResurrect()
            {
                ((IMessages)this).PlayerResurrect();
            }

            public event Action<RemoteEvents.UserPlayerResurrectArguments> UserPlayerResurrect;
            void IMessages.UserPlayerResurrect(int user)
            {
                if(UserPlayerResurrect == null) return;
                UserPlayerResurrect(new RemoteEvents.UserPlayerResurrectArguments { user = user });
            }
            void IPairedMessagesWithUser.UserPlayerResurrect(int user)
            {
                ((IMessages)this).UserPlayerResurrect(user);
            }

            public event Action<RemoteEvents.ServerPlayerAdvertiseArguments> ServerPlayerAdvertise;
            void IMessages.ServerPlayerAdvertise(int user, string name, int ego)
            {
                if(ServerPlayerAdvertise == null) return;
                ServerPlayerAdvertise(new RemoteEvents.ServerPlayerAdvertiseArguments { user = user, name = name, ego = ego });
            }

            public event Action<RemoteEvents.UndeployExplosiveBarrelArguments> UndeployExplosiveBarrel;
            void IMessages.UndeployExplosiveBarrel(int barrel)
            {
                if(UndeployExplosiveBarrel == null) return;
                UndeployExplosiveBarrel(new RemoteEvents.UndeployExplosiveBarrelArguments { barrel = barrel });
            }
            void IPairedMessagesWithoutUser.UndeployExplosiveBarrel(int barrel)
            {
                ((IMessages)this).UndeployExplosiveBarrel(barrel);
            }

            public event Action<RemoteEvents.UserUndeployExplosiveBarrelArguments> UserUndeployExplosiveBarrel;
            void IMessages.UserUndeployExplosiveBarrel(int user, int barrel)
            {
                if(UserUndeployExplosiveBarrel == null) return;
                UserUndeployExplosiveBarrel(new RemoteEvents.UserUndeployExplosiveBarrelArguments { user = user, barrel = barrel });
            }
            void IPairedMessagesWithUser.UserUndeployExplosiveBarrel(int user, int barrel)
            {
                ((IMessages)this).UserUndeployExplosiveBarrel(user, barrel);
            }

            public event Action<RemoteEvents.DeployExplosiveBarrelArguments> DeployExplosiveBarrel;
            void IMessages.DeployExplosiveBarrel(int weapon, int barrel, int x, int y)
            {
                if(DeployExplosiveBarrel == null) return;
                DeployExplosiveBarrel(new RemoteEvents.DeployExplosiveBarrelArguments { weapon = weapon, barrel = barrel, x = x, y = y });
            }
            void IPairedMessagesWithoutUser.DeployExplosiveBarrel(int weapon, int barrel, int x, int y)
            {
                ((IMessages)this).DeployExplosiveBarrel(weapon, barrel, x, y);
            }

            public event Action<RemoteEvents.UserDeployExplosiveBarrelArguments> UserDeployExplosiveBarrel;
            void IMessages.UserDeployExplosiveBarrel(int user, int weapon, int barrel, int x, int y)
            {
                if(UserDeployExplosiveBarrel == null) return;
                UserDeployExplosiveBarrel(new RemoteEvents.UserDeployExplosiveBarrelArguments { user = user, weapon = weapon, barrel = barrel, x = x, y = y });
            }
            void IPairedMessagesWithUser.UserDeployExplosiveBarrel(int user, int weapon, int barrel, int x, int y)
            {
                ((IMessages)this).UserDeployExplosiveBarrel(user, weapon, barrel, x, y);
            }

        }
        #endregion
    }
    #endregion
}
// 4.07.2008 17:42:38
