namespace Client
{
    partial class ChatApp
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
            this.lvMessage = new System.Windows.Forms.ListBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvMessage
            // 
            this.lvMessage.FormattingEnabled = true;
            this.lvMessage.ItemHeight = 16;
            this.lvMessage.Location = new System.Drawing.Point(46, 93);
            this.lvMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvMessage.Name = "lvMessage";
            this.lvMessage.Size = new System.Drawing.Size(584, 260);
            this.lvMessage.TabIndex = 4;
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(46, 380);
            this.tbMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(480, 22);
            this.tbMessage.TabIndex = 5;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(550, 373);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 36);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btName
            // 
            this.btName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btName.Location = new System.Drawing.Point(46, 44);
            this.btName.Name = "btName";
            this.btName.Size = new System.Drawing.Size(194, 33);
            this.btName.TabIndex = 7;
            this.btName.Text = "User: ";
            this.btName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btName.UseVisualStyleBackColor = false;
            // 
            // ChatApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 421);
            this.Controls.Add(this.btName);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lvMessage);
            this.Name = "ChatApp";
            this.Text = "ChatApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lvMessage;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btName;
    }
}