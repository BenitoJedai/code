using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircularGenericInterfaces
{
    public class INode : IEnumerable<INode>
    {
        public IEnumerator<INode> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class CustomSwitch<TIndex> : CustomSwitch<CustomSwitch<TIndex>, TIndex>
    {
        public bool Run(TIndex p)
        {
            return false;
        }
    }

    public class CustomSwitch<TOwner, TIndex>
    {

    }
}
