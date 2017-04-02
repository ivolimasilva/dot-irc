using System;
using System.Windows.Forms;

namespace Client.Views
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
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtboxChat
            // 
            this.txtboxChat.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxChat.Location = new System.Drawing.Point(12, 372);
            this.txtboxChat.Name = "txtboxChat";
            this.txtboxChat.Size = new System.Drawing.Size(414, 27);
            this.txtboxChat.TabIndex = 0;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMsg.Location = new System.Drawing.Point(432, 370);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(49, 30);
            this.btnSendMsg.TabIndex = 1;
            this.btnSendMsg.Text = "Send";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // rtbMessages
            // 
            this.rtbMessages.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMessages.Location = new System.Drawing.Point(12, 12);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.ReadOnly = true;
            this.rtbMessages.Size = new System.Drawing.Size(469, 352);
            this.rtbMessages.TabIndex = 2;
            this.rtbMessages.Text = "";
            // 
            // ChatRoom
            // 
            this.AcceptButton = this.btnSendMsg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 412);
            this.Controls.Add(this.rtbMessages);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.txtboxChat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChatRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChatRoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.closeChatroom);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtboxChat;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.RichTextBox rtbMessages;
    }
}