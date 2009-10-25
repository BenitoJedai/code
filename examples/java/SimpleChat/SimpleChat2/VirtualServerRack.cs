﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleChat.Library;
using System.IO;

namespace SimpleChat2
{
	public class VirtualServerRack
	{

		public int[] Ports;

	

		public Action Stop;

		public delegate void CommandRequestDelegate(Stream s, string path);

		public event CommandRequestDelegate CommandRequest;

		public void Start()
		{
			Action Stop =
				delegate
				{
					this.Stop = null;
				};

			foreach (var Port in Ports)
			{
				var t = Port.ToListener(
					s =>
					{
						var hr = new HeaderReader();
						var hr_Method_path = "";

						hr.Method +=
							(method, path) =>
							{
								hr_Method_path = path;
							};

						hr.Header +=
							(key, value) =>
							{
								//Console.WriteLine(key + ": " + value);
							};

						hr.Read(s);

						this.CommandRequest(s, hr_Method_path);

						s.Close();
					}
				);

				Stop +=
					delegate
					{
						t.Abort();
					};
			}

			this.Stop = Stop;
		}
	}
}
