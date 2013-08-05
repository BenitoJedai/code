using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLeaveTrapWithRethrow
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine();

                throw;
            }

            Console.WriteLine();

        }
    }
}
