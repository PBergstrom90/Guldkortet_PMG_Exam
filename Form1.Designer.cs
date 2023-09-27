namespace Guldkortet_PMG
{
    partial class Guldkortet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Guldkortet));
            this.connectBtn = new System.Windows.Forms.Button();
            this.IPlabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.msgBox1 = new System.Windows.Forms.TextBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.logoPic = new System.Windows.Forms.PictureBox();
            this.winImgList1 = new System.Windows.Forms.ImageList(this.components);
            this.infoLbl = new System.Windows.Forms.Label();
            this.msgBox2 = new System.Windows.Forms.TextBox();
            this.msgLbl = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.databaseLbl = new System.Windows.Forms.Label();
            this.statusLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(12, 462);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(132, 50);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Starta Server";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // IPlabel
            // 
            this.IPlabel.AutoSize = true;
            this.IPlabel.Location = new System.Drawing.Point(12, 416);
            this.IPlabel.Name = "IPlabel";
            this.IPlabel.Size = new System.Drawing.Size(68, 13);
            this.IPlabel.TabIndex = 1;
            this.IPlabel.Text = "IP: 127.0.0.1";
            this.IPlabel.Click += new System.EventHandler(this.IPlabel_Click);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(12, 434);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(62, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port: 12345";
            this.portLabel.Click += new System.EventHandler(this.portLabel_Click);
            // 
            // msgBox1
            // 
            this.msgBox1.Location = new System.Drawing.Point(12, 202);
            this.msgBox1.Name = "msgBox1";
            this.msgBox1.ReadOnly = true;
            this.msgBox1.Size = new System.Drawing.Size(267, 20);
            this.msgBox1.TabIndex = 4;
            this.msgBox1.TextChanged += new System.EventHandler(this.msgBox1_TextChanged);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(150, 463);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(129, 50);
            this.exitBtn.TabIndex = 5;
            this.exitBtn.Text = "Avsluta";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // logoPic
            // 
            this.logoPic.Image = global::Guldkortet_PMG.Properties.Resources.nos;
            this.logoPic.Location = new System.Drawing.Point(15, 12);
            this.logoPic.Name = "logoPic";
            this.logoPic.Size = new System.Drawing.Size(192, 145);
            this.logoPic.TabIndex = 6;
            this.logoPic.TabStop = false;
            this.logoPic.Click += new System.EventHandler(this.logoPic_Click);
            // 
            // winImgList1
            // 
            this.winImgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("winImgList1.ImageStream")));
            this.winImgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.winImgList1.Images.SetKeyName(0, "grattis.png");
            this.winImgList1.Images.SetKeyName(1, "dunderkatt.png");
            this.winImgList1.Images.SetKeyName(2, "kristallhäst.png");
            this.winImgList1.Images.SetKeyName(3, "överpanda.png");
            this.winImgList1.Images.SetKeyName(4, "eldtomat.png");
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Location = new System.Drawing.Point(12, 186);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(97, 13);
            this.infoLbl.TabIndex = 7;
            this.infoLbl.Text = "Användaruppgifter:";
            this.infoLbl.Click += new System.EventHandler(this.infoLbl_Click);
            // 
            // msgBox2
            // 
            this.msgBox2.Location = new System.Drawing.Point(12, 305);
            this.msgBox2.Name = "msgBox2";
            this.msgBox2.ReadOnly = true;
            this.msgBox2.Size = new System.Drawing.Size(267, 20);
            this.msgBox2.TabIndex = 8;
            this.msgBox2.TextChanged += new System.EventHandler(this.msgBox2_TextChanged);
            // 
            // msgLbl
            // 
            this.msgLbl.AutoSize = true;
            this.msgLbl.Location = new System.Drawing.Point(12, 289);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(69, 13);
            this.msgLbl.TabIndex = 9;
            this.msgLbl.Text = "Meddelande:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(316, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(421, 472);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // databaseLbl
            // 
            this.databaseLbl.AutoSize = true;
            this.databaseLbl.Location = new System.Drawing.Point(313, 12);
            this.databaseLbl.Name = "databaseLbl";
            this.databaseLbl.Size = new System.Drawing.Size(81, 13);
            this.databaseLbl.TabIndex = 11;
            this.databaseLbl.Text = "Databas status:";
            this.databaseLbl.Click += new System.EventHandler(this.databaseLbl_Click);
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(400, 12);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(10, 13);
            this.statusLbl.TabIndex = 12;
            this.statusLbl.Text = "-";
            this.statusLbl.Click += new System.EventHandler(this.statusLbl_Click);
            // 
            // Guldkortet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 525);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.databaseLbl);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.msgLbl);
            this.Controls.Add(this.msgBox2);
            this.Controls.Add(this.infoLbl);
            this.Controls.Add(this.logoPic);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.msgBox1);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.IPlabel);
            this.Controls.Add(this.connectBtn);
            this.Name = "Guldkortet";
            this.Text = "Guldkortet";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Label IPlabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox msgBox1;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.PictureBox logoPic;
        private System.Windows.Forms.ImageList winImgList1;
        private System.Windows.Forms.Label infoLbl;
        private System.Windows.Forms.TextBox msgBox2;
        private System.Windows.Forms.Label msgLbl;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label databaseLbl;
        private System.Windows.Forms.Label statusLbl;
    }
}

