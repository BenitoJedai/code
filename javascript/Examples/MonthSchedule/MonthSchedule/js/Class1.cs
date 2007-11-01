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
            Locals.Default.WhenDownloadComplete(
                locals =>
                {
                    Console.WriteLine(locals.Content.ToXMLString());

                    ("year localized: " + "year".Localize()).AttachToDocument();
                }
            );
            /*
            var print = new IStyleSheet();

            print.Owner.media = "print";
            print.AddRule(".noprint", "display: none;", 0);
            */

            IStyleSheet.Default.AddRule(".hover", "background-color: blue !important", 0);
            IStyleSheet.Default.AddRule(".hover", "color: white !important", 0);
            IStyleSheet.Default.AddRule("a", "text-decoration: none;", 0);

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
                GetText = i => "aasta: " + i,
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
                GetText = i => "kuu: " + i,
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

            var Make = new IHTMLAnchor("tee töögraafik!");

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
