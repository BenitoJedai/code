using flare.loaders;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flare3D.AssetsLibraryTemplates.Flare3DAssets
{
    //20141103
    // how many times have we templated so far?

    // notice Flare3DAssets is the suffix namespace we want to keep 
    // Flare3DLoaderAssetTemplate will be replaced by the file name
    // namespace suffix AssetsLibraryTemplates will be used to detect templates and will be removed
    public class Flare3DLoaderAssetTemplate : Flare3DLoader
    {
        // first we shall support only file name matching, later we could enable build time conversion 
        // here we actually define the pattern which shall be replaced to assets/Flare3DWaterShipComponent/ship.zf3d
        public const string AssetPath = "*.zf3d";

        public Flare3DLoaderAssetTemplate()
            : base(KnownEmbeddedResources.Default[AssetPath])
        {
            this.load();
        }
    }
}
