using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var EntryPoint = typeof(Reflector.ApplicationManager).Assembly.EntryPoint;

            //at System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray(Array array, RuntimeFieldHandle fldHandle)
            //at ᜀ.ᜁ..ctor(BinaryReader A_0)
            //at ᜀ.ᜀ()
            //at ᜀ.ᜂ()
            //at ᜀ.ᜃ()
            //at Reflector.ApplicationManager.ᜀ(IWindowManager A_0)
            //at Reflector.ApplicationManager..ctor(IWindowManager windowManager)
            //at Reflector.ApplicationManager.ᜁ()

            //            IL_0015:  ldtoken    field valuetype 'ᜆ'/*020000C8*//'ᜆ'/*020000CA*/ 'ᜆ'/*010000D7*/::'ᜁ' /* 0A000070 */
            //IL_001a:  call       void [mscorlib/*23000001*/]System.Runtime.CompilerServices.RuntimeHelpers/*010000D9*/::InitializeArray(class [mscorlib/*23000001*/]System.Array/*010000C3*/,

            //            .class /*020000CA*/ explicit ansi sealed nested private 'ᜆ'
            //       extends [mscorlib/*23000001*/]System.ValueType/*010000A4*/
            //{
            //  .pack 1
            //  .size 128
            //} // end of class 'ᜆ'


            //_message	"Bad Data.\r\n"	string

            //            at System.Security.Cryptography.CryptographicException.ThrowCryptographicException(Int32 hr)
            //at System.Security.Cryptography.Utils._DecryptData(SafeKeyHandle hKey, Byte[] data, Int32 ib, Int32 cb, Byte[]& outputBuffer, Int32 outputOffset, PaddingMode PaddingMode, Boolean fDone)
            //at System.Security.Cryptography.CryptoAPITransform.TransformFinalBlock(Byte[] inputBuffer, Int32 inputOffset, Int32 inputCount)
            //at ᜀ.ᜂ()
            //at ᜀ.ᜃ()
            //at Reflector.ApplicationManager.ᜀ(IWindowManager A_0)
            //at Reflector.ApplicationManager..ctor(IWindowManager windowManager)
            //at Reflector.ApplicationManager.ᜁ()


            EntryPoint.Invoke(null, new object[0]);

        }
    }
}
