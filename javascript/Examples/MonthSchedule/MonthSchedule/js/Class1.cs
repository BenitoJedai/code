//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
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
                    var about = new IHTMLAnchor("about".Localize())
                        {
                            className = "language_link"
                        }.AttachTo(langbar);
                    
                    var about_div = "div".AsElement().AttachTo(Native.Document.body);

                    about_div.className = "about";

                    
                    about_div.Show(!Storage.About);

                    about_div.innerText = "This Application lets you manage the workschedule of your workers. You can add or remove workers on the left, specify how many workers must be present at any given day at the bottom, which days are free days, when to come later and when to go earlier. It also calculates total workhours on the right. You can use your mousewheel or just click on the buttons.".Localize();

                    about.onclick +=
                        ev =>
                        {
                            ev.PreventDefault();

                            Storage.About = !about_div.ToggleVisible();
                        };

                    SpawnDefaultView();

                    loading.FadeOut();
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

            if (Storage.Workers == null)
                Storage.Workers = new[] { "Worker1", "Worker2" };

            var Make = new IHTMLAnchor("make new work schedule".Localize());



            Action MakeNewSchedule =
                delegate
                {
                    var schnew = new ScheduleController(ApplyStyle, new DateTime(YearSpinner.Value, MonthSpinner.Value, 1));

                    schnew.Control.AttachTo(Native.Document.body);

                    schnew.WorkersChanged +=
                        delegate
                        {
                            Storage.Workers = schnew.Workers.Where(w => !w.Name.IsNullOrEmpty()).Select(w => w.Name).ToArray();
                        };

                    schnew.LazyLoad(
                     () => { }, Storage.Workers.Concat(new[] { "" }).ToArray()
                     );
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
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, ScriptCoreLib.Shared.EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

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
