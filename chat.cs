using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static Queue<string> messageQueue = new Queue<string>();
    static object lockObject = new object();

    static void Main(string[] args)
    {
        // Start a thread for sending messages
        Thread senderThread = new Thread(SendMessages);
        senderThread.Start();

        // Start a thread for receiving messages
        Thread receiverThread = new Thread(ReceiveMessages);
        receiverThread.Start();

        // Keep the main thread running
        while (true)
        {
            // Read user input and send it as a message
            Console.Write("You: ");
            string message = Console.ReadLine();
            lock (lockObject)
            {
                messageQueue.Enqueue(message);
                Monitor.PulseAll(lockObject); // Notify receiver thread
            }
        }
    }

    static void SendMessages()
    {
        while (true)
        {
            string message;
            lock (lockObject)
            {
                while (messageQueue.Count == 0)
                {
                    Monitor.Wait(lockObject); // Wait for messages to be available
                }
                message = messageQueue.Dequeue();
            }

            // Simulate sending the message
            Console.WriteLine($"Sent: {message}");
        }
    }

    static void ReceiveMessages()
    {
        while (true)
        {
            string message;
            lock (lockObject)
            {
                while (messageQueue.Count == 0)
                {
                    Monitor.Wait(lockObject); // Wait for messages to be available
                }
                message = messageQueue.Dequeue();
            }

            // Simulate receiving and displaying the message
            Console.WriteLine($"Received: {message}");
        }
    }
}
