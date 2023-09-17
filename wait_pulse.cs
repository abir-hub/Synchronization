using System;
using System.Threading;

class Program
{
    static readonly object lockObject = new object();

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
            Console.WriteLine("Thread 1 is running...");
            Thread.Sleep(4000); // Simulate some work for 2 seconds

            Console.WriteLine("Thread 1 is waiting...");
            Monitor.Wait(lockObject);

            Console.WriteLine("Thread 1 is running again...");
            Thread.Sleep(2000); // Simulate some work for 2 seconds
        }
    }

    static void Thread2Method()
    {
        lock (lockObject)
        {
            Console.WriteLine("Thread 2 is running...");
            Thread.Sleep(3000); // Simulate some work for 2 seconds

            Console.WriteLine("Thread 2 is notifying Thread 1...");
            Monitor.Pulse(lockObject);

            // You can also use Monitor.PulseAll(lockObject) if there are multiple waiting threads.
        }
    }
}
