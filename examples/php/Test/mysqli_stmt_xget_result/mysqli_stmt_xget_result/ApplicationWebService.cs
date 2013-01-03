using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace mysqli_stmt_xget_result
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201301/20130101

            var x = new __ConsoleToDatabaseWriter(y);

            Native.API.error_reporting(-1);

            Console.WriteLine("before data");


            var m = new mysqli(
               "127.0.0.1",
               "root",
               ""
           );

            #region query
            Action<string> query =
              _sql =>
              {

                  (m.query(_sql) as mysqli_result).With(
                      r =>
                      {
                          r.close();
                      }
                  );

                  if (m.errno != 0)
                  {
                      var message = new { m.errno, m.error, _sql };
                      Console.WriteLine(message.ToString());
                  }

              };
            #endregion


            query("CREATE DATABASE IF NOT EXISTS `datasource1001`");
            query("use `datasource1001`");


            {
                #region create table.sql
                var sql = @"
create table if not exists TheGridTable 
(
    ContentKey INTEGER PRIMARY KEY AUTOINCREMENT
    , ContentValue text not null
    , ContentComment text not null
    , ParentContentKey INTEGER 
    , FOREIGN KEY(ParentContentKey) REFERENCES TheGridTable (ContentKey)
) 
";
                sql = ScriptCoreLib.PHP.Data.SQLiteToMySQLConversion.Convert(sql);
                #endregion

                query(sql);
            }

            {
                var sql = @"
insert into TheGridTable (ContentValue, ContentComment, ParentContentKey) values (
    ? /* text */
    , ?  /* text */
    , ? 
)
";

                (m.prepare(sql) as mysqli_stmt).With(
                    stmt =>
                    {
                        Console.WriteLine("in prepare " + new { sql });

                        string n = null;

                        stmt.bind_param_array("sss",
                              "a ContentValue",
                              "a ContentComment",
                              n
                          );

                        stmt.execute();

                        var id = m.insert_id;

                        Console.WriteLine("new item " + new { id });


                        (m.prepare(sql) as mysqli_stmt).With(
                            cstmt =>
                            {
                                //Console.WriteLine("in prepare " + new { sql });

                                //string n = null;

                                stmt.bind_param_array("ssi",
                                      "a ContentValue",
                                      "a ContentComment",
                                      id
                                  );

                                stmt.execute();

                                var cid = m.insert_id;

                                Console.WriteLine("new child item " + new { cid });

                            }
                        );
                    }
                );
            }

            {
                var sql = @"
select
        t1.ContentKey
        , t1.ContentValue
        , t1.ContentComment
        , (select count(*) from TheGridTable t2 where t2.ParentContentKey = t1.ContentKey) as ContentChildren 
            from TheGridTable t1


-- best way             
where (t1.ParentContentKey is null and (? is null or ? = '')) or t1.ParentContentKey = ?
";
                Console.WriteLine("x before select");

                var stmt = (m.prepare(sql) as mysqli_stmt);

                if (m.errno != 0)
                {
                    var message = new { m.errno, m.error, sql };
                    Console.WriteLine(message.ToString());
                }

                if (stmt != null)
                {
                    Console.WriteLine("x in prepare " + new { sql });

                    var aa = new List<object>();
                    //aa.Add(null);

                    // what if android cannot handle nulls?
                    aa.Add("");
                    aa.Add("");
                    aa.Add("");
                    var aaa = aa.ToArray();

                    // will it work?
                    stmt.bind_param_array("sss",
                        aaa
                    );

                    stmt.execute();

                    stmt.store_result();

                    Console.WriteLine("x store_result " + new { stmt.num_rows, stmt.field_count });


                    for (int i = 0; i < stmt.num_rows; i++)
                    {
                        var a = stmt.__fetch_array();


                        var ContentKey = (int)a["ContentKey"];
                        var ContentValue = (string)a["ContentValue"];
                        var ContentComment = (string)a["ContentComment"];
                        var ContentChildren = (int)a["ContentChildren"];

                        //var message = new { i, a = Native.DumpToString(a) };
                        var message = new { i, a = new { ContentKey, ContentValue, ContentComment, ContentChildren } };

                        Console.WriteLine(message.ToString());

                        if (ContentChildren > 0)
                        {


                            (m.prepare(sql) as mysqli_stmt).With(
                                cstmt =>
                                {
                                    cstmt.bind_param_array("iii",
                                        ContentKey,
                                        ContentKey,
                                        ContentKey
                                    );

                                    cstmt.execute();
                                    cstmt.store_result();
                                    Console.WriteLine("x child store_result " + new { cstmt.num_rows, cstmt.field_count });
                                    cstmt.free_result();
                                }
                            );
                        }

                    }
                    //reader.dispose
                    stmt.free_result();


                }
            }




            Console.WriteLine("all done");

            x.Dispose();
        }


    }

    static class X
    {
        // http://php.net/manual/en/mysqli-stmt.fetch.php
        [Script(OptimizedCode = @"

        $data = mysqli_stmt_result_metadata($stmt);
        $fields = array();
        $out = array();

        $fields[0] = &$stmt;
        $count = 1;

        while($field = mysqli_fetch_field($data)) {
            $fields[$count] = &$out[$field->name];
            $count++;
        }
        
        call_user_func_array('mysqli_stmt_bind_result', $fields);
        mysqli_stmt_fetch($stmt);
        return $out;

")]
        public static ScriptCoreLib.PHP.Runtime.IArray __fetch_array(this mysqli_stmt stmt)
        {

            return null;
        }


    }



    class __ConsoleToDatabaseWriter : TextWriter, IDisposable
    {
        protected override void Dispose(bool disposing)
        {
            var x = Native.API.ob_get_contents();
            Native.API.ob_end_clean();
            Console.WriteLine(x);


            Console.SetOut(o);

            // base calls broken for PHP?
            //base.Dispose(disposing);
        }

        public Action<string> AtWrite;

        public override void Write(string value)
        {
            Console.SetOut(o);
            AtWrite(value);
            Console.SetOut(this);
        }

        public override void WriteLine(string value)
        {
            var x = sw.ElapsedMilliseconds + "ms " + value;
            Console.SetOut(o);
            AtWrite(x + Environment.NewLine);
            Console.SetOut(this);
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        Stopwatch sw;

        public __ConsoleToDatabaseWriter(Action<string> xAtWrite)
        {
            sw = new Stopwatch();
            sw.Start();

            InitializeAndKeepOriginal(this);

            this.AtWrite += xAtWrite;
        }


        public static void InternalWriteLine(string x)
        {
            InternalWrite(x + Environment.NewLine);
        }

        public static void InternalWrite(string x)
        {
#if DEBUG
            var i = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //o.Write(new { session, id, x });
            //#endif

            //Log.i("ConsoleByCookie", x);

            if (o == null)
                Console.Out.Write(x);
            else
                o.Write(x);
            //#if DEBUG

            Console.ForegroundColor = i;
#endif
        }

        static TextWriter o;

        private static TextWriter InitializeAndKeepOriginal(__ConsoleToDatabaseWriter w)
        {
            // Console is not really thread safe!
            if (o == null)
                o = Console.Out;

            Console.SetOut(w);

            Native.API.ob_start();

            return o;
        }
    }
}
