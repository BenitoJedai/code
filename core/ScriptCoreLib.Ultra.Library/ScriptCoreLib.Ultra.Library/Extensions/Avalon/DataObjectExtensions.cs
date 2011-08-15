using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace ScriptCoreLib.Extensions.Avalon
{
    public static class DataObjectExtensions
    {
        public static object GetDataOrException(this IDataObject source, string format, bool ConvertToString = false)
        {
            var value = default(object);

            try
            {
                value = source.GetData(format);

                if (ConvertToString)
                    (value as MemoryStream).With(
                        Stream =>
                        {
                            value = Encoding.UTF8.GetString(Stream.ReadToEnd()).TakeUntilIfAny("\0");
                        }
                    );
            }
            catch (Exception exc)
            {
                value = exc;
            }

            return value;
        }
    }
}
