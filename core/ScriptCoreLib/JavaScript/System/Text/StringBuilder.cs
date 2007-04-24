using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Text.StringBuilder))]
    internal class StringBuilder
    {
        readonly IArray<string> data = new IArray<string>();

        public StringBuilder()
        {

        }

        public StringBuilder Append(string e)
        {
            if (e != null)
                data.push(e);

            return this;
        }

        public StringBuilder Append(object value)
        {
            if (value == null)
            {
                return this;
            }
            return this.Append(value.ToString());
        }


        public override string ToString()
        {
            return data.join();
        }



    }
}
