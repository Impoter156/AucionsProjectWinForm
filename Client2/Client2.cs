using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client2
{
    public partial class Client2 : Form
    {
        private UdpClient udpClient;
        private IPEndPoint receivePoint;
        private Thread receiveThread;
        private byte[] sharedKey;
        private System.Windows.Forms.Timer countdownTimerClient;
        private int countdownValueClient2;
        private string winner;


        public Client2()
        {
            InitializeComponent();
            InitializeUdpClient();
            InitializeCountdownTimer();
            StartReceiveThread();
        }

        private void InitializeUdpClient()
        {
            receivePoint = new IPEndPoint(IPAddress.Any, 0);
            udpClient = new UdpClient(5002); // Ensure this is different from the server port
            sharedKey = Encoding.UTF8.GetBytes("2251120449"); // Define your shared key here
        }

        private void InitializeCountdownTimer()
        {
            countdownTimerClient = new System.Windows.Forms.Timer
            {
                Interval = 1000 // Set interval to 1 second
            };
        }

        private void StartReceiveThread()
        {
            receiveThread = new Thread(WaitPacketsFromServer)
            {
                IsBackground = true
            };
            receiveThread.Start();
        }

        public async void WaitPacketsFromServer()
        {
            while (true)
            {
                var result = await udpClient.ReceiveAsync(); // Asynchronously receive data
                ProcessReceivedData(result.Buffer);
            }
        }
        private void ProcessReceivedData(byte[] data)
        {
            string strData = Encoding.ASCII.GetString(data.Take(data.Length - 32).ToArray()); // Assuming last 32 bytes are HMAC
            byte[] receivedHMAC = data.Skip(data.Length - 32).ToArray();

            MessageBox.Show("recieve");

            // Verify HMAC
            if (VerifyHMAC(strData, receivedHMAC))
            {
                var parts = strData.Split('|');
                if (parts.Length == 4)
                {
                    UpdateCheckbox(parts[0], parts[1], parts[2], parts[3]);
                    StartCountdown(strData);
                }
            }
            else
            {
                MessageBox.Show("HMAC verification failed.");
            }
        }
        private bool VerifyHMAC(string message, byte[] receivedHMAC)
        {
            byte[] computedHMAC = ComputeHMAC(message, sharedKey);
            return receivedHMAC.SequenceEqual(computedHMAC);
        }

        private byte[] ComputeHMAC(string message, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
        }

        private void UpdateCheckbox(string imageName, string productDescription, string productName, string startingPrice)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, string, string, string>(UpdateCheckbox), imageName, productDescription, productName, startingPrice);
                return;
            }

            checkBox1.Checked = imageName == "pictureBox1";
            checkBox2.Checked = imageName == "pictureBox2";
            checkBox3.Checked = imageName == "pictureBox3";
            checkBox4.Checked = imageName == "pictureBox4";
            textBox1.Text = productDescription;

            textBox_ProductName.Text = productName;
            textBox2.Text = startingPrice;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a luxurious watch, made of gold, and it's a limited edition.";
            textBox_ProductName.Text = "Golden Watch";
            textBox2.Text = "300";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is an antique plate from the 18th century, with intricate designs.";
            textBox_ProductName.Text = "Plate from 18th century";
            textBox2.Text = "150";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a bronze statue of a knight on horseback, from the Renaissance period.";
            textBox_ProductName.Text = "Broze Statue";
            textBox2.Text = "200";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";
            textBox_ProductName.Text = "Dial Telephone";
            textBox2.Text = "100";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void client_send_Click(object sender, EventArgs e)
        {
            string message = $"{textBox_bidderName.Text}|{textBox_price.Text}";
            byte[] hmac = ComputeHMAC(message, sharedKey);
            byte[] messageWithHMAC = Encoding.UTF8.GetBytes(message).Concat(hmac).ToArray();
            udpClient.Send(messageWithHMAC, messageWithHMAC.Length, "localhost", 5000);
        }

        private void textBox_bidderName_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartCountdown(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            // Ensure the UI update is performed on the UI thread
            if (textBox_countDownClient2.InvokeRequired)
            {
                textBox_countDownClient2.Invoke(new Action<string>(StartCountdown), message);
                return;
            }

            // Handle the winner message
            if (message.StartsWith("winner:"))
            {
                winner = message.Split(':')[1]; // Extract the winner's name
                textBox_Winner.Text = winner; // Display the winner
            }

            // Start the countdown if it's not already running
            if (!countdownTimerClient.Enabled && message == "start_countDown")
            {
                textBox_Winner.Text = ""; // Clear previous winner display
                countdownValueClient2 = 3; // Set the countdown starting value
                textBox_countDownClient2.Text = countdownValueClient2.ToString();

                // Clear existing event handlers to avoid multiple subscriptions
                countdownTimerClient.Tick -= CountdownTimerClient_Tick;
                countdownTimerClient.Tick += CountdownTimerClient_Tick; // Add new handler

                countdownTimerClient.Start(); // Start the countdown
            }
        }

        // Separate method for the countdown timer tick event
        private void CountdownTimerClient_Tick(object sender, EventArgs e)
        {
            if (countdownValueClient2 > 0)
            {
                countdownValueClient2--;
                textBox_countDownClient2.Text = countdownValueClient2.ToString(); // Update countdown display
            }
            else
            {
                countdownTimerClient.Start(); // Start the countdown
            }
        }

        private void Client2_Load(object sender, EventArgs e)
        {

        }
    }
}
