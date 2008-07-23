using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.RayCaster
{

    [Script]
    public sealed class MapInfo
    {
        public event Action Changed;

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

            }
        }

        Texture32 _Map;

        public Texture32 WorldMap
        {
            get
            {
                return _Map;
            }
            set
            {
                _Map = value;

                Update();
            }
        }

        Dictionary<uint, Texture64> _Textures;

        public Dictionary<uint, Texture64> Textures
        {
            get
            {
                return _Textures;
            }
            set
            {
                _Textures = value;

                Update();
            }
        }

        /// <summary>
        /// Generates the WallMap based on Map and Mappings
        /// </summary>
        public void Update()
        {
            if (_Map == null)
                return;

            if (_Textures == null)
                return;

            var w = new Texture32();
            var k = _Textures.Keys.ToList();

            for (int i = 0; i < Texture32.SizeConstant; i++)
                for (int j = 0; j < Texture32.SizeConstant; j++)
                {
                    var c = _Map[i, j];

                    if (k.Contains(c))
                        c = (uint)( k.IndexOf(c) + 1);
                    else
                        c = 0;

                    w[i, j] = c;
                }

            this.WallMap = w;

            if (Changed != null)
                Changed();
        }
    }

}
