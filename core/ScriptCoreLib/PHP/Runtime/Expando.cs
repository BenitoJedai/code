using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Serialized;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class MethodInfo
    {
        public string Name;

        public static MethodInfo Of(string name)
        {
            MethodInfo n = new MethodInfo();

            n.Name = name;

            return n;
        }

        public bool Exists
        {
            get
            {
                return Native.API.function_exists(Name);
            }
        }

        [Script(OptimizedCode=@"return $method($e);")]
        internal static TReturn InternalInvoke<TReturn, TArg0>(string method, TArg0 e)
        {
            return default(TReturn);
        }

        public TReturn Invoke<TReturn, TArg0>(TArg0 e)
        {
            return InternalInvoke<TReturn, TArg0>(Name, e);
        }
    }

    [Script(InternalConstructor = true)]
    public class Expando<TMember> : Expando
    {


        #region Constructor

        public Expando()
        {
            // InternalConstructor
        }


        static Expando<TMember> InternalConstructor()
        {
            return new Expando().To<Expando<TMember>>();
        }

        #endregion



        public new static Expando<TMember> Of(object e)
        {
            return Expando.Of(e).To<Expando<TMember>>();
        }

        public new TMember this[object e]
        {
            [Script(DefineAsStatic = true)]
            set
            {
                Expando.SetArrayMember(this, e, value);
            }
            [Script(DefineAsStatic = true)]
            get
            {
                return (TMember)Expando.GetArrayMember(this, e);
            }
        }
    }


    
    [Script(InternalConstructor=true)]
    public class Expando
    {
        public static void ResolveDualNotation<TType>(DualNotation<TType> dualNotation)
            where TType:class
        {
            if (dualNotation.Target == null)
            {
                dualNotation.Target = (TType)Expando.FromJSON<TType>(dualNotation.Stream, dualNotation.IsBase64);
            }
        }

        private static TReturn FromJSON<TReturn>(string stream, bool base64)
            where TReturn : class
        {
            if (base64)
                return JSON.DecodeFromBase64String<TReturn>(stream);

            return JSON.Decode<TReturn>(stream);
        }


        public Expando()
        {
        }

        [Script(OptimizedCode="return array();")]
        public static Expando InternalConstructor()
        {
            return default(Expando);
        }

        public static class API
        {
            #region string gettype ( mixed var )

            /// <summary>  
            /// Returns the type of the PHP variable var.   
            /// </summary>  
            /// <param name="_var">mixed var</param>  
            [Script(IsNative = true)]
            public static string gettype(object _var) { return default(string); }

            #endregion

        }



        [Script(DefineAsStatic = true)]
        [ScriptParameterByVal]
        public IArray<string> GetMembers()
        {
            return Native.API.get_object_vars(this);
        }

        [Script(DefineAsStatic = true)]
        public string GetValue()
        {
            return this.To<string>();
        }

        public string Literal
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (IsString)
                {
                    string z = "";
                    string e = this.GetValue();

                    foreach (char c in e)
                    {
                        z += @"\x" + Convert.ToHexString(c);
                    }

                    return z;
                }

                return null;

            }
        }

        
        #region typestring

        // http://ee.php.net/manual/en/function.gettype.php

        public bool IsBoolean
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "boolean";
            }
        }

        public bool IsDouble
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "double";
            }
        }

        public bool IsInteger
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "integer";
            }
        }
        public bool IsString
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "string";
            }
        }

        public bool IsNumber
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return IsInteger || IsDouble;
            }
        }

        public bool IsArray
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "array";
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

        public bool IsResource
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "resource";
            }
        }

        public bool IsNull
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return TypeString == "NULL";
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
                return API.gettype(this);
            }
        }
        #endregion

        public static Expando Of(object e)
        {

            return (Expando)e;
        }

		// http://php.net/manual/en/function.array-key-exists.php
		// 5.3.0	 This function doesn't work with objects anymore, property_exists() should be used in this case.

        [Script(OptimizedCode = @"return @array_key_exists($i, $o) ? $o[$i] : NULL;")]
        internal static object GetArrayMember(object o, object i)
        {
            return default(object);
        }

        [Script(OptimizedCode = @"$o[$i] = $v;")]
        internal static void SetArrayMember([ScriptParameterByRef] object o, object i, object v)
        {
        }

        [Script(DefineAsStatic = true)]
        public T To<T>()
        {

            return (T)((object)this);
        }


        /// <summary>
        /// copy database array to a type, also convert to utf-8
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="p"></param>
        /// <param name="pv"></param>
        /// <returns></returns>
        public static TReturn Copy<TReturn>(TReturn p, IArray pv)
            where TReturn : class
        {
            TReturn r = p;

            if (pv == null)
            {
                r = null;
            }
            else
            {
                foreach (object x in pv.Keys.ToArray())
                {
                    Expando v = Expando.Of(pv[x]);

                    if (v.IsString)
                    {
                        Expando.SetTypeMember(r, x,Convert.ToUTF8(v.GetValue(), true));
                    }
                    else
                        Expando.SetTypeMember(r, x, v);
                }

            }

            return r;
        }

        [Script(OptimizedCode=@"$e->$m = $v;")]
        private static void SetTypeMember([ScriptParameterByRef] object e, object m, object v)
        {
        }


        [Script(OptimizedCode=@"return clone($e);")]
        private static object InternalClone(object e)
        {
            return default(object);
        }


        [Script(DefineAsStatic=true)]
        public object Clone()
        {
            return InternalClone(this);
        }

        [Script(DefineAsStatic = true)]
        public string GetAnsiValue()
        {
            return  Native.API.iconv("UTF-8", "ISO-8859-1", this.GetValue());
        }

        [Script(DefineAsStatic = true)]
        public object[] ToArray()
        {
            return To<object[]>();
        }
    }
}
