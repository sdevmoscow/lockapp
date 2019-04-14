using System;
using System.Threading;

namespace LockApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new SomeClass().Go();



        }

        private static void MyEventHandler(object sender, EventArgs e)
        {

        }

        private static void MyMethod(CancellationToken token)
        {
            for (int i = 0; i < 1000; i++)
            {
                token.ThrowIfCancellationRequested();
                Console.Write(i + " ");
                Thread.Sleep(100);
            }
        }

    }



}
