using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(InternalConstructor=true)]
    public class IActiveX : ISink
    {
        public static bool IsSupported
        {
            get
            {
                if (Expando.Of(Native.Window).Contains("ActiveXObject"))
                    return true;

                //if (Expando.Of(Native.Window).Contains("GeckoActiveXObject"))
                //    return true;

     

                return false;
            }
        }

        #region constructors


        [Script(HasNoPrototype = true, ExternalTarget = "ActiveXObject")]
        class InternalIActiveX : ISink
        {
            public InternalIActiveX(string c)
            {
				// see: http://msdn.microsoft.com/en-us/library/ms753804(VS.85).aspx
            }
        }

        //[Script(HasNoPrototype = true, ExternalTarget = "GeckoActiveXObject")]
        //class InternalIGeckoActiveX : ISink
        //{
        //    public InternalIGeckoActiveX(string c)
        //    {

        //    }
        //}

  
        static public object TryCreate(string z)
        {
            try
            {

				return new InternalIActiveX(z);
            }
            catch
            {

            }

            //try
            //{

            //    return (IActiveX)(new InternalIGeckoActiveX(z) as object);
            //}
            //catch
            //{

            //}

            return null;
        }

		public IActiveX(params string[] e)
		{

		}

		static public IActiveX InternalConstructor(params string[] e)
		{
			IActiveX r = null;

			foreach (string z in e)
			{
				r = (IActiveX)TryCreate(z);

				if (r != null)
					break;

			}

			return r;
		}
        #endregion

    }
}