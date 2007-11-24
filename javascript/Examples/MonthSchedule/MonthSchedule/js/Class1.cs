//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using global::System.Linq;
using global::ScriptCoreLib.Shared.Lambda;




namespace MonthSchedule.js
{
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Lambda;

    using ScriptCoreLib.Shared.Drawing;

    using System;

    using legend = IHTMLLegend;
    using fieldset = IHTMLFieldset;
    using table = IHTMLTable;
    using tb = IHTMLTableBody;
    using tr = IHTMLTableRow;
    using td = IHTMLTableColumn;
    using input = IHTMLInput;
    using MonthSchedule.js.Controls;

    [Script, ScriptApplicationEntryPoint]
    public class Class1
    {

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1()
        {
            //Native.Window.alert( "cookie:" + Native.Document.cookie);

            //var c = new Cookie("xx");

            //c.Value = "hello world";

            //Native.Document.cookie = "bandwidth=4;path=/;expires=Tue, 2 Jun 2015 00:00:00 UTC;";


            //Native.Window.alert( "cookie:" + Native.Document.cookie);


            IStyleSheet.Default.AddRule(".hover", "background-color: blue !important", 0);
            IStyleSheet.Default.AddRule(".hover", "color: white !important", 0);
            IStyleSheet.Default.AddRule("a", "text-decoration: none;", 0);
            IStyleSheet.Default.AddRule(".language_link", "margin: 1em;", 0);
            IStyleSheet.Default.AddRule(".about",
                r =>
                {
                    r.style.padding = "1em";
                    r.style.margin = "1em";
                    r.style.border = "1px dashed gray";

                }
            );

            var langbar = "div".AsElement().AttachTo(Native.Document.body);
            langbar.style.Float = IStyle.FloatEnum.right;



            #region SpawnLanguageLink
            Action<string> SpawnLanguageLink =
                   lang_value =>
                       new IHTMLAnchor(lang_value)
                       {
                           className = "language_link"
                       }.AttachTo(langbar).onclick +=
                       ev =>
                       {
                           ev.PreventDefault();

                           var dict = Native.Document.location.ArgumentsToDictonary();

                           dict["lang"] = lang_value;

                           Native.Document.location.DictonaryToArguments(dict);

                       };
            #endregion


            SpawnLanguageLink("eng");
            SpawnLanguageLink("est");
            SpawnLanguageLink("de");


            var loading = "div".AsElement().WithText("Loading...").AttachTo(Native.Document.body);

            Locals.Default.WhenDownloadComplete(
                delegate
                {

                    int stage = 0;

                    try
                    {
                        stage = 100;

                        var about_text = "about".Localize();

                        stage = 200;

                        var about = new IHTMLAnchor(about_text)
                            {
                                className = "language_link"
                            }.AttachTo(langbar);


                        stage = 3;
                        var about_div = "div".AsElement().AttachTo(Native.Document.body);
                        stage = 4;
                        about_div.className = "about";
                        stage = 5;

                        about_div.Show(!Storage.About);
                        stage = 6;
                        about_div.innerText = "This Application lets you manage the workschedule of your workers. You can add or remove workers on the left, specify how many workers must be present at any given day at the bottom, which days are free days, when to come later and when to go earlier. It also calculates total workhours on the right. You can use your mousewheel or just click on the buttons.".Localize();
                        stage = 7;
                        about.onclick +=
                            ev =>
                            {
                                ev.PreventDefault();

                                Storage.About = !about_div.ToggleVisible();
                            };
                        stage = 7;
                        SpawnDefaultView();
                        stage = 8;
                        loading.FadeOut();
                        stage = 9;
                    }
                    catch (Exception exc)
                    {
                        loading.innerText = "WhenDownloadComplete failed: {" + exc.Message + "}; stage: " + stage;
                        loading.style.color = Color.Red;
                    }
                }
            );

            /*
            var print = new IStyleSheet();

            print.Owner.media = "print";
            print.AddRule(".noprint", "display: none;", 0);
            */





        }

