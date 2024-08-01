using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a luxurious watch, made of gold, and it's a limited edition.";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is an antique plate from the 18th century, with intricate designs.";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a bronze statue of a knight on horseback, from the Renaissance period.";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "This is a vintage rotary dial telephone with a classic design and brass details. The telephone, featuring a rotary dial, is a characteristic style from the mid-20th century.";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
