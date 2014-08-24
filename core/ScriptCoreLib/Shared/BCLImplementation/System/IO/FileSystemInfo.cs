using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.FileSystemInfo))]
	public abstract class __FileSystemInfo
	{
        // tested by ?
        public abstract void Delete();

		public abstract bool Exists { get; }

		public virtual string FullName
		{
			get
			{
				return "";
			}
		}

		public abstract string Name { get; }

        public override string ToString()
        {
            return this.FullName;
        }
    }


}
