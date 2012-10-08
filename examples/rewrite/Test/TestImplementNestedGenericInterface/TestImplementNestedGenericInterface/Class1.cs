class c<L> where L : c<L>.i
{
    //L s;

    public interface i
    {
        L foo();
    }
}