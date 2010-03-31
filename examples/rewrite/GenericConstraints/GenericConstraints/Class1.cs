using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericConstraints
{
	public interface I<T>
		where T : Class1
	{
		void X(T t);
	}

	class Class2 : I<Class1>
	{
		#region I<Class1> Members

		public void X(Class1 t)
		{
			throw new NotImplementedException();
		}

		#endregion
	}

	public class Class1
	{

	}
}
