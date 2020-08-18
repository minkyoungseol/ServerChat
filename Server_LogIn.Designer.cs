namespace ServerChat
{
    partial class Server_LogIn
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_PW = new System.Windows.Forms.TextBox();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_LogIn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIDSave = new System.Windows.Forms.CheckBox();
            this.chkPWSave = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txt_PW
            // 
            this.txt_PW.Location = new System.Drawing.Point(114, 132);
            this.txt_PW.Name = "txt_PW";
            this.txt_PW.PasswordChar = '*';
            this.txt_PW.Size = new System.Drawing.Size(100, 21);
            this.txt_PW.TabIndex = 11;
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(114, 81);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(100, 21);
            this.txt_ID.TabIndex = 10;
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(139, 244);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 23);
            this.btn_Exit.TabIndex = 9;
            this.btn_Exit.Text = "나가기";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_LogIn
            // 
            this.btn_LogIn.Location = new System.Drawing.Point(55, 244);
            this.btn_LogIn.Name = "btn_LogIn";
            this.btn_LogIn.Size = new System.Drawing.Size(75, 23);
            this.btn_LogIn.TabIndex = 8;
            this.btn_LogIn.Text = "로그인";
            this.btn_LogIn.UseVisualStyleBackColor = true;
            this.btn_LogIn.Click += new System.EventHandler(this.btn_LogIn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "PW";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID";
            // 
            // chkIDSave
            // 
            this.chkIDSave.AutoSize = true;
            this.chkIDSave.Location = new System.Drawing.Point(55, 175);
            this.chkIDSave.Name = "chkIDSave";
            this.chkIDSave.Size = new System.Drawing.Size(59, 16);
            this.chkIDSave.TabIndex = 12;
            this.chkIDSave.Text = "ID저장";
            this.chkIDSave.UseVisualStyleBackColor = true;
            // 
            // chkPWSave
            // 
            this.chkPWSave.AutoSize = true;
            this.chkPWSave.Location = new System.Drawing.Point(180, 175);
            this.chkPWSave.Name = "chkPWSave";
            this.chkPWSave.Size = new System.Drawing.Size(66, 16);
            this.chkPWSave.TabIndex = 13;
            this.chkPWSave.Text = "PW저장";
            this.chkPWSave.UseVisualStyleBackColor = true;
            // 
            // Server_LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 333);
            this.ControlBox = false;
            this.Controls.Add(this.chkPWSave);
            this.Controls.Add(this.chkIDSave);
            this.Controls.Add(this.txt_PW);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_LogIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Server_LogIn";
            this.Text = "서버로그인";
            this.Load += new System.EventHandler(this.Server_LogIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_PW;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_LogIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIDSave;
        private System.Windows.Forms.CheckBox chkPWSave;
    }
}

