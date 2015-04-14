using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace AIRServerSocketExperiment
{
	public sealed class ApplicationSprite : Sprite
	{
		// X:\util\air17_sdk_sa_win
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140911

		public ApplicationSprite()
		{
			// X:\jsc.svn\examples\actionscript\air\AIRStageWebViewExperiment\AIRStageWebViewExperiment\ApplicationSprite.cs

			// http://stackoverflow.com/questions/3170585/get-ip-address-with-adobe-air-2

			var t = new TextField
			{
				text = new
				{
					// http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/Capabilities.html
					Capabilities.os,
					Capabilities.version,
					Capabilities.playerType,


					//Error   CS0117  'Capabilities' does not contain a definition for 'cpuArchitecture'  AIRServerSocketExperiment   X:\jsc.svn\examples\actionscript\air\AIRServerSocketExperiment\AIRServerSocketExperiment\ApplicationSprite.cs   29

#if FPARTIAL
					Capabilities.cpuArchitecture,
					Capabilities.touchscreenType,
#endif

					// why is this commented out?
					//WorkerDomain = WorkerDomain.isSupported,

					// http://na5.brightidea.com/ct/ct_a_view_idea.bix?c=9D564F43-979A-4E35-AA21-85A61B6AB8DE&idea_id=F353B3CC-5F05-45E7-9A73-1093E0A1F9DD

					NetworkInfo = NetworkInfo.isSupported,
					//Worker.current.isPrimordial,

					this.loaderInfo.bytes.length
				}.ToString(),

				autoSize = TextFieldAutoSize.LEFT
			};

			t.AttachTo(this);



			//V:\web\AIRServerSocketExperiment\ApplicationSprite.as(46): col: 127 Error: Implicit coercion of a value of type __AS3__.vec:Vector.<flash.net:NetworkInterface> to an unrelated type __AS3__.vec:Vector.<*>.

			//            LinqExtensions.WithEach_f7a1155f_06000027(CommonExtensions.AsEnumerable_4ebbe596_06001850(NetworkInfo.networkInfo.findInterfaces()), new __Action_1(class21, __IntPtr.op_Explicit_4ebbe596_06001412("__ctor_b__1_f7a1155f_06000005")));
			//                                                                                                                              ^

			//{ os = Windows 7, version = WIN 13,0,0,133, playerType = Desktop, length = 489435 }
			// { name = {84D54A45-2ACC-42B2-A59B-E01D43897D2D}, displayName = Local Area Connection, active = false }
			// address { address = 169.254.45.8, broadcast = 169.254.255.255 }
			// { name = {978F5176-96B4-49D4-A14D-6CEA5CB3D505}, displayName = Bluetooth Network Connection, active = false }
			// address { address = 169.254.6.204, broadcast = 169.254.255.255 }
			// { name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, displayName = Wireless Network Connection, active = true }
			// address { address = 192.168.43.252, broadcast = 192.168.43.255 }
			// { name = {846EE342-7039-11DE-9D20-806E6F6E6963}, displayName = Loopback Pseudo-Interface 1, active = true }
			// address { address = ::1, broadcast =  }
			// address { address = 127.0.0.1, broadcast =  }
			// { name = {A5178906-144E-433A-9103-B2EB62A4C21E}, displayName = Teredo Tunneling Pseudo-Interface, active = true }
			// address { address = 2001:0:9d38:6abd:28d1:36ea:3f57:d403, broadcast =  }

			if (Capabilities.playerType == "Desktop")
			{
				// we dont want to crash flash player!
				// http://help.adobe.com/en_US/air/build/WS144092a96ffef7cc16ddeea2126bb46b82f-8000.html

				// VerifyError: Error #1014: Class flash.net::NetworkInterface could not be found.

				var ii = NetworkInfo.networkInfo.findInterfaces();
				for (int ij = 0; ij < ii.length; ij++)
				{
					var i = ii[ij];

					t.appendText("\n " + new { i.name, i.displayName, i.active });

					var aa = i.addresses;

					for (int aj = 0; aj < aa.length; aj++)
					{
						var a = aa[aj];

						t.appendText("\n address " + new { a.address, a.broadcast, a.ipVersion });
					}
				}

			}

			//V:\web\AIRServerSocketExperiment\ApplicationSprite___c__DisplayClass3.as(28): col: 105 Error: 
			// Implicit coercion of a value of type __AS3__.vec:Vector.<flash.net:InterfaceAddress> 
			// to an unrelated type __AS3__.vec:Vector.<*>.

			//            LinqExtensions.WithEach_f7a1155f_06000022(CommonExtensions.AsEnumerable_4ebbe596_06001850(i.addresses), new __Action_1(this, __IntPtr.op_Explicit_4ebbe596_06001413(this.__ctor_b__2_f7a1155f_06000006)));
			//                                                                                                        ^





			//i.addresses.AsEnumerable().WithEach(
			//    a =>
			//    {
			//    }
			//);





			////if (Capabilities.playerType == CapabilitiesPlayerType)
			//var s = new ServerSocket();
			//s.bind(8080);


			//s.connect +=
			//    e =>
			//    {

			//    };

			//s.listen(30);
		}

	}
}

