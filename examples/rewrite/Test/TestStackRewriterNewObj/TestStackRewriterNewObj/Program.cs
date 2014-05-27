using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStackRewriterNewObj
{
    class Program : IEnumerator
    {
        void MoveNext(IEnumerable<byte[]> bytes)
        {
            new GIFEncoderWorker(
                       96,
                       96,
                           delay: 1000 / 10,
                       frames: bytes,
                       AtFrame:
                         index =>
                          {
                              //Native.document.title = new { index }.ToString();
                          }


                   ).Task.ContinueWith(
                      t =>
                        {
                        }
                );

        }
        static void Main(string[] args)
        {
        }

        object IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        bool IEnumerator.MoveNext()
        {
            throw new NotImplementedException();
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class GIFEncoderWorker(int width,
        int height, int delay = 66, int repeat = 0,
        IEnumerable<byte[]> frames = null, Action<int> AtFrame = null)
    {

        public Task<object> Task;
    }
}
