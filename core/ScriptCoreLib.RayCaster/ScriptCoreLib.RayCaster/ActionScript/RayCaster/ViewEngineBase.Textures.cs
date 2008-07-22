using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.RayCaster.Extensions;
using ScriptCoreLib.ActionScript.Extensions;

namespace ScriptCoreLib.ActionScript.RayCaster
{
    partial class ViewEngineBase
    {
        protected const int texWidth = Texture64.SizeConstant;
        protected const int texHeight = Texture64.SizeConstant;

        protected Texture64[] _textures;

        public Texture64[] Textures
        {
            get { return _textures; }
            set { _textures = value; }
        }

    }
}
