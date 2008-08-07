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
	public interface IGameRoutedActions
	{
		RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage { get; }
		RoutedActionInfo<StarShip, Point> AddEnemy { get; }
		RoutedActionInfo<BulletInfo> AddBullet { get; }
		RoutedActionInfo<PlayerShip, Point> DoPlayerMovement { get; }
		RoutedActionInfo<PlayerShip, int> SetWeaponMultiplier { get; }

		RoutedActionInfo<string> SendTextMessage { get; }
	}

	partial class Game : IGameRoutedActions
	{
		void InitializeRoutedActions()
		{
			this.AddDamage = "AddDamage";
			this.AddEnemy = "AddEnemy";
			this.AddBullet = "AddBullet";
			this.DoPlayerMovement = "DoPlayerMovement";
			this.SetWeaponMultiplier = "SetWeaponMultiplier";
			this.SendTextMessage = "SendTextMessage";
		}

		#region routed events

		// gee... yet again.. mxmlc cannot handle properties with different modifiers

		public RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage { get; set; }
		public RoutedActionInfo<StarShip, Point> AddEnemy { get; set; }
		public RoutedActionInfo<BulletInfo> AddBullet { get; set; }
		public RoutedActionInfo<PlayerShip, Point> DoPlayerMovement { get; set; }
		public RoutedActionInfo<PlayerShip, int> SetWeaponMultiplier { get; set; }
		public RoutedActionInfo<string> SendTextMessage { get; set; }

		#endregion
	}
}
