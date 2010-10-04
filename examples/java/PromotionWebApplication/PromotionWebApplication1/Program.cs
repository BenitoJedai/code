using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionWebApplication1.Services;

namespace PromotionWebApplication1
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
            //new ThreeDWarehouse();

			global::jsc.meta.Commands.Rewrite.RewriteToUltraApplication.RewriteToUltraApplication.AsProgram.Launch(
				typeof(Application)
			);
		}
	}
}
