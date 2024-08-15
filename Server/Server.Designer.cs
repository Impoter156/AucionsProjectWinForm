using System;

namespace Server
{
    partial class Server
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bidder1Name_label = new System.Windows.Forms.Label();
            this.bidder1Price_label = new System.Windows.Forms.Label();
            this.productName_textbox = new System.Windows.Forms.TextBox();
            this.textBox_bidder1Name = new System.Windows.Forms.TextBox();
            this.textBox_bidder1Price = new System.Windows.Forms.TextBox();
            this.textBox_bidder2Price = new System.Windows.Forms.TextBox();
            this.textBox_bidder2Name = new System.Windows.Forms.TextBox();
            this.bidder2Price_label = new System.Windows.Forms.Label();
            this.bidder2Name_lablel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_countdownServer = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_Winner = new System.Windows.Forms.TextBox();
            this.startSelling_btn = new System.Windows.Forms.Button();
            this.countDown_btn = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_winneramount = new System.Windows.Forms.TextBox();
            this.CurrentPrice_textbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(977, 474);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(367, 22);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(237, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 54);
            this.label1.TabIndex = 2;
            this.label1.Text = "Auctions Home Page";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Choose product to sell: ";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(28, 302);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(659, 173);
            this.textBox1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Produce Information";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(181, 491);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(112, 29);
            this.label4.TabIndex = 9;
            this.label4.Text = "Auctions";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 550);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Product Name:";
            // 
            // bidder1Name_label
            // 
            this.bidder1Name_label.AutoSize = true;
            this.bidder1Name_label.Location = new System.Drawing.Point(24, 650);
            this.bidder1Name_label.Name = "bidder1Name_label";
            this.bidder1Name_label.Size = new System.Drawing.Size(97, 16);
            this.bidder1Name_label.TabIndex = 11;
            this.bidder1Name_label.Text = "Bidder1 Name:";
            // 
            // bidder1Price_label
            // 
            this.bidder1Price_label.AutoSize = true;
            this.bidder1Price_label.Location = new System.Drawing.Point(24, 708);
            this.bidder1Price_label.Name = "bidder1Price_label";
            this.bidder1Price_label.Size = new System.Drawing.Size(90, 16);
            this.bidder1Price_label.TabIndex = 12;
            this.bidder1Price_label.Text = "Bidder1 price:";
            // 
            // productName_textbox
            // 
            this.productName_textbox.Location = new System.Drawing.Point(187, 544);
            this.productName_textbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.productName_textbox.Name = "productName_textbox";
            this.productName_textbox.Size = new System.Drawing.Size(279, 22);
            this.productName_textbox.TabIndex = 15;
            // 
            // textBox_bidder1Name
            // 
            this.textBox_bidder1Name.Location = new System.Drawing.Point(186, 644);
            this.textBox_bidder1Name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_bidder1Name.Name = "textBox_bidder1Name";
            this.textBox_bidder1Name.Size = new System.Drawing.Size(279, 22);
            this.textBox_bidder1Name.TabIndex = 16;
            // 
            // textBox_bidder1Price
            // 
            this.textBox_bidder1Price.Location = new System.Drawing.Point(186, 702);
            this.textBox_bidder1Price.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_bidder1Price.Name = "textBox_bidder1Price";
            this.textBox_bidder1Price.Size = new System.Drawing.Size(279, 22);
            this.textBox_bidder1Price.TabIndex = 17;
            // 
            // textBox_bidder2Price
            // 
            this.textBox_bidder2Price.Location = new System.Drawing.Point(186, 815);
            this.textBox_bidder2Price.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_bidder2Price.Name = "textBox_bidder2Price";
            this.textBox_bidder2Price.Size = new System.Drawing.Size(279, 22);
            this.textBox_bidder2Price.TabIndex = 21;
            // 
            // textBox_bidder2Name
            // 
            this.textBox_bidder2Name.Location = new System.Drawing.Point(186, 757);
            this.textBox_bidder2Name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_bidder2Name.Name = "textBox_bidder2Name";
            this.textBox_bidder2Name.Size = new System.Drawing.Size(279, 22);
            this.textBox_bidder2Name.TabIndex = 20;
            // 
            // bidder2Price_label
            // 
            this.bidder2Price_label.AutoSize = true;
            this.bidder2Price_label.Location = new System.Drawing.Point(24, 821);
            this.bidder2Price_label.Name = "bidder2Price_label";
            this.bidder2Price_label.Size = new System.Drawing.Size(90, 16);
            this.bidder2Price_label.TabIndex = 19;
            this.bidder2Price_label.Text = "Bidder2 price:";
            // 
            // bidder2Name_lablel
            // 
            this.bidder2Name_lablel.AutoSize = true;
            this.bidder2Name_lablel.Location = new System.Drawing.Point(24, 763);
            this.bidder2Name_lablel.Name = "bidder2Name_lablel";
            this.bidder2Name_lablel.Size = new System.Drawing.Size(94, 16);
            this.bidder2Name_lablel.TabIndex = 18;
            this.bidder2Name_lablel.Text = "Bidder2 name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(644, 491);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(80, 29);
            this.label10.TabIndex = 22;
            this.label10.Text = "Time ";
            // 
            // textBox_countdownServer
            // 
            this.textBox_countdownServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_countdownServer.Location = new System.Drawing.Point(700, 539);
            this.textBox_countdownServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_countdownServer.Multiline = true;
            this.textBox_countdownServer.Name = "textBox_countdownServer";
            this.textBox_countdownServer.Size = new System.Drawing.Size(89, 127);
            this.textBox_countdownServer.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(605, 688);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 42);
            this.label11.TabIndex = 24;
            this.label11.Text = "Winner!";
            // 
            // textBox_Winner
            // 
            this.textBox_Winner.Font = new System.Drawing.Font("Mongolian Baiti", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Winner.Location = new System.Drawing.Point(552, 740);
            this.textBox_Winner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Winner.Multiline = true;
            this.textBox_Winner.Name = "textBox_Winner";
            this.textBox_Winner.ReadOnly = true;
            this.textBox_Winner.Size = new System.Drawing.Size(279, 43);
            this.textBox_Winner.TabIndex = 25;
            // 
            // startSelling_btn
            // 
            this.startSelling_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startSelling_btn.Location = new System.Drawing.Point(721, 326);
            this.startSelling_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startSelling_btn.Name = "startSelling_btn";
            this.startSelling_btn.Size = new System.Drawing.Size(93, 124);
            this.startSelling_btn.TabIndex = 26;
            this.startSelling_btn.Text = "Start";
            this.startSelling_btn.UseVisualStyleBackColor = true;
            this.startSelling_btn.Click += new System.EventHandler(this.Selling_btn_Click);
            // 
            // countDown_btn
            // 
            this.countDown_btn.Location = new System.Drawing.Point(539, 590);
            this.countDown_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.countDown_btn.Name = "countDown_btn";
            this.countDown_btn.Size = new System.Drawing.Size(131, 33);
            this.countDown_btn.TabIndex = 27;
            this.countDown_btn.Text = "Count down";
            this.countDown_btn.UseVisualStyleBackColor = true;
            this.countDown_btn.Click += new System.EventHandler(this.CountDown_btn_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Server.Properties.Resources.telephone;
            this.pictureBox4.Location = new System.Drawing.Point(649, 113);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(165, 150);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Server.Properties.Resources.bucTuong;
            this.pictureBox3.Location = new System.Drawing.Point(432, 113);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(165, 150);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Server.Properties.Resources.caiDia;
            this.pictureBox2.Location = new System.Drawing.Point(229, 113);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(165, 150);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Server.Properties.Resources.WatchPicture;
            this.pictureBox1.Location = new System.Drawing.Point(28, 113);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(165, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_winneramount
            // 
            this.textBox_winneramount.Font = new System.Drawing.Font("Mongolian Baiti", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_winneramount.Location = new System.Drawing.Point(552, 795);
            this.textBox_winneramount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_winneramount.Multiline = true;
            this.textBox_winneramount.Name = "textBox_winneramount";
            this.textBox_winneramount.ReadOnly = true;
            this.textBox_winneramount.Size = new System.Drawing.Size(279, 43);
            this.textBox_winneramount.TabIndex = 28;
            // 
            // CurrentPrice_textbox
            // 
            this.CurrentPrice_textbox.Location = new System.Drawing.Point(186, 590);
            this.CurrentPrice_textbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CurrentPrice_textbox.Name = "CurrentPrice_textbox";
            this.CurrentPrice_textbox.Size = new System.Drawing.Size(279, 22);
            this.CurrentPrice_textbox.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 596);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "Curent Price:";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 853);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CurrentPrice_textbox);
            this.Controls.Add(this.textBox_winneramount);
            this.Controls.Add(this.countDown_btn);
            this.Controls.Add(this.startSelling_btn);
            this.Controls.Add(this.textBox_Winner);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox_countdownServer);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_bidder2Price);
            this.Controls.Add(this.textBox_bidder2Name);
            this.Controls.Add(this.bidder2Price_label);
            this.Controls.Add(this.bidder2Name_lablel);
            this.Controls.Add(this.textBox_bidder1Price);
            this.Controls.Add(this.textBox_bidder1Name);
            this.Controls.Add(this.productName_textbox);
            this.Controls.Add(this.bidder1Price_label);
            this.Controls.Add(this.bidder1Name_label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Server";
            this.Text = "Auctions Server";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label bidder1Name_label;
        private System.Windows.Forms.Label bidder1Price_label;
        private System.Windows.Forms.TextBox productName_textbox;
        private System.Windows.Forms.TextBox textBox_bidder1Name;
        private System.Windows.Forms.TextBox textBox_bidder1Price;
        private System.Windows.Forms.TextBox textBox_bidder2Price;
        private System.Windows.Forms.TextBox textBox_bidder2Name;
        private System.Windows.Forms.Label bidder2Price_label;
        private System.Windows.Forms.Label bidder2Name_lablel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_countdownServer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_Winner;
        private System.Windows.Forms.Button startSelling_btn;
        private System.Windows.Forms.Button countDown_btn;
        private System.Windows.Forms.TextBox textBox_winneramount;
        private System.Windows.Forms.TextBox CurrentPrice_textbox;
        private System.Windows.Forms.Label label6;
    }
}

