using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private UdpClient udpServer;
        private IPEndPoint remoteEndPoint;
        private const int PORT = 5000;
        private string selectedImageName;
        private CancellationTokenSource cancellationTokenSource;
        private byte[] sharedKey = Encoding.UTF8.GetBytes("2251120449");
        private System.Windows.Forms.Timer countdownTimer;
        private int countdownValue;
        private List<Bid> bids; // To store incoming bids
        private readonly object bidLock = new object();

        public Server()
        {
            InitializeComponent();
            StartServer();
            InitializeComponents();
            bids = new List<Bid>();
            ReceiveMessagesAsync();
        }

        private void StartServer()
        {
            udpServer = new UdpClient(PORT);
            remoteEndPoint = new IPEndPoint(IPAddress.Any, PORT);
        }

        private void InitializeComponents()
        {
            countdownTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // Set interval to 1 second
            };
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        public static byte[] ComputeHMAC(string message, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
        }

        private async Task ReceiveMessagesAsync()
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
            // Simulate some asynchronous work, if needed
            await Task.Yield();

            if (string.IsNullOrEmpty(message))
            {
                // Handle the null or empty message case
                return; // or log an error
            }

            string[] parts = message.Split('|');
            if (parts.Length == 2 && !string.IsNullOrEmpty(parts[1]))
            {
                var bid = new Bid
                {
                    BidderName = parts[0],
                    Amount = decimal.Parse(parts[1]) // Ensure this is a valid decimal
                };

                int bidderIndex;

                // Locking to ensure thread safety
                lock (bidLock)
                {
                    bidderIndex = GetBidderIndex(bid.BidderName);

                    if (bidderIndex > 0)
                    {
                        bids.Add(bid);
                        UpdateTextBoxes(bid, bidderIndex);
                        CheckForTieBids();
                    }
                }
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
                        break;
                    case 2:
                        textBox_bidder2Name.Text = bid.BidderName;
                        textBox_bidder2Price.Text = bid.Amount.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void CheckForTieBids()
        {
            var highestBid = bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            var tiedBids = bids.Where(b => b.Amount == highestBid.Amount).ToList();

            if (tiedBids.Count > 1)
            {
                // Notify about the tie and initiate a secondary auction
                MessageBox.Show("Tie detected! Initiating secondary auction among bidders.");
                bids.Clear(); // Clear bids after processing

                //StartSecondaryAuction(tiedBids);
                //ReceiveMessagesAsync();
            }
        }

        private void StartSecondaryAuction(List<Bid> tiedBids)
        {
            var winner = tiedBids.First(); // In a real scenario, you would collect new bids from tied bidders
            bids.Clear(); // Clear bids after processing
        }

        private void Selling_btn_Click(object sender, EventArgs e)
        {
            // Assuming you want to send the current selected product details
            string imageName = selectedImageName; // Selected image name
            string productName = textBox2.Text;   // Product name
            string startingPrice = textBox3.Text; // Starting price
            string productDescription = textBox1.Text;

            // Send the selected image name, product name, and starting price to the client
            SendMessageToClient(imageName, productDescription ,productName, startingPrice);
        }


        private void SendMessageToClient(string imageName, string productDescription ,string productName, string startingPrice)
        {
            string message = $"{imageName}|{productDescription}|{productName}|{startingPrice}";
            MessageBox.Show(message);
            byte[] hmac = ComputeHMAC(message, sharedKey);
            byte[] messageWithHMAC = Encoding.UTF8.GetBytes(message).Concat(hmac).ToArray();
            udpServer.Send(messageWithHMAC, messageWithHMAC.Length, "localhost", 5001);
            udpServer.Send(messageWithHMAC, messageWithHMAC.Length, "localhost", 5002);
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox1.Name;
            textBox1.Text = "This is a luxurious watch, made of gold, and it's a limited edition.";
            textBox2.Text = "Golden Watch";
            textBox3.Text = "300";
            //SendMessageToClient(selectedImageName, textBox1.Text, textBox2.Text, textBox3.Text);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox2.Name;   
            textBox1.Text = "This is an antique plate from the 18th century, with intricate designs.";
            textBox2.Text = "Plate from 18th century";
            textBox3.Text = "150";
            //SendMessageToClient(selectedImageName, textBox1.Text, textBox2.Text, textBox3.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox3.Name;
            textBox1.Text = "This is a bronze statue of a knight on horseback, from the Renaissance period.";
            textBox2.Text = "Broze Statue";
            textBox3.Text = "200";
            //SendMessageToClient(selectedImageName, textBox1.Text, textBox2.Text, textBox3.Text);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox4.Name;
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";
            textBox2.Text = "Dial Telephone";
            textBox3.Text = "100";
            SendMessageToClient(selectedImageName, textBox1.Text, textBox2.Text, textBox3.Text);
        }


        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (countdownValue > 0)
            {
                textBox_countdown.Text = countdownValue.ToString();
                countdownValue--;
            }
            else
            {
                countdownTimer.Stop();
            }
        }

        private void CountDown_btn_Click(object sender, EventArgs e)
        {
            countdownValue = 5;
            textBox_countdown.Text = countdownValue.ToString();
            countdownTimer.Start();

            // Assuming you want to send the current selected product details

            string productName = textBox2.Text;  // Product name
            string startingPrice = textBox3.Text; // Starting price
            string productDescription = textBox1.Text;

            // Sending the countdown message along with the selected product details
            SendMessageToClient(selectedImageName, productDescription, productName, startingPrice);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            udpServer?.Close();
            base.OnFormClosing(e);
        }

        private void textBox_bidder2Price_TextChanged(object sender, EventArgs e)
        {

        }

        private void bidder2Name_lablel_Click(object sender, EventArgs e)
        {

        }

        private void textBox_bidder1Price_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Server_Load(object sender, EventArgs e)
        {

        }

        private void bidder2Price_label_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bidder1Name_label_Click(object sender, EventArgs e)
        {

        }

        private void bidder1Price_label_Click(object sender, EventArgs e)
        {

        }

        private void textBox_countdown_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class Bid
    {
        public string BidderName { get; set; }
        public decimal Amount { get; set; }
    }
}




