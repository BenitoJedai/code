using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{


	[Script(InternalConstructor = true, Implements = typeof(global::System.Exception))]
	internal class __Exception : global::System.Exception
	{

		public new string Message
		{
			[Script(DefineAsStatic = true)]
			get
			{
				return Expando<string>.Of(this)["message"];
			}
		}

		#region Constructor

		public __Exception(string message) : base(message) { }

		[Script(OptimizedCode = @"return new Exception($e);")]
		internal static __Exception InternalConstructor(string e)
		{
			return default(__Exception);
		}

		#endregion

	}

}
