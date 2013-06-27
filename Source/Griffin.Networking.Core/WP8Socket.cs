using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class WP8Socket
{
    public static void Connect(this Socket client, EndPoint remoteEP)
    {
        using (var done = new ManualResetEvent(false))
        {
            var args = new SocketAsyncEventArgs();
            args.Completed += (sender, eventArgs) =>
                {
                    done.Set();
                };
            client.ConnectAsync(args);
            done.WaitOne();
            if (args.SocketError != SocketError.Success)
                throw new SocketException((int)args.SocketError);
        }
    }    
}