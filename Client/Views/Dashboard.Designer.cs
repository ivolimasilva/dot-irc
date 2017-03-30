namespace Client.Views
{
    partial class Dashboard
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
            this.listUsers = new System.Windows.Forms.ListBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnStartChat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listUsers
            // 
            this.listUsers.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listUsers.FormattingEnabled = true;
            this.listUsers.ItemHeight = 18;
            this.listUsers.Location = new System.Drawing.Point(12, 33);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(313, 184);
            this.listUsers.TabIndex = 3;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(12, 12);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUserName.Size = new System.Drawing.Size(313, 18);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "Logged as";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStartChat
            // 
            this.btnStartChat.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartChat.Location = new System.Drawing.Point(12, 223);
            this.btnStartChat.Name = "btnStartChat";
            this.btnStartChat.Size = new System.Drawing.Size(313, 30);
            this.btnStartChat.TabIndex = 5;
            this.btnStartChat.Text = "Start Chat";
            this.btnStartChat.UseVisualStyleBackColor = true;
            this.btnStartChat.Click += new System.EventHandler(this.btnStartChat_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 261);
            this.Controls.Add(this.btnStartChat);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.listUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dashboard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.closeDashBoard);
            this.ResumeLayout(false);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        #endregion
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnStartChat;
    }
}