﻿namespace Client.Views
{
    partial class ChatRoom
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
            this.txtboxChat = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.listUsers = new System.Windows.Forms.ListBox();
            this.lvChat = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // txtboxChat
            // 
            this.txtboxChat.Location = new System.Drawing.Point(85, 593);
            this.txtboxChat.Name = "txtboxChat";
            this.txtboxChat.Size = new System.Drawing.Size(711, 20);
            this.txtboxChat.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(793, 590);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // listUsers
            // 
            this.listUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listUsers.FormattingEnabled = true;
            this.listUsers.ItemHeight = 17;
            this.listUsers.Location = new System.Drawing.Point(-2, -1);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(85, 599);
            this.listUsers.TabIndex = 3;
            // 
            // lvChat
            // 
            this.lvChat.Location = new System.Drawing.Point(85, -1);
            this.lvChat.Name = "lvChat";
            this.lvChat.Size = new System.Drawing.Size(783, 588);
            this.lvChat.TabIndex = 4;
            this.lvChat.UseCompatibleStateImageBehavior = false;
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 617);
            this.Controls.Add(this.lvChat);
            this.Controls.Add(this.listUsers);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtboxChat);
            this.Name = "ChatRoom";
            this.Text = "ChatRoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.closeChatRoom);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtboxChat;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.ListView lvChat;
    }
}