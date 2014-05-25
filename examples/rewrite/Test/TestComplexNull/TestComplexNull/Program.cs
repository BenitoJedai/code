using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComplexNull
{
    class Program : IEnumerator
    {
        // 0x57 is missing stack 0x50
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.CreateMethodBaseEmitToArguments.cs
        // [0x00000001] = {[0x0057] brtrue.s   +0 -1{[0x0050] ldfld      +1 -1{[0x004f] ldarg.3    +1 -0} } }
        // 55 tries to reload stack entry
        // what about 57?
        // [0x00000001] = { Consumer0 = {[0x0057] brtrue.s   +0 -1{[0x0050] ldfld      +1 -1{[0x004f] ldarg.3    +1 -0} } }, IsConsumer = true, AllSingleStackInstruction = true }
        // well it is being considered?
        // what about instead of doing a before emit, we could do an after emit?
        // switch rewriter wants to save stack at 0x7b
        // then 0x7d, then 0x41, then 0x45 then 0x2e then 0x32, 0x1b, 0x1f

        //PEVerify [IL]: Error: [X:\jsc.svn\examples\rewrite\test\TestComplexNull\TestComplexNull\bin\Debug\xTestComplexNull.exe : TestComplexNull.Program+<MoveNext>0600000b::<0000> nop][offset 0x00000011] Stack underflow.

        //private void SerializeValue(JsonWriter writer, object value, JsonContract valueContract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
        public void MoveNext(JsonWriter writer, object value, JsonContract valueContract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            JsonConverter converter;
            if ((((converter = (member != null) ? member.Converter : null) != null)
                 || ((converter = (containerProperty != null) ? containerProperty.ItemConverter : null) != null)
                 || ((converter = (containerContract != null) ? containerContract.ItemConverter : null) != null)
                 || ((converter = valueContract.Converter) != null)
                 || ((converter = Serializer.GetMatchingConverter(valueContract.UnderlyingType)) != null)
                 || ((converter = valueContract.InternalConverter) != null))
                && converter.CanWrite)
            {
                SerializeConvertable(writer, converter, value, valueContract, containerContract, containerProperty);
                return;
            }

        }

        private void SerializeConvertable(JsonWriter writer, JsonConverter converter, object value, JsonContract valueContract, JsonContainerContract containerContract, JsonProperty containerProperty)
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
        }

        class Serializer
        {
            internal static JsonConverter GetMatchingConverter(object p)
            {
                throw new NotImplementedException();
            }
        }

        public object Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
