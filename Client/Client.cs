using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace Client
{
    public partial class Client : Form
    {
        private UdpClient udpClient;
        private IPEndPoint receivePoint;
        private Thread receiveThread;
        private byte[] sharedKey;
        private System.Windows.Forms.Timer countdownTimerClient; // Add a Timer for countdown
        private int countdownValue; // Store the countdown value

        public Client()
        {
            InitializeComponent();
            receivePoint = new IPEndPoint(IPAddress.Any, 0);
            udpClient = new UdpClient(5001); // Ensure this is different from the server port
            sharedKey = Encoding.UTF8.GetBytes("2251120449"); // Define your shared key here
            receiveThread = new Thread(new ThreadStart(WaitPacketsFromServer))
            {
                IsBackground = true
            };
            receiveThread.Start();
            countdownTimerClient = new System.Windows.Forms.Timer();
            countdownTimerClient.Interval = 1000; // Set interval to 1 second
            countdownTimerClient.Tick += countdownTimerClient_Tick;
        }

        public void WaitPacketsFromServer()
        {
            while (true)
            {
                sharedKey = Encoding.UTF8.GetBytes("2251120449");
                byte[] data = udpClient.Receive(ref receivePoint);
                string strData = Encoding.ASCII.GetString(data.Take(data.Length - 32).ToArray()); // Assuming last 32 bytes are HMAC
                byte[] receivedHMAC = data.Skip(data.Length - 32).ToArray();

                // Verify HMAC
                byte[] computedHMAC = ComputeHMAC(strData, sharedKey);
                if (receivedHMAC.SequenceEqual(computedHMAC))
                {
                    UpdateCheckbox(strData);
                    Start_countDown(strData);
                }
                else
                {
                    MessageBox.Show("HMAC verification failed.");
                }
            }
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
            // Check if we need to invoke
            if (checkBox1.InvokeRequired || checkBox2.InvokeRequired || checkBox3.InvokeRequired || checkBox4.InvokeRequired)
            {
                // Create a delegate to update the CheckBox
                this.Invoke(new Action<string>(UpdateCheckbox), imageName);
            }
            else
            {
                // Logic to check the checkbox based on the image name
                checkBox1.Checked = imageName == "pictureBox1";
                checkBox2.Checked = imageName == "pictureBox2";
                checkBox3.Checked = imageName == "pictureBox3";
                checkBox4.Checked = imageName == "pictureBox4";
            }
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
        private void countdownTimerClient_Tick(object sender, EventArgs e)
        {
            if (countdownValue > 0)
            {
                textBox_countDown.Text = countdownValue.ToString(); // Update the countdown textbox
                countdownValue--; // Decrement the countdown value
            }
            else
            {
                countdownTimerClient.Stop(); // Stop the timer when countdown reaches 0
            }
        }

        private void Start_countDown(string message)
        {
            if (textBox_countDown.InvokeRequired)
            {
                textBox_countDown.Invoke(new Action<string>(Start_countDown), message);
            }
            else
            {
                countdownValue = 3; // Set the countdown starting value
                textBox_countDown.Text = countdownValue.ToString(); // Display the initial value

                // Start the countdown only if it's not already running
                if (!countdownTimerClient.Enabled)
                {
                    countdownTimerClient.Start(); // Start the countdown
                }
            }

        }
    }
}
