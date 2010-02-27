using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;
using jsc;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToAssembly
	{
		public event Action<AtShouldCopyTypeTuple> AtShouldCopyType;

		private bool ShouldCopyType(Type ContextType)
		{
			var t = new AtShouldCopyTypeTuple { ContextType = ContextType };

			if (AtShouldCopyType != null)
				AtShouldCopyType(t);

			if (t.DisableCopyType)
				return false;

			return PrimaryTypes.Any(k => k.Assembly == ContextType.Assembly)
				||
				this.merge.Any(k => k.name == ContextType.Assembly.GetName().Name)
				|| IsMarkedForMerge(ContextType);
		}



	}
}
