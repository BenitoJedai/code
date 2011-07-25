namespace com.sun.corba.se.impl.dynamicany
{
    abstract class DynAnyImpl : org.omg.DynamicAny.DynAny, org.omg.DynamicAny.DynAnyOperations
    {
        public virtual org.omg.CORBA.Any to_any()
        {
            return null;
        }
    }

    class DynAnyBasicImpl : DynAnyImpl, org.omg.DynamicAny.DynAnyOperations
    {
        public override org.omg.CORBA.Any to_any()
        {
            return null;
        }

    }
}

namespace org.omg.DynamicAny
{
    interface DynAny : DynAnyOperations
    {

    }

    interface DynAnyOperations
    {
        org.omg.CORBA.Any to_any();
    }
}

namespace org.omg.CORBA
{
    class Any
    {

    }
}