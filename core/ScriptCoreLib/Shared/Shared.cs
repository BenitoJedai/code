using Serializable = System.SerializableAttribute;

namespace ScriptCoreLib.Shared
{
    [Script]
    public delegate TRet EventHandler<TRet, var0, var1>(var0 e, var1 p);
    [Script]
    public delegate void EventHandler<var0, var1>(var0 e, var1 p);
    [Script]
    public delegate void EventHandler<var0>(var0 e);
    [Script]
    public delegate void EventHandler();

    [Script]
    public delegate void Action();

    [Script]
    public delegate void Action<A>(A a);

    [Script]
    public delegate void Action<A, B>(A a, B b);

    [Script]
    public delegate void Action<A, B, C>(A a, B b, C c);

    [Script]
    public delegate void ActionParams<T>(params T[] e);
       
    [Script]
    public delegate R FuncParams<T, R>(params T[] e);


    [Script]
    public delegate T Func<T>();

    [Script]
    public delegate T Func<A, T>(A a);

    [Script]
    public delegate T Func<A, B, T>(A a, B b);

    [Script]
    public delegate T Func<A, B, C, T>(A a, B b, C c);



    [Script]
    public class ConvertTo<TIn, TOut> : Predicate<TIn, TOut>
    {
        public EventHandler<Predicate<TIn, TIn>> TargetInComparer;



        public TOut this[TIn e]
        {
            set
            {
                if (Predicate.Invoke(this.TargetIn, e, TargetInComparer))
                {
                    this.TargetOut = value;
                    this.Value = true;
                }
            }
        }

        public void Invoke(EventHandler<ConvertTo<TIn, TOut>> h)
        {
            Helper.Invoke(h, this);
        }

        public static TOut Convert(TIn v, EventHandler<ConvertTo<TIn, TOut>> h)
        {
            var c = new ConvertTo<TIn, TOut>();

            c.TargetIn = v;

            c.Invoke(h);

            return c.TargetOut;
        }
    }

    [Script]
    public class Predicate<TIn, TOut> : Predicate
    {
        public TIn TargetIn;
        public TOut TargetOut;

        public static bool Invoke(TIn a, TOut b, EventHandler<Predicate<TIn, TOut>> h)
        {
            var p = Predicate<TIn, TOut>.Of(a, b);

            p.Invoke(h);

            return p.Value;
        }

        public static Predicate<TIn, TOut> Of(TIn a, TOut b)
        {
            var p = new Predicate<TIn, TOut>();

            p.TargetIn = a;
            p.TargetOut = b;

            return p;
        }


        public void Invoke(EventHandler<Predicate<TIn, TOut>> h)
        {
            Helper.Invoke(h, this);
        }
    }

    [Script]
    public class Predicate<T> : Predicate
    {
        public T Target;


        public void Invoke(EventHandler<Predicate<T>> h)
        {
            Helper.Invoke(h, this);
        }

        public static implicit operator T(Predicate<T> p)
        {
            return p.Target;
        }
    }

    [Script]
    public class Predicate
    {
        public bool Value;

        public void Invoke(EventHandler<Predicate> h)
        {
            Helper.Invoke(h, this);
        }



        public static bool Is(EventHandler<Predicate> h)
        {
            return Is(h, false);
        }

        public static bool Is(EventHandler<Predicate> h, bool value)
        {
            var p = new Predicate();

            p.Value = value;

            p.Invoke(h);

            return p.Value;
        }

        public static bool Invoke<T>(T a, EventHandler<Predicate<T>> h)
        {
            var p = new Predicate<T>();

            p.Target = a;
            p.Invoke(h);

            return p.Value;
        }

        public static bool Invoke<TIn, TOut>(TIn a, TOut b, EventHandler<Predicate<TIn, TOut>> h)
        {
            var p = Predicate<TIn, TOut>.Of(a, b);

            p.Invoke(h);

            return p.Value;
        }

        public static implicit operator bool(Predicate e)
        {
            return e.Value;
        }


    }

    [Script]
    public class JSONBase
    {
        public const string Protocol = "json://";

    }

    [Script]
    public class Pair<TA, TB>
    {
        public TA A;
        public TB B;



        public Pair(TA a, TB b)
        {
            this.A = a;
            this.B = b;
        }
    }

    [Script]
    public class Pair<T>
    {
        public T A;
        public T B;
    }

    [Serializable]
    [Script]
    public class MyTransportDescriptor<TType> : JSONBase
    {
        /// <summary>
        /// callback id, so we can send back info, if file upload
        /// </summary>
        public string Callback;

        /// <summary>
        /// desribes what kind of transport this is
        /// </summary>
        public string Description;

        /// <summary>
        /// user defined data
        /// </summary>
        public TType Data;
    }


    [Script(IsStringEnum = true)]
    public enum HTMLInputTypeEnum
    {
        text, password, checkbox, radio, submit, reset, file, hidden, image, button,
        /// <summary>
        /// this is not actually a part of html input tag, but denotes to multiline text
        /// </summary>
        textarea
    }

    [Script(IsStringEnum = true)]
    public enum HTTPMethodEnum
    {
        GET, POST, HEAD
    }
}
