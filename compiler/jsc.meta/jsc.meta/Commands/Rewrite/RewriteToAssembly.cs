using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Commands.Rewrite
{
	public class RewriteToAssembly
	{
		/// <summary>
		/// We should be translating complex IL to more simple IL.
		/// For example switch statements could be translated to
		/// if statements. We might want to use jsx and then after simplifing 
		/// IL run jsc on the generated assembly.
		/// 
		/// jsc is the default translation provider which just happens
		/// to understand our ScriptAttribute
		/// 
		/// jsx happens to be a more advanced IL reader than jsc
		/// 
		/// if in the future any vendor comes up with a better solution
		/// which we can implement we will consider them too.
		/// 
		/// Some target languages wont implement specific features
		/// for which we will need to simplify IL anyhow.
		/// 
		/// We could also be inlining methods and delete them.
		/// </summary>
		public bool simplify;

		/// <summary>
		/// We can provide obfuscation features. Simply by renaming all
		/// methods would do. We could also make the IL harder for disassamblers
		/// like reflector.
		/// </summary>
		public bool obfuscate;


		class NamespaceRenameInstructions
		{
			// we could provide namespace renaming to provide 
			// brand support
		}

		public void Invoke()
		{

		}
	}
}
