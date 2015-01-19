using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestprocessorID
{
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150119



        // when will a process be able to run across 
        // lan processors?
        // first step with jsc?
        // how do the devices learn they exist and exchange trust?
        // udp proadcast?

        // http://www.vcskicks.com/hardware_id.php
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();

            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                //if (cpuInfo == "")
                {
                    // mo = {\\ASUS7\root\cimv2:Win32_Processor.DeviceID="CPU0"}

                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();


                    // cpuInfo = "BFEBFBFF000206A7"

                    break;
                }
            }


            // { ElapsedMilliseconds = 1132, cpuInfo = BFEBFBFF000206A7 }
            // { ElapsedMilliseconds = 1090, cpuInfo = BFEBFBFF000206A7 }
            Console.WriteLine(
                new
                {
                    sw.ElapsedMilliseconds,
                    cpuInfo
                }
                );

            Debugger.Break();

        }
    }
}
