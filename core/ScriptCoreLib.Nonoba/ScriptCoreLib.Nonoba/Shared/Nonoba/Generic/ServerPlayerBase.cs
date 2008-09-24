using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.Nonoba.Generic
{
	[Script]
	public abstract class ServerPlayerBase<RemoteEvents, RemoteMessages>
	{

		public RemoteEvents FromPlayer;


		public IEventsDispatch FromPlayerDispatch;

		public RemoteMessages ToPlayer;
		public RemoteMessages ToOthers;

		/// <summary>
		/// Adds score to ranking list as a delta
		/// </summary>
		public Action<string, int> AddScore;
		public Action<string, int> SetScore;

		/// <summary>
		/// Adds score to highscore list as absolute value
		/// </summary>
		public Action<string, int> AddHighscore;

		// http://nonoba.com/developers/documentation/multiplayerapi/classnonobagameuserserverside#server.nonobagameuser.awardachievement
		public Func<string, uint> AwardAchievement;

		public int UserId;
		public string Username;

		public ServerPlayerBase()
		{
			AddScore = EmptyAddScore;
			SetScore = EmptySetScore;

		}

		#region empty
		public void EmptyAddScore(string key, int value)
		{
		}


		public void EmptySetScore(string key, int value)
		{
		}
		#endregion
	}
}
