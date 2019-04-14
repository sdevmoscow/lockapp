using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockApp
{
    class SomeClass
    {
        private int count = 0;
        private int lockCount = 0;
        private object lockObject = new object();
        private bool locked = false;

        public int GetCount()
        {
            while (lockCount > 0) ;

            Console.WriteLine("GetCount()={0}", count);
            return count;
        }

        public void AddToCount(int value)
        {
            lockCount++;
            lock(lockObject)
            {
                Console.WriteLine("locked, value={0}", value);
                locked = true;
                count += value;
                Thread.Sleep(1500);
            }
            lockCount--;
            locked = false;
            Console.WriteLine("lock off, value={0}", value);
        }

        public void Go()
        {
            var tasks = new Task[5];

            tasks[0] = Task.Run(() => AddToCount(1));
            Thread.Sleep(100);
            tasks[1] = Task.Run(() => AddToCount(2));
            Thread.Sleep(100);
            tasks[2] = Task.Run(() => GetCount());
            Thread.Sleep(100);
            tasks[3] = Task.Run(() => AddToCount(3));
            Thread.Sleep(100);
            tasks[4] = Task.Run(() => GetCount());
            Thread.Sleep(100);

            Task.WaitAll(tasks);

            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }
}
