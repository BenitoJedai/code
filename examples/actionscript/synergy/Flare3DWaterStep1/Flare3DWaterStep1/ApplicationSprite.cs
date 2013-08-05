using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace Flare3DWaterStep1
{
    //[Flare3D.SWCReferenceImportAttribute.Flare3D_2_5_18_Trial]
    //[Flare3D.SWCReferenceImportAttribute]
    public sealed class ApplicationSprite : Step01_BasicSetup
    {
        // VerifyError: Error #1014: Class flare.basic::Scene3D could not be found.

        //flare.basic.Scene3D ref0;

        //C:\util\flex_sdk_4.6\bin\mxmlc.exe
        // -static-link-runtime-shared-libraries=true  +configname=airmobile   -debug -verbose-stacktraces -sp=. -swf-version=15 --target-player=11.2.0  -locale en_US -strict -output="R:\web\Flare3DWaterStep1.ApplicationSprite.swf" -library-path+="R:\web\assets\Flare3D\Flare3D_2.5.18_Trial.swc" -library-path+="R:\web\assets\Flare3DWaterStep1\Flare3DWaterStep1.AssetsLibrary.swc" Flare3DWaterStep1\ApplicationSprite.as
        //Loading configuration file C:\util\flex_sdk_4.6\frameworks\airmobile-config.xml
        //R:\web\Flare3DWaterStep1.ApplicationSprite.swf (1678498 bytes)
        //C:\util\flex_sdk_4.6\runtimes\player\11.1\win\FlashPlayerDebugger.exe
        //looking at BCLImplementationMergeAssemblies...
        //looking at BCLImplementationMergeAssemblies... done


        //    SecurityError: Error #2148: SWF file file:///X|/jsc.svn/examples/actionscript/synergy/Flare3DWaterStep1/Flare3DWaterStep1/bin/Debug/staging/Flare3DWaterStep1.ApplicationSprite/web/Flare3DWaterStep1.ApplicationSprite.swf cannot access 
        // local resource file:///X|/jsc.svn/examples/actionscript/synergy/Flare3DWaterStep1/Flare3DWaterStep1/bin/Debug/staging/Flare3DWaterStep1.ApplicationSprite/web/assets/ship.zf3d. Only local-with-filesystem and trusted local SWF files may access local resources.
        //at flash.net::URLStream/load()
        //at flare.loaders::Flare3DLoader/load()[Z:\projects\flare3d 2.5\src\flare\loaders\Flare3DLoader.as:100]
        //at Step01_BasicSetup()[T:\opensource\src\Step01_BasicSetup.as:33]
        //at Flare3DWaterStep1::ApplicationSprite()[R:\web\Flare3DWaterStep1\ApplicationSprite.as:23]


        // Error	2	The type 'flare.Flare3D' exists in both 'X:\jsc.svn\examples\actionscript\synergy\Flare3DWaterStep1\packages\Flare3D.1.0.0.0\lib\Flare3D.dll' and 'X:\jsc.svn\examples\actionscript\synergy\Flare3DWaterStep1\Flare3DWaterStep1\bin\staging.AssetsLibrary\Flare3DWaterStep1.AssetsLibrary.dll'	X:\jsc.svn\examples\actionscript\synergy\Flare3DWaterStep1\Flare3DWaterStep1\ApplicationSprite.cs	9	15	Flare3DWaterStep1
        //flare.Flare3D ref0;

        public ApplicationSprite()
        {
        }

    }
}
