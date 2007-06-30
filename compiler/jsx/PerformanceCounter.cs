using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsx
{
    public class PerformanceCounter
    {
        public readonly List<Tuple<StackFrame, Stopwatch>> Values = new List<Tuple<StackFrame, Stopwatch>>();

        public readonly StackFrame Creator = new StackFrame(1);


        public static IDisposable operator ~(PerformanceCounter e)
        {
            var x = new StackFrame(1);
            var s = new Stopwatch();

            s.Start();

            return (DisposableEvent)delegate
            {
                s.Stop();

                e.Values.Add(new Tuple<StackFrame, Stopwatch>(x, s));
            };
        }

        public TimeSpan Total
        {
            get
            {
                return new TimeSpan((from v in Values select v.Second.Elapsed.Ticks).Sum());
            }
        }

        public void ToConsole()
        {
            ToConsole(null);
        }

        public void ToConsole(TimeSpan? xtotal)
        {
            var u = from v in Values
                    let elapsed =  v.Second.Elapsed
                    let text = v.First.GetMethod().Name
                    select new { text, elapsed };


            var total = new { 
                    text = Creator.GetMethod().DeclaringType.Name,
                    elapsed = new TimeSpan( (from v in u select v.elapsed.Ticks).Sum() )
                };


            u = u.Concat(new [] { total });


            var a = from v in u
                      let percentage = (100 * v.elapsed.Ticks / total.elapsed.Ticks) + "%"
                      orderby v.elapsed descending
                      select new { percentage, v.text, v.elapsed };

            var textwidth = (from v in a select v.text.Length).Max();
            var percentagewidth = (from v in a select v.percentage.Length).Max();


            foreach (var x in a)
            {
                if (x.text == total.text)
                {
                    using (new ConsoleColorText(ConsoleColor.White))
                    {
                        if (xtotal != null)
                            Console.WriteLine(x.text.PadLeft(textwidth) + " : " + ((100 * x.elapsed.Ticks / xtotal.Value.Ticks) + "%").PadLeft(percentagewidth) + " " + x.elapsed);
                        else
                            Console.WriteLine(x.text.PadLeft(textwidth) + " : " + x.percentage.PadLeft(percentagewidth) + " " + x.elapsed);
                    }
                }
                else
                    using (new ConsoleColorText(ConsoleColor.Gray))
                        Console.WriteLine(x.text.PadLeft(textwidth) + " : " + x.percentage.PadLeft(percentagewidth) + " " + x.elapsed);

            }
        }
    }
}
