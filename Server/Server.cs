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

        public Server()
        {
            InitializeComponent();
            StartReceiveThread();
            StartServer();

            // Assign click event handler to each PictureBox
            pictureBox1.Click += pictureBox_Click;
            pictureBox2.Click += pictureBox_Click;
            pictureBox3.Click += pictureBox_Click;
            pictureBox4.Click += pictureBox_Click;
        }

        private void StartServer()
        {
            udpServer = new UdpClient(PORT);
            remoteEndPoint = new IPEndPoint(IPAddress.Any, PORT);
            //udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            isRunning = true;
        }

        private void StartReceiveThread()
        {
            receiveThread = new Thread(ReceiveMessages)
            {
                IsBackground = true
            };
            receiveThread.Start();
        }

        public static byte[] ComputeHMAC(string message, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
        }

        private byte[] sharedKey = Encoding.UTF8.GetBytes("2251120449");
        private void ReceiveMessages()
        {
            while (isRunning)
            {
                byte[] data = udpServer.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.ASCII.GetString(data, 0, data.Length - 32); // Assuming last 32 bytes are HMAC
                byte[] receivedHMAC = new byte[32];
                Array.Copy(data, data.Length - 32, receivedHMAC, 0, 32);

                //Compute HMAC
                byte[] computedHMAC = ComputeHMAC(receivedMessage, sharedKey);
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

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;

            if (clickedPictureBox != null)
            {
                // Get the name of the clicked image
                selectedImageName = clickedPictureBox.Name; // Assuming picture boxes are named accordingly
                MessageBox.Show($"Selected Image: {selectedImageName}");
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false;
            udpServer?.Close();
            receiveThread?.Join(); // Ensure the receive thread stops before closing the form
            base.OnFormClosing(e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
