using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultitierExample.Shared;

namespace MultitierExample.Client
{
	public class ClientProvider
	{
		// write Windows Forms client here

		// first we will start coping fields without their types

		public string FieldString1;
		public int FieldInt2;
		public LocalType1 Field3;


		public int PropertyInt1 { get; set; }

		public void Method1(LocalType1 a, int b)
		{
			var xa = a;
			var xb = b;

			//a.FieldInt4 = b;
		}

		SharedLogic logic1;

	}

	public class LocalType1
	{
		public string FieldString3;
		public int FieldInt4;
		public LocalType2 Field5;

		public class LocalType2
		{
			public string FieldString1;
			public int FieldInt2;

			public ClientProvider Field3;

			public LocalType2()
			{
				this.FieldString1 = "hello";
			}
		}
	}
}
