using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.DOM
{
    // 20140824 WinRT ?
    [System.Obsolete("ActiveX no longer part of the web?")]
    [Script(InternalConstructor = true)]
    public class IActiveX : IEventTarget
    {
        public static bool IsSupported
        {
            get
            {
                if (Expando.Of(Native.self).Contains("ActiveXObject"))
                    return true;

                //if (Expando.Of(Native.Window).Contains("GeckoActiveXObject"))
                //    return true;



                return false;
            }
        }

        #region constructors


        [Script(HasNoPrototype = true, ExternalTarget = "ActiveXObject")]
        class InternalIActiveX : IEventTarget
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
            //TryCreate(string) : Object
            //Analysis
            //Attributes
            //Signature Types
            //Declaring Module
            //Declaring Type
            //arg.0 z : string
            //loc.0 : object
            //maxstack 6 (used 1)
            //IL Code (21)
            //0x0000 nop 
            //.try
            //0x0001 nop 
            //0x0002 . ldarg.0      c <- arg.0 z : string
            //0x0003 . newobj       [ScriptCoreLib] ScriptCoreLib.JavaScript.DOM.IActiveX+InternalIActiveX..ctor(c : string)
            //0x0008 stloc.0        loc.0 : object
            //0x0009 leave 
            //.endtry
            //0x0027 nop 
            //0x0028 . ldloc.0      loc.0 : object
            //0x0029 ret 
            //.catch object 
            //[mscorlib] System.Object
            //0x0013 pop 
            //0x0014 nop 
            //0x0015 nop 
            //0x0016 leave 
            //.endtry
            //0x0022 nop 
            //0x0023 . ldnull       null
            //0x0024 stloc.0        loc.0 : object
            //0x0025 br.s 
            //0x0009 0x0025 -> 0x0027
            //0x0027 nop 
            //0x0028 . ldloc.0      loc.0 : object
            //0x0029 ret 




            var x = default(InternalIActiveX);

            try
            {

                x = new InternalIActiveX(z);
            }
            catch
            {

            }

            //            020001ba ScriptCoreLib.JavaScript.DOM.IActiveX
            //script: error JSC1000: Method: TryCreate, Type: ScriptCoreLib.JavaScript.DOM.IActiveX; emmiting failed : System.NotSupportedException: current OpCodes.Leave cannot be understood at
            // type: ScriptCoreLib.JavaScript.DOM.IActiveX
            // offset: 0x0016

            //try
            //{

            //    return (IActiveX)(new InternalIGeckoActiveX(z) as object);
            //}
            //catch
            //{

            //}

            return x;
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