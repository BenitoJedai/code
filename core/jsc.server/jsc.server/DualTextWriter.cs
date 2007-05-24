using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.server
{
    public delegate void Action();

    public class DualTextWriter : System.IO.TextWriter, IDisposable
    {
        public System.IO.TextWriter Channel1;

        public System.IO.TextWriter Channel2;


        public override void Write(char value)
        {
            this.Channel1.Write(value);

            if (this.Channel2 == null)
                System.Diagnostics.Debug.Write(value);
            else
                this.Channel2.Write(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.Channel1.Write(buffer, index, count);

            if (this.Channel2 == null)
                System.Diagnostics.Debug.Write(new string(buffer, index, count));
            else
                this.Channel2.Write(buffer, index, count);
        }

        public override Encoding Encoding
        {
            get { return Channel1.Encoding; }
        }

        public Action Disposing;



        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (Disposing != null)
                Disposing();
        }

        #endregion


        /// <summary>
        /// during using this instance the console output will be mirrored to a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DualTextWriter StreamToFile(System.IO.FileInfo file)
        {
            var dual = new DualTextWriter();

            dual.Channel1 = Console.Out;
            dual.Channel2 = new System.IO.StreamWriter(file.OpenWrite());

            Console.SetOut(dual);

            dual.Disposing =
                delegate
                {
                    dual.Channel2.Close();

                    Console.SetOut(dual.Channel1);
                };

            return dual;
        }

        public static DualTextWriter StreamToDiagnostics()
        {
            var dual = new DualTextWriter();

            dual.Channel1 = Console.Out;

            Console.SetOut(dual);

            dual.Disposing =
                delegate
                {
                    Console.SetOut(dual.Channel1);
                };

            return dual;
        }
    }
}
