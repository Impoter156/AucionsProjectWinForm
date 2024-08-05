using System;
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

        public Server()
        {
            InitializeComponent();
            StartServer();
            receiveThread = new Thread(ReceiveMessages)
            {
                IsBackground = true
            };
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false;
            udpServer?.Close();
            receiveThread?.Join(); // Ensure the receive thread stops before closing the form
            base.OnFormClosing(e);
        }
    }
}
