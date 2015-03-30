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

namespace WebGLVRHZTeaser
{
	/// <summary>
	/// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public class ApplicationWebService
	{
		// redux needs 2012 build for java
		// integrate as GearVr / Google Cardboard


		//        F/libc(27439): Fatal signal 11 (SIGSEGV), code 1, fault addr 0x8 in tid 27484 (Thread-280)
		//I/libc(27439): Suppressing debuggerd output because prctl(PR_GET_DUMPABLE)==0
		//I/WindowState(  459): WIN DEATH: Window{2f487af6 u0 WebGLVRHZTeaser.Activities/WebGLVRHZTeaser.Activities.ApplicationWebServiceActivity
		//    }
		//    I/Zygote(  218): Process 27439 exited due to signal(11)
		//I/ActivityManager(  459): Process WebGLVRHZTeaser.Activities(pid 27439) has died
		//W/ActivityManager(  459): Scheduling restart of crashed service WebGLVRHZTeaser.Activities/.ApplicationWebServiceXWidgetsWindow in 1000ms
		//W/ActivityManager(  459): Force removing ActivityRecord{2e6a3104 u0 WebGLVRHZTeaser.Activities/.ApplicationWebServiceActivity t289}: app died, no saved state



		//		0001 02000524 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Linq.__IdentityFunction+<>c__0`1
		//script: error JSC1000: Java : Opcode not implemented: brtrue.s at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass6_0.<GetEnumerator>b__0
		//internal compiler error at method
		// assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
		// type: ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass6_0, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
		// method: <GetEnumerator>b__0
		// Java : Opcode not implemented: brtrue.s at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__OrderedEnumerable`1+<>c__DisplayClass6_0.<GetEnumerator>b__0

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
