using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeApplet
{
	internal interface IAssemblyReferenceToken : 
		ScriptCoreLibJava.IAssemblyReferenceToken,
		ScriptCoreLib.Shared.Maze.IAssemblyReferenceTokenJava
	{
	}
}
