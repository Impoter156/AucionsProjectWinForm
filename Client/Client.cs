using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        private UdpClient udpClient;
        private IPEndPoint receivePoint;
        private Thread receiveThread;
        private byte[] sharedKey;
        private System.Windows.Forms.Timer countdownTimerClient;
        private int countdownValueClient;
        private string winner;

        public Client()
        {
            InitializeComponent();
            InitializeUdpClient();
            InitializeCountdownTimer();
            StartReceiveThread();
        }

        private void InitializeUdpClient()
        {
            receivePoint = new IPEndPoint(IPAddress.Any, 0);
            udpClient = new UdpClient(5001); // Ensure this is different from the server port
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

            // Verify HMAC
            if (VerifyHMAC(strData, receivedHMAC))
            {
                MessageBox.Show(strData);
                UpdateCheckbox(strData);
                StartCountdown(strData);
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

        private void UpdateCheckbox(string imageName)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateCheckbox), imageName);
                return;
            }

            checkBox1.Checked = imageName == "pictureBox1";
            checkBox2.Checked = imageName == "pictureBox2";
            checkBox3.Checked = imageName == "pictureBox3";
            checkBox4.Checked = imageName == "pictureBox4";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";
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
            if (textBox_countDownClient.InvokeRequired)
            {
                textBox_countDownClient.Invoke(new Action<string>(StartCountdown), message);
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
                countdownValueClient = 3; // Set the countdown starting value
                textBox_countDownClient.Text = countdownValueClient.ToString();

                // Clear existing event handlers to avoid multiple subscriptions
                countdownTimerClient.Tick -= CountdownTimerClient_Tick;
                countdownTimerClient.Tick += CountdownTimerClient_Tick; // Add new handler

                countdownTimerClient.Start(); // Start the countdown
            }
        }

        // Separate method for the countdown timer tick event
        private void CountdownTimerClient_Tick(object sender, EventArgs e)
        {
            if (countdownValueClient > 0)
            {
                countdownValueClient--;
                textBox_countDownClient.Text = countdownValueClient.ToString(); // Update countdown display
            }
            else
            {
                countdownTimerClient.Stop(); // Stop the timer
            }
        }

        private void textBox_Winner_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
