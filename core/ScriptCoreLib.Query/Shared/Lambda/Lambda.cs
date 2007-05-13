using ScriptCoreLib;

namespace ScriptCoreLib.Shared.Lambda
{
    [Script]
    public static partial class Lambda
    {

        public static Func<T> FixParam<A, T>(this Func<A, T> f, A a)
        {
            return () => f(a);
        }

        public static Func<A, T> FixParam<A, B, T>(this Func<A, B, T> f, B b)
        {
            return (a) => f(a, b);
        }

        #region FixFirstParam

        public static Func<B, T> FixFirstParam<A, B, T>(this Func<A, B, T> f, A a)
        {
            return (b) => f(a, b);
        }

        public static Action<B> FixFirstParam<A, B>(this Action<A, B> f, A a)
        {
            return (b) => f(a, b);
        }

        #endregion

        #region FixFirstParam

        public static Func<A, T> FixLastParam<A, B, T>(this Func<A, B, T> f, B b)
        {
            return (a) => f(a, b);
        }

        public static Action<A> FixLastParam<A, B>(this Action<A, B> f, B b)
        {
            return (a) => f(a, b);
        }

        public static Func<A, B, T> FixLastParam<A, B, C, T>(this Func<A, B, C, T> f, C c)
        {
            return (a, b) => f(a, b, c);
        }

        public static Action<A, B> FixLastParam<A, B, C>(this Action<A, B, C> f, C c)
        {
            return (a, b) => f(a, b, c);
        }

        #endregion

        public static Action<A> AsAction<A, T>(this Func<A, T> f)
        {
            return (a) => f(a);
        }

        public static Action<int, int> WithOffset(this Action<int, int> f, int x, int y)
        {
            return (ix, iy) => f(ix + x, iy + y);
        }
    }

    [Script]
    public delegate Func<A, T> YFunc<A, T>(Func<A, T> e);

    [Script]
    public delegate Func<A, B, T> YFunc<A, B, T>(Func<A, B, T> e);



    partial class Lambda
    {
        public static Func<A, T> Y<A, T>(this YFunc<A, T> le)
        {
            var me = default(Func<A, T>); return me = (a) => le(me)(a);
        }

        public static Func<A, B, T> Y<A, B, T>(this YFunc<A, B, T> le)
        {
            var me = default(Func<A, B, T>); return me = (a, b) => le(me)(a, b);
        }


    }



    [Script]
    public delegate Action<A> YAction<A>(Action<A> e);

    [Script]
    public delegate Action<A, B> YAction<A, B>(Action<A, B> e);



    partial class Lambda
    {
        public static Action<A> Y<A>(this YAction<A> le)
        {
            var me = default(Action<A>); return me = (a) => le(me)(a);
        }

        public static Action<A, B> Y<A, B>(this YAction<A, B> le)
        {
            var me = default(Action<A, B>); return me = (a, b) => le(me)(a, b);
        }
    }

    partial class Lambda
    {
        public static TSeed Aggregate<TSeed>(this TSeed e, Action<TSeed> a)
        {
            a(e);

            return e;
        }
    }
}
