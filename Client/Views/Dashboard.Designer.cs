using System.Windows.Forms;

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
            this.lblUsers = new System.Windows.Forms.Label();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // listUsers
            // 
            this.listUsers.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listUsers.FormattingEnabled = true;
            this.listUsers.ItemHeight = 18;
            this.listUsers.Location = new System.Drawing.Point(12, 33);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(170, 184);
            this.listUsers.TabIndex = 3;
            this.listUsers.SelectedIndexChanged += new System.EventHandler(this.listUsers_SelectedIndexChanged);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(459, 9);
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
            this.btnStartChat.Size = new System.Drawing.Size(170, 30);
            this.btnStartChat.TabIndex = 5;
            this.btnStartChat.Text = "Start Private Chat";
            this.btnStartChat.UseVisualStyleBackColor = true;
            this.btnStartChat.Click += new System.EventHandler(this.btnStartChat_Click);
            // 
            // lblUsers
            // 
            this.lblUsers.AutoSize = true;
            this.lblUsers.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsers.Location = new System.Drawing.Point(12, 9);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(85, 18);
            this.lblUsers.TabIndex = 6;
            this.lblUsers.Text = "List of users:";
            // 
            // txtChat
            // 
            this.txtChat.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChat.Location = new System.Drawing.Point(188, 225);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(503, 27);
            this.txtChat.TabIndex = 7;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(697, 223);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 30);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rtbChat
            // 
            this.rtbChat.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbChat.Location = new System.Drawing.Point(188, 33);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(584, 184);
            this.rtbChat.TabIndex = 9;
            this.rtbChat.Text = "";
            // 
            // Dashboard
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.rtbChat);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.lblUsers);
            this.Controls.Add(this.btnStartChat);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.listUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.closeDashBoard);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnStartChat;
        private Label lblUsers;
        private TextBox txtChat;
        private Button btnSend;
        private RichTextBox rtbChat;
    }
}