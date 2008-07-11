using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace LightsOut.ActionScript.Shared.Generic
{
#if !NoAttributes
    [Script]
#endif
    public abstract class ServerPlayerBase<RemoteEvents, RemoteMessages>
    {

        public RemoteEvents FromPlayer;


        public IEventsDispatch FromPlayerDispatch;

        public RemoteMessages ToPlayer;
        public RemoteMessages ToOthers;

        public Action<string, int> AddScore;

        // http://nonoba.com/developers/documentation/multiplayerapi/classnonobagameuserserverside#server.nonobagameuser.awardachievement
        public Func<string, uint> AwardAchievement;

        public int UserId;
        public string Username;
    }
}
