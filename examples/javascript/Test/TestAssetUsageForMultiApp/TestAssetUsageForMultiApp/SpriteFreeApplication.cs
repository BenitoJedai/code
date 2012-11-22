using TestAssetUsageForMultiApp.HTML.Pages;
using ScriptCoreLib.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAssetUsageForMultiApp
{
    sealed class SpriteFreeApplication
    {
        public SpriteFreeApplication(ISpriteFreeApplication foo)
        {
            Native.Window.alert("hey");


        }
    }

  
    public sealed partial class ApplicationWebService
    {
       
    }


}
