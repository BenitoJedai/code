using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrivialAsyncFromResult
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static async Task<string> yield()
        {
            return "foo";
        }

        static async Task<string> GetString()
        {
            return "foo";
        }
    }
}
