using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using VB = Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Data.SqlClient;
using ADODB;
using ServerChat;

namespace Module
{
    public class CM_Main
    {
		//파일 경로
		public static string COMMON_PATH;
		public static string EXEPATH;
		public static string RPTPATH;
		public static string LANPATH;
		public static string WINDOWSPATH;
		public static string SYSTEMPATH;

		//서버 접속을 위한 정보 저장
		public static string ADDRESS;
		public static string DATABASE;
		public static string S_PWD;
		public static string S_UID;
		public static string QUERYTIME;

		//사용자 ID 비번 이름 저장
		public static string S_ID;
		public static string S_PW;
		public static string S_NM;
        public static string GUBUN = "Server";

        public static void Read_INI()
        {
            try
            {
                // App.Config에 기존 저장해둔 ID, PW 정보가 있다면 가져오기
                S_ID = ConfigurationManager.AppSettings["S_ID"];
                S_PW = ConfigurationManager.AppSettings["S_PW"];

                // DB서버 접속 정보 App.Config에서 가져오기
                ADDRESS = ConfigurationManager.AppSettings["ADDRESS"];
                DATABASE = ConfigurationManager.AppSettings["DATABASE"];
                S_UID = ConfigurationManager.AppSettings["S_UID"];
                S_PWD = ConfigurationManager.AppSettings["S_PWD"];
                QUERYTIME = ConfigurationManager.AppSettings["QUERYTIME"];

                // DB서버 연결 주소
                string strDBCon = "Server=" + ADDRESS + ";Database=" + DATABASE + ";User ID=" + S_UID + ";Password=" + S_PWD;
                Server_LogIn.strDBCon = strDBCon;
                Server_Main.strDBCon = strDBCon;

                // App.Config에서 받은 주소, 데이터베이스, 아이디, 패스워드 사용하여 DB서버 연결
                SqlConnection sqlConnection = new SqlConnection(strDBCon);
                if(sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("프로그램 실행을 위한 조건이 불충분합니다!!" + "\r\n" + ex.Message);
                Application.Exit();
            }
        }
    }
}