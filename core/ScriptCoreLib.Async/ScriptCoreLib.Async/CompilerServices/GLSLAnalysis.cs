using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.CompilerServices
{
	// first step to provide glsl rewrite services.
	// this could be forwarded into a special nuget later
	// inspired by the jsc idl parser, linq to sql 
	// intended to run in CLR and in chrome worker threads, or on android L
	// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150318
	// webgl2/vr should be the trigger for effort

	// ref
	// https://www.opengl.org/documentation/glsl/
	// "X:\jsc.svn\examples\glsl\future\GLSLRemote\GLSLRemote.sln"

	// https://bitbucket.org/daspork/opentkshader/src/535868e04c5efb3f101da5495c74a5dbc79320bd/GLSLShader.cs?at=default
	// http://laurent.le-brun.eu/site/index.php/2010/06/07/54-fsharp-and-fparsec-a-glsl-parser-example
	// http://laurent.le-brun.eu/fsharp/glsl_parse.fs
	// http://stackoverflow.com/questions/29048161/how-to-export-a-three-js-scene-into-a-360-texture-for-photosphere

	// we would also know how the shader intends to use the uniforms
	// like do they need random texture, cubemap, audio or keyboard

	// jsc, cant we just import the spec? GLSLangSpec.4.40.pdf and generate our analyser? its 2015!!:D
	[Obsolete("experimental")]
	public class GLSLAnalysis
	{
		// where is our glsl highlighter?

		// how to run it from commandline?
		// jsc.meta "X:\jsc.svn\examples\javascript\chrome\apps\WebGL"


		// we should not be on UI thread, nor should we switch threads ourselves
		// when will it work for js workers?
		// can we do syntax highlighting?
		public static async Task WorkerThreadAnalyzeFragmentShaders(
			string[] SourceFiles,
			Action<double> AtProgress
			)
		{
			// this method could split analysis per core. 

			// if the files change
			// we have to discard and rewind, restart analysis

			// first we could inspect whats the first char of the shaders
			var SourceStreams = Enumerable.ToArray(
				from f in SourceFiles
				let s = File.OpenRead(f)
				select new { f, s }
			);

			var cFirstChar = Enumerable.ToArray(
				from x in SourceStreams
				let xReadByte = x.s.ReadByte()
				select new { xReadByte, x.f, x.s }
				);

			// [0] = { xReadByte = 239, x = { f = X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyAlbertArchesByDr2\ChromeShaderToyAlbertArchesByDr2\Shaders\Program.frag, s = System.IO.FileStream } }
			// "X:\util\xvi32\XVI32.exe"
			// whats 239?
			// ah its a bom
			// 0xEF BB BF

			// The Unicode Standard permits the BOM in UTF-8,[2] but does not require or recommend its use.
			// [3] Byte order has no meaning in UTF-8,[4] so its only use in UTF-8 is to signal at the start that the text stream is encoded in UTF-8. 

			// ok. lets skip bom
			// sonds lik ZIP parsing we did a while a go


			// ---------------------------
			//Unable to set the next statement.The next statement cannot be changed until the current statement has completed.

			// should we record the links we open in tabs for future ref?



			// a sub type of chars. x marks the spot
			const char letter_char = 'x';
			const char WhiteSpace_char = ' ';

			#region cFirstCharAfterByteOrderMark
			var cFirstCharAfterByteOrderMark = Enumerable.ToArray(
				from c in cFirstChar
				where c.xReadByte == 0xEF
				let xBB = c.s.ReadByte()
				where xBB == 0xbb
				let xBF = c.s.ReadByte()
				where xBF == 0xBF
				let xReadByte = c.s.ReadByte()
				// we are in ascii mode. does GLSL do utf encoding? when will we need it?
				let xChar = (char)xReadByte

				let z = new { xReadByte, xChar, c.f, c.s }

				#region group for parallel diagnostics and data driven development
				//+		[0]	{ count = 1, xChar = 99 'c', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[1]	{ count = 1, xChar = 102 'f', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				let IsLetter = char.IsLetter(z.xChar)

				// https://msdn.microsoft.com/en-us/library/system.char.iswhitespace%28v=vs.110%29.aspx
				let IsWhiteSpace = char.IsWhiteSpace(z.xChar)

				group z by new
				{
					IsLetter,
					IsWhiteSpace,
					xChar =
						IsLetter ? letter_char :
						IsWhiteSpace ? WhiteSpace_char : z.xChar
				} into g

				let count = g.Count()

				// lets look bigger volumes first
				orderby count descending

				select new { count, g.Key.xChar, g.Key.IsWhiteSpace, g }
				#endregion

			);


			//+		[2]	{ count = 4, xChar = 13 '\r', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[3]	{ count = 8, xChar = 35 '#', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[4]	{ count = 11, xChar = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>


			//+		[0]	{ count = 11, xChar = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 8, xChar = 35 '#', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 4, xChar = 13 '\r', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[3]	{ count = 2, xChar = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

			// # means a define! either a value, or a code fragment?
			// we need more examples.
			// long path error, bypass via subst for now?
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL
			// subst w: X:\jsc.svn\examples\javascript\chrome\apps\WebGL

			//+		[0]	{ count = 104, xChar = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 25, xChar = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 24, xChar = 35 '#', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[3]	{ count = 13, xChar = 13 '\r', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[4]	{ count = 7, xChar = 32 ' ', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[5]	{ count = 1, xChar = 9 '\t', g = {System.Linq.Lookup<<>f__AnonymousType10<bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

			// we should implement / processing first.
			// and we could group whitespace. its not fsharp where tabs matter is it.
			// what can we do with comments? discard or keep as documentation? for the next element in the stream?
			// 

			//+		[0]	{ count = 104, xChar = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 25, xChar = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 24, xChar = 35 '#', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[3]	{ count = 21, xChar = 32 ' ', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			#endregion

			// the defines are scoped to the stream. otherwise clashes will occur
			// could we skip the spaces?


			// how many iterations:
			#region cNoSpace
			var cNoSpace = cFirstCharAfterByteOrderMark;
			// keep an eye on time costs, we are doing a lot of grouping..
			var cNoSpacePassIterations = new List<Stopwatch>();

			while (cNoSpace.Any(g => g.IsWhiteSpace))
			{

				var cNoSpacePass = Stopwatch.StartNew();

				cNoSpace = Enumerable.ToArray(
				   from g in cNoSpace

				   from c in g.g

					   // if we are a space lets read a new byte otherwise keep the byte we have

				   let xReadByte = char.IsWhiteSpace(c.xChar) ? c.s.ReadByte() : c.xReadByte
				   let xChar = (char)xReadByte

				   let z = new { xReadByte, xChar, c.f, c.s }


				   #region group for parallel diagnostics and data driven development
				   //+		[0]	{ count = 1, xChar = 99 'c', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				   //+		[1]	{ count = 1, xChar = 102 'f', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				   let IsLetter = char.IsLetter(z.xChar)

				   // https://msdn.microsoft.com/en-us/library/system.char.iswhitespace%28v=vs.110%29.aspx
				   let IsWhiteSpace = char.IsWhiteSpace(z.xChar)

				   group z by new
				   {
					   IsLetter,
					   IsWhiteSpace,
					   xChar =
						   IsLetter ? letter_char :
						   IsWhiteSpace ? WhiteSpace_char : z.xChar
				   } into g

				   let count = g.Count()

				   // lets look bigger volumes first
				   orderby count descending

				   select new { count, g.Key.xChar, g.Key.IsWhiteSpace, g }
				   #endregion
 );
				cNoSpacePass.Stop();
				cNoSpacePassIterations.Add(cNoSpacePass);

				// pass complete

				//-		cFirstCharAfterByteOrderMark	{<>f__AnonymousType13<int,char,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[4]}	<>f__AnonymousType13<int,char,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[]
				//+		[0]	{ count = 104, xChar = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[1]	{ count = 25, xChar = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[2]	{ count = 24, xChar = 35 '#', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[3]	{ count = 21, xChar = 32 ' ', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//		cNoSpacePass.ElapsedTicks	11609	long
				//-		cNoSpace	{<>f__AnonymousType13<int,char,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[4]}	<>f__AnonymousType13<int,char,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[]
				//+		[0]	{ count = 105, xChar = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[1]	{ count = 25, xChar = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[2]	{ count = 25, xChar = 35 '#', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[3]	{ count = 19, xChar = 32 ' ', g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

				// on the first pass we lost 2 spaces and gained a / and a #


			}

			var cNoSpacePassIterationsElapsed = TimeSpan.FromMilliseconds(cNoSpacePassIterations.Sum(x => x.ElapsedMilliseconds));
			#endregion


			//-		cNoSpace	{<>f__AnonymousType13<int,char,bool,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[8]}	<>f__AnonymousType13<int,char,bool,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[]
			//+		[0]	{ count = 104, xChar = 47 '/', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 41, xChar = 120 'x', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 24, xChar = 35 '#', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

			#region bugcheck
			//+		[3]	{ count = 1, xChar = 45 '-', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[4]	{ count = 1, xChar = 59 ';', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[5]	{ count = 1, xChar = 61 '=', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[6]	{ count = 1, xChar = 53 '5', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[7]	{ count = 1, xChar = 48 '0', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

			// -		cNoSpacePassIterations	Count = 83	System.Collections.Generic.List<System.Diagnostics.Stopwatch>

			// how is it possible to start with ; - = 5 0 ?

			// what files are they. 
			// are they valid?
			// are we to discard it as invalid state?
			// is there a bug in our code already?

			// +		[0]	{ xReadByte = 45, xChar = 45 '-', f = "W:\\ChromeShaderToyDigitsByFabrice\\ChromeShaderToyDigitsByFabrice\\Shaders\\Program.frag", s = {System.IO.FileStream} }	<Anonymous Type>
			//Error ENC0279 Modifying 'method' which contains a query expression will prevent the debug session from continuing.ScriptCoreLib.Async X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\CompilerServices\GLSLAnalysis.cs    50
			// +		cNoSpacePassIterationsElapsed	{00:00:00.0050000}	System.TimeSpan
			// x:\jsc.svn\examples\javascript\chrome\apps\webgl\chromeshadertoydigitsbyfabrice\chromeshadertoydigitsbyfabrice\shaders\program.frag
			// no we must have a bug.
			// can we isolate it?
			// was a bug. rerun the async test.
			#endregion

			//-		cNoSpace	{<>f__AnonymousType13<int,char,bool,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[3]}	<>f__AnonymousType13<int,char,bool,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[]
			//+		[0]	{ count = 110, xChar = 47 '/', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 32, xChar = 35 '#', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 32, xChar = 120 'x', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		cNoSpacePassIterations	Count = 34	System.Collections.Generic.List<System.Diagnostics.Stopwatch>
			//+		cNoSpacePassIterationsElapsed	{00:00:00.0050000}	System.TimeSpan

			// 110 shaders seem to start with a comment.
			// shall we extract a comment from the stream?

			// GLSLComment
			// GLSLDefine
			// GLSLSymbol

			// jsc could run the analyser on assetslib run.


			//-		cNoSpace	{<>f__AnonymousType13<int,char,bool,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[3]}	<>f__AnonymousType13<int,char,bool,System.Linq.IGrouping<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>>[]
			//+		[0]	{ count = 156, xChar = 47 '/', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 40, xChar = 35 '#', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 37, xChar = 120 'x', IsWhiteSpace = false, g = {System.Linq.Lookup<<>f__AnonymousType11<bool,bool,char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		cNoSpacePassIterations	Count = 34	System.Collections.Generic.List<System.Diagnostics.Stopwatch>

			// the only thing to do is the read the next char too..


			#region cNoSpaceWORD
			var cNoSpaceWORD = Enumerable.ToArray(
				   from g in cNoSpace

				   from c in g.g

				   let xReadByte0 = c.xReadByte
				   let xChar0 = c.xChar

				   let xReadByte1 = c.s.ReadByte()
				   let xChar1 = (char)xReadByte1

				   // x? or #?
				   let IsLetter0 = char.IsLetter(xChar0)
				   let IsLetter1 = char.IsLetter(xChar1)

				   let IsLineComment = xChar0 == '/' && xChar1 == '/'
				   let IsBlockComment = xChar0 == '/' && xChar1 == '*'

				   // placeholder
				   let xLineCommentContentByte = -1
				   let xLineCommentContentByteIsLineFeed = false

				   // a placeholder for all comment content until \n
				   let xLineCommentStringBuilder = IsLineComment ? new StringBuilder() : null


				   //let z = new { xReadByte0, xReadByte1, xChar0, xChar1, c.f, c.s, IsLineComment, IsBlockComment, IsLetter0, IsLetter1 }
				   //let z = new { xChar0, xChar1, xLineCommentContentByte, xLineCommentContentByteIsLineFeed, c.f, c.s, IsLineComment, IsBlockComment, IsLetter0, IsLetter1, xLineCommentStringBuilder }
				   let z = new { IsLineComment, xLineCommentStringBuilder, xChar0, xChar1, xLineCommentContentByte, xLineCommentContentByteIsLineFeed, c.f, c.s, IsBlockComment, IsLetter0, IsLetter1 }

				   group z by new
				   {

					   //IsLetter0,
					   // lets prep the grouping for comment parsing

					   IsLineComment,
					   xLineCommentContentByteIsLineFeed,


					   xChar0 = IsLetter0 ? letter_char : xChar0,

					   // how to group it?
					   xChar1 = IsLetter1 ? letter_char : xChar1,
				   } into g

				   let count = g.Count()

				   // lets look bigger volumes first
				   orderby count descending

				   //select new { count, g.Key.xChar0, g.Key.xChar1, g.Key.IsLetter0, g }
				   select new { count, g.Key.IsLineComment, g.Key.xLineCommentContentByteIsLineFeed, g.Key.xChar0, g.Key.xChar1, g }
			);

			//+		[0]	{ count = 148, xChar0 = 47 '/', xChar1 = 47 '/', IsLetter0 = false, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[5]	{ count = 8, xChar0 = 47 '/', xChar1 = 42 '*', IsLetter0 = false, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

			//+		[1]	{ count = 30, xChar0 = 35 '#', xChar1 = 100 'd', IsLetter0 = false, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[4]	{ count = 10, xChar0 = 35 '#', xChar1 = 105 'i', IsLetter0 = false, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

			// reading a typeref?
			//+		[2]	{ count = 16, xChar0 = 120 'x', xChar1 = 108 'l', IsLetter0 = true, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[3]	{ count = 12, xChar0 = 120 'x', xChar1 = 111 'o', IsLetter0 = true, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[6]	{ count = 4, xChar0 = 120 'x', xChar1 = 101 'e', IsLetter0 = true, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[7]	{ count = 2, xChar0 = 120 'x', xChar1 = 97 'a', IsLetter0 = true, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[8]	{ count = 2, xChar0 = 120 'x', xChar1 = 116 't', IsLetter0 = true, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[9]	{ count = 1, xChar0 = 120 'x', xChar1 = 114 'r', IsLetter0 = true, g = {System.Linq.Lookup<<>f__AnonymousType27<bool,char,char>,<>f__AnonymousType26<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>


			//+		[0]	{ count = 148, xChar0 = 47 '/', xChar1 = 47 '/', IsLetter0 = false, g = {System.Linq.Lookup<<>f__AnonymousType28<bool,char,char>,<>f__AnonymousType27<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 40, xChar0 = 35 '#', xChar1 = 120 'x', IsLetter0 = false, g = {System.Linq.Lookup<<>f__AnonymousType28<bool,char,char>,<>f__AnonymousType27<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 37, xChar0 = 120 'x', xChar1 = 120 'x', IsLetter0 = true, g = {System.Linq.Lookup<<>f__AnonymousType28<bool,char,char>,<>f__AnonymousType27<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
			//+		[3]	{ count = 8, xChar0 = 47 '/', xChar1 = 42 '*', IsLetter0 = false, g = {System.Linq.Lookup<<>f__AnonymousType28<bool,char,char>,<>f__AnonymousType27<int,int,char,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>

			// GLSLComment
			// GLSLBlockComment
			// GLSLPragmaDefine
			// GLSLPragmaIfDefined
			// GLSLSymbol

			// comment needs a newline to terminate
			// block comment needs */ to terminate
			#endregion



			//lets resolve the comments

			var cNoLineCommentPassIterations = new List<Stopwatch>();
			#region cNoLineComment
			var cNoLineComment = cNoSpaceWORD;

			// any open line comments?
			while (cNoLineComment.Any(g => g.IsLineComment && !g.xLineCommentContentByteIsLineFeed))
			{
				var cNoLineCommentPass = Stopwatch.StartNew();

				cNoLineComment = Enumerable.ToArray(
					from g in cNoLineComment

					from c in g.g

						//let xb = new StringBuilder()
						//let xb0 = xb.Append(


						// how can we collect/aggregate the current bytes?

						// applicable for IsComment
						// unless xLineCommentContentByteIsLineFeed, then stall and keep current state for next phase
					let xLineCommentContentByte =
						c.IsLineComment ?
							c.xLineCommentContentByteIsLineFeed ?
								c.xLineCommentContentByte : c.s.ReadByte() : -1

					// did it terminate the line yet?
					// even if we did, how can we resume on the next line?
					// http://stackoverflow.com/questions/3267311/what-is-newline-character-n

					// terminates line comment yet? check for -1?
					let xLineCommentContentByteIsLineFeed = xLineCommentContentByte == '\n'


					let xLineCommentStringBuilderAppend =
						c.IsLineComment &&
							// was the comment already complete?
							!c.xLineCommentContentByteIsLineFeed
							// is the comment complete now?
							//!xLineCommentContentByteIsLineFeed

							// what happens if the stream ends mid way?
							// is the previous byte there?
							&& c.xLineCommentContentByte >= 0

							// how can we skip \r ?
							&& !(xLineCommentContentByteIsLineFeed && c.xLineCommentContentByte == '\r') ?
								c.xLineCommentStringBuilder.Append((char)c.xLineCommentContentByte) : null


					//do  C.
					//let ref0 = 

					//let z = new { c.xChar0, c.xChar1, xLineCommentContentByte, xLineCommentContentByteIsLineFeed, c.f, c.s, c.IsLineComment, c.IsBlockComment }
					//let z = new { c.xChar0, c.xChar1, xLineCommentContentByte, xLineCommentContentByteIsLineFeed, c.f, c.s, c.IsLineComment, c.IsBlockComment, c.IsLetter0, c.IsLetter1 }
					let z = new { c.IsLineComment, c.xLineCommentStringBuilder, c.xChar0, c.xChar1, xLineCommentContentByte, xLineCommentContentByteIsLineFeed, c.f, c.s, c.IsBlockComment, c.IsLetter0, c.IsLetter1 }

					// will it group the manual #define s near each other?
					orderby Convert.ToString(z.xLineCommentStringBuilder)

					group z by new
					{
						// can we mark the comment to be terminated yet and load a new word?
						// or do it on the next pass once all are at the same state?

						// we are to focus on comments
						c.IsLineComment,
						xLineCommentContentByteIsLineFeed,

						xChar0 = c.IsLetter0 ? letter_char : c.xChar0,

						// how to group it?
						xChar1 = c.IsLetter1 ? letter_char : c.xChar1,
					} into g

					let count = g.Count()

					// lets look bigger volumes first
					orderby g.Key.xLineCommentContentByteIsLineFeed, count descending

					select new { count, g.Key.IsLineComment, g.Key.xLineCommentContentByteIsLineFeed, g.Key.xChar0, g.Key.xChar1, g }
				);

				//+		[0]	{ count = 148, IsLineComment = true, xLineCommentContentByteIsLineFeed = false, xChar0 = 47 '/', xChar1 = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType35<bool,bool,char,char>,<>f__AnonymousType34<char,char,int,bool,string,System.IO.FileStream,bool,bool>>.Grouping} }	<Anonymous Type>
				//+		[1]	{ count = 40, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 35 '#', xChar1 = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType35<bool,bool,char,char>,<>f__AnonymousType34<char,char,int,bool,string,System.IO.FileStream,bool,bool>>.Grouping} }	<Anonymous Type>
				//+		[2]	{ count = 37, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 120 'x', xChar1 = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType35<bool,bool,char,char>,<>f__AnonymousType34<char,char,int,bool,string,System.IO.FileStream,bool,bool>>.Grouping} }	<Anonymous Type>
				//+		[3]	{ count = 8, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 47 '/', xChar1 = 42 '*', g = {System.Linq.Lookup<<>f__AnonymousType35<bool,bool,char,char>,<>f__AnonymousType34<char,char,int,bool,string,System.IO.FileStream,bool,bool>>.Grouping} }	<Anonymous Type>


				// the partial comments we are still reading
				//+		[0]	{ count = 147, IsLineComment = true, xLineCommentContentByteIsLineFeed = false, xChar0 = 47 '/', xChar1 = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<char,char,int,bool,string,System.IO.FileStream,bool,bool,bool,bool,System.Text.StringBuilder>>.Grouping} }	<Anonymous Type>
				// the pragmas
				//+		[1]	{ count = 40, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 35 '#', xChar1 = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<char,char,int,bool,string,System.IO.FileStream,bool,bool,bool,bool,System.Text.StringBuilder>>.Grouping} }	<Anonymous Type>
				// the typerefs?
				//+		[2]	{ count = 37, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 120 'x', xChar1 = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<char,char,int,bool,string,System.IO.FileStream,bool,bool,bool,bool,System.Text.StringBuilder>>.Grouping} }	<Anonymous Type>
				// the block comments
				//+		[3]	{ count = 8, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 47 '/', xChar1 = 42 '*', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<char,char,int,bool,string,System.IO.FileStream,bool,bool,bool,bool,System.Text.StringBuilder>>.Grouping} }	<Anonymous Type>
				// the completed line comments, need to save them and move to next bytes. 

				//-		[4]	{ count = 1, IsLineComment = true, xLineCommentContentByteIsLineFeed = true, xChar0 = 47 '/', xChar1 = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<char,char,int,bool,string,System.IO.FileStream,bool,bool,bool,bool,System.Text.StringBuilder>>.Grouping} }	<Anonymous Type>
				//+		[0]	{ xChar0 = 47 '/', xChar1 = 47 '/', xLineCommentContentByte = 10, xLineCommentContentByteIsLineFeed = true, f = "W:\\ChromeShaderToyTextCandyByCPU\\ChromeShaderToyTextCandyByCPU\\Shaders\\Program.frag", s = {System.IO.FileStream}, IsLineComment = true, IsBlockComment = false, IsLetter0 = false, IsLetter1 = false ... }	<Anonymous Type>
				//		IsLineComment	true	bool
				//		xChar0	47 '/'	char
				//		xChar1	47 '/'	char
				//		xLineCommentContentByteIsLineFeed	true	bool
				//+		cNoLineCommentPassIterations	Count = 1	System.Collections.Generic.List<System.Diagnostics.Stopwatch>



				//block comment reading will be done by 2chars at the time
				//a comment can contain anything, including a #define


				/*

				should we care about #defines ? like a manual switch?

				*/
				cNoLineCommentPass.Stop();
				cNoLineCommentPassIterations.Add(cNoLineCommentPass);
			}
			#endregion
			var cNoLineCommentPassIterationsElapsed = TimeSpan.FromMilliseconds(cNoLineCommentPassIterations.Sum(x => x.ElapsedMilliseconds));


			//+		[0]	{ count = 40, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 35 '#', xChar1 = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<bool,System.Text.StringBuilder,char,char,int,bool,string,System.IO.FileStream,bool,bool,bool>>.Grouping} }	<Anonymous Type>
			//+		[1]	{ count = 37, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 120 'x', xChar1 = 120 'x', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<bool,System.Text.StringBuilder,char,char,int,bool,string,System.IO.FileStream,bool,bool,bool>>.Grouping} }	<Anonymous Type>
			//+		[2]	{ count = 8, IsLineComment = false, xLineCommentContentByteIsLineFeed = false, xChar0 = 47 '/', xChar1 = 42 '*', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<bool,System.Text.StringBuilder,char,char,int,bool,string,System.IO.FileStream,bool,bool,bool>>.Grouping} }	<Anonymous Type>
			
			//+		[3]	{ count = 148, IsLineComment = true, xLineCommentContentByteIsLineFeed = true, xChar0 = 47 '/', xChar1 = 47 '/', g = {System.Linq.Lookup<<>f__AnonymousType33<bool,bool,char,char>,<>f__AnonymousType32<bool,System.Text.StringBuilder,char,char,int,bool,string,System.IO.FileStream,bool,bool,bool>>.Grouping} }	<Anonymous Type>
			//+		cNoLineCommentPassIterations	Count = 92	System.Collections.Generic.List<System.Diagnostics.Stopwatch>

			//NLP on comments?

			// now we need to stash the comments, and read a new byte in

			Debugger.Break();
		}
	}
}
