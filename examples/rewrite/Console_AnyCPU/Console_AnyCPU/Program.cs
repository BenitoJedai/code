﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console_AnyCPU
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new { Environment.Is64BitProcess, Environment.Is64BitOperatingSystem });
        }
    }
}
