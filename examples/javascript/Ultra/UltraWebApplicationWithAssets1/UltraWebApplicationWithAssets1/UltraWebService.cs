using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScriptCoreLib.Ultra.Library.Delegates;
using System.Diagnostics;

namespace UltraWebApplicationWithAssets1
{
	public delegate void EnumerateAction(string Key, string Field1);
	public delegate void ContinuationAction(string dummy);


	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			Debugger.Break();

			result(x + DateTime.Now);
		}


		public void Enumerate(EnumerateAction yield)
		{
			using (var ctx = new DataClasses1DataContext())
			{

				foreach (var item in ctx.Table1s)
				{
					yield("" + item.Key, item.Field1);
				}
			}

		}

		public void Add(string Field1, ContinuationAction Continuation)
		{
			using (var ctx = new DataClasses1DataContext())
			{

				ctx.Table1s.InsertOnSubmit(
					new Table1
					{
						Field1 = Field1
					}
				);

				ctx.SubmitChanges();
			}

			if (Continuation != null)
				Continuation("");
		}

		public void Delete(string Key, ContinuationAction Continuation)
		{
			using (var ctx = new DataClasses1DataContext())
			{

				ctx.Table1s.DeleteAllOnSubmit(
					ctx.Table1s.Where(k => k.Key == Convert.ToInt32(Key))
				);

				ctx.SubmitChanges();
			}

			if (Continuation != null)
				Continuation("");
		}
	}
}