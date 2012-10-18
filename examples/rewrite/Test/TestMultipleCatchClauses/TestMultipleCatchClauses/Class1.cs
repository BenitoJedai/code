using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMultipleCatchClauses
{
    public class Class1
    {
        public static void foo()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121018-todo

            try
            {


            }
            catch (InvalidOperationException e)
            {

                throw;
            }
            catch
            {

            }

        }

        public static object foo_nested()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121018-todo

            try
            {
                try
                {


                }
                catch (InvalidOperationException e)
                {

                    throw;
                }
            }
            catch
            {
              
            }

            return null;
        }

        public static object foo_nested2()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121018-todo

            try
            {
                return null;
            }
            catch
            {
                try
                {


                }
                catch
                {

                    return null;
                }
            }

            return null;
        }
    }
}
