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

        public Action<string, int> AddScore;

        // http://nonoba.com/developers/documentation/multiplayerapi/classnonobagameuserserverside#server.nonobagameuser.awardachievement
        public Func<string, uint> AwardAchievement;

        public int UserId;
        public string Username;
    }
}