// build with 2012?
// not available on this device?



//0007 02000189 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Reflection.__ICustomAttributeProvider
//{ exc = System.AggregateException: One or more errors occurred. ---> System.NotSupportedException: Unable to transform overloaded constructors to a single constructor via optional parameters for ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets.__UdpReceiveResult
//   at jsc.Languages.ActionScript.ActionScriptCompiler.ConstructorInlineInfo..ctor(Type z, Boolean DisableThrow) in x:\jsc.internal.git\compiler\jsc\Languages\ActionScript\ActionScriptCompiler.WriteTypeInstanceConstructors.cs:line 123

//000a 02000346 ScriptCoreLib::ScriptCoreLib.ActionScript.Extensions.DynamicContainer
//{ exc = System.AggregateException: One or more errors occurred. ---> System.InvalidOperationException: internal compiler error at method
// assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
// type: ScriptCoreLib.Shared.BCLImplementation.System.IO.__FileSystemInfo, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
// method: get_LastWriteTime
// ActionScript : unable to emit ret at 'ScriptCoreLib.Shared.BCLImplementation.System.IO.__FileSystemInfo.get_LastWriteTime'#0013: ActionScript : unable to emit call at
// BCL needs another method, please define it.
// Cannot call type without script attribute :
// System.DateTime for System.DateTime ToLocalTime() used at
// ScriptCoreLib.Shared.BCLImplementation.System.IO.__FileSystemInfo.get_LastWriteTime at offset 000a.
// If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements=typeof(...)] set. You may have mistyped it.
//   at jsc.Script.CompilerBase.BreakToDebugger(String e) in x:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 267

//0001 020004c7 ScriptCoreLib::ScriptCoreLib.ActionScript.BCLImplementation.System.__Object
//script: error JSC1000: ActionScript : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
//internal compiler error at method
// assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
// type: ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
// method: CreatePaddedBuffer
// ActionScript : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer


//0001 02000009 AIRServerSocketExperiment.ApplicationSprite::<>f__AnonymousType$74$2`3
//x:\util\flex_sdk_4.6\bin\mxmlc.exe
// -static-link-runtime-shared-libraries=true +configname=airmobile  -sp=. -swf-version=22 --target-player=11.9.0  -locale en_US -strict -output="W:\web\AIRServerSocketExperiment.ApplicationSprite.swf" AIRServerSocketExperiment\ApplicationSprite.as
//2608:02:01 after worker yield...

// http://flex.apache.org/installer.html
//Unhandled Exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.ComponentModel.Win32Exception: The system cannot find the file specified