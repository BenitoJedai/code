using ScriptCoreLib;

using Flags = System.FlagsAttribute;

// C Standard Library
namespace WavePlayer
{
	using IntPtr = global::System.IntPtr;
	using System.Runtime.InteropServices;


	[Script(IsNative = true, Header = "math.h", IsSystemHeader = true)]
	public static class math_h
	{
		public static double sin(double e)
		{
			return default(double);
		}

		public static double cos(double e)
		{
			return default(double);
		}
	}


	[Script(IsNative = true, Header = "crtdbg.h", IsSystemHeader = true)]
	public static class crtdbg_h
	{



	}

	[Script(IsNative = true, Header = "assert.h", IsSystemHeader = true)]
	public static class assert_h
	{
		public static void assert(int expression) { }
	}

	[Script(IsNative = true, Header = "Windows.h", IsSystemHeader = true)]
	public static class windows_h
	{
		[Flags]
		public enum SoundFlags
		{
			/// <summary>play synchronously (default)</summary>
			SND_SYNC = 0x0000,
			/// <summary>play asynchronously</summary>
			SND_ASYNC = 0x0001,
			/// <summary>silence (!default) if sound not found</summary>
			SND_NODEFAULT = 0x0002,
			/// <summary>pszSound points to a memory file</summary>
			SND_MEMORY = 0x0004,
			/// <summary>loop the sound until next sndPlaySound</summary>
			SND_LOOP = 0x0008,
			/// <summary>don't stop any currently playing sound</summary>
			SND_NOSTOP = 0x0010,
			/// <summary>Stop Playing Wave</summary>
			SND_PURGE = 0x40,
			/// <summary>don't wait if the driver is busy</summary>
			SND_NOWAIT = 0x00002000,
			/// <summary>name is a registry alias</summary>
			SND_ALIAS = 0x00010000,
			/// <summary>alias is a predefined id</summary>
			SND_ALIAS_ID = 0x00110000,
			/// <summary>name is file name</summary>
			SND_FILENAME = 0x00020000,
			/// <summary>name is resource name or atom</summary>
			SND_RESOURCE = 0x00040004
		}

		// http://msdn.microsoft.com/en-us/library/ms712879(VS.85).aspx
		// http://www.pinvoke.net/default.aspx/winmm/PlaySound.html
		// http://www.toymaker.info/Games/html/sound.html
		[Script(LibraryImport = "winmm.lib")]
		public static bool PlaySound(object pszSound, object hmod, SoundFlags fdwSound)
		{
			return false;
		}

		//[DllImport("winmm.dll", SetLastError = true)]
		//public static extern bool PlaySound(byte[] pszSound, IntPtr hmod, SoundFlags fdwSound);



		/// <summary>
		/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/windowclasses/windowclassreference/windowclassstructures/wndclassex.asp
		/// </summary>
		[Script(PointerName = "PWNDCLASSEX", HasNoPrototype = true)]
		public class WNDCLASSEX
		{
			public static uint TypeSize
			{
				[Script(OptimizedCode = "return sizeof(WNDCLASSEX);")]
				get
				{
					return default(uint);
				}
			}

			public uint cbSize = TypeSize;
			public uint style;
			public IntPtr lpfnWndProc;
			public int cbClsExtra;
			public int cbWndExtra;
			public int hInstance;
			public int hIcon;
			public int hCursor;
			public int hbrBackground;
			public string lpszMenuName;
			public string lpszClassName;
			public int hIconSm;
		};

		public const long WS_EX_APPWINDOW = 0x00040000L;

		public const long WS_POPUP = 0x80000000L;

		public static object CreateWindowEx(
	long dwExStyle,
	string lpClassName,
	string lpWindowName,
	long dwStyle,
	int x,
	int y,
	int nWidth,
	int nHeight,
	object hWndParent,
	object hMenu,
	object hInstance,
	object lpParam
) { return default(int); }

		public static int RegisterClassEx(WNDCLASSEX lpwcx) { return default(int); }
		public static int GetLastError() { return default(int); }

