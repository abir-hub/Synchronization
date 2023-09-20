//One way chat

using System;
using System.Threading;

class Program
{
    static readonly object lockObject = new object();
    static string message = "";

    static void Main(string[] args)
    {
        Thread senderThread = new Thread(SendMessage);
        Thread receiverThread = new Thread(ReceiveMessage);

        senderThread.Start();
        receiverThread.Start();

        senderThread.Join();
        receiverThread.Join();
    }

    static void SendMessage()
    {
        while (true)
        {
            Console.WriteLine("Enter your message (or type 'exit' to quit):");
            string input = Console.ReadLine();

            lock (lockObject)
            {
                if (input.ToLower() == "exit")
                {
                    message = "exit";
                    break;
                }

                message = input;
            }
        }
    }

    static void ReceiveMessage()
    {
        while (true)
        {
            lock (lockObject)
            {
                if (message.ToLower() == "exit")
                    break;

                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine("Received: " + message);
                    message = "";
                }
            }
        }
    }
}
