using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static byte[] sharedKey = Encoding.UTF8.GetBytes("2251120449");

    static void Main(string[] args)
    {
        Task[] tasks = new Task[2];

        tasks[0] = Task.Run(() => SendBid("Bidder1", 150));
        tasks[1] = Task.Run(() => SendBid("Bidder2", 150));

        Task.WaitAll(tasks);

        //Console.WriteLine("Bids sent. Press any key to exit...");
        //Console.ReadKey();
    }

    private static void SendBid(string bidderName, decimal amount)
    {
        using (UdpClient udpClient = new UdpClient())
        {
            string message = $"{bidderName}|{amount}";
            byte[] hmac = ComputeHMAC(message, sharedKey);
            byte[] messageWithHMAC = Encoding.UTF8.GetBytes(message).Concat(hmac).ToArray();

            udpClient.Send(messageWithHMAC, messageWithHMAC.Length, "localhost", 5000);
            Console.WriteLine($"Sent bid from {bidderName}: ${amount}");
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
