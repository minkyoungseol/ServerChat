using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Module;

namespace ServerChat
{
    public partial class Server_Main : Form
    {
        // 서버 소켓
        Socket listen_socket;
        // 클라이언트 소켓
        Socket client_socket;
        // 데이터
        string data;
        // Thread
        Thread receive_msg;
        public static string strDBCon;

        public Server_Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 해당 페이지 시작(서버 IP주소 comboBox 설정)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void Server_Main_Load(object sender, EventArgs e)
        {
            Search_Host();            
        }
        /// <summary>
        /// 서버 연결 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            int port;
            if (!int.TryParse(txt_PortNum.Text, out port))
            {
                MessageBox.Show("포트가 정확히 입력되지 않았습니다. 다시 입력해주세요.", "알림");
                txt_PortNum.Focus();
                txt_PortNum.SelectAll();
                return;
            }
            Server_Start(comboBox1.Text, port, 10);

        }
        /// <summary>
        ///  host, port, backlog 가지고 서버 연결 시작
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="backlog"></param>
       
        private void Server_Start(string host, int port, int backlog)
        {
            byte[] bytes = new Byte[1024];
            // 서버 소켓 초기화(TCP/IP 소켓 생성)
            listen_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress addr;
            if(host == "0.0.0.0")
            {
                addr = IPAddress.Any;
            }
            else
            {
                addr = IPAddress.Parse(host);
            }
            IPEndPoint ep = new IPEndPoint(addr, port);

            // 소켓 연결 및 클라이언트 소켓 대기
            try
            {
                listen_socket.Bind(ep);
                listen_socket.Listen(backlog);
                listBox1.Items.Add("서버연결완료!! 클라이언트 대기중.............");
                txt_PortNum.Enabled = false;
                comboBox1.Enabled = false;
                btn_Connect.Enabled = false;
                receive_msg = new Thread(Receive_MSg);
                receive_msg.IsBackground = true;
                CheckForIllegalCrossThreadCalls = false;
                receive_msg.Start();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Receive_MSg()
        {            
            try
            {
                client_socket = listen_socket.Accept();
                add_ListBox("클라이언트 접속 완료!! 메세지 전송 가능!!");

                while (true)
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = client_socket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    if (data.IndexOf("<EOF>") == -1)
                    {
                        break;
                    }
                    else
                    {
                        data = data.Replace("<EOF>", "");
                        add_ListBox("[받은 메세지]" + data);
                        data = "";
                    }
                }
            }catch(Exception e)
            {   
                if(e.Message == "스레드가 중단되었습니다.")
                {
                    MessageBox.Show("서버 연결이 해제되었습니다.");
                }
                else
                {
                    client_socket.Close();
                    Thread.Sleep(500);
          
                    Server_Start(comboBox1.Text, Int32.Parse(txt_PortNum.Text), 10);
                    MessageBox.Show("클라이언트가 접속을 종료하였습니다.");
                   

                }
                // MessageBox.Show("서버 연결이 해제되었습니다.");
                // 클라이언트 서버 연결 종료 시 현재 연결은 사용자 호스트의 .... 왜...뭘로 구분해야하지.....????ㅠㅠㅠㅠㅠㅠㅠㅠ
                // 서버 연결 해제 누르면 스레드 중단 팝업 (나중에 이 곳에서 다른 팝업이 나오는지 확인하고 문구 처리 다시 )
            }
        }
        
        /// <summary>
        /// Thread(Background)에서 ListBox1에 메세지 띄울 때 ListBox위치 선택(띄울 메세지 추가) 
        /// </summary>
        /// <param name="temp"></param> 
        private void add_ListBox(string temp)
        {
            try
            {
                if (listBox1.InvokeRequired)
                {
                    listBox1.Invoke((MethodInvoker)delegate ()
                    {
                        listBox1.Items.Add(temp);
                    });
                }
                else
                {
                    listBox1.Items.Add(temp);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #region Search_Host함수 - 네트워크 검색 IP주소 형식만 콤보박스에 넣기
        /// <summary>
        /// 내 컴퓨터에서 확인되는 네트워크 검색 후 IP 주소 형식만 찾아서 서버 IP 선택 ComboBox에 넣기
        /// </summary>
        private void Search_Host()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] IPs = host.AddressList;
            for (int i = 0; i < IPs.Length; i++)
            {
                if (IPs[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    comboBox1.Items.Add(IPs[i]);
                }
            }
        }
        #endregion

        /// <summary>
        /// 글 입력 후 전송 버튼 클릭 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            byte[] msg = Encoding.ASCII.GetBytes(String.Format("{0}<EOF>", txt_Msg.Text));
            if (client_socket == null)
            {
                listBox1.Items.Add("연결된 클라이언트가 없습니다!!");
                txt_Msg.Clear();
            }
            else
            {
                if(client_socket.Connected == false)
                {
                    MessageBox.Show("연결된 클라이언트가 없습니다.");
                    txt_Msg.Clear();
                    btn_Connect.Focus();
                }
                else
                {
                    int byteSent = client_socket.Send(msg);
                    listBox1.Items.Add("[보낸 메세지]" + txt_Msg.Text);
                    Chat_Log_Insert(CM_Main.GUBUN, CM_Main.S_ID, txt_Msg.Text);
                    txt_Msg.Clear();
                }
                
            }            
        }

        /// <summary>
        /// 포트 번호 입력 후 엔터키 눌렀을 때 연결버튼 클릭 이벤트 발생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_PortNum_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btn_Connect_Click(sender, e);
            }
        }

        private void Chat_Log_Insert(string gubun, string id, string text)
        {
            SqlConnection conn = new SqlConnection(strDBCon);
            try
            {
                SqlCommand sqlCommand = new SqlCommand(strDBCon);
                sqlCommand.Connection = conn;
                sqlCommand.CommandText = "insert into ChatLog (Gubun, UserID, Text, DateTime) values (@param1, @param2, @param3, getdate())";
                sqlCommand.Parameters.AddWithValue("@param1", gubun);
                sqlCommand.Parameters.AddWithValue("@param2", id);
                sqlCommand.Parameters.AddWithValue("@param3", text);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  보낼 메세지 입력 후 엔터키 눌렀을 때 전송버튼 클릭 이벤트 발생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Msg_KeyUp(object sender, KeyEventArgs e)
        
        {
            if(e.KeyCode == Keys.Enter)
            {
                btn_Send_Click(sender, e);
            }
        }


        /// <summary>
        /// 해제 버튼 클릭 시 이벤트 발생(채팅thread종료 및 소켓서버 닫고 초기화) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DisConnect_Click(object sender, EventArgs e)
        {
            if(client_socket != null)
            {
                client_socket.Close();
            }   
            if(listen_socket != null)
            {
                listen_socket.Close();
            }
            if(receive_msg != null)
            {
                receive_msg.Abort();
            }            
            comboBox1.Enabled = true;
            txt_PortNum.Enabled = true;
            btn_Connect.Enabled = true;
        }

        private void Server_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
