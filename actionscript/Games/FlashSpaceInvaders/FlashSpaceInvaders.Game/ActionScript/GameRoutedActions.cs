using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.ActionScript.FragileEntities;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public partial interface IGameRoutedActions
	{
		RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage { get; }
		RoutedActionInfo<StarShip, Point> AddEnemy { get; }


		RoutedActionInfo<PlayerShip, Point> DoPlayerMovement { get; }
		RoutedActionInfo<PlayerShip, int> SetWeaponMultiplier { get; }

		RoutedActionInfo<string> SendTextMessage { get; }

		RoutedActionInfo<int, Action<PlayerShip>> CreateCoPlayer { get; }
		RoutedActionInfo<PlayerShip, Point> MoveCoPlayer { get; }

		RoutedActionInfo<StarShip, int, Point, Point, double, Action<BulletInfo>> FireBullet { get; }
	}

	partial class Game : IGameRoutedActions
	{
		void InitializeRoutedActions()
		{
			this.AddDamage = "AddDamage";
			this.AddEnemy = "AddEnemy";
			this.DoPlayerMovement = "DoPlayerMovement";
			this.SetWeaponMultiplier = "SetWeaponMultiplier";
			this.SendTextMessage = "SendTextMessage";

			this.CreateCoPlayer = "CreateCoPlayer";
			this.MoveCoPlayer = "MoveCoPlayer";

			this.FireBullet = "FireBullet";
		}

		#region routed events

		// gee... yet again.. mxmlc cannot handle properties with different modifiers

		public RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage { get; set; }
		public RoutedActionInfo<StarShip, Point> AddEnemy { get; set; }
		public RoutedActionInfo<PlayerShip, Point> DoPlayerMovement { get; set; }
		public RoutedActionInfo<PlayerShip, int> SetWeaponMultiplier { get; set; }
		public RoutedActionInfo<string> SendTextMessage { get; set; }

		public RoutedActionInfo<int, Action<PlayerShip>> CreateCoPlayer { get; set; }
		public RoutedActionInfo<PlayerShip, Point> MoveCoPlayer { get; set; }

		public RoutedActionInfo<StarShip, int, Point, Point, double, Action<BulletInfo>> FireBullet { get; set; }

		#endregion
	}
}
