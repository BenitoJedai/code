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
            var date = System.DateTime.Now;

            IStyleSheet.Default.AddRule(".hover",
                r =>
                {
                    r.style.backgroundColor = Color.Blue;
                    r.style.color = Color.White;
                }
            );

            // http://simiandesign.com/2006/04/18/superscripts-subscripts-css-and-the-line-height/

            IStyleSheet.Default.AddRule(".sub",
                            r =>
                            {
                                r.style.fontSize = "small";
                                r.style.lineHeight = "0";
                            }
                        );

            IStyleSheet.Default.AddRule(".sup",
                            r =>
                            {
                                r.style.fontSize = "small";
                                r.style.lineHeight = "0";
                            }
                        );


            Action<td, DateTime> ApplyStyle =
                (col, x) =>
                {


                    if (x.DayOfWeek == DayOfWeek.Saturday)
                        col.style.borderLeft = "1px solid black";

                    if (x.DayOfWeek == DayOfWeek.Sunday)
                        col.style.borderRight = "1px solid black";

                    col.style.width = "2em";
                    col.style.height = "2em";
                    col.style.textAlign = IStyle.TextAlignEnum.center;
                };

            //new IHTMLDiv("days: " + DateTime.DaysInMonth(2007, 10)).attachToDocument();
            new IHTMLDiv("&laquo;" + "year: " + date.Year + "&raquo;").attachToDocument();
            new IHTMLDiv("&laquo;" + "month: " + date.Month + "&raquo;").attachToDocument();
            //new IHTMLDiv("day: " + date.Day).attachToDocument();
            //new IHTMLDiv("Hour: " + date.Hour).attachToDocument();
            //new IHTMLDiv("Minute: " + date.Minute).attachToDocument();
            //new IHTMLDiv("Second: " + date.Second).attachToDocument();
            //new IHTMLDiv("Millisecond: " + date.Millisecond).attachToDocument();
            //new IHTMLDiv("ticks: " + date.Ticks).attachToDocument();

            var t = new table();

            t.border = 0;
            t.cellSpacing = 0;
            t.cellPadding = 0;

            var b = t.AddBody();

            var r0 = b.AddRow();
            var c0 = r0.AddColumn();

            c0.rowSpan = 2;
            c0.innerText = "NIMI";

            var days = date.DaysInMonth();



            for (int i = 0; i < days; i++)
            {
                var c1 = r0.AddColumn();

                ApplyStyle(c1, date.GetDayWithinMonth(i + 1));

                c1.innerText = "" + 1 + i;
            }

            var c2 = r0.AddColumn();

            c2.rowSpan = 2;

            var r1 = b.AddRow();






            for (int i = 0; i < days; i++)
            {
                var c3 = r1.AddColumn();
                var x = date.GetDayWithinMonth(i + 1);

                ApplyStyle(c3, x);

                c3.innerText = x.DayOfWeek.AsString();
            }

            // dynamic

            var TotalPersentageStates = new[]
                {
                    new ToggleButtonState { Text = "2", Percentage = 2 },
                    new ToggleButtonState { Text = "2½", Percentage = 2.5 },
                    new ToggleButtonState { Text = "3", Percentage = 3 },
                    new ToggleButtonState { Text = "3½", Percentage = 3.5 },
                    new ToggleButtonState { Text = "0", Percentage = 0 },
                    new ToggleButtonState { Text = "1", Percentage = 1 },
                    new ToggleButtonState { Text = "1½", Percentage = 1.5 },
                };

            var WorkerStates = new[]
                {
                    new ToggleButtonState { Text = "", Hours = 12, Percentage = 1 },
                    new ToggleButtonState { Text = "P", Hours = 0, Percentage = 0 },
                    new ToggleButtonState { Text = "13", StyleClass = "sup", Hours = 13 - 9, Percentage = 0.5 },
                    new ToggleButtonState { Text = "17", StyleClass = "sub", Hours = 21 - 17, Percentage = 0.5 },
                };


            var ActualPercentageRow = b.AddRow();
            ActualPercentageRow.AddColumn();

            var ActualPercentageColumns = days.AsRange().Select(
                    i =>
                    {
                        var c5 = ActualPercentageRow.AddColumn();

                        c5.innerHTML = "&nbsp;";

                        ApplyStyle(c5, date.GetDayWithinMonth(i + 1));

                        return c5;
                    }
            ).ToArray();

            var UpdateActualPercentage = default(Action<int>);

            Action UpdateActualPercentages = () => days.AsRange().ForEach(UpdateActualPercentage);

            var TotalPercentageRow = b.AddRow();
            TotalPercentageRow.AddColumn();

            var TotalPercentageButtons = days.AsRange().Select(
                    i =>
                    {
                        var c5 = TotalPercentageRow.AddColumn();

                        var x = date.GetDayWithinMonth(i + 1);

                        ApplyStyle(c5, x);

                        var settings = new ToggleButtonSettings
                        {
                            States = TotalPersentageStates,

                        };


                        var btn = new ToggleButton
                        {
                            Settings = settings,
                            Control = c5,
                            Changed = () => UpdateActualPercentage(i),

                        }.Attach();

                        if (x.IsWeekend())
                            btn.SetValueSilently(TotalPersentageStates[1]);

                        return btn;
                    }
                ).ToArray();

            var AddWorker = default(Func<Worker>);
            var Workers = new List<Worker>();


            Func<int, double> ActualPercentageOfWorkers =
                 idx =>
                     Workers.Where(w => !w.Name.IsNullOrEmpty()).Sum(w => w.Days[idx].Value.Percentage)
                        - TotalPercentageButtons[idx].Value.Percentage;



            AddWorker =
                delegate
                {
                    var TSettings = new ToggleButtonSettings
                    {
                        States = WorkerStates,


                    };

                    var row = new tr();

                    b.insertBefore(row, ActualPercentageRow);

                    var c4 = row.AddColumn();

                    c4.style.paddingRight = "2px";

                    var w = new Worker
                        {
                            Row = row,
                            NameInput = new input(HTMLInputTypeEnum.text).AttachTo(c4),
                            SettingsForDays = TSettings
                        }.AddTo(Workers);

                    // disable planning on a nameless worker
                    TSettings.IsEnabled = () => !w.Name.IsNullOrEmpty();

                    w.Disposing += () => Workers.Remove(w);


                    w.Days = days.AsRange().Select(
                        i =>
                        {
                            var c5 = row.AddColumn();

                            ApplyStyle(c5, date.GetDayWithinMonth(i + 1));

                            return new ToggleButton
                            {
                                Settings = TSettings,
                                Control = c5,
                                Changed = () => UpdateActualPercentage(i)
                            }.Attach();
                        }
                    ).ToArray();

                    w.count = row.AddColumn();

                    w.count.style.paddingLeft = "2px";

                    TSettings.Changed = delegate
                    {
                        if (w.Name.IsNullOrEmpty())
                        {
                            w.count.innerText = "";

                            return;
                        }

                        var total = w.Days.Sum(i => i.Value.Hours);

                        w.count.innerText = "" + total;
                    };

                    TSettings.Changed();

                    w.NameInput.WhenNoLongerEmpty(
                        delegate
                        {
                            AddWorker();
                            UpdateActualPercentages();
                            w.SettingsForDays.Changed();
                        });

                    // compiler bug: cannot take implicit delegate from a type with Script(HasNoPrototype=true)
                    // compiler bug: will loose parameters if statement is complex - workaround: use locals

                    Func<Worker, bool> IsNamelessWorker = i => i.NameInput.value.IsNullOrEmpty();

                    w.NameInput.WhenNoLongerNeeded(() => Workers.Count(IsNamelessWorker) > 1,
                        delegate
                        {
                            w.Dispose();
                            UpdateActualPercentages();
                        });

                    return w;
                };


            UpdateActualPercentage =
                idx =>
                {
                    Console.WriteLine("update idx: " + idx);

                    var a = ActualPercentageOfWorkers(idx);
                    var target = ActualPercentageColumns[idx];


                    if (a == 0)
                        target.innerHTML = "&nbsp;";
                    else
                    {
                        if (a > 0)
                        {
                            target.innerHTML = "<small>+" + a + "</small>";
                            target.style.color = Color.Gray;
                        }
                        else
                        {
                            target.innerHTML = "<small>-</small>" + (-a);
                            target.style.color = Color.Red;
                        }
                    }
                };

            AddWorker().Name = "Krista";
            AddWorker().Name = "Birgit";
            AddWorker().Name = "Õie";

            AddWorker();

            UpdateActualPercentages();

            t.attachToDocument();




        }

        [Script]
        public class ToggleButtonState
        {
            public string Text;

            public string StyleClass;

            /// <summary>
            /// how many hours the worker is there
            /// </summary>
            public int Hours;

            /// <summary>
            /// is it a fullday or half a day
            /// </summary>
            public double Percentage;
        }

        [Script]
        public class ToggleButtonSettings
        {
            public Func<bool> IsEnabled;

            public ToggleButtonState[] States;

            public Action Changed;
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

        [Script]
        public class ToggleButton
        {



            public ToggleButtonSettings Settings;
            public IHTMLElement Control;

            ToggleButtonState _Value_Old;

            public Action Changed;

            public ToggleButtonState Value
            {
                get
                {
                    return this.Settings.States.Where(i => i.Text == this.Control.innerHTML).Single();
                }

                set
                {
                    if (value == null)
                        throw new Exception("value null");

                    if (Control.innerHTML != value.Text)
                    {
                        SetValueSilently(value);


                        if (this.Changed != null)
                            this.Changed();

                        if (this.Settings.Changed != null)
                            this.Settings.Changed();
                    }
                }
            }

            public void SetValueSilently(ToggleButtonState value)
            {
                Control.innerHTML = value.Text;

                var n = new ClassNameLiteralSupport { Control = Control };

                if (_Value_Old != null)
                {
                    n.className -= _Value_Old.StyleClass;

                }

                n.className += value.StyleClass;

                _Value_Old = value;
            }


            public bool IsEnabled
            {
                get
                {
                    if (this.Settings.IsEnabled == null)
                        return true;

                    return this.Settings.IsEnabled();
                }
            }


            public ToggleButton Attach()
            {
                Action Next =
                    () => this.Value = this.Settings.States.Next(i => i.Text == this.Control.innerHTML);
                Action Previous =
                    () => this.Value = this.Settings.States.Previous(i => i.Text == this.Control.innerHTML);

                SetValueSilently(this.Settings.States.First());

                Control.style.cursor = IStyle.CursorEnum.pointer;
                Control.onclick +=
                    delegate
                    {
                        if (!IsEnabled) return;

                        Next();
                    };

                Control.onmouseover +=
                    delegate
                    {


                        var n = new ClassNameLiteralSupport { Control = Control };

                        n.className += "hover";
                        //Control.className += "hover";
                    };


                Control.onmouseout +=
                    delegate
                    {
                        try
                        {

                            var n = new ClassNameLiteralSupport { Control = Control };

                            n.className -= "hover";
                        }
                        catch
                        {

                        }
                    };

                Control.onmousewheel +=
                    ev =>
                    {
                        if (!IsEnabled) return;

                        if (ev.WheelDirection > 0)
                            Next();
                        else
                            Previous();
                    };

                return this;
            }
        }

        [Script]
        class Worker
        {
            public tr Row;

            public input NameInput;

            public ToggleButtonSettings SettingsForDays;

            public ToggleButton[] Days;
            public td count;

            public event Action Disposing;

            public void Dispose()
            {
                if (Disposing != null)
                    Disposing();

                Row.Dispose();
            }

            public string Name { get { return NameInput.value; } set { NameInput.value = value; SettingsForDays.Changed(); } }
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
