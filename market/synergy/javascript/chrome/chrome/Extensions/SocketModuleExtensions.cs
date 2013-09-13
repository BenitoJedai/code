using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace chromelabs.Extensions
//{
public static class SocketModuleExtensions
{
    public static Task<int> setMulticastTimeToLiveAsync(this SocketModule chrome_socket, int socketId, int ttl = 30)
    {
        var x = new TaskCompletionSource<int>();

        chrome_socket.setMulticastTimeToLive(socketId, ttl,
                    callback:
                        new Action<int>(value_setMulticastTimeToLive =>
                        {
                            x.SetResult(value_setMulticastTimeToLive);
                        }
            )
        );

        return x.Task;
    }

    public static Task<int> bindAsync(this SocketModule chrome_socket, int socketId, string address, int port)
    {
        var x = new TaskCompletionSource<int>();

        chrome_socket.bind(socketId, address, port,

            callback:
                    new Action<int>(
                         value_bind =>
                         {
                             x.SetResult(value_bind);
                         }
                    )

        );
        return x.Task;
    }

    public static Task<int> joinGroupAsync(this SocketModule m, int socketId, string address)
    {
        var x = new TaskCompletionSource<int>();
        chrome.socket.joinGroup(socketId, address,
          callback: new Action<int>(
                  value_joinGroup =>
                  {
                      x.SetResult(value_joinGroup);
                  }
          )
          );
        return x.Task;
    }

    public static Task<RecvFromInfo> recvFromAsync(this SocketModule m, int socketId, int bufferSize = 1048576)
    {
        var x = new TaskCompletionSource<RecvFromInfo>();

        m.recvFrom(socketId, bufferSize,
            callback: new Action<RecvFromInfo>(
                 result =>
                 {
                     x.SetResult(result);
                 }
            )
        );

        return x.Task;
    }
}
//}
