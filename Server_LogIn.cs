using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Module;

namespace ServerChat
{
    public partial class Server_LogIn : Form
    {
        public static string strDBCon;

        public Server_LogIn()
        {
            InitializeComponent();
        }

        private void Server_LogIn_Load(object sender, EventArgs e)
        {
            // ID, PW 입력 필드 초기화 > App.Config 파일 열어서 저장된 ID. PW 있는지 차례로 확인 있으면 있는것만 값 추가 및 체크박스 체크

            txt_ID.Text = "";
            txt_PW.Text = "";

            CM_Main.S_ID = "";
            CM_Main.S_PW = "";
            CM_Main.S_NM = "";

            CM_Main.Read_INI(); // App.Config 값 가져옴

            if (CM_Main.S_ID != "")
            {
                txt_ID.Text = CM_Main.S_ID;
                chkIDSave.Checked = true;
            }
            else
            {
                txt_ID.Text = "";
                chkIDSave.Checked = false;
            }
            if(CM_Main.S_PW.Replace("\0","") != "")
            {
                txt_PW.Text = CM_Main.S_PW;
                chkPWSave.Checked = true;
            }
            else
            {
                txt_PW.Text = "";
                chkPWSave.Checked = false;
            }
        }

        private void btn_LogIn_Click(object sender, EventArgs e)
        {
            // ID, PW 가져오기
            string S_ID = txt_ID.Text;
            string S_PW = txt_PW.Text;

            string S_PWPW = EncryptSHA512(S_PW);

            // ID, PW 저장하기 체크박스 체크 되어 있으면 App.config에 로그인 아이디 비번 저장하기 
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (chkIDSave.Checked == true)
                {
                    config.AppSettings.Settings["S_ID"].Value = S_ID;
                }
                if (chkPWSave.Checked == true)
                {
                    config.AppSettings.Settings["S_PW"].Value = S_PW;
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch(Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
            
            CM_Main.S_ID = S_ID;
            CM_Main.S_PW = S_PW;

            SqlConnection conn = new SqlConnection(strDBCon);
            try
            {
                conn.Open();
                string query = "Select count(*) as search_Count from UserInfo where UserID='" + S_ID + "' and UserPW='" + S_PWPW + "'";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);

                DataTable dtDetail = new DataTable();

                sqlDataAdapter.Fill(dtDetail);

                if (dtDetail.Rows[0][0].ToString() == "1")
                {                    
                    this.Close();
                    //Server_Main server_Main = new Server_Main();
                    //server_Main.Show();
                }
                else
                {
                    MessageBox.Show("아이디와 비밀번호를 확인해 주세요!");
                    return;
                }
                // 값을 가지고 서버 연결해서 데이터가 있으면 로그인 아이디, 패스워드, 사용자이름을 받아와야함

                
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);             
            }
            finally
            {
                conn.Close();
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            CM_Main.S_ID = "";
            CM_Main.S_PW = "";
            CM_Main.S_NM = "";

            Application.Exit();
        }

        public static string EncryptSHA512(string Data)
        {
            SHA512 sha = new SHA512Managed();
            //byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(Data));
            StringBuilder strb = new StringBuilder();
            foreach (byte b in hash)
            {
                strb.AppendFormat("{0:x2}", b);
            }
            return strb.ToString();
        }
    }
}

