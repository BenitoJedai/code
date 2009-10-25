using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleChat
{
	public class RemoteUserInfo
	{
		public MessageEndpoint[] EndPoints = new MessageEndpoint[0];

		public TreeNode Node;
	}

	public static class RemoteUserInfoExtensions
	{
		public static RemoteUserInfo[] Concat(this RemoteUserInfo[] f, RemoteUserInfo v)
		{
			if (f == null)
				return new[] { v };

			var a = new RemoteUserInfo[f.Length + 1];

			Array.Copy(f, a, f.Length);

			a[f.Length] = v;

			return a;
		}
	}

}
