using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestInterfaceInheritanceWithMethods;

namespace TestInterfaceInheritanceWithMethods
{
    public interface Key
    {
        void clearKey();
    }

    public interface PublicKey : Key
    {
    }
}


namespace Foo
{
    public interface IClass1 : PublicKey
    {

    }

    public class Class1 : IClass1
    {
        public void clearKey()
        {
            throw new NotImplementedException();
        }
    }
}
