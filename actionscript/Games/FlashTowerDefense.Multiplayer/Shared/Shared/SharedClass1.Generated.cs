using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ScriptCoreLib.Shared.Nonoba;
using ScriptCoreLib;

namespace FlashTowerDefense.Shared
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
            AddKillScore,
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
            event Action<RemoteEvents.AddKillScoreArguments> AddKillScore;
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
            public void TeleportTo(int x, int y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.TeleportTo, args = new object[] { x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.TeleportTo(x, y);
                    }
                }
            }
            public void UserTeleportTo(int user, int x, int y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserTeleportTo, args = new object[] { user, x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserTeleportTo(user, x, y);
                    }
                }
            }
            public void WalkTo(int x, int y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.WalkTo, args = new object[] { x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.WalkTo(x, y);
                    }
                }
            }
            public void UserWalkTo(int user, int x, int y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserWalkTo, args = new object[] { user, x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserWalkTo(user, x, y);
                    }
                }
            }
            public void CancelServerRandomNumbers()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.CancelServerRandomNumbers, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.CancelServerRandomNumbers();
                    }
                }
            }
            public void ReadyForServerRandomNumbers()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ReadyForServerRandomNumbers, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ReadyForServerRandomNumbers();
                    }
                }
            }
            public void TakeBox(int box)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.TakeBox, args = new object[] { box } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.TakeBox(box);
                    }
                }
            }
            public void UserTakeBox(int user, int box)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserTakeBox, args = new object[] { user, box } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserTakeBox(user, box);
                    }
                }
            }
            public void FiredWeapon(int weapon)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.FiredWeapon, args = new object[] { weapon } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.FiredWeapon(weapon);
                    }
                }
            }
            public void UserFiredWeapon(int user, int weapon)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserFiredWeapon, args = new object[] { user, weapon } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserFiredWeapon(user, weapon);
                    }
                }
            }
            public void ServerRandomNumbers(double[] e)
            {
                if (this.Send != null)
                {
                    var args = new object[e.Length + 0];
                    Array.Copy(e, 0, args, 0, e.Length);
                    Send(new SendArguments { i = Messages.ServerRandomNumbers, args = args });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerRandomNumbers(e);
                    }
                }
            }
            public void ServerMessage(string text)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ServerMessage, args = new object[] { text } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerMessage(text);
                    }
                }
            }
            public void UserEnterMachineGun(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserEnterMachineGun, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserEnterMachineGun(user);
                    }
                }
            }
            public void UserExitMachineGun(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserExitMachineGun, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserExitMachineGun(user);
                    }
                }
            }
            public void UserStartMachineGun(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserStartMachineGun, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserStartMachineGun(user);
                    }
                }
            }
            public void UserStopMachineGun(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserStopMachineGun, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserStopMachineGun(user);
                    }
                }
            }
            public void EnterMachineGun()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.EnterMachineGun, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.EnterMachineGun();
                    }
                }
            }
            public void ExitMachineGun()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ExitMachineGun, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ExitMachineGun();
                    }
                }
            }
            public void StartMachineGun()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.StartMachineGun, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.StartMachineGun();
                    }
                }
            }
            public void StopMachineGun()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.StopMachineGun, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.StopMachineGun();
                    }
                }
            }
            public void Ping()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.Ping, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.Ping();
                    }
                }
            }
            public void AddDamage(int target, int damage)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.AddDamage, args = new object[] { target, damage } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.AddDamage(target, damage);
                    }
                }
            }
            public void UserAddDamage(int user, int target, int damage)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserAddDamage, args = new object[] { user, target, damage } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserAddDamage(user, target, damage);
                    }
                }
            }
            public void AddDamageFromDirection(int target, int damage, int arc)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.AddDamageFromDirection, args = new object[] { target, damage, arc } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.AddDamageFromDirection(target, damage, arc);
                    }
                }
            }
            public void UserAddDamageFromDirection(int user, int target, int damage, int arc)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserAddDamageFromDirection, args = new object[] { user, target, damage, arc } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserAddDamageFromDirection(user, target, damage, arc);
                    }
                }
            }
            public void ShowBulletsFlying(int x, int y, int arc, int weaponType)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ShowBulletsFlying, args = new object[] { x, y, arc, weaponType } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ShowBulletsFlying(x, y, arc, weaponType);
                    }
                }
            }
            public void UserShowBulletsFlying(int user, int x, int y, int arc, int weaponType)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserShowBulletsFlying, args = new object[] { user, x, y, arc, weaponType } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserShowBulletsFlying(user, x, y, arc, weaponType);
                    }
                }
            }
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
            public void PlayerAdvertise(int ego)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.PlayerAdvertise, args = new object[] { ego } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.PlayerAdvertise(ego);
                    }
                }
            }
            public void PlayerResurrect()
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.PlayerResurrect, args = new object[] {  } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.PlayerResurrect();
                    }
                }
            }
            public void UserPlayerResurrect(int user)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserPlayerResurrect, args = new object[] { user } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserPlayerResurrect(user);
                    }
                }
            }
            public void ServerPlayerAdvertise(int user, string name, int ego)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.ServerPlayerAdvertise, args = new object[] { user, name, ego } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.ServerPlayerAdvertise(user, name, ego);
                    }
                }
            }
            public void UndeployExplosiveBarrel(int barrel)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UndeployExplosiveBarrel, args = new object[] { barrel } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UndeployExplosiveBarrel(barrel);
                    }
                }
            }
            public void UserUndeployExplosiveBarrel(int user, int barrel)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserUndeployExplosiveBarrel, args = new object[] { user, barrel } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserUndeployExplosiveBarrel(user, barrel);
                    }
                }
            }
            public void DeployExplosiveBarrel(int weapon, int barrel, int x, int y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.DeployExplosiveBarrel, args = new object[] { weapon, barrel, x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.DeployExplosiveBarrel(weapon, barrel, x, y);
                    }
                }
            }
            public void UserDeployExplosiveBarrel(int user, int weapon, int barrel, int x, int y)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.UserDeployExplosiveBarrel, args = new object[] { user, weapon, barrel, x, y } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.UserDeployExplosiveBarrel(user, weapon, barrel, x, y);
                    }
                }
            }
            public void AddKillScore(int killscore)
            {
                if (this.Send != null)
                {
                    Send(new SendArguments { i = Messages.AddKillScore, args = new object[] { killscore } });
                }
                if (this.VirtualTargets != null)
                {
                    foreach (var Target__ in this.VirtualTargets())
                    {
                        Target__.AddKillScore(killscore);
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
                    value.TeleportTo += this.UserTeleportTo;
                    value.WalkTo += this.UserWalkTo;
                    value.TakeBox += this.UserTakeBox;
                    value.FiredWeapon += this.UserFiredWeapon;
                    value.EnterMachineGun += this.UserEnterMachineGun;
                    value.ExitMachineGun += this.UserExitMachineGun;
                    value.StartMachineGun += this.UserStartMachineGun;
                    value.StopMachineGun += this.UserStopMachineGun;
                    value.AddDamage += this.UserAddDamage;
                    value.AddDamageFromDirection += this.UserAddDamageFromDirection;
                    value.ShowBulletsFlying += this.UserShowBulletsFlying;
                    value.PlayerResurrect += this.UserPlayerResurrect;
                    value.UndeployExplosiveBarrel += this.UserUndeployExplosiveBarrel;
                    value.DeployExplosiveBarrel += this.UserDeployExplosiveBarrel;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.TeleportTo -= this.UserTeleportTo;
                    value.WalkTo -= this.UserWalkTo;
                    value.TakeBox -= this.UserTakeBox;
                    value.FiredWeapon -= this.UserFiredWeapon;
                    value.EnterMachineGun -= this.UserEnterMachineGun;
                    value.ExitMachineGun -= this.UserExitMachineGun;
                    value.StartMachineGun -= this.UserStartMachineGun;
                    value.StopMachineGun -= this.UserStopMachineGun;
                    value.AddDamage -= this.UserAddDamage;
                    value.AddDamageFromDirection -= this.UserAddDamageFromDirection;
                    value.ShowBulletsFlying -= this.UserShowBulletsFlying;
                    value.PlayerResurrect -= this.UserPlayerResurrect;
                    value.UndeployExplosiveBarrel -= this.UserUndeployExplosiveBarrel;
                    value.DeployExplosiveBarrel -= this.UserDeployExplosiveBarrel;
                }
                #endregion

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
            #region WithUserArgumentsRouter_SinglecastView
            [Script]
            [CompilerGenerated]
            public sealed partial class WithUserArgumentsRouter_SinglecastView : WithUserArguments
            {
                public IMessages Target;
                #region Routing
                public void UserTeleportTo(int x, int y)
                {
                    this.Target.UserTeleportTo(this.user, x, y);
                }
                public void UserTeleportTo(UserTeleportToArguments e)
                {
                    this.Target.UserTeleportTo(this.user, e.x, e.y);
                }
                public void UserWalkTo(int x, int y)
                {
                    this.Target.UserWalkTo(this.user, x, y);
                }
                public void UserWalkTo(UserWalkToArguments e)
                {
                    this.Target.UserWalkTo(this.user, e.x, e.y);
                }
                public void UserTakeBox(int box)
                {
                    this.Target.UserTakeBox(this.user, box);
                }
                public void UserTakeBox(UserTakeBoxArguments e)
                {
                    this.Target.UserTakeBox(this.user, e.box);
                }
                public void UserFiredWeapon(int weapon)
                {
                    this.Target.UserFiredWeapon(this.user, weapon);
                }
                public void UserFiredWeapon(UserFiredWeaponArguments e)
                {
                    this.Target.UserFiredWeapon(this.user, e.weapon);
                }
                public void UserEnterMachineGun()
                {
                    this.Target.UserEnterMachineGun(this.user);
                }
                public void UserEnterMachineGun(UserEnterMachineGunArguments e)
                {
                    this.Target.UserEnterMachineGun(this.user);
                }
                public void UserExitMachineGun()
                {
                    this.Target.UserExitMachineGun(this.user);
                }
                public void UserExitMachineGun(UserExitMachineGunArguments e)
                {
                    this.Target.UserExitMachineGun(this.user);
                }
                public void UserStartMachineGun()
                {
                    this.Target.UserStartMachineGun(this.user);
                }
                public void UserStartMachineGun(UserStartMachineGunArguments e)
                {
                    this.Target.UserStartMachineGun(this.user);
                }
                public void UserStopMachineGun()
                {
                    this.Target.UserStopMachineGun(this.user);
                }
                public void UserStopMachineGun(UserStopMachineGunArguments e)
                {
                    this.Target.UserStopMachineGun(this.user);
                }
                public void UserAddDamage(int target, int damage)
                {
                    this.Target.UserAddDamage(this.user, target, damage);
                }
                public void UserAddDamage(UserAddDamageArguments e)
                {
                    this.Target.UserAddDamage(this.user, e.target, e.damage);
                }
                public void UserAddDamageFromDirection(int target, int damage, int arc)
                {
                    this.Target.UserAddDamageFromDirection(this.user, target, damage, arc);
                }
                public void UserAddDamageFromDirection(UserAddDamageFromDirectionArguments e)
                {
                    this.Target.UserAddDamageFromDirection(this.user, e.target, e.damage, e.arc);
                }
                public void UserShowBulletsFlying(int x, int y, int arc, int weaponType)
                {
                    this.Target.UserShowBulletsFlying(this.user, x, y, arc, weaponType);
                }
                public void UserShowBulletsFlying(UserShowBulletsFlyingArguments e)
                {
                    this.Target.UserShowBulletsFlying(this.user, e.x, e.y, e.arc, e.weaponType);
                }
                public void UserPlayerResurrect()
                {
                    this.Target.UserPlayerResurrect(this.user);
                }
                public void UserPlayerResurrect(UserPlayerResurrectArguments e)
                {
                    this.Target.UserPlayerResurrect(this.user);
                }
                public void UserUndeployExplosiveBarrel(int barrel)
                {
                    this.Target.UserUndeployExplosiveBarrel(this.user, barrel);
                }
                public void UserUndeployExplosiveBarrel(UserUndeployExplosiveBarrelArguments e)
                {
                    this.Target.UserUndeployExplosiveBarrel(this.user, e.barrel);
                }
                public void UserDeployExplosiveBarrel(int weapon, int barrel, int x, int y)
                {
                    this.Target.UserDeployExplosiveBarrel(this.user, weapon, barrel, x, y);
                }
                public void UserDeployExplosiveBarrel(UserDeployExplosiveBarrelArguments e)
                {
                    this.Target.UserDeployExplosiveBarrel(this.user, e.weapon, e.barrel, e.x, e.y);
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
                    value.UserTeleportTo += this.UserTeleportTo;
                    value.UserWalkTo += this.UserWalkTo;
                    value.UserTakeBox += this.UserTakeBox;
                    value.UserFiredWeapon += this.UserFiredWeapon;
                    value.UserEnterMachineGun += this.UserEnterMachineGun;
                    value.UserExitMachineGun += this.UserExitMachineGun;
                    value.UserStartMachineGun += this.UserStartMachineGun;
                    value.UserStopMachineGun += this.UserStopMachineGun;
                    value.UserAddDamage += this.UserAddDamage;
                    value.UserAddDamageFromDirection += this.UserAddDamageFromDirection;
                    value.UserShowBulletsFlying += this.UserShowBulletsFlying;
                    value.UserPlayerResurrect += this.UserPlayerResurrect;
                    value.UserUndeployExplosiveBarrel += this.UserUndeployExplosiveBarrel;
                    value.UserDeployExplosiveBarrel += this.UserDeployExplosiveBarrel;
                }

                public void RemoveDelegates(IEvents value)
                {
                    value.UserTeleportTo -= this.UserTeleportTo;
                    value.UserWalkTo -= this.UserWalkTo;
                    value.UserTakeBox -= this.UserTakeBox;
                    value.UserFiredWeapon -= this.UserFiredWeapon;
                    value.UserEnterMachineGun -= this.UserEnterMachineGun;
                    value.UserExitMachineGun -= this.UserExitMachineGun;
                    value.UserStartMachineGun -= this.UserStartMachineGun;
                    value.UserStopMachineGun -= this.UserStopMachineGun;
                    value.UserAddDamage -= this.UserAddDamage;
                    value.UserAddDamageFromDirection -= this.UserAddDamageFromDirection;
                    value.UserShowBulletsFlying -= this.UserShowBulletsFlying;
                    value.UserPlayerResurrect -= this.UserPlayerResurrect;
                    value.UserUndeployExplosiveBarrel -= this.UserUndeployExplosiveBarrel;
                    value.UserDeployExplosiveBarrel -= this.UserDeployExplosiveBarrel;
                }
                #endregion

                #region Routing
                public void UserTeleportTo(UserTeleportToArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserTeleportTo(this.user, e.x, e.y);
                }
                public void UserWalkTo(UserWalkToArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserWalkTo(this.user, e.x, e.y);
                }
                public void UserTakeBox(UserTakeBoxArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserTakeBox(this.user, e.box);
                }
                public void UserFiredWeapon(UserFiredWeaponArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserFiredWeapon(this.user, e.weapon);
                }
                public void UserEnterMachineGun(UserEnterMachineGunArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserEnterMachineGun(this.user);
                }
                public void UserExitMachineGun(UserExitMachineGunArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserExitMachineGun(this.user);
                }
                public void UserStartMachineGun(UserStartMachineGunArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserStartMachineGun(this.user);
                }
                public void UserStopMachineGun(UserStopMachineGunArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserStopMachineGun(this.user);
                }
                public void UserAddDamage(UserAddDamageArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserAddDamage(this.user, e.target, e.damage);
                }
                public void UserAddDamageFromDirection(UserAddDamageFromDirectionArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserAddDamageFromDirection(this.user, e.target, e.damage, e.arc);
                }
                public void UserShowBulletsFlying(UserShowBulletsFlyingArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserShowBulletsFlying(this.user, e.x, e.y, e.arc, e.weaponType);
                }
                public void UserPlayerResurrect(UserPlayerResurrectArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserPlayerResurrect(this.user);
                }
                public void UserUndeployExplosiveBarrel(UserUndeployExplosiveBarrelArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserUndeployExplosiveBarrel(this.user, e.barrel);
                }
                public void UserDeployExplosiveBarrel(UserDeployExplosiveBarrelArguments e)
                {
                    var _target = this.Target(e.user);
                    if (_target == null) return;
                    _target.UserDeployExplosiveBarrel(this.user, e.weapon, e.barrel, e.x, e.y);
                }
                #endregion
            }
            #endregion
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
            #region WalkToArguments
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            [Script]
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
            #region AddKillScoreArguments
            [Script]
            [CompilerGenerated]
            public sealed partial class AddKillScoreArguments
            {
                public int killscore;
                [DebuggerHidden]
                public override string ToString()
                {
                    return new StringBuilder().Append("{ killscore = ").Append(this.killscore).Append(" }").ToString();
                }
            }
            #endregion
            public event Action<AddKillScoreArguments> AddKillScore;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<IDispatchHelper>>
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
                            { Messages.AddKillScore, e => { AddKillScore(new AddKillScoreArguments { killscore = e.GetInt32(0) }); } },
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
                            { Messages.AddKillScore, e => AddKillScore },
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

            public event Action<RemoteEvents.WalkToArguments> WalkTo;
            void IMessages.WalkTo(int x, int y)
            {
                if(WalkTo == null) return;
                var v = new RemoteEvents.WalkToArguments { x = x, y = y };
                this.VirtualLatency(() => this.WalkTo(v));
            }

            public event Action<RemoteEvents.UserWalkToArguments> UserWalkTo;
            void IMessages.UserWalkTo(int user, int x, int y)
            {
                if(UserWalkTo == null) return;
                var v = new RemoteEvents.UserWalkToArguments { user = user, x = x, y = y };
                this.VirtualLatency(() => this.UserWalkTo(v));
            }

            public event Action<RemoteEvents.CancelServerRandomNumbersArguments> CancelServerRandomNumbers;
            void IMessages.CancelServerRandomNumbers()
            {
                if(CancelServerRandomNumbers == null) return;
                var v = new RemoteEvents.CancelServerRandomNumbersArguments {  };
                this.VirtualLatency(() => this.CancelServerRandomNumbers(v));
            }

            public event Action<RemoteEvents.ReadyForServerRandomNumbersArguments> ReadyForServerRandomNumbers;
            void IMessages.ReadyForServerRandomNumbers()
            {
                if(ReadyForServerRandomNumbers == null) return;
                var v = new RemoteEvents.ReadyForServerRandomNumbersArguments {  };
                this.VirtualLatency(() => this.ReadyForServerRandomNumbers(v));
            }

            public event Action<RemoteEvents.TakeBoxArguments> TakeBox;
            void IMessages.TakeBox(int box)
            {
                if(TakeBox == null) return;
                var v = new RemoteEvents.TakeBoxArguments { box = box };
                this.VirtualLatency(() => this.TakeBox(v));
            }

            public event Action<RemoteEvents.UserTakeBoxArguments> UserTakeBox;
            void IMessages.UserTakeBox(int user, int box)
            {
                if(UserTakeBox == null) return;
                var v = new RemoteEvents.UserTakeBoxArguments { user = user, box = box };
                this.VirtualLatency(() => this.UserTakeBox(v));
            }

            public event Action<RemoteEvents.FiredWeaponArguments> FiredWeapon;
            void IMessages.FiredWeapon(int weapon)
            {
                if(FiredWeapon == null) return;
                var v = new RemoteEvents.FiredWeaponArguments { weapon = weapon };
                this.VirtualLatency(() => this.FiredWeapon(v));
            }

            public event Action<RemoteEvents.UserFiredWeaponArguments> UserFiredWeapon;
            void IMessages.UserFiredWeapon(int user, int weapon)
            {
                if(UserFiredWeapon == null) return;
                var v = new RemoteEvents.UserFiredWeaponArguments { user = user, weapon = weapon };
                this.VirtualLatency(() => this.UserFiredWeapon(v));
            }

            public event Action<RemoteEvents.ServerRandomNumbersArguments> ServerRandomNumbers;
            void IMessages.ServerRandomNumbers(double[] e)
            {
                if(ServerRandomNumbers == null) return;
                var v = new RemoteEvents.ServerRandomNumbersArguments { e = e };
                this.VirtualLatency(() => this.ServerRandomNumbers(v));
            }

            public event Action<RemoteEvents.ServerMessageArguments> ServerMessage;
            void IMessages.ServerMessage(string text)
            {
                if(ServerMessage == null) return;
                var v = new RemoteEvents.ServerMessageArguments { text = text };
                this.VirtualLatency(() => this.ServerMessage(v));
            }

            public event Action<RemoteEvents.UserEnterMachineGunArguments> UserEnterMachineGun;
            void IMessages.UserEnterMachineGun(int user)
            {
                if(UserEnterMachineGun == null) return;
                var v = new RemoteEvents.UserEnterMachineGunArguments { user = user };
                this.VirtualLatency(() => this.UserEnterMachineGun(v));
            }

            public event Action<RemoteEvents.UserExitMachineGunArguments> UserExitMachineGun;
            void IMessages.UserExitMachineGun(int user)
            {
                if(UserExitMachineGun == null) return;
                var v = new RemoteEvents.UserExitMachineGunArguments { user = user };
                this.VirtualLatency(() => this.UserExitMachineGun(v));
            }

            public event Action<RemoteEvents.UserStartMachineGunArguments> UserStartMachineGun;
            void IMessages.UserStartMachineGun(int user)
            {
                if(UserStartMachineGun == null) return;
                var v = new RemoteEvents.UserStartMachineGunArguments { user = user };
                this.VirtualLatency(() => this.UserStartMachineGun(v));
            }

            public event Action<RemoteEvents.UserStopMachineGunArguments> UserStopMachineGun;
            void IMessages.UserStopMachineGun(int user)
            {
                if(UserStopMachineGun == null) return;
                var v = new RemoteEvents.UserStopMachineGunArguments { user = user };
                this.VirtualLatency(() => this.UserStopMachineGun(v));
            }

            public event Action<RemoteEvents.EnterMachineGunArguments> EnterMachineGun;
            void IMessages.EnterMachineGun()
            {
                if(EnterMachineGun == null) return;
                var v = new RemoteEvents.EnterMachineGunArguments {  };
                this.VirtualLatency(() => this.EnterMachineGun(v));
            }

            public event Action<RemoteEvents.ExitMachineGunArguments> ExitMachineGun;
            void IMessages.ExitMachineGun()
            {
                if(ExitMachineGun == null) return;
                var v = new RemoteEvents.ExitMachineGunArguments {  };
                this.VirtualLatency(() => this.ExitMachineGun(v));
            }

            public event Action<RemoteEvents.StartMachineGunArguments> StartMachineGun;
            void IMessages.StartMachineGun()
            {
                if(StartMachineGun == null) return;
                var v = new RemoteEvents.StartMachineGunArguments {  };
                this.VirtualLatency(() => this.StartMachineGun(v));
            }

            public event Action<RemoteEvents.StopMachineGunArguments> StopMachineGun;
            void IMessages.StopMachineGun()
            {
                if(StopMachineGun == null) return;
                var v = new RemoteEvents.StopMachineGunArguments {  };
                this.VirtualLatency(() => this.StopMachineGun(v));
            }

            public event Action<RemoteEvents.PingArguments> Ping;
            void IMessages.Ping()
            {
                if(Ping == null) return;
                var v = new RemoteEvents.PingArguments {  };
                this.VirtualLatency(() => this.Ping(v));
            }

            public event Action<RemoteEvents.AddDamageArguments> AddDamage;
            void IMessages.AddDamage(int target, int damage)
            {
                if(AddDamage == null) return;
                var v = new RemoteEvents.AddDamageArguments { target = target, damage = damage };
                this.VirtualLatency(() => this.AddDamage(v));
            }

            public event Action<RemoteEvents.UserAddDamageArguments> UserAddDamage;
            void IMessages.UserAddDamage(int user, int target, int damage)
            {
                if(UserAddDamage == null) return;
                var v = new RemoteEvents.UserAddDamageArguments { user = user, target = target, damage = damage };
                this.VirtualLatency(() => this.UserAddDamage(v));
            }

            public event Action<RemoteEvents.AddDamageFromDirectionArguments> AddDamageFromDirection;
            void IMessages.AddDamageFromDirection(int target, int damage, int arc)
            {
                if(AddDamageFromDirection == null) return;
                var v = new RemoteEvents.AddDamageFromDirectionArguments { target = target, damage = damage, arc = arc };
                this.VirtualLatency(() => this.AddDamageFromDirection(v));
            }

            public event Action<RemoteEvents.UserAddDamageFromDirectionArguments> UserAddDamageFromDirection;
            void IMessages.UserAddDamageFromDirection(int user, int target, int damage, int arc)
            {
                if(UserAddDamageFromDirection == null) return;
                var v = new RemoteEvents.UserAddDamageFromDirectionArguments { user = user, target = target, damage = damage, arc = arc };
                this.VirtualLatency(() => this.UserAddDamageFromDirection(v));
            }

            public event Action<RemoteEvents.ShowBulletsFlyingArguments> ShowBulletsFlying;
            void IMessages.ShowBulletsFlying(int x, int y, int arc, int weaponType)
            {
                if(ShowBulletsFlying == null) return;
                var v = new RemoteEvents.ShowBulletsFlyingArguments { x = x, y = y, arc = arc, weaponType = weaponType };
                this.VirtualLatency(() => this.ShowBulletsFlying(v));
            }

            public event Action<RemoteEvents.UserShowBulletsFlyingArguments> UserShowBulletsFlying;
            void IMessages.UserShowBulletsFlying(int user, int x, int y, int arc, int weaponType)
            {
                if(UserShowBulletsFlying == null) return;
                var v = new RemoteEvents.UserShowBulletsFlyingArguments { user = user, x = x, y = y, arc = arc, weaponType = weaponType };
                this.VirtualLatency(() => this.UserShowBulletsFlying(v));
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
            void IMessages.PlayerAdvertise(int ego)
            {
                if(PlayerAdvertise == null) return;
                var v = new RemoteEvents.PlayerAdvertiseArguments { ego = ego };
                this.VirtualLatency(() => this.PlayerAdvertise(v));
            }

            public event Action<RemoteEvents.PlayerResurrectArguments> PlayerResurrect;
            void IMessages.PlayerResurrect()
            {
                if(PlayerResurrect == null) return;
                var v = new RemoteEvents.PlayerResurrectArguments {  };
                this.VirtualLatency(() => this.PlayerResurrect(v));
            }

            public event Action<RemoteEvents.UserPlayerResurrectArguments> UserPlayerResurrect;
            void IMessages.UserPlayerResurrect(int user)
            {
                if(UserPlayerResurrect == null) return;
                var v = new RemoteEvents.UserPlayerResurrectArguments { user = user };
                this.VirtualLatency(() => this.UserPlayerResurrect(v));
            }

            public event Action<RemoteEvents.ServerPlayerAdvertiseArguments> ServerPlayerAdvertise;
            void IMessages.ServerPlayerAdvertise(int user, string name, int ego)
            {
                if(ServerPlayerAdvertise == null) return;
                var v = new RemoteEvents.ServerPlayerAdvertiseArguments { user = user, name = name, ego = ego };
                this.VirtualLatency(() => this.ServerPlayerAdvertise(v));
            }

            public event Action<RemoteEvents.UndeployExplosiveBarrelArguments> UndeployExplosiveBarrel;
            void IMessages.UndeployExplosiveBarrel(int barrel)
            {
                if(UndeployExplosiveBarrel == null) return;
                var v = new RemoteEvents.UndeployExplosiveBarrelArguments { barrel = barrel };
                this.VirtualLatency(() => this.UndeployExplosiveBarrel(v));
            }

            public event Action<RemoteEvents.UserUndeployExplosiveBarrelArguments> UserUndeployExplosiveBarrel;
            void IMessages.UserUndeployExplosiveBarrel(int user, int barrel)
            {
                if(UserUndeployExplosiveBarrel == null) return;
                var v = new RemoteEvents.UserUndeployExplosiveBarrelArguments { user = user, barrel = barrel };
                this.VirtualLatency(() => this.UserUndeployExplosiveBarrel(v));
            }

            public event Action<RemoteEvents.DeployExplosiveBarrelArguments> DeployExplosiveBarrel;
            void IMessages.DeployExplosiveBarrel(int weapon, int barrel, int x, int y)
            {
                if(DeployExplosiveBarrel == null) return;
                var v = new RemoteEvents.DeployExplosiveBarrelArguments { weapon = weapon, barrel = barrel, x = x, y = y };
                this.VirtualLatency(() => this.DeployExplosiveBarrel(v));
            }

            public event Action<RemoteEvents.UserDeployExplosiveBarrelArguments> UserDeployExplosiveBarrel;
            void IMessages.UserDeployExplosiveBarrel(int user, int weapon, int barrel, int x, int y)
            {
                if(UserDeployExplosiveBarrel == null) return;
                var v = new RemoteEvents.UserDeployExplosiveBarrelArguments { user = user, weapon = weapon, barrel = barrel, x = x, y = y };
                this.VirtualLatency(() => this.UserDeployExplosiveBarrel(v));
            }

            public event Action<RemoteEvents.AddKillScoreArguments> AddKillScore;
            void IMessages.AddKillScore(int killscore)
            {
                if(AddKillScore == null) return;
                var v = new RemoteEvents.AddKillScoreArguments { killscore = killscore };
                this.VirtualLatency(() => this.AddKillScore(v));
            }

        }
        #endregion
    }
    #endregion
}
// 25.09.2008 15:30:21
