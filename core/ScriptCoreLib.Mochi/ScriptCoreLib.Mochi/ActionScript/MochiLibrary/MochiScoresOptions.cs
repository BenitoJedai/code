using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.MochiLibrary.Ad;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.MochiLibrary
{
    [Script]
    public sealed class MochiScoresOptions
    {
        readonly DynamicContainer Data;

		public MochiScoresOptions()
        {
            Data = new DynamicContainer { Subject = new object() };

        }

		public string boardID 
		{
			set { Data["boardID"] = value; }
		}

		public int score
		{
			set { Data["score"] = value; }
		}

       
		public void showLeaderboard()
        {

			MochiScores.showLeaderboard(Data.Subject);
        }
    }


}
