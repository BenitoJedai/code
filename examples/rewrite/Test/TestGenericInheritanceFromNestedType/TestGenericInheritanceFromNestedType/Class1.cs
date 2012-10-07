
public abstract class a<T>
{
    public abstract void foo(ref T t);
}

public class d : a<d.s>
{
    public struct s
    {

    }

    public override void foo(ref d.s t)
    {

    }
}
