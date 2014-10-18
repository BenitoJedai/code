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
using System.Data.SQLite;

namespace WebCamAvatarsExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //        looking for referenced assemblies to be deployed for WebService...
        //        WebCamAvatarsExperiment.ApplicationWebService
        //        <> f__AnonymousType0`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //<>f__AnonymousType1`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //<>f__AnonymousType2`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //<>f__AnonymousType3`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        //found dependancy: ScriptCoreLib.Extensions.dll


        public void Insert0(string base64)
        {
            Console.WriteLine(new { base64.Length });
            // { Length = 52398 }
        }



        // ctor shall be invoked for page builder
        // add a new button
        // add onclick

        public void Reset()
        {
            //new global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatars.Sheet1.Queries().WithConnection(
            //    c =>
            //    {
            //        #region drop
            //        Action<string> dropsql = sql =>
            //        {
            //            try
            //            {
            //                Console.WriteLine(new { sql });
            //                var xvalue = new System.Data.SQLite.SQLiteCommand(sql, c).ExecuteNonQuery();
            //            }
            //            catch
            //            {
            //            }
            //        };
            //        #endregion


            //        dropsql(global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatars.Sheet1.Queries.DropCommandText);

            //        return "".AsResult();
            //    }
            //);

        }

        public void Insert(Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row y)
        {
            Console.WriteLine("Insert!!");

            //DateTimeConvertFromString { e = 1388579900081 }
            //DateTimeConvertFromInt64 { Kind = Utc, value = 1/1/2014 12:38:20 PM }
            //{ Length = 58370 }

            if (y.Avatar96gif != null)
                Console.WriteLine(new { Avatar96gif = y.Avatar96gif.Length });


            // http://dev.mysql.com/doc/refman/5.0/en/storage-requirements.html
            if (y.Avatar640x480 != null)
                Console.WriteLine(new { Avatar640x480 = y.Avatar640x480.Length });

            //Insert!!
            //{ Avatar96gif = 48946 }
            //{ Avatar640x480 = 566890 }

            // 566890 bytes needs a blob!
            y.Avatar640x480 = null;

            //X:\jsc.svn\examples\javascript\DropFileIntoSQLite\DropFileIntoSQLite\Schema\Table1\create.sql

            try
            {
                var ref0 = typeof(SQLiteConnection);

                // Caused by: java.lang.RuntimeException: Unknown column 'Tag' in 'field list'
                //at ScriptCoreLibJava.BCLImplementation.System.Data.SQLite.__SQLiteCommand.ExecuteNonQuery(__SQLiteCommand.java:277)
                //at Abstractatech.JavaScript.Avatar.Design.WebCamAvatars_Sheet1_Queries.Insert(WebCamAvatars_Sheet1_Queries.java:68)
                //at Abstractatech.JavaScript.Avatar.Design.WebCamAvatars_Sheet1__Insert_closure.yield(WebCamAvatars_Sheet1__Insert_closure.java:25)

                // Caused by: java.sql.SQLException: Data truncation: Data too long for column 'Avatar640x480' at row 1

                //var avatars = new global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatars.Sheet1();


                //var key = avatars.Insert(y);

                //var c = avatars.Count();

                var c = 0;


                Console.WriteLine(
                    new { c }
                    );
            }
            catch
            {
                // +		$exception	{"Could not load file or assembly 'System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.":"System.Data.XSQLite, Version=3.7.7.1, Culture=neutral, PublicKeyToken=null"}	System.Exception {System.IO.FileNotFoundException}
                // what the flip
                Debugger.Break();
            }

            //          about to load params for { WebMethod = { IsConstructor = false, MetadataToken = 06000002, Name = Insert, TypeFullName = WebCamAvatarsExperiment.ApplicationWebService, Parameters = 1 } }
            //enter invoke { WebMethod = { IsConstructor = false, MetadataToken = 06000002, Name = Insert, TypeFullName = WebCamAvatarsExperiment.ApplicationWebService, Parameters = 1 } }
            //enter NewGlobalInvokeMethod
            //check NewGlobalInvokeMethod { Name = Insert0 }
            //check NewGlobalInvokeMethod { Name = Insert }
            //enter NewGlobalInvokeMethod { Name = Insert }
            //before call NewGlobalInvokeMethod { Name = Insert }
            //enter { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before xml parse { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before ElementsToFields { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //ElementsToFields { Name = Key }
            //ElementsToFields { Name = Avatar640x480 }
            //ElementsToFields { Name = Avatar96gif }
            //ElementsToFields { Name = Avatar96frame0 }
            //ElementsToFields { Name = Avatar96frame1 }
            //ElementsToFields { Name = Avatar96frame2 }
            //ElementsToFields { Name = Avatar96frame3 }
            //ElementsToFields { Name = ExternalKey }
            //ElementsToFields { Name = Tag }
            //ElementsToFields { Name = Timestamp }
            //DateTimeConvertFromString { e = 1388586659840 }
            //DateTimeConvertFromInt64 { Kind = 0, value = 01.01.2014 16:30:59 }
            //Insert
            //java.lang.NullPointerException

            //before call NewGlobalInvokeMethod { Name = Insert }
            //enter { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before xml parse { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //before ElementsToFields { ConvertTypeName = Abstractatech.JavaScript.Avatar.ConvertToString$2$<0200001c> }
            //ElementsToFields { Name = Key }
            //ElementsToFields { Name = Avatar640x480 }


        }
    }
}
