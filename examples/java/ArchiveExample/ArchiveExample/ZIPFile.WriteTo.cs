using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Collections;

namespace ArchiveExample
{
	partial class ZIPFile
	{
		public static uint ToMsDosDateTime(DateTime dateTime)
		{
			uint num = 0;
			num |= (uint)((dateTime.Second / 2) & 0x1f);
			num |= (uint)((dateTime.Minute & 0x3f) << 5);
			num |= (uint)((dateTime.Hour & 0x1f) << 11);
			num |= (uint)((dateTime.Day & 0x1f) << 0x10);
			num |= (uint)((dateTime.Month & 15) << 0x15);
			return (num | ((uint)(((dateTime.Year - 0x7bc) & 0x7f) << 0x19)));
		}

		public static DateTime FromMsDosDateTime(uint dosDateTime)
		{
			// jsc should prevent javac from reporting possible loss of precision

			long year =  (0x7bc + (((int)(dosDateTime >> 0x19)) & 0x7f));
			long second = ((int)((dosDateTime & 0x1f) << 1));
			long minute = (((int)(dosDateTime >> 5)) & 0x3f);
			long hour = (((int)(dosDateTime >> 11)) & 0x1f);
			long day = (((int)(dosDateTime >> 0x10)) & 0x1f);
			long month = (((int)(dosDateTime >> 0x15)) & 15);
			return new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second);
		}


	}

}
