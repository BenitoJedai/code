using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestEncryptedPrivateFields
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public ClassWithEncryptedFields SpecialData = new ClassWithEncryptedFields();
    }

    public class ClassWithEncryptedFields(private string PrivateField = "were we able to keep it private across machines?")
    {
        public string PublicField;


        public void ToConsole()
        {
            // Error	1	Parameters of a primary constructor can only be accessed in instance variable initializers and arguments to the base constructor.	X:\jsc.svn\examples\javascript\Test\TestEncryptedPrivateFields\TestEncryptedPrivateFields\ApplicationWebService.cs	49	36	TestEncryptedPrivateFields

            // since it was transmitted as public, is the client allowed to read it?
            // jsc would need to see if the private is ever read on the client.
            // if it is not. it can be encrypted and sent back later
            // otherwise jsc may need to decrypt it in client?

            // 0:32ms {{ PublicField = null, PrivateField = were we able to keep it private across machines? }}

            Console.WriteLine(

                new { PublicField, this.PrivateField }

                );
        }
    }
}
