using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestAssetAsComponent.Design
{
    public partial class VJLogoComponent : ImageLinkComponent
    {

        public VJLogoComponent()
        {
            this.InitializeComponent();

            this.img = new HTML.Images.FromAssets.VJLogo();
        }


    }
}
