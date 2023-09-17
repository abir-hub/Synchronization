//Before deadlock


// After deadlock problem

using System;
using System.Threading;

class Program
{
    static readonly object lockObject = new object();
    static bool thread1HasControl = true;

    static void Main()
    {
        Thread thread1 = new Thread(Thread1Method);
        Thread thread2 = new Thread(Thread2Method);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Both threads have finished.");
    }

    static void Thread1Method()
    {
        lock (lockObject)
        {
            while (!thread1HasControl)
            {
                Monitor.Wait(lockObject);
            }

            Console.WriteLine("Thread 1 is running...");
            Thread.Sleep(2000); // Simulate some work for 2 seconds

            Console.WriteLine("Thread 1 is passing control to Thread 2...");
            thread1HasControl = false;
            Monitor.Pulse(lockObject);
        }
    }

    static void Thread2Method()
    {
        lock (lockObject)
        {
            while (thread1HasControl)
            {
                Monitor.Wait(lockObject);
            }

            Console.WriteLine("Thread 2 is running...");
            Thread.Sleep(3000); // Simulate some work for 3 seconds

            Console.WriteLine("Thread 2 is passing control to Thread 1...");
            thread1HasControl = true;
            Monitor.Pulse(lockObject);
        }
    }
}
