using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace MonthSchedule.js.Controls
{
    using legend = IHTMLLegend;
    using fieldset = IHTMLFieldset;
    using table = IHTMLTable;
    using tb = IHTMLTableBody;
    using tr = IHTMLTableRow;
    using td = IHTMLTableColumn;
    using input = IHTMLInput;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.Shared.Drawing;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.JavaScript.Runtime;

    [Script]
    public class ScheduleController
    {
        public void LazyLoad(Action done, params string[] e)
        {
            int stage = 1;

            try
            {
                stage = 2;
                var w = new WorkPool();
                stage = 3;
                foreach (var v0 in e)
                {
                    var v1 = v0;

                    w.Add(
                        delegate
                        {
                            this.AddWorker().Name = v1;
                        }
                    );
                }
                stage = 4;
                w.Add(
                     delegate
                     {
                         this.UpdateActualPercentages();

                         done();
                     }
                );
                stage = 5;
            }
            catch (Exception exc)
            {
                throw new Exception("LazyLoad failed: {" + exc.Message + "}; stage: " + stage);
                
            }
        }

        public ScheduleController(Action<td, DateTime> ApplyStyle)
            : this(ApplyStyle, DateTime.Now)
        {

        }

        public Action WorkersChanged;

        public ScheduleController(Action<td, DateTime> ApplyStyle, DateTime date)
        {
            this.date = date;
            this.ApplyStyle = ApplyStyle;

            TotalPersentageStates = new[]
                {
                    new ToggleButtonState {  Percentage = 2 },
                    new ToggleButtonState {  Percentage = 2.5 },
                    new ToggleButtonState {  Percentage = 3 },
                    new ToggleButtonState {  Percentage = 3.5 },
                    new ToggleButtonState {  Percentage = 4 },
                    new ToggleButtonState {  Percentage = 0 },
                    new ToggleButtonState {  Percentage = 1 },
                    new ToggleButtonState {  Percentage = 1.5 },
                };

            WorkerStates = new[]
                {
                    new ToggleButtonState { Text = "·", Hours = 11.5, Percentage = 1 },
                    new ToggleButtonState { Text = "P", Hours = 0, Percentage = 0 },
                    new ToggleButtonState { Text = "17", Hours = 17 - 9.5, Percentage = 0.5 },
                    new ToggleButtonState { Text = "13", Hours = 21 - 13, Percentage = 0.5 },
                };

            days = date.DaysInMonth();

            this.ScheduleTable.style.border = "1px solid gray";
            this.ScheduleTable.border = 0;
            this.ScheduleTable.cellSpacing = 0;
            this.ScheduleTable.cellPadding = 0;

            this.Body = this.ScheduleTable.AddBody();







            this.RowOfDateNumbers = this.Body.AddRow();
            this.RowOfDayNames = this.Body.AddRow();

            //Console.WriteLine(DateTime.Now + " step 2");

            BuildTop();

            //Console.WriteLine(DateTime.Now + " step 3");

            BuildBottom();

            //Console.WriteLine(DateTime.Now + " step 4");


            Control.style.padding = "2em";

            Header = new IHTMLElement(IHTMLElement.HTMLElementEnum.h2);
            Header.innerText = "work schedule".Localize();

            var h4 = new IHTMLElement(IHTMLElement.HTMLElementEnum.h4);
            h4.style.Float = IStyle.FloatEnum.right;
            h4.innerText = date.Year + " / " + date.Month;

            var br = new IHTMLBreak();
            br.style.clear = "both";

            var close = new IHTMLAnchor("close!".Localize());
            close.className = "noprint";

            close.onclick +=
                ev =>
                {
                    ev.PreventDefault();

                    this.Control.Dispose();
                };

            close.style.Float = IStyle.FloatEnum.right;

            var c = new IHTMLElement(IHTMLElement.HTMLElementEnum.center, close, Header, h4, br, this.ScheduleTable);

            c.AttachTo(this.Control);


        }

        public readonly IHTMLElement Header;

        private void BuildTop()
        {
            #region top

            var c0 = RowOfDateNumbers.AddColumn();

            c0.rowSpan = 2;
            c0.innerText = "name".Localize();
            c0.style.padding = "2px";

            for (int i = 0; i < this.days; i++)
            {
                var c1 = RowOfDateNumbers.AddColumn();

                this.ApplyStyle(c1, this.date.GetDayWithinMonth(i + 1));

                c1.innerText = "" + 1 + i;
            }

            var c2 = RowOfDateNumbers.AddColumn();

            c2.rowSpan = 2;


            for (int i = 0; i < this.days; i++)
            {
                var c3 = RowOfDayNames.AddColumn();
                var x = this.date.GetDayWithinMonth(i + 1);

                this.ApplyStyle(c3, x);

                c3.innerText = x.DayOfWeek.AsString();
                c3.style.fontWeight = "bold";
            }

            #endregion
        }

        private void BuildBottom()
        {

            #region bottom

            this.ActualPercentageRow = this.Body.AddRow();
            this.ActualPercentageRow.AddColumn();

            this.ActualPercentageColumns = this.days.AsRange().Select(
                    i =>
                    {
                        var c5 = this.ActualPercentageRow.AddColumn();

                        c5.innerHTML = "&nbsp;";

                        this.ApplyStyle(c5, this.date.GetDayWithinMonth(i + 1));

                        return c5;
                    }
            ).ToArray();



            this.TotalPercentageRow = this.Body.AddRow();
            this.TotalPercentageRow.AddColumn();

            this.TotalPercentageButtons = this.days.AsRange().Select(
                    i =>
                    {
                        var c5 = this.TotalPercentageRow.AddColumn();

                        var x = this.date.GetDayWithinMonth(i + 1);

                        this.ApplyStyle(c5, x);

                        var settings = new ScheduleController.ToggleButtonSettings
                        {
                            States = this.TotalPersentageStates,

                        };


                        var btn = new ScheduleController.ToggleButton
                        {
                            Settings = settings,
                            Control = c5,
                            Changed = () => this.UpdateActualPercentage(i),

                        }.Attach();

                        if (x.IsWeekend())
                            btn.SetValueSilently(this.TotalPersentageStates[1]);

                        return btn;
                    }
                ).ToArray();
            #endregion
        }

        readonly tr RowOfDateNumbers;
        readonly tr RowOfDayNames;

        public readonly tb Body;

        public readonly Action<td, DateTime> ApplyStyle;

        public tr TotalPercentageRow;
        public ToggleButton[] TotalPercentageButtons;

        public tr ActualPercentageRow;
        public td[] ActualPercentageColumns;

        public readonly IHTMLDiv Control = new IHTMLDiv();
        public readonly table ScheduleTable = new table();

        public readonly List<ScheduleController.Worker> Workers = new List<ScheduleController.Worker>();

        // compiler bug: cannot field init - must init field within ctor as a workaround
        // whiy is that?
        public ToggleButtonState[] TotalPersentageStates;
        public ToggleButtonState[] WorkerStates;

        public DateTime date = System.DateTime.Now;
        public int days;

        public Worker AddWorker()
        {
            var TSettings = new ScheduleController.ToggleButtonSettings
                                {
                                    States = this.WorkerStates,


                                };

            var row = new tr();

            this.Body.insertBefore(row, this.ActualPercentageRow);

            var NameColumn = row.AddColumn();

            NameColumn.style.padding = "2px";

            var w = new ScheduleController.Worker
                {
                    Row = row,
                    NameInput = new input(HTMLInputTypeEnum.text).AttachTo(NameColumn),
                    SettingsForDays = TSettings
                }.AddTo(this.Workers);

            // disable planning on a nameless worker
            TSettings.IsEnabled = () => !w.Name.IsNullOrEmpty();

            w.Disposing += () => this.Workers.Remove(w);


            w.Days = this.days.AsRange().Select(
                i =>
                {
                    var c5 = row.AddColumn();

                    this.ApplyStyle(c5, this.date.GetDayWithinMonth(i + 1));

                    return new ScheduleController.ToggleButton
                    {
                        Settings = TSettings,
                        Control = c5,
                        Changed = () => this.UpdateActualPercentage(i)
                    }.Attach();
                }
            ).ToArray();

            w.CountColumn = row.AddColumn();

            w.CountColumn.style.padding = "2px";

            TSettings.Changed = delegate
            {
                if (w.Name.IsNullOrEmpty())
                {
                    w.CountColumn.innerText = "";

                    return;
                }

                var total = w.Days.Sum(i => i.Value.Hours);//.GetFractString();

                // compiler bug: cannot chain this call?
                var total_str = total.GetFractString();

                w.CountColumn.innerText = total_str;
            };

            TSettings.Changed();

            w.NameInput.onchange +=
                delegate
                {
                    if (WorkersChanged != null)
                        WorkersChanged();
                };

            w.NameInput.WhenNoLongerEmpty(
                delegate
                {
                    AddWorker();
                    this.UpdateActualPercentages();
                    w.SettingsForDays.Changed();
                });

            // compiler bug: cannot take implicit delegate from a type with Script(HasNoPrototype=true)
            // compiler bug: will loose parameters if statement is complex - workaround: use locals

            Func<ScheduleController.Worker, bool> IsNamelessWorker = i => i.NameInput.value.IsNullOrEmpty();

            w.NameInput.WhenNoLongerNeeded(() => this.Workers.Count(IsNamelessWorker) > 1,
                delegate
                {
                    w.Dispose();
                    this.UpdateActualPercentages();
                });

            return w;
        }

        public double ActualPercentageOfWorkers(int idx)
        {
            return this.Workers.Where(w => !w.Name.IsNullOrEmpty()).Sum(w => w.Days[idx].Value.Percentage)
               - this.TotalPercentageButtons[idx].Value.Percentage;
        }


        public void UpdateActualPercentages()
        {
            days.AsRange().ForEach(this.UpdateActualPercentage);
        }

        public void UpdateActualPercentage(int idx)
        {
            var a = this.ActualPercentageOfWorkers(idx);
            var target = this.ActualPercentageColumns[idx];


            if (a == 0)
                target.innerHTML = "&nbsp;";
            else
            {
                if (a > 0)
                {
                    target.innerHTML = "<small>+" + a.GetAbsFractString() + "</small>";
                    target.style.color = Color.Gray;
                }
                else
                {
                    target.innerHTML = "<small>-" + a.GetAbsFractString() + "</small>";
                    target.style.color = Color.Red;
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
                    return this.Settings.States.Where(i => i.TextOrPercentage == this.Control.innerHTML).Single();
                }

                set
                {
                    if (value == null)
                        throw new Exception("value null");

                    if (Control.innerHTML != value.TextOrPercentage)
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
                Control.innerHTML = value.TextOrPercentage;

                var n = new ClassNameLiteralSupport { Control = Control };

                //if (_Value_Old != null)
                //{
                //    n.className -= _Value_Old.StyleClass;

                //}

                //n.className += value.StyleClass;

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
                    () => this.Value = this.Settings.States.Next(i => i.TextOrPercentage == this.Control.innerHTML);
                Action Previous =
                    () => this.Value = this.Settings.States.Previous(i => i.TextOrPercentage == this.Control.innerHTML);

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


                        try
                        {
                            //var n = new ClassNameLiteralSupport { Control = Control };

                            //n.className += "hover";

                            if (IsEnabled)
                                Control.className = "hover";
                        }
                        catch
                        {

                        }
                    };


                Control.onmouseout +=
                    delegate
                    {
                        try
                        {

                            //var n = new ClassNameLiteralSupport { Control = Control };

                            //n.className -= "hover";
                            Control.className = "";
                        }
                        catch
                        {

                        }
                    };

                Control.onmousewheel +=
                    ev =>
                    {
                        if (!IsEnabled) return;

                        ev.PreventDefault();

                        if (ev.WheelDirection > 0)
                            Next();
                        else
                            Previous();
                    };

                return this;
            }
        }



        [Script]
        public class ToggleButtonState
        {
            public string TextOrPercentage
            {
                get
                {
                    if (this.Text.IsNullOrEmpty())
                        return this.Percentage.GetFractString();

                    return Text;
                }
            }

            public string Text;

            //public string StyleClass;

            /// <summary>
            /// how many hours the worker is there
            /// </summary>
            public double Hours;

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
        public class Worker
        {
            public tr Row;

            public input NameInput;

            public ToggleButtonSettings SettingsForDays;

            public ToggleButton[] Days;
            public td CountColumn;

            public event Action Disposing;

            public void Dispose()
            {
                if (Disposing != null)
                    Disposing();

                Row.Dispose();
            }

            public string Name { get { return NameInput.value; } set { NameInput.value = value; SettingsForDays.Changed(); } }
        }




    }
}
