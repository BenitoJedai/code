using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;

namespace TestStopwatch
{
	public sealed class ApplicationSprite : Sprite
	{
		long InternalOffsetMilliseconds;


		public ApplicationSprite()
		{
			// jsc needs to init long fields!
			//InternalOffsetMilliseconds = 0
			//	;

			var sw = Stopwatch.StartNew();

			// hello {{ now = 14.04.2015 14:19:36, ElapsedMilliseconds = NaN }}
			// hello {{ now = 14.04.2015 14:20:26, IsRunning = true, ElapsedMilliseconds = NaN }}
			var InternalStart = DateTime.Now;
			var InternalStop = DateTime.Now;

			//  public var InternalOffsetMilliseconds:Number;


			var diff = (InternalStop - InternalStart);
			// hello {{ InternalStart = 14.04.2015 14:22:16, diff = 00:00:00.0, IsRunning = true, ElapsedMilliseconds = NaN }}
			var offset = TimeSpan.FromMilliseconds(InternalOffsetMilliseconds);

			// hello {{ InternalStart = 14.04.2015 14:28:56, diff = 00:00:00.0, InternalOffsetMilliseconds = NaN, offset = 00:00:00.NaN, Elapsed = 00:00:00.NaN, IsRunning = true, ElapsedMilliseconds = NaN }}


			// hello {{ InternalStart = 14.04.2015 14:24:20, diff = 00:00:00.0, offset = 00:00:00.NaN, Elapsed = 00:00:00.NaN, IsRunning = true, ElapsedMilliseconds = NaN }}

			var Elapsed = diff + offset;

			// hello {{ ElapsedMilliseconds = NaN }}
			var t = new TextField
			{
				multiline = true,



				autoSize = TextFieldAutoSize.LEFT,

				text = "hello " + new
				{
					InternalStart,
					diff,
					InternalOffsetMilliseconds,
					offset,
					Elapsed,
					sw.IsRunning,
					sw.ElapsedMilliseconds
				}
			};

			t.AttachToSprite();
		}

	}
}

//000a 020004ed ScriptCoreLib::ScriptCoreLib.ActionScript.BCLImplementation.System.Text.__UTF8Encoding
//{ exc = System.AggregateException: One or more errors occurred. ---> System.InvalidOperationException: internal compiler error at method
// assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
// type: ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
// method: CreatePaddedBuffer
// ActionScript : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
//	at jsc.Script.CompilerBase.BreakToDebugger(String e) in X:\jsc.internal.git\compiler\jsc\Languages\CompilerBase.cs:line 267