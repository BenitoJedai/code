using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Input
{
	[Script(Implements = typeof(global::System.Windows.Input.KeyInterop))]
	internal class __KeyInterop
	{
		// http://forums.msdn.microsoft.com/en-US/wpf/thread/84013415-8567-49a4-be64-73238e7fbc80/

		public static Key KeyFromVirtualKey(int virtualKey)
		{
			if (virtualKey == 3)
				return Key.Cancel;

			if (virtualKey == 8)
				return Key.Back;

			if (virtualKey == 9)
				return Key.Tab;

			if (virtualKey == 12)
				return Key.Clear;

			if (virtualKey == 13)
				return Key.Return;

			if (virtualKey == 0x10)
				//if (virtualKey == 160)
					return Key.LeftShift;

			if (virtualKey == 0x11)
				//if (virtualKey == 0xa2)
					return Key.LeftCtrl;

			if (virtualKey == 0x12)
				//if (virtualKey == 0xa4)
					return Key.LeftAlt;

			if (virtualKey == 0x13)
				return Key.Pause;

			if (virtualKey == 20)
				return Key.Capital;

			if (virtualKey == 0x15)
				return Key.KanaMode;

			if (virtualKey == 0x17)
				return Key.JunjaMode;

			if (virtualKey == 0x18)
				return Key.FinalMode;

			if (virtualKey == 0x19)
				return Key.HanjaMode;

			if (virtualKey == 0x1b)
				return Key.Escape;

			if (virtualKey == 0x1c)
				return Key.ImeConvert;

			if (virtualKey == 0x1d)
				return Key.ImeNonConvert;

			if (virtualKey == 30)
				return Key.ImeAccept;

			if (virtualKey == 0x1f)
				return Key.ImeModeChange;

			if (virtualKey == 0x20)
				return Key.Space;

			if (virtualKey == 0x21)
				return Key.Prior;

			if (virtualKey == 0x22)
				return Key.Next;

			if (virtualKey == 0x23)
				return Key.End;

			if (virtualKey == 0x24)
				return Key.Home;

			if (virtualKey == 0x25)
				return Key.Left;

			if (virtualKey == 0x26)
				return Key.Up;

			if (virtualKey == 0x27)
				return Key.Right;

			if (virtualKey == 40)
				return Key.Down;

			if (virtualKey == 0x29)
				return Key.Select;

			if (virtualKey == 0x2a)
				return Key.Print;

			if (virtualKey == 0x2b)
				return Key.Execute;

			if (virtualKey == 0x2c)
				return Key.Snapshot;

			if (virtualKey == 0x2d)
				return Key.Insert;

			if (virtualKey == 0x2e)
				return Key.Delete;

			if (virtualKey == 0x2f)
				return Key.Help;

			if (virtualKey == 0x30)
				return Key.D0;

			if (virtualKey == 0x31)
				return Key.D1;

			if (virtualKey == 50)
				return Key.D2;

			if (virtualKey == 0x33)
				return Key.D3;

			if (virtualKey == 0x34)
				return Key.D4;

			if (virtualKey == 0x35)
				return Key.D5;

			if (virtualKey == 0x36)
				return Key.D6;

			if (virtualKey == 0x37)
				return Key.D7;

			if (virtualKey == 0x38)
				return Key.D8;

			if (virtualKey == 0x39)
				return Key.D9;

			if (virtualKey == 0x41)
				return Key.A;

			if (virtualKey == 0x42)
				return Key.B;

			if (virtualKey == 0x43)
				return Key.C;

			if (virtualKey == 0x44)
				return Key.D;

			if (virtualKey == 0x45)
				return Key.E;

			if (virtualKey == 70)
				return Key.F;

			if (virtualKey == 0x47)
				return Key.G;

			if (virtualKey == 0x48)
				return Key.H;

			if (virtualKey == 0x49)
				return Key.I;

			if (virtualKey == 0x4a)
				return Key.J;

			if (virtualKey == 0x4b)
				return Key.K;

			if (virtualKey == 0x4c)
				return Key.L;

			if (virtualKey == 0x4d)
				return Key.M;

			if (virtualKey == 0x4e)
				return Key.N;

			if (virtualKey == 0x4f)
				return Key.O;

			if (virtualKey == 80)
				return Key.P;

			if (virtualKey == 0x51)
				return Key.Q;

			if (virtualKey == 0x52)
				return Key.R;

			if (virtualKey == 0x53)
				return Key.S;

			if (virtualKey == 0x54)
				return Key.T;

			if (virtualKey == 0x55)
				return Key.U;

			if (virtualKey == 0x56)
				return Key.V;

			if (virtualKey == 0x57)
				return Key.W;

			if (virtualKey == 0x58)
				return Key.X;

			if (virtualKey == 0x59)
				return Key.Y;

			if (virtualKey == 90)
				return Key.Z;

			if (virtualKey == 0x5b)
				return Key.LWin;

			if (virtualKey == 0x5c)
				return Key.RWin;

			if (virtualKey == 0x5d)
				return Key.Apps;

			if (virtualKey == 0x5f)
				return Key.Sleep;

			if (virtualKey == 0x60)
				return Key.NumPad0;

			if (virtualKey == 0x61)
				return Key.NumPad1;

			if (virtualKey == 0x62)
				return Key.NumPad2;

			if (virtualKey == 0x63)
				return Key.NumPad3;

			if (virtualKey == 100)
				return Key.NumPad4;

			if (virtualKey == 0x65)
				return Key.NumPad5;

			if (virtualKey == 0x66)
				return Key.NumPad6;

			if (virtualKey == 0x67)
				return Key.NumPad7;

			if (virtualKey == 0x68)
				return Key.NumPad8;

			if (virtualKey == 0x69)
				return Key.NumPad9;

			if (virtualKey == 0x6a)
				return Key.Multiply;

			if (virtualKey == 0x6b)
				return Key.Add;

			if (virtualKey == 0x6c)
				return Key.Separator;

			if (virtualKey == 0x6d)
				return Key.Subtract;

			if (virtualKey == 110)
				return Key.Decimal;

			if (virtualKey == 0x6f)
				return Key.Divide;

			if (virtualKey == 0x70)
				return Key.F1;

			if (virtualKey == 0x71)
				return Key.F2;

			if (virtualKey == 0x72)
				return Key.F3;

			if (virtualKey == 0x73)
				return Key.F4;

			if (virtualKey == 0x74)
				return Key.F5;

			if (virtualKey == 0x75)
				return Key.F6;

			if (virtualKey == 0x76)
				return Key.F7;

			if (virtualKey == 0x77)
				return Key.F8;

			if (virtualKey == 120)
				return Key.F9;

			if (virtualKey == 0x79)
				return Key.F10;

			if (virtualKey == 0x7a)
				return Key.F11;

			if (virtualKey == 0x7b)
				return Key.F12;

			if (virtualKey == 0x7c)
				return Key.F13;

			if (virtualKey == 0x7d)
				return Key.F14;

			if (virtualKey == 0x7e)
				return Key.F15;

			if (virtualKey == 0x7f)
				return Key.F16;

			if (virtualKey == 0x80)
				return Key.F17;

			if (virtualKey == 0x81)
				return Key.F18;

			if (virtualKey == 130)
				return Key.F19;

			if (virtualKey == 0x83)
				return Key.F20;

			if (virtualKey == 0x84)
				return Key.F21;

			if (virtualKey == 0x85)
				return Key.F22;

			if (virtualKey == 0x86)
				return Key.F23;

			if (virtualKey == 0x87)
				return Key.F24;

			if (virtualKey == 0x90)
				return Key.NumLock;

			if (virtualKey == 0x91)
				return Key.Scroll;

			if (virtualKey == 0xa1)
				return Key.RightShift;

			if (virtualKey == 0xa3)
				return Key.RightCtrl;

			if (virtualKey == 0xa5)
				return Key.RightAlt;

			if (virtualKey == 0xa6)
				return Key.BrowserBack;

			if (virtualKey == 0xa7)
				return Key.BrowserForward;

			if (virtualKey == 0xa8)
				return Key.BrowserRefresh;

			if (virtualKey == 0xa9)
				return Key.BrowserStop;

			if (virtualKey == 170)
				return Key.BrowserSearch;

			if (virtualKey == 0xab)
				return Key.BrowserFavorites;

			if (virtualKey == 0xac)
				return Key.BrowserHome;

			if (virtualKey == 0xad)
				return Key.VolumeMute;

			if (virtualKey == 0xae)
				return Key.VolumeDown;

			if (virtualKey == 0xaf)
				return Key.VolumeUp;

			if (virtualKey == 0xb0)
				return Key.MediaNextTrack;

			if (virtualKey == 0xb1)
				return Key.MediaPreviousTrack;

			if (virtualKey == 0xb2)
				return Key.MediaStop;

			if (virtualKey == 0xb3)
				return Key.MediaPlayPause;

			if (virtualKey == 180)
				return Key.LaunchMail;

			if (virtualKey == 0xb5)
				return Key.SelectMedia;

			if (virtualKey == 0xb6)
				return Key.LaunchApplication1;

			if (virtualKey == 0xb7)
				return Key.LaunchApplication2;

			if (virtualKey == 0xba)
				return Key.Oem1;

			if (virtualKey == 0xbb)
				return Key.OemPlus;

			if (virtualKey == 0xbc)
				return Key.OemComma;

			if (virtualKey == 0xbd)
				return Key.OemMinus;

			if (virtualKey == 190)
				return Key.OemPeriod;

			if (virtualKey == 0xbf)
				return Key.Oem2;

			if (virtualKey == 0xc0)
				return Key.Oem3;

			if (virtualKey == 0xc1)
				return Key.AbntC1;

			if (virtualKey == 0xc2)
				return Key.AbntC2;

			if (virtualKey == 0xdb)
				return Key.Oem4;

			if (virtualKey == 220)
				return Key.Oem5;

			if (virtualKey == 0xdd)
				return Key.Oem6;

			if (virtualKey == 0xde)
				return Key.Oem7;

			if (virtualKey == 0xdf)
				return Key.Oem8;

			if (virtualKey == 0xe2)
				return Key.Oem102;

			if (virtualKey == 0xe5)
				return Key.ImeProcessed;

			if (virtualKey == 240)
				return Key.OemAttn;

			if (virtualKey == 0xf1)
				return Key.OemFinish;

			if (virtualKey == 0xf2)
				return Key.OemCopy;

			if (virtualKey == 0xf3)
				return Key.OemAuto;

			if (virtualKey == 0xf4)
				return Key.OemEnlw;

			if (virtualKey == 0xf5)
				return Key.OemBackTab;

			if (virtualKey == 0xf6)
				return Key.Attn;

			if (virtualKey == 0xf7)
				return Key.CrSel;

			if (virtualKey == 0xf8)
				return Key.ExSel;

			if (virtualKey == 0xf9)
				return Key.EraseEof;

			if (virtualKey == 250)
				return Key.Play;

			if (virtualKey == 0xfb)
				return Key.Zoom;

			if (virtualKey == 0xfc)
				return Key.NoName;

			if (virtualKey == 0xfd)
				return Key.Pa1;

			if (virtualKey == 0xfe)
				return Key.OemClear;
			return Key.None;

		}

	}
}
