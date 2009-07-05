using System.Threading;
using System;

using ScriptCoreLib;


namespace OrcasJavaConsoleApplication
{
	//[Script]
	//public class G<T>
	//{
	//    public T FieldT;
	//}

    [Script]
    public class Program
    {
		public static string Text { get; set; }


        public static void Main(string[] args)
        {
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

            // doubleclicking on the jar will not show the console

			//var x = new G<string> { FieldT = "zx" };

            Text = "Hello World";


			var w = new Worker { Count = 5, Delay = 300 };

			w.Handler +=
				delegate
				{
					Console.Write(".");
				};

			w.Invoke();

            Console.WriteLine(Text);
        }

		[Script]
		public class Worker
		{
			public event Action Handler;

			public int Count { get; set; }
			public int Delay { get; set; }

			public void Invoke()
			{
				for (int i = 0; i < Count; i++)
				{
					Handler();
					Thread.Sleep(Delay);
				}
			}
		}
    }


}
