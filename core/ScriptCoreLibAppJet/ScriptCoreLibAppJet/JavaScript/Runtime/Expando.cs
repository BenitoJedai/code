using ScriptCoreLib;
using ScriptCoreLibAppJet.JavaScript.DOM;


namespace ScriptCoreLibAppJet.JavaScript.Runtime
{

    [Script]
    public class ExpandoMember
    {
        public object Invoke(params object[] args)
        {
            return Self.To<IFunction>().apply(Owner, args);
        }



        public ExpandoMember(Expando o, string m)
        {
            Owner = o;
            Name = m;
        }

        public readonly Expando Owner;
        public readonly string Name;

        //public int Index
        //{
        //    get
        //    {
        //        if (Owner.IsArray)
        //        {
        //            return int.Parse(Name);
        //        }

        //        return -1;
        //    }
        //}

        public string Value
        {
            get
            {
                return Owner.GetMember<string>(Name);
            }
            set
            {
                Owner.SetMember(Name, value);
            }
        }

        public Expando TypeConstructorData
        {
            get
            {
                if (Owner.Metadata == null)
                    return null;

                return Owner.Metadata[Name];
            }
        }

        //static IFunction ConstructorOfTypeName(string e)
        //{
        //    return Expando.Of(Native.Window).GetMember<IFunction>(e);
        //}

        //public IFunction TypeConstructor
        //{
        //    get
        //    {
        //        Expando e = TypeConstructorData;

        //        if (e.IsString)
        //        {
        //            return ConstructorOfTypeName(e.GetValue());
        //        }

        //        if (e.IsArray)
        //        {
        //            return ConstructorOfTypeName(e[0].GetValue());
        //        }

        //        return null;
        //    }
        //}

        /// <summary>
        /// returns the member object as an expando
        /// </summary>
        public Expando Self
        {
            get
            {
                return Owner.GetMember<Expando>(Name);
            }
        }

        public void CopyTo(Expando e)
        {
            e[Name] = Self;
        }
    }


    [Script(InternalConstructor = true)]
    public class Expando<TIndex, TMember> : Expando
    {
        #region Constructor

        public Expando()
        {
            // InternalConstructor
        }


        static Expando<TIndex, TMember> InternalConstructor()
        {
            return new Expando().To<Expando<TIndex, TMember>>();
        }

        #endregion

        public new static Expando<TMember, TMember> Of(object e)
        {
            return Expando.Of(e).To<Expando<TMember, TMember>>();
        }

        public new TMember this[TIndex e]
        {
            [Script(DefineAsStatic = true)]
            set
            {
                Expando.InternalSetMember(this, e, value);
            }
            [Script(DefineAsStatic = true)]
            get
            {
                return (TMember)Expando.InternalGetMember(this, e);
            }
        }




    }

    [Script(InternalConstructor = true)]
    public class Expando
    {

        [Script(OptimizedCode = "return a === b;")]
        public static bool ReferenceEquals<A, B>(A a, B b)
        {
            return false;
        }

  

        [Script(DefineAsStatic = true)]
        public Expando Clone()
        {
            return new Expando(this.GetMembers());
        }

  
     
  

        [Script(OptimizedCode = @"return (a<b)?-1:(b<a?1:0);")]
        public static int Compare(object a, object b)
        {
            return default(int);
        }

        [Script]
        public class TypeNameResolver
        {
            public readonly Expando Type;
            public string TypeName;

            public TypeNameResolver(Expando m, string t)
            {
                Type = m;
                TypeName = t;
            }
        }

        [Script]
        public class TypeActivator
        {
            public object Type;
            public readonly string TypeName;

            Expando MemberActivator = new Expando();

            public Expando TypeExpando
            {
                get
                {
                    return Expando.Of(Type);
                }
            }

            public TypeActivator(string t)
            {
                TypeName = t;
            }

            //public EventHandler<TypeActivator> this[string e]
            //{
            //    set
            //    {
            //        MemberActivator.SetMember(e, value);
            //    }
            //    get
            //    {
            //        return MemberActivator.GetMember<EventHandler<TypeActivator>>(e);
            //    }
            //}
        }

