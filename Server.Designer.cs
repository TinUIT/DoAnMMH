namespace DoAnMMH
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.dgvServer = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServer)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP SERVER:";
            // 
            // tbIP
            // 
            this.tbIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIP.Location = new System.Drawing.Point(272, 85);
            this.tbIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbIP.Name = "tbIP";
            this.tbIP.ReadOnly = true;
            this.tbIP.Size = new System.Drawing.Size(242, 39);
            this.tbIP.TabIndex = 2;
            // 
            // dgvServer
            // 
            this.dgvServer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServer.Location = new System.Drawing.Point(37, 152);
            this.dgvServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvServer.Name = "dgvServer";
            this.dgvServer.ReadOnly = true;
            this.dgvServer.RowHeadersWidth = 51;
            this.dgvServer.RowTemplate.Height = 24;
            this.dgvServer.Size = new System.Drawing.Size(585, 385);
            this.dgvServer.TabIndex = 3;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 552);
            this.Controls.Add(this.dgvServer);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Server";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.Shown += new System.EventHandler(this.Server_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.DataGridView dgvServer;
    }
}

