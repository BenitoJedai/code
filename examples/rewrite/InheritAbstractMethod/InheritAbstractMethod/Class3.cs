using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.util
{
    public abstract class Calendar
    {
        protected abstract void computeFields();
    }

    public class JapaneseImperialCalendar : Calendar
    {
        protected override void computeFields()
        {
        }
    }
}
