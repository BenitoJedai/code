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
	public class GameRoutedActions
	{
		internal RoutedActionInfo<StarShip, Point> AddEnemy = "AddEnemy";


		// public RoutedActionInfo<IFragileEntity, BulletInfo> AddDamage = "AddDamage";
		public RoutedActionInfo<IFragileEntity, double, StarShip> AddDamage = "AddDamage";

		public RoutedActionInfo<PlayerShip, Point> DoPlayerMovement = "DoPlayerMovement";
		public RoutedActionInfo<PlayerShip, int> SetWeaponMultiplier = "SetWeaponMultiplier";
		public RoutedActionInfo<string> SendTextMessage = "SendTextMessage";

		public RoutedActionInfo<int, Action<PlayerShip>> CreateCoPlayer = "CreateCoPlayer";
		public RoutedActionInfo<int> RemoveCoPlayer = "RemoveCoPlayer";
		public RoutedActionInfo<PlayerShip, Point> MoveCoPlayer = "MoveCoPlayer";

		public RoutedActionInfo<StarShip, int, Point, Point, double, Action<BulletInfo>> FireBullet = "FireBullet";
		public RoutedActionInfo<StarShip> RestoreStarship = "RestoreStarship";


		public RoutedActionInfo<int> AddRankingScore = "AddRankingScore";
		public RoutedActionInfo AddAchivementFiver = "AddAchivementFiver";
		public RoutedActionInfo AddAchivementUFO = "AddAchivementUFO";
		public RoutedActionInfo AddAchivementMaxGun = "AddAchivementMaxGun";

	}

	partial class Game 
	{
		void InitializeRoutedActions()
		{

		}

		public readonly GameRoutedActions RoutedActions = new GameRoutedActions();
	}
}
