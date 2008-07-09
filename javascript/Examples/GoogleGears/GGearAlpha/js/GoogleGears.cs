//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using System;
using System.Linq;

using global::System.Collections.Generic;



namespace GGearAlpha.js
{


    [Script]
    public static class GoogleGearsFactoryExtensions
    {
        static public void DeleteAndInsert<T>(this GoogleGearsFactory.Database db, string table, T value) where T : ISerializedObject
        {
            if (value.VirtualId == null)
                throw new Exception("VirtualId is null");


            db.execute("delete from " + table + " where Id = ?", value.VirtualId);
            db.Insert(table, value);
        }

        static public void Insert(this GoogleGearsFactory.Database db, string table, object e)
        {
            var w = new global::System.Text.StringBuilder();

            w.Append("insert into ");
            w.Append(table);
            w.Append("(");

            var t = e.GetType();
            var f = t.GetFields();

            for (int i = 0; i < f.Length; i++)
            {
                if (i > 0)
                    w.Append(",");

                w.Append(f[i].Name);
            }

            w.Append(") values (");

            for (int i = 0; i < f.Length; i++)
            {
                if (i > 0)
                    w.Append(",");

                w.Append("?");
            }
            w.Append(")");

            var command = w.ToString();
            var values = f.Select(i => i.GetValue(e)).ToArray();

            db.execute(command, values);

            //db.execute("insert into Demo (Phrase, Timestamp) values (?, ?)", textfield.value, IDate.Now.getTime());
        }

        static public IEnumerable<T> AsEnumerable<T>(this GoogleGearsFactory.Database db, string command, Type e)
        {
            return
               new DynamicEnumerable<T>
               {

                   DynamicGetEnumerator = delegate
                   {
                       var rs = db.execute(command);
                       bool dirty = false;
                       T c = default(T);

                       return
                          new DynamicEnumerator<T>
                          {
                              // yet another bug within the compiler...
                              // we need to wrap this action, because the method is native
                              DynamicDispose = () => rs.close(),
                              DynamicMoveNext = delegate
                              {
                                  if (dirty)
                                      rs.next();

                                  dirty = true;

                                  return rs.isValidRow().Aggregate(
                                      valid =>
                                      {
                                          c = (T)Activator.CreateInstance(e);

                                          for (int i = 0; i < rs.fieldCount(); i++)
                                          {
                                              var n = rs.fieldName(i);
                                              var f = e.GetField(n);

                                              if (f == null)
                                                  throw new Exception("A known field is missing for the activated object. Check the implementation for Activator.CreateInstance.");

                                              f.SetValue(c, rs.field(i));
                                          }
                                      }
                                  );

                              },
                              DynamicCurrent = delegate
                              {
                                  return c;
                              }
                          };
                   }
               };
        }

        static public IEnumerable<object[]> GetColumns(this GoogleGearsFactory.IRecordset rs)
        {
            var a = new System.Collections.Generic.List<object[]>();
            var columns = rs.fieldCount();

            while (rs.isValidRow())
            {
                var x = new object[columns];

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

    // http://code.google.com/apis/gears/api_factory.html
    [Script(InternalConstructor = true)]
    public class GoogleGearsFactory
    {
        [Script(InternalConstructor = true)]
        public class IRecordset
        {
            // http://code.google.com/apis/gears/api_database.html

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

            public int fieldCount()
            {
                throw new Exception();

            }
            public string fieldName(int fieldIndex)
            {
                throw new Exception();
            }

            public object field(int i)
            {
                throw new Exception();
            }

            public object fieldByName(string i)
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

        public string getBuildInfo()
        {
            throw new Exception();
        }

        public Database create(string id, string version)
        {
            throw new Exception();
        }

        internal static GoogleGearsFactory InternalConstructor()
        {
            object r = null;

            var error = "Google Gears is not installed or not supported for current browser!";

            // Mozilla
            try
            {
                r = IFunction.Of("GearsFactory").CreateType();
            }
            catch
            {

            }

            // IE
            if (r == null)
                try
                {
                    r = new ScriptCoreLib.JavaScript.DOM.IActiveX("Gears.Factory");
                }
                catch
                {

                }

            // Safari
            if (r == null)
                try
                {
                    var s = new IFunction("return !!navigator.mimeTypes['application/x-googlegears']").apply(null);


                    error = "Google Gears for safari is not yet supported (June 2007); plugin installed: " + s;
                }
                catch
                {

                }

            if (r == null)
                throw new System.Exception(error);

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


}
