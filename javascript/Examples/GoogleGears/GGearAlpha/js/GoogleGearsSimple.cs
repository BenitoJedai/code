//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using System;

using global::System.Collections.Generic;
using System.Linq;



namespace GGearAlpha.js
{


    [Script]
    public sealed class DemoDataEntity
    {
        public string Phrase;
        public long Timestamp;
    }


    [Script]
    class __Type2
    {
        public IHTMLElement ListItem;
        public DemoDataEntity Data;
    }

    [Script, ScriptApplicationEntryPoint]
    public class GoogleGearsSimple
    {
        //public const string Alias = "Class1";
        //public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public GoogleGearsSimple()
        {
            // this ctor creates a new div which has a text and a button element
            // on mouseover over the color text is changed
            // on pressing the button the next message in text element is displayed
            IHTMLDiv Control = new IHTMLDiv();

            Control.AttachToDocument();

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

            IStyleSheet.Default.AddRule(".odd").style.backgroundColor = Color.FromGray(0xa0);
            IStyleSheet.Default.AddRule(".even").style.backgroundColor = Color.FromGray(0xef);

            if (db != null)
            {
                db.open("demo1");
                db.execute(@"
    create table if not exists Demo
    (Phrase varchar(255), Timestamp int)
            ");

                var textfield = new IHTMLInput(HTMLInputTypeEnum.text, "text1", "");

                var btnadd = new IHTMLButton("Add new entry");
                var btnrefresh = new IHTMLButton("Refresh");
                var btnclear = new IHTMLButton("Clear");

                Control.appendChild(textfield, btnadd, btnclear, btnrefresh,
                    
                    new IHTMLCode(GoogleGearsFactory.Default.getBuildInfo())
                        
                        );



                var list = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol);

                Control.appendChild(list);

                var read = default(Action);

                read = delegate
                {
                    //from i in Demo
                    //select new { Phrase, Timestamp }
                    //order by Timestamp desc

                    list.removeChildren();

                    Func<string, IHTMLElement> AddItem = 
                        text => new IHTMLElement(IHTMLElement.HTMLElementEnum.li, text).Aggregate(v => list.appendChild(v));



                    // this could be rewritten as an expression once they are supported by jsc

                    int counter = 0;

                    var query = from Data in db.AsEnumerable<DemoDataEntity>(
                                                "select * from Demo order by Timestamp desc",
                                                typeof(DemoDataEntity)
                                            )
                                // let ListItem = AddItem(Data.Timestamp + " - " + Data.Phrase)
                                select new __Type2 { ListItem = AddItem(Data.Timestamp + " - " + Data.Phrase), Data  = Data};

                    foreach (var v in query)
                    {
                        counter++;
                        var vx = v;


                        if (counter % 2 == 0)
                            v.ListItem.className = "odd";
                        else
                            v.ListItem.className = "even";

                        #region -
                        var btndel = new IHTMLButton("-");

                        btndel.style.color = Color.Red;

                        btndel.onclick +=
                            delegate
                            {
                                db.execute("delete from Demo where Timestamp = ?", vx.Data.Timestamp);

                                read();
                            };
                        #endregion

                        #region +
                        var btnclone = new IHTMLButton("+");

                        btnclone.style.color = Color.Blue;

                        btnclone.onclick +=
                            delegate
                            {
                                db.Insert("Demo", vx.Data);


                                read();
                            };
                        #endregion

                        v.ListItem.insertBefore(btnclone, v.ListItem.firstChild);
                        v.ListItem.insertBefore(btndel, v.ListItem.firstChild);

                    }


                    #region raw
                    //var rs = db.execute("select * from Demo order by Timestamp desc");

                    //while (rs.isValidRow())
                    //{
                    //    var xt = typeof(DemoDataEntity);
                    //    var xx = (DemoDataEntity)Activator.CreateInstance(xt);

                    //    for (int i = 0; i < rs.fieldCount(); i++)
                    //    {
                    //        xt.GetField(rs.fieldName(i)).SetValue(xx, rs.field(i));
                    //    }

                    //    AddItem(xx.Timestamp + " - " + xx.Phrase);

                    //    rs.next();
                    //}

                    //rs.close();
                    #endregion

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
                        db.Insert("Demo",
                            new DemoDataEntity
                            {
                                Phrase = textfield.value,
                                Timestamp = IDate.Now.getTime()
                            }
                        );

                        //db.execute("insert into Demo (Phrase, Timestamp) values (?, ?)", textfield.value, IDate.Now.getTime());

                        textfield.value = "";

                        read();
                    };

                btnrefresh.onclick +=
                    delegate
                    {
                        read();
                    };

                read();

            }


            // not array
            // is object
            // no prototype
        }




        static GoogleGearsSimple()
        {
            typeof(GoogleGearsSimple).Spawn();

   

        }


    }

}
