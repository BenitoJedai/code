using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba.Generic;
using ScriptCoreLib.Shared.Nonoba;


namespace LightsOut.ActionScript.Shared
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
                        delegate
                        {
                            var a = new double[GetLength(null)];
                            
                            for (int i = 0; i < a.Length; i++)
                                a[i] = this.GetDouble((uint)i);

                            return a;
                        };

                    this.GetInt32Array =
                          delegate
                          {
                              var a = new int[GetLength(null)];

                              for (int i = 0; i < a.Length; i++)
                                  a[i] = this.GetInt32((uint)i);

                              return a;
                          };

                    this.GetStringArray =
                          delegate
                          {
                              var a = new string[GetLength(null)];

                              for (int i = 0; i < a.Length; i++)
                                  a[i] = this.GetString((uint)i);

                              return a;
                          };
                }
            }
        }


    }
}
