
public abstract class a<T> where T : a<T>.i
{
    public interface i
    {
        T foo();
    }

    public abstract void bar(ref T t);
}

public class d : a<d.s>
{
    public struct s : a<d.s>.i
    {
        // Error	1	Struct member 'd.s.sfield' of type 'System.Nullable<d.s>' causes a cycle in the struct layout	X:\jsc.svn\examples\rewrite\Test\TestGenericInheritanceFromNestedType\TestGenericInheritanceFromNestedType\Class1.cs	16	21	TestGenericInheritanceFromNestedType
        //public d.s? sfield;

        // are we using the shadow s or the real s?
        public s foo()
        {
            throw new System.NotImplementedException();
        }
    }

    public d.s? sfield;

    public override void bar(ref d.s t)
    {

    }
}
