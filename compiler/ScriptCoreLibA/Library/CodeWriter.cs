using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Library
{
	[Obsolete("see ScriptCoreLib.Ultra.Studio.Languages")]
	public class CodeWriter : IDisposable
	{
		readonly StreamWriter InternalWriter;

		public readonly Action<Type, Action, Action> PartialTypeBlock;
		public readonly Action<string, Action> Block;
		public readonly Action<string> Statement;
		public readonly Action<string, Action> Region;

		public CodeWriter(StreamWriter s)
		{
			this.InternalWriter = s;

			var Indent = 0;

			this.Statement =
				text => s.WriteLine(new string('\t', Indent) + text);

			#region Block
			this.Block =
				(header, body) =>
				{
					Statement(header);
					Statement("{");
					Indent++;
					if (body != null)
						body();
					Indent--;
					Statement("}");
				};
			#endregion

			this.Region =
				(header,body) =>
				{
					Statement("#region " + header);
					body();
					Statement("#endregion");
				};


			#region PartialTypeBlock
			this.PartialTypeBlock =
				(type, attributes, body) =>
				{
					Block("namespace " + type.Namespace,
						delegate
						{
							if (attributes != null)
								attributes();
							Block("public partial class " + type.Name,
								delegate
								{

									body();
								}
							);
						}
					);
				};
			#endregion
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
