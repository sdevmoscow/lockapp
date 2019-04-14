using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace LockApp
{
    class AsyncCaller 
    {
        private delegate void MyDelegate(CancellationToken token);
        static MyDelegate obj;
        private static EventHandler handler;

        public AsyncCaller(EventHandler h)
        {
            handler = h;
        }

        public bool Invoke(int duration, object sender, EventArgs e)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            IAsyncResult result = handler.BeginInvoke(sender, e, null, null);//  obj.BeginInvoke(token, null, null);
            new Thread(() => 
            {
                Thread.Sleep(duration);
                if (result.IsCompleted != true)
                {
                    token.ThrowIfCancellationRequested();
                }
            }).Start();

            try
            {
                obj.EndInvoke(result);
                Console.WriteLine("\nOperation finished successfully.");
                return true;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nOperation was canceled.");
                return false;
            }
        }
        //EventHandler h = new EventHandler(this.myEventHandler);

    }
}
