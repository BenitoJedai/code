using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.Collections.ArrayList))]
    internal class __ArrayList
    {
        readonly IArray<object> InternalList = new IArray<object>();

        public virtual int Add(object e)
        {
            InternalList.push(e);

			return InternalList.length - 1;
        }

		public int IndexOf(object e)
		{
			return InternalList.indexOf(e);
		}

		public virtual int Count
		{
			get
			{
				return InternalList.length;
			}
		}

		public virtual object this[int index]
		{
			get
			{
				return InternalList[index];
			}
			set
			{
				InternalList[index] = value;
			}
		}
    }
}