        public string TypeMetaName
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (Metadata == null)
                    return null;


                // todo: return gettype(xxx).Name
                return Metadata[ScriptAttribute.MetadataMemberTypeName].GetValue();
            }
        }

        public string TypeDefaultConstructor
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (Metadata == null)
                    return null;

                return Metadata[ScriptAttribute.MetadataMemberDefaultConstructor].GetValue();
            }
        }

        #region constructors



        public Expando Metadata
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return this[ScriptAttribute.MetadataMember];
            }
        }

        /// <summary>
        /// creates new object
        /// </summary>
        public Expando()
        {
        }

        /// <summary>
        /// creates new object and copies values
        /// </summary>
        /// <param name="m">members to be copied</param>
        public Expando(ExpandoMember[] m)
        {
        }

        /// <summary>
        /// creates new object from function
        /// </summary>
        /// <param name="ctor"></param>
        public Expando(IFunction ctor)
        {
        }

        [Script(OptimizedCode = @"return {};")]
        static Expando InternalConstructor()
        {
            return default(Expando);
        }

        static Expando InternalConstructor(ExpandoMember[] m)
        {
            Expando x = InternalConstructor();
            Expando.CopyTo(m, x);

            return x;
        }

        [Script(OptimizedCode = @"return new ctor();")]
        static Expando InternalConstructor(IFunction ctor)
        {
            return default(Expando);
        }

        #endregion


        [Script]
        public class FindArgs<T>
        {
            public bool Found = false;

            public ExpandoMember Member;

            public T Item;
        }

        /// <summary>
        /// returns the function used for construction of the type
        /// not that it is not the c# constructor as it is defined as a method
        /// </summary>
        public readonly IFunction constructor;
        public readonly Expando prototype;

        //[Script(DefineAsStatic = true)]
        //public FindArgs<T> Find<T>(EventHandler<FindArgs<T>> e)
        //{
        //    ExpandoMember[] m = GetMembers();

        //    FindArgs<T> a = new FindArgs<T>();

        //    foreach (ExpandoMember x in m)
        //    {
        //        a.Member = x;

        //        a.Item = x.Self.To<T>();

        //        e(a);

        //        if (a.Found)
        //        {
        //            break;
        //        }
        //    }

        //    return a.Found ? a : null;
        //}


        public static void CopyTo(ExpandoMember[] m, Expando to)
        {
            foreach (ExpandoMember x in m)
            {
                x.CopyTo(to);
            }
        }

        [Script(DefineAsStatic = true)]
        public string GetValue()
        {
            return this.ToString();
        }

        [Script(DefineAsStatic = true)]
        public T To<T>()
        {

            return (T)((object)this);
        }

        #region type info
        [Script(OptimizedCode = "return typeof e;")]
        internal static string InternalType(object e)
        {
            return default(string);
        }

        [Script(OptimizedCode = @"var x = []; for (var z in e) x.push(z); return x;")]
        internal static string[] InternalGetMemberNames(object e)
        {
            return default(string[]);
        }

        [Script(DefineAsStatic = true)]
        public object[] GetMemberNames()
        {
            return InternalGetMemberNames(this);
        }

        [Script(DefineAsStatic = true)]
        public ExpandoMember[] GetMembers(
            bool bString,
            bool bBoolean,
            bool bNumber,
            bool bObject,
            bool bFunction,
            bool bVoid
            )
        {
            IArray<ExpandoMember> e = new IArray<ExpandoMember>();

            foreach (string n in this.GetMemberNames())
            {
                bool ok = true;

                if (n == ScriptAttribute.MetadataMember)
                    ok = false;

                if (ok)
                {
                    ExpandoMember m = new ExpandoMember(this, n);


                    bool fString = (m.Self.IsString && bString);
                    bool fBoolean = (m.Self.IsBoolean && bBoolean);
                    bool fNumber = (m.Self.IsNumber && bNumber);
                    bool fObject = (m.Self.IsObject && bObject);
                    bool fFunction = (m.Self.IsFunction && bFunction);
                    bool fVoid = (m.Self.IsUndefined && bVoid);

                    if (fString
                        || fBoolean
                        || fNumber
                        || fObject
                        || fFunction
                        || fVoid)
                        e.push(m);
                }
            }

            return e.ToArray();
        }

        [Script(DefineAsStatic = true)]
        public ExpandoMember[] GetMembers()
        {
            IArray<ExpandoMember> e = new IArray<ExpandoMember>();

            foreach (string n in this.GetMemberNames())
                e.push(new ExpandoMember(this, n));

            return e.ToArray();
        }

        /// <summary>
        /// wont return functions
        /// </summary>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public ExpandoMember[] GetFields()
        {
            return GetMembers(true, true, true, true, false, false);
        }

        /// <summary>
        /// returns only functions
        /// </summary>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public ExpandoMember[] GetFunctions()
        {
            return GetMembers(false, false, false, false, true, false);
        }

        //public int MaxMemberNameLength
        //{
        //    [Script(DefineAsStatic = true)]
        //    get
        //    {
        //        return Helper.Max(this.GetMemberNames(), 0, ( p) => p.TargetOut = p.TargetIn.Length);
        //    }
        //}

        [Script(OptimizedCode = "return (e instanceof c);")]
        internal static bool InternalIsInstanceOf(object e, object c)
        {
            return default(bool);
        }

        [Script(DefineAsStatic = true)]
        public bool IsInstanceOf(object e)
        {
            return InternalIsInstanceOf(this, e);
        }

        //public bool IsArray
        //{
        //    [Script(DefineAsStatic = true)]
        //    get
        //    {
        //        return IsObject && this.IsInstanceOf(Native.Window.Array);
        //    }
        //}


        //[Script(DefineAsStatic = true)]
        //public bool IsArrayOf(object e)
        //{
        //    if (IsArray)
        //    {
        //        IArray<object> a = To<IArray<object>>();

        //        if (a.length > 0)
        //        {
        //            return IsSameType(e, a[0]);
        //        }
        //    }

        //    return false;
        //}

        #region typestring

        public bool IsString
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "string";
            }
        }

        public bool IsFunction
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "function";
            }
        }

        public bool IsBoolean
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "boolean";
            }
        }


        //public bool IsDouble
        //{
        //    [Script(DefineAsStatic = true)]
        //    get
        //    {
        //        if (!IsNumber)
        //            return false;

        //        double d = this.To<double>();

        //        return System.Math.Round(d) != d;
        //    }
        //}

        [Script(OptimizedCode = @"return e instanceof Number;")]
        public static bool IsNativeNumberObject(object e)
        {
            return default(bool);
        }

        public bool IsNumber
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (IsNativeNumberObject(this))
                    return true;

                return TypeString == "number";
            }
        }

        public bool IsObject
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "object";
            }
        }

        public bool IsUndefined
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "undefined";
            }
        }

        public bool IsNull
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return IsObject && To<object>() == null;
            }
        }

        /// <summary>
        /// returns the typeof string
        /// </summary>
        public string TypeString
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return InternalType(this);
            }
        }
        #endregion

        #endregion


        public static Expando Of(object e)
        {
            return (Expando)e;
        }

        [Script(OptimizedCode = "try { return o[m] != void(0); } catch (exc) { return 'unknown'; } ")]
        //[Script(OptimizedCode = "return m in o;")]
        public static bool InternalIsMember(object o, string m)
        {
            return default(bool);
        }

        [Script(OptimizedCode = "return o[m]")]
        internal static object InternalGetMember(object o, object m)
        {
            return default(Expando);
        }

        [Script(OptimizedCode = "o[m] = v")]
        internal static object InternalSetMember(object o, object m, object v)
        {
            return default(object);
        }

        [Script(DefineAsStatic = true)]
        public T GetMember<T>(object m)
        {
            return (T)Expando.InternalGetMember(this, m);
        }

        [Script(DefineAsStatic = true)]
        public void SetMember<T>(object m, T value)
        {
            Expando.InternalSetMember(this, m, value);
        }

        /// <summary>
        /// returns either the first or the second member, if they exists, or the default value for the given object
        /// </summary>
        /// <typeparam name="T">type of member</typeparam>
        /// <param name="o">object</param>
        /// <param name="m0">first membername</param>
        /// <param name="m1">second membername</param>
        /// <param name="def">default value</param>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        static public T GetMemberOf<T>(object o, string m0, string m1, T def)
        {
            return Expando.Of(o).GetMember<T>(m0, m1, def);
        }


        [Script(DefineAsStatic = true)]
        public T GetMember<T>(string m0, string m1, T def)
        {
            if (InternalIsMember(this, m0))
                return (T)Expando.InternalGetMember(this, m0);

            if (InternalIsMember(this, m1))
                return (T)Expando.InternalGetMember(this, m1);

            return def;
        }

        public Expando this[object e]
        {
            [Script(DefineAsStatic = true)]
            set
            {
                Expando.InternalSetMember(this, e, value);
            }
            [Script(DefineAsStatic = true)]
            get
            {
                return (Expando)Expando.InternalGetMember(this, e);
            }
        }

        public static bool IsSameType(params object[] e)
        {
            bool b = true;

            if (e.Length > 1)
            {
                IFunction c = Expando.Of(e[0]).constructor;

                for (int i = 1; i < e.Length; i++)
                    if (Expando.Of(e[i]).constructor != c)
                        return false;
            }

            return b;
        }

        [Script(OptimizedCode = "o[m]();")]
        public static void Invoke(object o, string m)
        {

        }

        [Script(DefineAsStatic = true)]
        public void Invoke(string p)
        {
            Invoke(this, p);
        }

        /// <summary>
        /// creates type based on another type and calls the default constructor
        /// </summary>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public Expando CreateType()
        {
            Expando n = this.constructor.CreateType();

            n.Invoke(this.TypeDefaultConstructor);

            return n;
        }




        //public static void ExportCallback(string name, IFunction f)
        //{
        //    System.Console.WriteLine("ExportCallback @ " + name);

        //    Expando.Of(Native.Window).SetMember(name, f);
        //}

        //public static void ExportCallback<TArg>(string name, EventHandler<TArg> h)
        //{
        //    ExportCallback(name, IFunction.Of(h));
        //}

        //public static string GetUniqueID(string e)
        //{
        //    return e + Convert.ToHexString((int)(new System.Random().Next(32000)));
        //}

        //public static void ResolveDualNotation<TType>(DualNotation<TType> dualNotation)
        //{
        //    if (dualNotation.Target == null)
        //    {
        //        dualNotation.Target = Expando.FromJSON(dualNotation.Stream, dualNotation.IsBase64).To<TType>();
        //    }
        //}

        //[Script(DefineAsStatic = true)]
        //public void ToConsole()
        //{
        //    System.Console.WriteLine("functions:");

        //    var max = 20;

        //    foreach (ExpandoMember v in this.GetFunctions())
        //    {
        //        System.Console.WriteLine(v.Name.PadLeft(max));

        //    }

        //    System.Console.WriteLine("fields:");

        //    foreach (ExpandoMember v in this.GetFields())
        //    {
        //        System.Console.WriteLine(v.Name.PadLeft(max) + " = (" + v.Self.TypeString + ")" + v.Value);

        //    }
        //}

        [Script(OptimizedCode="return (m in t);")]
        static bool InternalContains(object m, object t)
        {
            return default(bool);
        }

        [Script(DefineAsStatic = true)]
        public bool Contains(object p)
        {
            return InternalContains(p, this);
        }




        [Script(DefineAsStatic = true)]
        public void CopyTo(object e)
        {
            var x = Expando.Of(e);

            foreach (ExpandoMember v in GetMembers())
            {
                v.CopyTo(x);
            }
        }

        [Script(OptimizedCode = "delete t[key];")]
        internal static void InternalRemove(object t, object key)
        {
        }

        [Script(DefineAsStatic = true)]
        public void Remove(object key)
        {
            InternalRemove(this, key);
        }

        [Script(OptimizedCode = "for (var i in t) delete t[i];")]
        internal static void InternalRemoveAll(object t)
        {
        }

        internal void RemoveAll()
        {
            InternalRemoveAll(this);
        }
    }
}
