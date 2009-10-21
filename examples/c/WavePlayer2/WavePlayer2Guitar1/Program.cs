using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using WavePlayer2Guitar1.WaveLibrary;
using System.Media;
using WavePlayer2Guitar1.Library;

namespace WavePlayer2Guitar1
{



	public static class Program
	{


		public static void Main()
		{
			// we need a mic to record the chords
			// and we need mixer support

			Action
				E0 = () => Guitar.E.Default._0.PlaySync(),
				E1 = () => Guitar.E.Default._1.PlaySync(),
				E2 = () => Guitar.E.Default._2.PlaySync(),
				E3 = () => Guitar.E.Default._3.PlaySync(),
				E4 = () => Guitar.E.Default._4.PlaySync();

			E0();
			E3();
			E2();
			E1();
			E1();
			E4();
		}



	}


}
