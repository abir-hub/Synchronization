//Before using lock
using System;
using System.Threading;

namespace ThreadStateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(SomeMethod)
            {
                Name = "Thread 1"
            };

            Thread thread2 = new Thread(SomeMethod)
            {
                Name = "Thread 2"
            };

            Thread thread3 = new Thread(SomeMethod)
            {
                Name = "Thread 3"
            };

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Console.ReadKey();
        }

        public static void SomeMethod()
        {
            Console.Write("[Welcome To The ");
            Thread.Sleep(1000);
            Console.WriteLine("World of Dotnet!]");
        }
    }
}





//After using lock
using System;
using System.Threading;

namespace ThreadStateDemo
{
    class Program
    {
        static object lockObject = new object();
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(SomeMethod)
            {
                Name = "Thread 1"
            };

            Thread thread2 = new Thread(SomeMethod)
            {
                Name = "Thread 2"
            };

            Thread thread3 = new Thread(SomeMethod)
            {
                Name = "Thread 2"
            };

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Console.ReadKey();
        }

        public static void SomeMethod()
        {
            // Locking the Shared Resource for Thread Synchronization
            lock (lockObject)
            {
                Console.Write("[Welcome To The ");
                Thread.Sleep(1000);
                Console.WriteLine("World of Dotnet!]");
            }
        }
    }
}
