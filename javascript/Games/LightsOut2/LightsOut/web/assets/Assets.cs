using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;

[assembly: ScriptResources(LightsOut.web.assets.LocalAsset.LocalRelativePath)]


namespace LightsOut.web.assets
{
    [Script]
    public class Asset
    {
        public string RelativePath { get; set;  }

        public string FileName { get; set; }

        public static implicit operator Asset(string FileName)
        {
            return new Asset { FileName = FileName };
        }

        public string FullName
        {
            get
            {
                return RelativePath + "/" + this.FileName;
            }
        }

        public string StyleSheetURL
        {
            get
            {
                return "url(" + FullName + ")";
            }
        }

        IHTMLImage _AsImage;

        public IHTMLImage AsImage()
        {
            if (_AsImage == null)
                _AsImage = new IHTMLImage(this.FullName);

            return _AsImage;
        }
    }

    [Script]
    public class LocalAsset : Asset
    {
        /// <summary>
        /// all Embedded Resources at web at this relative path are extracted on jsc build
        /// </summary>
        public const string LocalRelativePath = "assets/LightsOut";


        public static implicit operator LocalAsset(string FileName)
        {
            return new LocalAsset { RelativePath = LocalRelativePath, FileName = FileName };
        }
    }

    [Script]
    public class Assets
    {
        public LocalAsset Background = "background.png";
        public LocalAsset LogoOff = "vistaLogoOff.png";
        public LocalAsset LogoOn = "vistaLogoOn.png";

        static Assets _Default;

        public static Assets Default
        {
            get
            {
                if (_Default == null)
                    _Default = new Assets();

                return _Default;
            }
        }
    }
}
