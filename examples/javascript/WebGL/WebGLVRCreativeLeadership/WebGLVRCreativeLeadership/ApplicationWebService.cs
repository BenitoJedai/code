using android.view;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// Your APK's package name must be in the following format "com.example.myapp". 
// It may contain letters (a-z), numbers, and underscores (_). It must start with a lowercase character. It must be 150 characters or fewer.

// <manifest package="WebGLVRCreativeLeadership.Activities"

//namespace WebGLVRCreativeLeadership
namespace com.abstractatech.vr
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class WebGLVRCreativeLeadershipApplicationWebService
    {

		static WebGLVRCreativeLeadershipApplicationWebService()
		{
			// X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\ApplicationWebService.cs

			// http://stackoverflow.com/questions/19750700/detecting-when-system-buttons-are-visible-while-using-immersive-mode
			// https://developer.android.com/training/system-ui/immersive.html
			// http://stackoverflow.com/questions/22265945/full-screen-action-bar-immersive

			(ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext as ScriptCoreLib.Android.CoreAndroidWebServiceActivity).With(
				activity =>
				{
					activity.AtResume +=
					delegate
					{
						Console.WriteLine("set SYSTEM_UI_FLAG_IMMERSIVE_STICKY");

						//activity.get
						// Set the IMMERSIVE flag.
						// Set the content to appear under the system bars so that the content
						// doesn't resize when the system bars hide and show.
						activity.getWindow().getDecorView().setSystemUiVisibility(
							View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
								| View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
								| View.SYSTEM_UI_FLAG_FULLSCREEN
								| View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
					};

				}
			);



		}
	}
}
