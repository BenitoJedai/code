using Serializable = System.SerializableAttribute;




namespace ScriptCoreLib.Shared
{



    //[Script]
    //[System.Obsolete("System.Func<>", false)]
    //public delegate R FuncParams<T, R>(params T[] e);





    //#if BLOAT

    [Script]
    [System.Obsolete]
    public class ConvertTo<TIn, TOut> : Predicate<TIn, TOut>
    {
        public System.Action<Predicate<TIn, TIn>> TargetInComparer;



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

        public void Invoke(System.Action<ConvertTo<TIn, TOut>> h)
        {
            Helper.Invoke(h, this);
        }

        public static TOut Convert(TIn v, System.Action<ConvertTo<TIn, TOut>> h)
        {
            var c = new ConvertTo<TIn, TOut>();

            c.TargetIn = v;

            c.Invoke(h);

            return c.TargetOut;
        }
    }
    //#endif
    [Script]
    [System.Obsolete]
    public class Predicate<TIn, TOut> : Predicate
    {
        public TIn TargetIn;
        public TOut TargetOut;

        public static bool Invoke(TIn a, TOut b, System.Action<Predicate<TIn, TOut>> h)
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


        public void Invoke(System.Action<Predicate<TIn, TOut>> h)
        {
            Helper.Invoke(h, this);
        }
    }

    [Script]
    [System.Obsolete]
    public class Predicate<T> : Predicate
    {
        public T Target;


        public void Invoke(System.Action<Predicate<T>> h)
        {
            Helper.Invoke(h, this);
        }

        public static implicit operator T(Predicate<T> p)
        {
            return p.Target;
        }
    }


    [Script]
    [System.Obsolete]
    public class Predicate
    {
        public bool Value;

        public void Invoke(System.Action<Predicate> h)
        {
            Helper.Invoke(h, this);
        }



        public static bool Is(System.Action<Predicate> h)
        {
            return Is(h, false);
        }

        public static bool Is(System.Action<Predicate> h, bool value)
        {
            var p = new Predicate();

            p.Value = value;

            p.Invoke(h);

            return p.Value;
        }

        public static bool Invoke<T>(T a, System.Action<Predicate<T>> h)
        {
            var p = new Predicate<T>();

            p.Target = a;
            p.Invoke(h);

            return p.Value;
        }

        public static bool Invoke<TIn, TOut>(TIn a, TOut b, System.Action<Predicate<TIn, TOut>> h)
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
    [System.Obsolete]
    public class JSONBase
    {
        public const string Protocol = "json://";

    }

    [Script]
    [System.Obsolete]
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
    [System.Obsolete]
    public class Pair<T>
    {
        public T A;
        public T B;
    }

    [Serializable]
    [Script]
    [System.Obsolete]
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
        text,
        password, 
        checkbox, 
        radio, 
        submit, 
        reset, 
        file, 
        hidden, 
        image, 
        button, 
        date,
        email,
        tel,


        // X:\jsc.svn\examples\javascript\forms\FormsTrackBar\FormsTrackBar\ApplicationControl.cs
        range,

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
