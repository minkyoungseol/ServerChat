using System.Windows.Forms;
using System;
using Microsoft.VisualBasic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using VB = Microsoft.VisualBasic;

namespace Module
{
    sealed class CM_File
    {
        [DllImport("kernel32", EntryPoint = "CreateFileA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("user32", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)] //콤보박스 드랍다운을 위한 api함수 정의
        public static extern long SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);


        //Ini 파일을 자동 생성한다
        public static void WriteIniFile(string Path)
        {
            try
            {
                string TEMP_PATH = "";
                TEMP_PATH = Path;

                //읽기전용 속성 해제
                if (Microsoft.VisualBasic.FileSystem.Dir(Path, (Microsoft.VisualBasic.FileAttribute)0) != "")
                {
                    Microsoft.VisualBasic.FileSystem.SetAttr(Path, Constants.vbNormal);
                }

                //파일 속성변경
                VB.FileSystem.SetAttr(Path, Constants.vbNormal);

                //SQLPATH
                //WritePrivateProfileString 한줄 타면 변수 값이 변해 버린다..
                if (WritePrivateProfileString("SQLPATH", "ADDRESS", CM_Function.EncryptString(Module.CM_Main.ADDRESS, true), Path) == false)
                {
                    return;
                }
                if (WritePrivateProfileString("SQLPATH", "DATABASE", CM_Function.EncryptString(Module.CM_Main.DATABASE, true), Path) == false)
                {
                    return;
                }
                if (WritePrivateProfileString("SQLPATH", "S_UID", CM_Function.EncryptString(Module.CM_Main.S_UID, true), Path) == false)
                {
                    return;
                }
                if (WritePrivateProfileString("SQLPATH", "S_PWD", CM_Function.EncryptString(Module.CM_Main.S_PWD, true), Path) == false)
                {  
                    return;
                }
                if (WritePrivateProfileString("SQLPATH", "QUERYTIME", CM_Function.EncryptString(Module.CM_Main.QUERYTIME, true), Path) == false)
                {
                    return;
                }

                //읽기전용 속성으로 변경
                VB.FileSystem.SetAttr(Path, Constants.vbReadOnly);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " \n ini 파일 작성을 실패하였습니다.", "확인");
                //Interaction.MsgBox("ini 파일 작성을 실패하였습니다.", MsgBoxStyle.Critical, null);
            }

        }

        //Ini 파일을 읽는다
        public static string ReadIniFile(string Path, string Section, string Key)
        {
            string returnValue = default(string);

            try
            {
                int MAX_SIZE = 100;

                StringBuilder Buffer = new StringBuilder(MAX_SIZE);
                //Buffer = VB.Strings.Space(MAX_SIZE);

                if (GetPrivateProfileString(Section, Key, " ", Buffer, MAX_SIZE, Path) > 0)
                {
                    //Buffer2 = Convert.ToString(Buffer);
                    //MessageBox.Show(Buffer2);
                    returnValue = StripTerminator(Convert.ToString(Buffer)).Trim();
                }
                else
                {
                    returnValue = " ";
                }
                Buffer.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " \n ini 파일 읽기에 실패하였습니다.", "확인");
                //Interaction.MsgBox(e.Message  + " \n ini 파일 읽기에 실패하였습니다.", MsgBoxStyle.Critical, null);
                return "";
            }

            return returnValue;
        }

