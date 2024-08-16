using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static byte[] sharedKey = Encoding.UTF8.GetBytes("auctions");
    private const int Port = 5000;

    static async Task Main(string[] args)
    {
        // Start the UDP listener in a separate task
        Task listenerTask = StartUdpListenerAsync();

        // Simulate sending bids from clients
        Task[] tasks = new Task[2];
        tasks[0] = SendBidAsync("Bidder1", 150);
        tasks[1] = SendBidAsync("Bidder2", 150);

        await Task.WhenAll(tasks);

        Console.WriteLine("Bids sent. Press any key to exit...");
        Console.ReadKey();
    }

    private static async Task StartUdpListenerAsync()
    {
        using (UdpClient udpServer = new UdpClient(Port))
        {
            Console.WriteLine("UDP Server is listening...");

            while (true)
            {
                var result = await udpServer.ReceiveAsync();
                ProcessReceivedBid(result);
            }
        }
    }

    private static void ProcessReceivedBid(UdpReceiveResult result)
    {
        string message = Encoding.UTF8.GetString(result.Buffer.Take(result.Buffer.Length - 32).ToArray());
        byte[] receivedHMAC = result.Buffer.Skip(result.Buffer.Length - 32).ToArray();

        // Validate HMAC
        byte[] computedHMAC = ComputeHMAC(message, sharedKey);
        if (receivedHMAC.SequenceEqual(computedHMAC))
        {
            Console.WriteLine($"Received bid: {message}");
        }
        else
        {
            Console.WriteLine("Received bid with invalid HMAC.");
        }
    }

    private static async Task SendBidAsync(string bidderName, decimal amount)
    {
        using (UdpClient udpClient = new UdpClient())
        {
            string message = $"{bidderName}|{amount}";
            byte[] hmac = ComputeHMAC(message, sharedKey);
            byte[] messageWithHMAC = Encoding.UTF8.GetBytes(message).Concat(hmac).ToArray();

            try
            {
                await udpClient.SendAsync(messageWithHMAC, messageWithHMAC.Length, "localhost", Port);
                Console.WriteLine($"Sent bid from {bidderName}: ${amount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending bid from {bidderName}: {ex.Message}");
            }
        }
    }

    public static byte[] ComputeHMAC(string message, byte[] key)
    {
        using (var hmac = new HMACSHA256(key))
        {
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        }
    }
}
