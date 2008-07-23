using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.RayCaster
{

    [Script]
    public sealed class MapInfo
    {
        public event Action WallMapChanged;

        Texture32 _WallMap;

        /// <summary>
        /// This property defines where are the walls. 0 is for opening, otherwise the value is the index for texture
        /// </summary>
        public Texture32 WallMap
        {
            get
            {
                return _WallMap;
            }
            set
            {
                _WallMap = value;

                if (WallMapChanged != null)
                    WallMapChanged();
            }
        }

        Texture32 _Map;

        public Texture32 Map
        {
            get
            {
                return _Map;
            }
            set
            {
                _Map = value;
            }
        }
    }

}
