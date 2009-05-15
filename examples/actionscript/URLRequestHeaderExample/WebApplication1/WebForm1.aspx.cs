using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var data = new
			{
				XMyHeader = this.Request.Headers["XMyHeader"],
				name = this.Request.Params["name"],
				age = this.Request.Params["age"],

			};

			this.Response.Write("hi! " + data.ToString());
			this.Response.End();
			this.Response.Close();
		}
	}
}
