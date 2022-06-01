namespace Client
{
    partial class Connect
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
            this.tbIP = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.Label();
            this.btConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(143, 56);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(227, 22);
            this.tbIP.TabIndex = 13;
            this.tbIP.TextChanged += new System.EventHandler(this.tbIP_TextChanged);
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP.Location = new System.Drawing.Point(43, 59);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(94, 17);
            this.IP.TabIndex = 12;
            this.IP.Text = "IP SERVER:";
            // 
            // btConnect
            // 
            this.btConnect.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btConnect.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btConnect.FlatAppearance.BorderSize = 2;
            this.btConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConnect.Location = new System.Drawing.Point(201, 95);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(105, 39);
            this.btConnect.TabIndex = 15;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = false;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 199);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.IP);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Connect";
            this.Text = "Connect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Connect_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.Button btConnect;
    }
}