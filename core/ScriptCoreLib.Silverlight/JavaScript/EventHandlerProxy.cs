using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.System;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using ScriptCoreLib.Shared.Query;


namespace ScriptCoreLib.JavaScript.Silverlight
{
    [Script]
    public static class EventHandlerProxy
    {
        static readonly List<Item> Cache = new List<Item>();
        static int Counter = 1;

        [Script]
        public class Item
        {
            public int Index;

            public global::System.Delegate Handler;

            public string FunctionName
            {
                get { return "_EventHandlerProxy$$" + Index; }
            }

 
        }

        static bool DelegateEquals(global::System.Delegate a, global::System.Delegate b)
        {
            // there is a bug within jsc compiler that does not allow to use
            // virtual methods with the native types that are redirected to
            // scriptcorelib implementations

            // the other bug is that when == operator is defined, one cannot compare the reference to null anymore
            // this is why such an ugly hack is now neccesary

            var _a = (DelegateImpl)(object)a;
            var _b = (DelegateImpl)(object)b;

            // return DelegateImpl.IsEqual(_a, _b);

            return _a == _b;
        }

        [Script(OptimizedCode="delete {arg0}[{arg1}];", UseCompilerConstants=true)]
        static void NativeDelete(object target, string member)
        {
        }

        static public Item Remove(global::System.Delegate Handler)
        {
            var e = Cache.ToArray().Where(i => DelegateEquals(i.Handler, Handler)).GetEnumerator();
            var x = default(Item);


            if (e.MoveNext())
            {
                x = e.Current;

                Cache.Remove(x);

                NativeDelete(Native.Window, x.FunctionName);

            }

            return x;
        }

        static public string GetValueString(object Target, string Member)
        {
            return Expando.Of(Target)[Member].To<string>();
        }

        static public string GetValueStringOrDefault(object Target, string Member)
        {
            var v = default(string);

            try
            {
                v = GetValueString(Target, Member);
            }
            catch
            {

            }

            return v;
        }

        static public void SetValueString(object Target, string Member, string Value)
        {
            Expando.Of(Target).SetMember(Member, Value);
        }




        static public Item Add(global::System.Delegate Handler)
        {
            var n = new Item
                {
                    Index = Counter,
                    Handler = Handler
                };

            ((DelegateImpl)(object)(Handler)).InvokePointer.Export(n.FunctionName);

            Counter++;

            Cache.Add(n);

            return n;
        }
    }



}