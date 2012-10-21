using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestNestedFinallyClause
{
    public class Class1
    {
        public static void CheckExists(IDisposable e = null)
        {
            using (e)
                try
                {
                    try
                    {
                        CheckExists();
                    }
                    catch (ArgumentException)
                    {

                    }
                    catch
                    {

                    }
                }
                catch (InvalidOperationException)
                {

                }
                finally
                {

                }
        }
    }
}
