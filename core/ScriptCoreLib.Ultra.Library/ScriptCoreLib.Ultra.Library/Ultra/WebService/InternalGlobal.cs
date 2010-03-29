using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Ultra.WebService
{
	public abstract class InternalGlobal : HttpApplication
	{
		HttpApplication InternalApplicationOverride;
		public HttpApplication InternalApplication
		{
			get
			{
				if (InternalApplicationOverride != null)
					return InternalApplicationOverride;

				return this;
			}
		}

		public void SetApplication(HttpApplication value)
		{
			this.InternalApplicationOverride = value;
		}


		public bool FileExists()
		{
			return InternalGlobalExtensions.FileExists(this);
		}

		public abstract InternalFileInfo[] GetFiles();

		public abstract InternalWebMethodInfo[] GetWebMethods();

		public abstract void Invoke(InternalWebMethodInfo e);

		public abstract InternalScriptApplication[] GetScriptApplications();

		public abstract void Serve(WebServiceHandler h);
	}

}
