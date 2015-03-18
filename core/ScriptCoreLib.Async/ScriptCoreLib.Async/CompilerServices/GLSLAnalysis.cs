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

	// jsc, cant we just import the spec? GLSLangSpec.4.40.pdf and generate our analyser? its 2015!!:D
	[Obsolete("experimental")]
	public class GLSLAnalysis
	{
		// where is our glsl highlighter?

		// how to run it from commandline?
		// jsc.meta "X:\jsc.svn\examples\javascript\chrome\apps\WebGL"


		// we should not be on UI thread, nor should we switch threads ourselves
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


				//+		[0]	{ count = 1, xChar = 99 'c', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				//+		[1]	{ count = 1, xChar = 102 'f', g = {System.Linq.Lookup<<>f__AnonymousType9<char>,<>f__AnonymousType8<int,char,string,System.IO.FileStream>>.Grouping} }	<Anonymous Type>
				let IsLetter = char.IsLetter(z.xChar)

				group z by new { IsLetter, xChar = IsLetter ? letter_char : xChar } into g

				let count = g.Count()

				// lets look bigger volumes first
				orderby count descending

				select new { count, g.Key.xChar, g }
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


			Debugger.Break();
		}
	}
}
