﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PortCloner.Library
{
	public static class MyExtensions
	{
		public static void Sleep(this int delay)
		{
			Thread.Sleep(delay);

		}
	}
}
