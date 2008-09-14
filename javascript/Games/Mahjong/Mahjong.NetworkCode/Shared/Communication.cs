using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Nonoba;

namespace Mahjong.NetworkCode.Shared
{
	public partial class Communication
	{
		// members defined over here can be used on client and on server
		// x


		// A pattern like this gets special treatment:
		// void User...(int user, ...)


		/// <summary>
		/// this interface is to be used in a generator
		/// </summary>
		public partial interface IMessages
		{

			// this will generate lots of overkill boilerplate code :)


			void ServerPlayerHandshake(int[] version);

			void ServerPlayerHello(int user, string name);
			void ServerPlayerJoined(int user, string name);
			void ServerPlayerLeft(int user, string name);

			void PlayerAdvertise(string name);
			void UserPlayerAdvertise(int user, string name);


			void MouseMove(int x, int y, int color);
			void UserMouseMove(int user, int x, int y, int color);


			void MouseOut(int color);
			void UserMouseOut(int user, int color);

			void VectorChanged(int x, int y);
			void UserVectorChanged(int user, int x, int y);

			void FireBullet(int starship, int multiplier, int from_x, int from_y, int to_x, int to_y, int limit);
			void UserFireBullet(int user, int starship, int multiplier, int from_x, int from_y, int to_x, int to_y, int limit);

			void AddDamage(int target, double damage, int shooter);
			void UserAddDamage(int user, int target, double damage, int shooter);

			void RestoreStarship(int starship);
			void UserRestoreStarship(int user, int starship);

			void TeleportTo(int x, int y);
			void UserTeleportTo(int user, int x, int y);

			void EatApple(int x, int y);
			void UserEatApple(int user, int x, int y);

			void EatThisWormBegin(int food);
			void UserEatThisWormBegin(int user, int food);

			void EatThisWormEnd(int food);
			void UserEatThisWormEnd(int user, int food);

			void LevelHasEnded();
			void UserLevelHasEnded(int user);

			void ServerSendMap();

			void SendMap(int[] buttons);
			void UserSendMap(int user, int[] buttons);

			void SendMapLater();
			void UserSendMapLater(int user);

			void SetFlag(int button, int value);
			void UserSetFlag(int user, int button, int value);

			void Reveal(int button);
			void UserReveal(int user, int button);

			// registered nonoba rankings
			void AddScore(int score);

			void KillAllInvaders();
			void UserKillAllInvaders(int user);

			void AwardAchievementFirst();
			void AwardAchievementFiver();
			void AwardAchievementUFOKill();
			void AwardAchievementMaxGun();

			void SendPassword(string password);
			void ServerPasswordStatus(int status);


			void LockGame();
			void UnlockGame();

		}


		partial class RemoteEvents : IEventsDispatch
		{
			public void EmptyHandler<T>(T Arguments)
			{
			}

			bool IEventsDispatch.DispatchInt32(int e, IDispatchHelper h)
			{
				return Dispatch((Messages)e, h);
			}

			partial class DispatchHelper : IDispatchHelper
			{
				public Converter<object, int> GetLength { get; set; }

				public DispatchHelper()
				{
					this.GetDoubleArray =
						delegate
						{
							var a = new double[GetLength(null)];

							for (var i = 0; i < a.Length; i++)
								a[i] = this.GetDouble((uint)i);

							return a;
						};

					this.GetInt32Array =
						  delegate
						  {
							  var a = new int[GetLength(null)];

							  for (var i = 0; i < a.Length; i++)
								  a[i] = this.GetInt32((uint)i);

							  return a;
						  };

					this.GetStringArray =
						  delegate
						  {
							  var a = new string[GetLength(null)];

							  for (var i = 0; i < a.Length; i++)
								  a[i] = this.GetString((uint)i);

							  return a;
						  };
				}
			}
		}


	}
}
