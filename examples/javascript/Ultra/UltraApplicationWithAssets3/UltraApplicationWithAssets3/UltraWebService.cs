using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Delegates;

namespace UltraApplicationWithAssets3
{

	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			var ctx = new global::UltraApplicationWithAssets3.DataClasses1DataContext();

			// Can't perform Create, Update, or Delete operations on 'Table(Table1)' because it has no primary key.

			
			
			ctx.Table1s.InsertOnSubmit(
				new Table1 { Field1 = x  , Key = Guid.NewGuid() }
			);

			// String or binary data would be truncated.
			// The statement has been terminated.

			ctx.SubmitChanges();

			// An attempt to attach an auto-named database for file C:\work\jsc.svn\examples\javascript\Ultra\UltraApplicationWithAssets3\UltraApplicationWithAssets3\bin\Debug\staging.debug\UltraApplicationWithAssets3.UltraWebService\staging.net\App_Data\Database1.mdf failed. A database with the same name exists, or specified file cannot be opened, or it is located on UNC share.

			foreach (var item in ctx.Table1s)
			{
				result(DateTime.Now + " - " + item.Field1);
			}
		}
	}
}
