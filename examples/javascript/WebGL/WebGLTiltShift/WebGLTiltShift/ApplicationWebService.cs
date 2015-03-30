using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace WebGLTiltShift
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // jsc java does not yet like roslyn 2015

        //0001 02000178 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProvider
        //script: error JSC1000: Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
        //internal compiler error at method
        // assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
        // type: ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
        // method: CreatePaddedBuffer
        // Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer


        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

		static ApplicationWebService()
		{
			// X:\jsc.svn\examples\javascript\WebGL\WebGLVRHZTeaser\WebGLVRHZTeaser\ApplicationWebService.cs
			// X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

			// http://stackoverflow.com/questions/19750700/detecting-when-system-buttons-are-visible-while-using-immersive-mode
			// https://developer.android.com/training/system-ui/immersive.html
			// http://stackoverflow.com/questions/22265945/full-screen-action-bar-immersive

			(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as ScriptCoreLib.Android.CoreAndroidWebServiceActivity).With(
				activity =>
				{
					activity.AtResume += delegate
					{
						Console.WriteLine("set SYSTEM_UI_FLAG_IMMERSIVE_STICKY");

						//activity.get
						// Set the IMMERSIVE flag.
						// Set the content to appear under the system bars so that the content
						// doesn't resize when the system bars hide and show.
						activity.getWindow().getDecorView().setSystemUiVisibility(
							android.view.View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
								| android.view.View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
								| android.view.View.SYSTEM_UI_FLAG_FULLSCREEN
								| android.view.View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
					};

				}
			);

		}
	}
}
