using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        // http://blog.tanelpoder.com/2013/08/22/scalar-subqueries-in-oracle-sql-where-clauses-and-a-little-bit-of-exadata-stuff-too/
        //  from k in new PerformanceResourceTimingData2 
        // could allow to query on meta and stats?
        // what about access control and logging?
        // what about saving stacktrace?
        // on every acccess?
        // with full debugging details?
        // intellitrace way?
        // with locals visible?

        var f = (
            from x in new xTable()

            select new
            {

                f = (from y in new xTable()
                     select y.Tag).FirstOrDefault()
            }
        ).FirstOrDefault();

    }
}
