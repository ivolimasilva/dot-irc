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
            this.rchtxtboxChat = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtboxChat
            // 
            this.txtboxChat.Location = new System.Drawing.Point(12, 382);
            this.txtboxChat.Name = "txtboxChat";
            this.txtboxChat.Size = new System.Drawing.Size(414, 20);
            this.txtboxChat.TabIndex = 0;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMsg.Location = new System.Drawing.Point(432, 382);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(49, 23);
            this.btnSendMsg.TabIndex = 1;
            this.btnSendMsg.Text = "Send";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // rchtxtboxChat
            // 
            this.rchtxtboxChat.Location = new System.Drawing.Point(12, 12);
            this.rchtxtboxChat.Name = "rchtxtboxChat";
            this.rchtxtboxChat.ReadOnly = true;
            this.rchtxtboxChat.Size = new System.Drawing.Size(469, 364);
            this.rchtxtboxChat.TabIndex = 2;
            this.rchtxtboxChat.Text = "";
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 412);
            this.Controls.Add(this.rchtxtboxChat);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.txtboxChat);
            this.Name = "ChatRoom";
            this.Text = "ChatRoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtboxChat;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.RichTextBox rchtxtboxChat;
    }
}