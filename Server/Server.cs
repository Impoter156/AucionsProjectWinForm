using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Server
{
    public partial class Server : Form
    {
        private UdpClient udpServer;
        private IPEndPoint remoteEndPoint;
        private const int PORT = 5000;
        private string selectedImageName;
        private byte[] sharedKey = Encoding.UTF8.GetBytes("auctions");
        private System.Windows.Forms.Timer countdownTimerServer;
        private int countdownValueServer;
        private List<Bid> bids; // To store incoming bids
        private List<Product> products; // List to hold products
        private readonly object bidLock = new object();
        private Product currentProduct; // Currently selected product
        private Dictionary<string, decimal> firstBids = new Dictionary<string, decimal>(); // To store the first bid of each bidder
        private bool breakCountDown = false;


        public Server()
        {
            InitializeComponent();
            StartServer();
            InitializeComponents();
            bids = new List<Bid>();
            products = new List<Product>
            {
                new Product("pictureBox1", "Golden Watch", 300, "This is a luxurious watch, made of gold, and it's a limited edition."),
                new Product("pictureBox2", "Plate from 18th century", 150, "This is an antique plate from the 18th century, with intricate designs."),
                new Product("pictureBox3", "Broze Statue", 200, "This is a bronze statue of a knight on horseback, from the Renaissance period."),
                new Product("pictureBox4", "Dial Telephone", 100, "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.")
            };

            // Set up PictureBox click event handlers
            pictureBox1.Click += pictureBox_Click;
            pictureBox2.Click += pictureBox_Click;
            pictureBox3.Click += pictureBox_Click;
            pictureBox4.Click += pictureBox_Click;

            // Start receiving messages asynchronously
            ReceiveMessagesAsync();
        }

        private void StartServer()
        {
            udpServer = new UdpClient(PORT);
            remoteEndPoint = new IPEndPoint(IPAddress.Any, PORT);
        }

        private void InitializeComponents()
        {
            countdownTimerServer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // Set interval to 1 second
            };
        }

        public static byte[] ComputeHMAC(string message, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
        }

        private async void ReceiveMessagesAsync()
        {
            while (true)
            {
                var result = await udpServer.ReceiveAsync();
                string receivedMessage = Encoding.ASCII.GetString(result.Buffer.Take(result.Buffer.Length - 32).ToArray());
                byte[] receivedHMAC = result.Buffer.Skip(result.Buffer.Length - 32).ToArray();

                if (VerifyHMAC(receivedMessage, receivedHMAC))
                {
                    await ProcessBidAsync(receivedMessage);
                }
            }
        }

        private bool VerifyHMAC(string message, byte[] receivedHMAC)
        {
            byte[] computedHMAC = ComputeHMAC(message, sharedKey);
            return receivedHMAC.SequenceEqual(computedHMAC);
        }

        private async Task ProcessBidAsync(string message)
        {
            string[] parts = message.Split('|');
            if (parts.Length == 2)
            {
                var bid = new Bid
                {
                    BidderName = parts[0],
                    Amount = decimal.Parse(parts[1])
                };

                // Locking to ensure thread safety
                await Task.Run(() =>
                {
                    lock (bidLock)
                    {
                        //breakCountDown = true;

                        // Check if the bid amount is lower than the starting price
                        if (bid.Amount <= currentProduct.Price)
                        {
                            SendMessageToClient($"{bid.BidderName} your bid must be greater than starting price of {currentProduct.Price}");
                            return;
                        }

                        // Check if the bid amount already exists
                        if (bids.Any(b => b.Amount == bid.Amount))
                        {
                            SendMessageToClient($"{bid.BidderName} amount {bid.Amount} is already taken.");
                            return;
                        }

                        // Check if this is the first bid from the bidder
                        if (!firstBids.ContainsKey(bid.BidderName))
                        {
                            firstBids[bid.BidderName] = bid.Amount; // Store the first bid
                        }
                        else
                        {
                            // Ensure the new bid is greater than the first bid
                            if (bid.Amount <= firstBids[bid.BidderName])
                            {
                                SendMessageToClient($"{bid.BidderName} your bid must be greater than your first bid of {firstBids[bid.BidderName]}.");
                                return;
                            }
                        }

                        // Add the bid to the list and update the current product price
                        bids.Add(bid);
                        currentProduct.Price = bid.Amount; // Update the product price
                        UpdateTextBoxes(bid, GetBidderIndex(bid.BidderName));

                        ResetCountdown(); // Reset countdown if a new price is received

                    }
                });
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox clickedPictureBox)
            {
                int productIndex = int.Parse(clickedPictureBox.Name.Substring(clickedPictureBox.Name.Length - 1)) - 1;
                currentProduct = products[productIndex];
                selectedImageName = currentProduct.NameBox;

                AppendText_Server(currentProduct.Description); // Display the product description
                productName_textbox.Text = currentProduct.Name; // Display the product name
                CurrentPrice_textbox.Text = currentProduct.Price.ToString(); // Display the product price
            }
        }

        private int GetBidderIndex(string bidderName)
        {
            if (bids.Any(b => b.BidderName.Equals(bidderName, StringComparison.OrdinalIgnoreCase)))
            {
                return bids.FindIndex(b => b.BidderName.Equals(bidderName, StringComparison.OrdinalIgnoreCase)) + 1;
            }

            return bids.Count + 1; // Example logic for new bidders
        }

        private void UpdateTextBoxes(Bid bid, int bidderIndex)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Bid, int>(UpdateTextBoxes), bid, bidderIndex);
            }
            else
            {
                switch (bidderIndex)
                {
                    case 1:
                        textBox_bidder1Name.Text = bid.BidderName;
                        textBox_bidder1Price.Text = bid.Amount.ToString();
                        AppendText_Server($"Bid accepted: {textBox_bidder1Name.Text} has bid {textBox_bidder1Price.Text} for {currentProduct.Name}.");
                        SendMessageToClient($"Bid accepted: {textBox_bidder1Name.Text} has bid {textBox_bidder1Price.Text} for {currentProduct.Name}.");
                        break;
                    case 2:
                        textBox_bidder2Name.Text = bid.BidderName;
                        textBox_bidder2Price.Text = bid.Amount.ToString();
                        AppendText_Server($"Bid accepted: {textBox_bidder2Name.Text} has bid {textBox_bidder2Price.Text} for {currentProduct.Name}.");
                        SendMessageToClient($"Bid accepted: {textBox_bidder2Name.Text} has bid {textBox_bidder2Price.Text} for {currentProduct.Name}.");
                        break;
                    default:
                        break;
                }
            }
        }

        private void Selling_btn_Click(object sender, EventArgs e)
        {
            SendMessageToClient($"{selectedImageName}|{productName_textbox.Text}|{CurrentPrice_textbox.Text}|{textBox1.Text}");
        }

        private void SendMessageToClient(string message)
        {
            byte[] hmac = ComputeHMAC(message, sharedKey);
            byte[] messageWithHMAC = Encoding.UTF8.GetBytes(message).Concat(hmac).ToArray();
            udpServer.Send(messageWithHMAC, messageWithHMAC.Length, "localhost", 5001);
            udpServer.Send(messageWithHMAC, messageWithHMAC.Length, "localhost", 5002);
        }

        private void CountDown_btn_Click(object sender, EventArgs e)
        {

            textBox_Winner.Text = ""; // Clear previous winner display

            countdownValueServer = 3; // Reset the countdown value
            textBox_countdownServer.Text = countdownValueServer.ToString(); // Display initial countdown

            // Clear existing event handlers to avoid multiple subscriptions
            countdownTimerServer.Tick -= CountdownTimerServer_Tick;
            countdownTimerServer.Tick += CountdownTimerServer_Tick; // Add new handler

            // Sending the countdown message along with the selected product details
            SendMessageToClient("start_countDown");
            countdownTimerServer.Start(); // Start the countdown timer
        }

        private void CountdownTimerServer_Tick(object sender, EventArgs e)
        {
            if (countdownValueServer > 0)
            {
                countdownValueServer--;
                textBox_countdownServer.Text = countdownValueServer.ToString(); // Update countdown display
            }
            else
            {
                countdownTimerServer.Stop(); // Stop the timer
                var highestBid = bids.OrderByDescending(b => b.Amount).FirstOrDefault();
                if (highestBid != null)
                {
                    textBox_Winner.Text = highestBid.BidderName; // Display the winner
                    textBox_winneramount.Text = highestBid.Amount.ToString();
                    SendMessageToClient($"winner: {highestBid.BidderName}"); // Notify client of the winner
                }
            }
        }
        private void ResetCountdown()
        {
            if (countdownTimerServer.Enabled)
            {
                countdownTimerServer.Stop(); // Stop the timer
            }
            countdownValueServer = 3; // Reset to the starting value
        }

        private void AppendText_Server(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendText_Server), message);
                return;
            }
            textBox1.AppendText(Environment.NewLine + message);
        }

    }


    public class Product
    {
        public string NameBox { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } // New property for description

        public Product(string namebox, string name, decimal price, string description)
        {
            NameBox = namebox;
            Name = name;
            Price = price;
            Description = description;
        }
    }

    public class Bid
    {
        public string BidderName { get; set; }
        public decimal Amount { get; set; }
    }
}
