#define SOUND
#define moredetails
#define moredetail


using Abstractatech.ActionScript.Audio;
using Box2D.Collision;
using Box2D.Common.Math;
using Box2D.Dynamics;
using Box2D.Dynamics.Contacts;
using FlashHeatZeekerWithStarlingT04.ActionScript.Images;
using FlashHeatZeekerWithStarlingT04.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using ScriptCoreLib.Shared.Lambda;
using starling.core;
using starling.display;
using starling.filters;
using starling.text;
using starling.textures;
using starling.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FlashHeatZeekerWithStarlingT04
{

    public interface IRemoteGame
    {
        event Action<string> AtTitleChange;
        event Action<string> AtWriteLine;
    }

    public class RemoteGame : IRemoteGame
    {
        public Game Context;

        public int networkframe_dx
        {
            get
            {
                return Context.ego_remotegame_networkframe - this.networkframe + this.initial_networkframe_delta;
            }
        }

        public int rx_messagecount;

        public int networkid { get; set; }
        public int networkframe { get; set; }

        public int initial_networkframe_delta { get; set; }

        public event Action<string> AtTitleChange;
        public void RaiseTitleChange(string e)
        {
            if (AtTitleChange != null)
                AtTitleChange(e);
        }

        public event Action<string> AtWriteLine;
        public void RaiseWriteLine(string e)
        {
            if (AtWriteLine != null)
                AtWriteLine(e);
        }
    }

}
