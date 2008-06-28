using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

#if !NoAttributes
using ScriptCoreLib;
#endif

namespace FlashTowerDefense.Shared
{


    public partial class SharedClass1
    {
        
        partial class RemoteEvents
        {
            public void EmptyHandler<T>(T Arguments)
            {
            }

            partial class DispatchHelper
            {
                public Converter<object, int> GetLength;

                public DispatchHelper()
                {
                    this.GetDoubleArray =
                        delegate
                        {
                            var a = new double[GetLength(null)];
                            
                            for (uint i = 0; i < a.Length; i++)
                                a[i] = this.GetDouble(i);

                            return a;
                        };

                    this.GetInt32Array =
                          delegate
                          {
                              var a = new int[GetLength(null)];

                              for (uint i = 0; i < a.Length; i++)
                                  a[i] = this.GetInt32(i);

                              return a;
                          };

                    this.GetStringArray =
                          delegate
                          {
                              var a = new string[GetLength(null)];

                              for (uint i = 0; i < a.Length; i++)
                                  a[i] = this.GetString(i);

                              return a;
                          };
                }
            }
        }


    }
}
