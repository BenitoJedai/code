using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba;

namespace FlashTreasureHunt.Shared
{


	public partial class SharedClass1
	{

		partial class RemoteEvents : IEventsDispatch
		{
			public void EmptyHandler<T>(T Arguments)
			{
			}

			bool IEventsDispatch.DispatchInt32(int e, IDispatchHelper h)
			{
				return Dispatch((Messages)e, h);
			}

			partial class DispatchHelper : IDispatchHelper
			{
				public Converter<object, int> GetLength { get; set; }

				public DispatchHelper()
				{
					this.GetDoubleArray =
						offset =>
						{
							int offseti = (int)offset;
							int len = GetLength(null) - offseti;

							var a = new double[len];

							for (var i = 0; i < a.Length; i++)
							{
								uint ii = (uint)i;
								uint j = ii + offset;

								a[i] = this.GetDouble(j);
							}

							return a;
						};

					this.GetInt32Array =
						offset =>
						{
							int offseti = (int)offset;
							int len = GetLength(null) - offseti;
							var a = new int[len];

							for (var i = 0; i < a.Length; i++)
							{
								uint ii = (uint)i;
								uint j = ii + offset;

								a[i] = this.GetInt32(j);
							}

							return a;
						};

					this.GetStringArray =
						offset =>
						{
							int offseti = (int)offset;
							int len = GetLength(null) - offseti;
							var a = new string[len];

							for (var i = 0; i < a.Length; i++)
							{
								uint ii = (uint)i;
								uint j = ii + offset;

								a[i] = this.GetString(j);
							}

							return a;
						};
				}
			}
		}


	}
}