		public static int GetModuleHandle(string lpModuleName) { return default(int); }

		#region std stream
		/// <summary>
		///  	Handle to the standard input device. Initially, this is a handle to the console input buffer, CONIN$.
		/// </summary>
		public const int STD_INPUT_HANDLE = -10;


		/// <summary>
		/// Handle to the standard output device. Initially, this is a handle to the active console screen buffer, CONOUT$.
		/// </summary>
		public const int STD_OUTPUT_HANDLE = -11;

		/// <summary>
		/// Handle to the standard error device. Initially, this is a handle to the active console screen buffer, CONOUT$.
		/// </summary>
		public const int STD_ERROR_HANDLE = -12;
		#endregion

		/// <summary>
		/// he Sleep function suspends the execution of the current thread for at least the specified interval.
		/// </summary>
		/// <param name="dwMilliseconds">[in] Minimum time interval for which execution is to be suspended, in milliseconds. A value of zero causes the thread to relinquish the remainder of its time slice to any other thread of equal priority that is ready to run. If there are no other threads of equal priority ready to run, the function returns immediately, and the thread continues execution. A value of INFINITE indicates that the suspension should not time out.</param>
		public static void Sleep(int dwMilliseconds) { }

		public static void ExitProcess(
		  uint uExitCode   // exit code for all threads
		) { }

		public static bool SetConsoleTextAttribute(object hConsoleOutput, int wAttributes)
		{
			return default(bool);
		}

		public static object GetStdHandle(int nStdHandle)
		{
			return default(object);
		}

		public static bool Beep(
			  int dwFreq,
			  int dwDuration
			)
		{
			return default(bool);
		}

	}

	/// <summary>
	/// http://www.cplusplus.com/ref/cstdio/
	/// </summary>
	[Script(IsNative = true, Header = "stdio.h", IsSystemHeader = true)]
	public static class stdio_h
	{
		public static int getchar()
		{
			return default(int);
		}

		/// <summary>
		/// Copies the string to standard output stream (stdout) and appends a new line character (\n).
		/// </summary>
		/// <param name="e">Null-terminated string to be outputed.</param>
		/// <returns>On success, a non-negative value is returned. On error EOF value is returned.</returns>
		public static int puts(string e)
		{
			return default(int);
		}

		public static int putchar(int character)
		{
			return default(int);
		}

		public static object fopen(string filename, string mode)
		{
			return default(object);
		}

		/// <summary>
		/// Close the file associated with the specified stream after flushing all buffers associated with it.
		/// </summary>
		/// <param name="stream">Pointer to FILE structure specifying the stream to be closed.</param>
		/// <returns>If the stream is successfully closed 0 is returned. If any error EOF is returned.</returns>
		public static int fclose(object stream)
		{
			return default(int);
		}


		/// <summary>
		/// The function begins copying from the address specified (string) until it reaches a null character ('\0') that ends the string. The final null-character is not copied to the stream.
		/// </summary>
		/// <param name="_string">Null-terminated string to be written.</param>
		/// <param name="stream">pointer to an open file.</param>
		/// <returns>On success, a non-negative value is returned. On error the function returns EOF.</returns>
		public static int fputs(string _string, object stream)
		{
			return default(int);
		}

		public static int fprintf(object stream, string format, __arglist) { return default(int); }

		public static int printf(string format, __arglist) { return default(int); }

	}

	[Script(IsNative = true, Header = "string.h", IsSystemHeader = true)]
	public static class string_h
	{
		public static int strlen(string e)
		{
			return default(int);
		}
	}

	/// <summary>
	/// http://www.unet.univie.ac.at/aix/libs/basetrf1/malloc.htm
	/// </summary>
	/// 
	[Script(IsNative = true, Header = "stdlib.h", IsSystemHeader = true)]
	public static class stdlib_h
	{
		public static object realloc(object ptr, int size)
		{
			return default(object);
		}

		public static void free(object e)
		{

		}
	}

}
