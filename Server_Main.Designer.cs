namespace ServerChat
{
    partial class Server_Main
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txt_PortNum = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txt_Msg = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_DisConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "서버주소";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "포트 번호";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 414);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "메세지";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(83, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(152, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // txt_PortNum
            // 
            this.txt_PortNum.Location = new System.Drawing.Point(313, 15);
            this.txt_PortNum.Name = "txt_PortNum";
            this.txt_PortNum.Size = new System.Drawing.Size(100, 21);
            this.txt_PortNum.TabIndex = 4;
            this.txt_PortNum.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_PortNum_KeyUp);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(419, 14);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(58, 23);
            this.btn_Connect.TabIndex = 5;
            this.btn_Connect.Text = "연결";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // txt_Msg
            // 
            this.txt_Msg.Location = new System.Drawing.Point(71, 411);
            this.txt_Msg.Name = "txt_Msg";
            this.txt_Msg.Size = new System.Drawing.Size(394, 21);
            this.txt_Msg.TabIndex = 6;
            this.txt_Msg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_Msg_KeyUp);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(471, 409);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 7;
            this.btn_Send.Text = "전송";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(26, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(520, 364);
            this.listBox1.TabIndex = 8;
            // 
            // btn_DisConnect
            // 
            this.btn_DisConnect.Location = new System.Drawing.Point(483, 13);
            this.btn_DisConnect.Name = "btn_DisConnect";
            this.btn_DisConnect.Size = new System.Drawing.Size(63, 23);
            this.btn_DisConnect.TabIndex = 9;
            this.btn_DisConnect.Text = "해제";
            this.btn_DisConnect.UseVisualStyleBackColor = true;
            this.btn_DisConnect.Click += new System.EventHandler(this.btn_DisConnect_Click);
            // 
            // Server_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 450);
            this.Controls.Add(this.btn_DisConnect);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.txt_Msg);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txt_PortNum);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Server_Main";
            this.Text = "서버 채팅";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Server_Main_FormClosed);
            this.Load += new System.EventHandler(this.Server_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txt_PortNum;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txt_Msg;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_DisConnect;
    }
}