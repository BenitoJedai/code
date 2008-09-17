﻿using System;
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

			void ServerPlayerHello(int user, string name, int others);
			void ServerPlayerJoined(int user, string name);
			void ServerPlayerLeft(int user, string name);

			void UserPlayerAdvertise(int user, string name);

			void UserMapRequest(int user);
			void UserMapResponse(int user, int[] bytes);


			void MouseMove(int x, int y);
			void UserMouseMove(int user, int x, int y);


			void MouseOut(int color);
			void UserMouseOut(int user, int color);


			void LevelHasEnded();
			void UserLevelHasEnded(int user);

		

			void SetFlag(int button, int value);
			void UserSetFlag(int user, int button, int value);

			void Reveal(int button);
			void UserReveal(int user, int button);

			// registered nonoba rankings
			void AddScore(int score);

		
			void AwardAchievementFirst();
		

			void SendPassword(string password);
			void ServerPasswordStatus(int status);


			void LockGame();
			void UnlockGame();

			// some user to user messages

			void UserSayLine(int user, string text);

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
						offset =>
						{
							int offseti = (int)offset;
							int len = GetLength(null) - offseti;

							var a = new double[len];

							for (var i = 0; i < a.Length; i++)
							{
								uint ii = (uint)i;
								uint j = ii + offset;

								a[i] = this.GetDouble(j);
							}

							return a;
						};

					this.GetInt32Array =
						offset =>
						{
							int offseti = (int)offset;
							int len = GetLength(null) - offseti;
							var a = new int[len];

							for (var i = 0; i < a.Length; i++)
							{
								uint ii = (uint)i;
								uint j = ii + offset;

								a[i] = this.GetInt32(j);
							}

							return a;
						};

					this.GetStringArray =
						offset =>
						{
							int offseti = (int)offset;
							int len = GetLength(null) - offseti;
							var a = new string[len];

							for (var i = 0; i < a.Length; i++)
							{
								uint ii = (uint)i;
								uint j = ii + offset;

								a[i] = this.GetString(j);
							}

							return a;
						};
				}
			}
		}


	}
}