        private static string StripTerminator(string strString)
        {
            string returnValue = default(string);

            int intZeroPos = default(int);
            try
            {
                //intZeroPos = (short)(strString.IndexOf(('\0').ToString()) + 1);
                intZeroPos = VB.Strings.InStr("", "");


                if (intZeroPos > 0)
                {
                    returnValue = strString.Substring(0, intZeroPos - 1);
                }
                else
                {
                    returnValue = strString;
                }

                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " \n Function Error! .", "확인");
                return "";
            }
            finally { }
        }

        // 구조체 선언
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NETRESOURCE
        {
            public uint dwScope;
            public uint dwType;
            public uint dwDisplayType;
            public uint dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        // API 함수 선언
        [DllImport("mpr.dll", CharSet = CharSet.Auto)]
        public static extern int WNetUseConnection(
                    IntPtr hwndOwner,
                    [MarshalAs(UnmanagedType.Struct)] ref NETRESOURCE lpNetResource,
                    string lpPassword,
                    string lpUserID,
                    uint dwFlags,
                    StringBuilder lpAccessName,
                    ref int lpBufferSize,
                    out uint lpResult);

        // API 함수 선언 (공유해제)
        [DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2", CharSet = CharSet.Auto)]
        public static extern int WNetCancelConnection2A(string lpName, int dwFlags, int fForce);

        // 공유연결
        public static int ConnectRemoteServer(string server, string ID, string Pw)
        {

            int capacity = 64;
            uint resultFlags = 0;
            uint flags = 0;
            string TServer = @"\\" + server;
            System.Text.StringBuilder sb = new System.Text.StringBuilder(capacity);
            NETRESOURCE ns = new NETRESOURCE();
            ns.dwType = 1;              // 공유 디스크
            ns.lpLocalName = null;      // 로컬 드라이브 지정하지 않음
            ns.lpRemoteName = TServer;
            ns.lpProvider = null;
            int result = 0;

            try
            {
                result = WNetUseConnection(IntPtr.Zero, ref ns, Pw, ID, flags, sb, ref capacity, out resultFlags);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result;
            }
        }
        // 공유해제
        // 네트웍 한참뒤에 끊어짐.
        public static int CencelRemoteServer(string server)
        {
            int result = 0;

            try
            {
                string TServer = @"\\" + server;
                result = WNetCancelConnection2A(TServer, 1, 0);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 9999;
            }
        }

        public static Boolean FN_FileFind(string server, string ID, string Pw, string FilePathFile)
        {
            try
            {
                if (0 == ConnectRemoteServer(server, ID, Pw))
                {
                    string sDir = FilePathFile;
                    FileInfo fileinfo = new FileInfo(sDir);
                    if (fileinfo.Exists)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("해당경로에 파일이 없습니다.");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("공유폴더 로그인 실패");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static Boolean FN_FileCopy(string server, string ID, string Pw, string FilePathFile, string SavePathFile)
        {
            try
            {
                if (0 == ConnectRemoteServer(server, ID, Pw))
                {
                    string sDir = FilePathFile;

                    string FileNameOnly = Path.GetFileName(SavePathFile);

                    string FoldOnly = Path.GetDirectoryName(SavePathFile);

                    FileInfo fileinfo = new FileInfo(sDir);
                    if (fileinfo.Exists)
                    {
                        DirectoryInfo di = new DirectoryInfo(FoldOnly);
                        if (di.Exists == false)
                        {
                            di.Create();
                        }

                        fileinfo.CopyTo(SavePathFile, true);
                    }
                    else
                    {
                        MessageBox.Show("해당경로에 파일이 없습니다.");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("공유폴더 로그인 실패");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static Boolean FN_FileMove(string server, string ID, string Pw, string FilePathFile, string SavePathFile)
        {
            try
            {
                if (0 == ConnectRemoteServer(server, ID, Pw))
                {
                    string sDir = FilePathFile;

                    string FoldOnly = Path.GetDirectoryName(SavePathFile);

                    FileInfo fileinfo = new FileInfo(sDir);
                    if (fileinfo.Exists)
                    {
                        DirectoryInfo di = new DirectoryInfo(FoldOnly);
                        if (di.Exists == false)
                        {
                            di.Create();
                        }

                        fileinfo.MoveTo(SavePathFile);
                    }
                    else
                    {
                        MessageBox.Show("해당경로에 파일이 없습니다.");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("공유폴더 로그인 실패");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static Boolean FN_FileDel(string server, string ID, string Pw, string FilePathFile)
        {
            try
            {
                if (0 == ConnectRemoteServer(server, ID, Pw))
                {
                    string sDir = FilePathFile;
                    FileInfo fileinfo = new FileInfo(sDir);

                    if (fileinfo.Exists)
                    {
                        fileinfo.Delete();
                    }
                    else
                    {
                        MessageBox.Show("해당경로에 파일이 없습니다.");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("공유폴더 로그인 실패");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static Boolean FN_FoldDel(string server, string ID, string Pw, string DelPath)
        {
            try
            {
                if (0 == ConnectRemoteServer(server, ID, Pw))
                {
                    DirectoryInfo di = new DirectoryInfo(DelPath);
                    if (di.Exists == true)
                    {
                        di.Delete(true);
                    }
                    else
                    {
                        MessageBox.Show("삭제할 경로가 없습니다.");
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("공유폴더 로그인 실패");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}