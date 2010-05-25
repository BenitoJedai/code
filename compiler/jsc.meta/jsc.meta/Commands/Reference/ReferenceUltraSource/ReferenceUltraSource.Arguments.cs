using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{
		/// <summary>
		/// While true any html in the solution is selected as UltraSource input.
		/// </summary>
		public bool SelectAll;

		// step 3 do not event filter by folder just import everything
		public const string UltraSource = "UltraSource";

		// todo: to be phased out once moved to ReferenceUltraSource
		const string WebSource_HTML = "WebSource.HTML";

		public ConfigurationInfo Configuration = "Assets";

		public class ConfigurationInfo
		{
			public string Name;

			public static implicit operator ConfigurationInfo(string Name)
			{
				return new ConfigurationInfo { Name = Name };
			}

			public bool BuildAlways
			{
				get
				{
					return "Assets" == Name;
				}
			}
		}

        /// <summary>
        /// The Browser Application may want to link some files as static assets
        /// </summary>
        public LinkedAssetInfo[] LinkedAssets = new LinkedAssetInfo[0];

        public class LinkedAssetInfo
        {
            public string TargetRoot;

            public static implicit operator LinkedAssetInfo(string Target)
            {
                return new LinkedAssetInfo { TargetRoot = Target };
            }
        }
	}
}
