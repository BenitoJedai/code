//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using System;

using global::System.Collections.Generic;



namespace GGearAlpha.js
{
    [Script]
    public static class GoogleGearsFactoryExtensions
    {
        static public IEnumerable<string[]> GetColumns(this GoogleGearsFactory.IRecordset rs, int columns)
        {
            var a = new List<string[]>();

            while (rs.isValidRow())
            {
                var x = new string[columns];

                for (int i = 0; i < columns; i++)
                {
                    x[i] = rs.field(i);
                }

                a.Add(x);

                rs.next();
            }

            rs.close();

            return a;
        }
    }

    [Script(InternalConstructor = true)]
    public class GoogleGearsFactory
    {
        [Script(InternalConstructor = true)]
        public class IRecordset
        {
            public bool isValidRow()
            {
                throw new Exception();
            }

            public void next()
            {
            }

            public void close()
            {

            }

            public string field(int i)
            {
                throw new Exception();
            }
        }

        [Script(InternalConstructor = true)]
        public class Database
        {
            public void open(string dbname)
            {
            }

            public IRecordset execute(string command, params object[] parameters)
            {
                throw new Exception();
            }

            public Database()
            {

            }

            internal static Database InternalConstructor()
            {
                return GoogleGearsFactory.Default.create("beta.database", "1.0");
            }

        }

        static GoogleGearsFactory _Default;

        public static GoogleGearsFactory Default
        {
            get
            {
                if (_Default == null)
                    _Default = new GoogleGearsFactory();

                return _Default;
            }
        }

        public GoogleGearsFactory()
        {

        }

        public Database create(string id, string version)
        {
            throw new Exception();
        }

        internal static GoogleGearsFactory InternalConstructor()
        {
            object r = null;

            try
            {
                r = IFunction.Of("GearsFactory").CreateType();
            }
            catch
            {
                try
                {
                    r = new ScriptCoreLib.JavaScript.DOM.IActiveX("Gears.Factory");
                }
                catch
                {

                }
            }

            if (r == null)
                throw new System.Exception("Google Gears is not installed or not supported!");

            return (GoogleGearsFactory)r;

            //// Firefox
            //if (typeof GearsFactory != 'undefined') {
            //  factory = new GearsFactory();
            //} else {
            //  // IE
            //  try {
            //    factory = new ActiveXObject('Gears.Factory');
            //  } catch (e) {
            //    // Safari
            //    if (navigator.mimeTypes["application/x-googlegears"]) {
            //      factory = document.createElement("object");
            //      factory.style.display = "none";
            //      factory.width = 0;
            //      factory.height = 0;
            //      factory.type = "application/x-googlegears";
            //      document.documentElement.appendChild(factory);
            //    }
            //  }
            //}
        }
    }



    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            // this ctor creates a new div which has a text and a button element
            // on mouseover over the color text is changed
            // on pressing the button the next message in text element is displayed


            IHTMLDiv Control = new IHTMLDiv();


            DataElement.insertNextSibling(
                Control

            );

            Func<string, string, IHTMLElement> link = (href, text) => new IHTMLDiv(new IHTMLAnchor(href, text));


            Control.appendChild(link("http://gears.google.com/", "Google Gears"));
            Control.appendChild(link("http://code.google.com/apis/gears/samples/hello_world_database.html", "Google Example # 1"));

            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h3, "This page uses Google Gears to record your entries on the local disk. If you navigate away and revisit this page, all your data will still be here. Try it!"));

            // http://code.google.com/apis/gears/samples/hello_world_database.html

            GoogleGearsFactory.Database db = null;

            try
            {
                db = new GoogleGearsFactory.Database();
            }
            catch (Exception exc)
            {
                var err = new IHTMLCode(exc.Message);

                err.style.color = Color.Red;


                Control.appendChild(err);
            }

            if (db != null)
            {
                db.open("demo1");
                db.execute(@"
    create table if not exists Demo
    (Phrase varchar(255), Timestamp int)
            ");

                var textfield = new IHTMLInput(HTMLInputTypeEnum.text, "text1", "");

                var btnadd = new IHTMLButton("Add new entry");
                var btnclear = new IHTMLButton("Clear");

                Control.appendChild(textfield, btnadd, btnclear);



                var list = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol);

                Control.appendChild(list);

                Action read = delegate
                {
                    //from i in Demo
                    //select new { Phrase, Timestamp }
                    //order by Timestamp desc

                    list.removeChildren();

                    foreach (var v in db.execute("select Phrase, Timestamp from Demo order by Timestamp desc").GetColumns(2))
                    {
                        list.appendChild(
                             new IHTMLElement(IHTMLElement.HTMLElementEnum.li,
                                          v[1] + " - " + v[0]
                             )
                         );
                    }

                    // raw style
                    /*
                    var rs = db.execute("select * from Demo order by Timestamp desc");

                    while (rs.isValidRow())
                    {
                        list.appendChild(
                            new IHTMLElement(IHTMLElement.HTMLElementEnum.li,
                                         rs.field(1) + " - " + rs.field(0)
                            )
                        );

                        rs.next();
                    }

                    rs.close();
                     * */
                };

                btnclear.onclick +=
                    delegate
                    {
                        db.execute("delete from Demo");

                        read();
                    };

                btnadd.onclick +=
                    delegate
                    {
                        db.execute("insert into Demo values (?, ?)", textfield.value, IDate.Now.getTime());

                        textfield.value = "";

                        read();
                    };

                read();

            }


            // not array
            // is object
            // no prototype
        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, ScriptCoreLib.Shared.EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
