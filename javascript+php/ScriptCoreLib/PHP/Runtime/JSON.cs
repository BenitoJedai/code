using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.PHP;

namespace ScriptCoreLib.PHP.Runtime
{
    [Script]
    public class JSON
    {
        [Script(InternalConstructor = true)]
        class JSONImplementation
        {
            #region Constructor

            public JSONImplementation(int use)
            {
                // InternalConstructor
            }


            [Script(OptimizedCode = @"return new Services_JSON($use);")]
            static JSON InternalConstructor(int use)
            {

                return default(JSON);
            }

            #endregion


            public string encode(object e)
            {
                return default(string);
            }

            public object decode(string e)
            {
                return default(object);
            }

        }

        public static IArray DecodeArray(string e)
        {
            JSONImplementation j = new JSONImplementation(16);

            return (IArray)j.decode(e);
        }


        public static TReturn Decode<TReturn>(string d)
            where TReturn : class
        {
            JSONImplementation j = new JSONImplementation(0);

            return (TReturn)j.decode(d);
        }

        public static string EncodeArray(IArray e)
        {
            JSONImplementation j = new JSONImplementation(0);

            return j.encode(e);
        }

        public static string Encode<T>(T e)
        {
            JSONImplementation j = new JSONImplementation(16);

            return j.encode(e);
        }

        public static string EncodeToBase64String<T>(T e)
        {
            return Convert.ToBase64String(Encode(e));
        }



        public static TReturn DecodeFromBase64String<TReturn>(string data)
            where TReturn : class
        {
            return Decode<TReturn>(Convert.FromBase64String(data));
        }
    }
}

