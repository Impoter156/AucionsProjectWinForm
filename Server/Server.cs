using System;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private UdpClient udpServer;
        private IPEndPoint remoteEndPoint;
        private const int PORT = 5000;
        private string selectedImageName;
        private Thread receiveThread;
        private bool isRunning;
        private byte[] sharedKey = Encoding.UTF8.GetBytes("2251120449");
        private System.Windows.Forms.Timer countdownTimer; // Add a Timer for countdown
        private int countdownValue; // Store the countdown value

        public Server()
        {
            InitializeComponent();
            StartServer();
            receiveThread = new Thread(ReceiveMessages)
            {
                IsBackground = true
            };

            // Initialize the countdown timer
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000; // Set interval to 1 second
            countdownTimer.Tick += CountdownTimer_Tick;
            receiveThread.Start();
        }

        private void StartServer()
        {
            udpServer = new UdpClient(PORT);
            remoteEndPoint = new IPEndPoint(IPAddress.Any, PORT);
            isRunning = true;
        }

        public static byte[] ComputeHMAC(string message, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
        }
        private void ReceiveMessages()
        {
            while (isRunning)
            {
                byte[] data = udpServer.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.ASCII.GetString(data.Take(data.Length - 32).ToArray());
                byte[] receivedHMAC = data.Skip(data.Length - 32).ToArray();

                // Compute HMAC
                byte[] computedHMAC = ComputeHMAC(receivedMessage, sharedKey);
                if (receivedHMAC.SequenceEqual(computedHMAC))
                {
                    UpdateTextBoxes(receivedMessage);
                }
            }
        }
        private void UpdateTextBoxes(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTextBoxes), message);
            }
            else
            {
                string[] parts = message.Split('|');
                if (parts.Length == 2)
                {
                    textBox_bidder1Name.Text = parts[0];
                    textBox_bidder1Price.Text = parts[1];
                }
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Reset the remote endpoint for the incoming message
                remoteEndPoint = new IPEndPoint(IPAddress.Any, PORT);
                byte[] receivedData = udpServer.EndReceive(ar, ref remoteEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(receivedData);

                // Continue listening for incoming messages
                udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving data: {ex.Message}");
            }
        }

        private void Selling_btn_Click(object sender, EventArgs e)
        {
            SendMessageToClient(selectedImageName);
        }

        private void SendMessageToClient(string message)
        {
            sharedKey = Encoding.UTF8.GetBytes("2251120449");
            byte[] hmac = ComputeHMAC(message, sharedKey);
            byte[] messageWithHMAC = Encoding.UTF8.GetBytes(message).Concat(hmac).ToArray();
            udpServer.Send(messageWithHMAC, messageWithHMAC.Length, "localhost", 5001);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox1.Name;
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox2.Name;
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox3.Name;
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            selectedImageName = pictureBox4.Name;
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (countdownValue > 0)
            {
                textBox_countdown.Text = countdownValue.ToString(); // Update the countdown textbox
                countdownValue--; // Decrement the countdown value
            }
            else
            {
                countdownTimer.Stop(); // Stop the timer when countdown reaches 0
            }
        }

        private void countDown_btn_Click(object sender, EventArgs e)
        {
            countdownValue = 3; // Set the countdown starting value
            textBox_countdown.Text = countdownValue.ToString(); // Display the initial value
            countdownTimer.Start(); // Start the countdown
            string messageToClient = "start_countDown";
            SendMessageToClient(messageToClient);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false;
            udpServer?.Close();
            receiveThread?.Join(); // Ensure the receive thread stops before closing the form
            base.OnFormClosing(e);
        }
    }
}
