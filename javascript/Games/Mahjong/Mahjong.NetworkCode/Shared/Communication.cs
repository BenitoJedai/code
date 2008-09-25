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



			void ServerPlayerHello(int user, string name, int others, int navbar, int vote, int layoutinput, int hints, int[] handshake);
			void ServerPlayerJoined(int user, string name);
			void ServerPlayerLeft(int user, string name);

			void UserPlayerAdvertise(int user, string name);

			void UserMapRequest(int user);
			void UserMapResponse(int user, int[] bytes);


			void MapReload(int[] bytes);
			void UserMapReload(int user, int[] bytes);


			void MouseMove(int x, int y);
			void UserMouseMove(int user, int x, int y);


			void MouseOut(int color);
			void UserMouseOut(int user, int color);


			void LevelHasEnded();
			void UserLevelHasEnded(int user);

		

		
			// registered nonoba rankings
			void AddScore(int score);

			void AwardAchievementLayoutCompleted();

		

			void LockGame();
			void UnlockGame();

			// some user to user messages

			void UserSayLine(int user, string text);

			// lock management
			void UserLockEnter(int user, int id);
			void UserLockValidate(int user, int id);
			void UserLockExit(int user, int id);

			// locked events
			void RemovePair(int a, int b);
			void UserRemovePair(int user, int a, int b);

			void GoBack();
			void UserGoBack(int user);

			void GoForward();
			void UserGoForward(int user);

			#region voteing
			void VoteRequest(string text);
			void UserVoteRequest(int user, string text);

			void UserVoteResponse(int user, int value);

		
			void VoteAbort();
			void UserVoteAbort(int user);
			#endregion
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
					new DefaultImplementationForIDispatchHelper(this);
				}
			}
		}


	}
}