        private static void SpawnDefaultView()
        {
            // http://simiandesign.com/2006/04/18/superscripts-subscripts-css-and-the-line-height/

            #region ApplyStyle
            Action<td, DateTime> ApplyStyle = (col, x) =>
            {

                // users will want to notice weekends

                if (x.IsFirstDayOfMonth())
                    col.style.borderLeft = "1px solid gray";

                if (x.DayOfWeek == DayOfWeek.Saturday)
                {
                    col.style.borderLeft = "1px solid gray";
                    col.style.backgroundColor = "#efefef";
                }

                if (x.DayOfWeek == DayOfWeek.Sunday)
                {
                    col.style.borderRight = "1px solid gray";
                    col.style.backgroundColor = "#efefef";
                }

                if (x.IsLastDayOfMonth())
                    col.style.borderRight = "1px solid gray";

                col.style.width = "2em";
                col.style.height = "2em";
                col.style.textAlign = IStyle.TextAlignEnum.center;
            };
            #endregion

            //var sch = new ScheduleController(ApplyStyle);


            var DateSelectionChanged = default(Action);

            var date = DateTime.Now;

            #region spinners

            var YearSpinner = new SpinnerSpan<int>.SpinnerSpanSettings
            {
                Value = date.Year,
                GetText = i => "year".Localize() + ": " + i,
                GetNext = date.Year.MakeNextFunc(3),
                GetPrevious = date.Year.MakePreviousFunc(3),
                Changed =
                                    delegate
                                    {
                                        if (DateSelectionChanged != null) DateSelectionChanged();
                                    }

            }.Create();

            var MonthSpinner = new SpinnerSpan<int>.SpinnerSpanSettings
            {
                Value = date.Month,
                GetText = i => "month".Localize() + ": " + i,
                GetNext = i =>
                {
                    if (i == 12)
                    {
                        YearSpinner.Silent = true;
                        YearSpinner.GoNext();
                        YearSpinner.Silent = false;

                        return 1;
                    }

                    return i++;
                },
                GetPrevious = i =>
                {
                    if (i == 1)
                    {
                        YearSpinner.Silent = true;
                        YearSpinner.GoPrevious();
                        YearSpinner.Silent = false;
                        return 12;
                    }

                    return i--;
                },
                Changed =
                    delegate
                    {
                        if (DateSelectionChanged != null) DateSelectionChanged();
                    }
            }.Create();

            YearSpinner.Control.AttachTo(Native.Document.body);
            MonthSpinner.Control.AttachTo(Native.Document.body);

            var _DefaultWorkers = new[] { "Worker1", "Worker2" };

            if (Storage.Workers == null)
            {
                // safari wont save cookies when document isnt from a webserver
                Storage.Workers = _DefaultWorkers;


                //if (Storage.Workers == null)
                //    throw new Exception( "Cookies are not working!!");
                //if (Storage.Workers.Length != _new.Length)
                //    throw new Exception( "Cookies are not working!");
            }


            var Make = new IHTMLAnchor("make new work schedule".Localize());



            Action MakeNewSchedule =
                delegate
                {
                    int stage = 1;

                    try
                    {
                        stage = 2;
                        var schnew = new ScheduleController(ApplyStyle, new DateTime(YearSpinner.Value, MonthSpinner.Value, 1));

                        stage = 3;
                        schnew.Control.AttachTo(Native.Document.body);
                        stage = 4;
                        schnew.WorkersChanged +=
                            delegate
                            {
                                Storage.Workers = schnew.Workers.Where(w => !w.Name.IsNullOrEmpty()).Select(w => w.Name).ToArray();
                            };
                        stage = 5;

                        // compiler bug: passing params attribute when they are not first should use local variable!

                        var _Workers = Storage.Workers;

                        if (_Workers == null)
                            _Workers = _DefaultWorkers;

                        _Workers = _Workers.Concat(new[] { "" }).ToArray();

                        stage = 6;

                        schnew.LazyLoad(
                            () => { }, _Workers
                        );
                        stage = 7;
                    }
                    catch (Exception exc)
                    {
                        throw new Exception("MakeNewSchedule failed: {" + exc.Message + "}; stage: " + stage);
                    }
                };

            Make.AttachTo(Native.Document.body).onclick +=
                ev =>
                {
                    ev.PreventDefault();

                    MakeNewSchedule();
                };

            #endregion

            DateSelectionChanged =
                delegate
                {
                    // compiler bug: opcode strobj not supported - cannot store struct to field directly.

                    //var newdate = new DateTime(YearSpinner.Value, MonthSpinner.Value, 1);

                    //date = newdate;
                    //days = newdays;

                };

            //sch.Control.AttachTo(Native.Document.body);



            MakeNewSchedule();
        }






        static Class1()
        {
            typeof(Class1).SpawnTo(i => new Class1());


        }



    }


    [Script]
    public class ClassNameLiteral
    {
        public string Value;

        public static ClassNameLiteral operator -(ClassNameLiteral e, string name)
        {
            return string.Join(" ", e.ToArray().Where(i => i != name).ToArray());
        }

        public static ClassNameLiteral operator +(ClassNameLiteral e, string name)
        {
            return string.Join(" ", e.ToArray().Concat(new[] { name }).ToArray());
        }

        public string[] ToArray()
        {
            return this.Value.Split(' ');
        }

        public static implicit operator ClassNameLiteral(string e)
        {
            return new ClassNameLiteral { Value = e };
        }

        public static implicit operator string(ClassNameLiteral e)
        {
            return e.Value;
        }
    }

    [Script]
    public class ClassNameLiteralSupport
    {
        public IHTMLElement Control;

        public ClassNameLiteral className
        {
            get
            {
                return this.Control.className;
            }
            set
            {
                this.Control.className = value;
            }
        }
    }

}
