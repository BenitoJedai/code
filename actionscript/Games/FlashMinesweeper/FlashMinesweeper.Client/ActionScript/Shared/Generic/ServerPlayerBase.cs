﻿using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashMinesweeper.ActionScript.Shared.Generic
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


        public int UserId;
        public string Username;
    }
}
