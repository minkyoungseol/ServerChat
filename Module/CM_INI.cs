using System.Runtime.InteropServices;
using System.Text;

namespace Module
{
    public class CM_INI
    {
        private static CM_INI _default;
        public static CM_INI Default
        {
            get
            {
                if (_default == null)
                    _default = new CM_INI();
                return _default;
            }
        }

        [DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,

                                                        int size, string filePath);
        // string FilePath = Application.StartupPath + @"\\config.ini";

        #region 비암호화 
        /// <summary>
        /// 설정 파일 을 읽어 오는 부분 
        /// </summary>
        /// <param name="skey">구분자</param>
        /// <param name="key">읽어올 자료</param>
        /// <returns></returns>
        public string INIRead(string FilePath, string skey, string key)
        {
            StringBuilder iniread = new StringBuilder(255);
            int ret = GetPrivateProfileString(skey, key, "", iniread, 255, FilePath);

            string seedstring = string.Empty;
            // seedstring = Seed.Seed.Decrypt(iniread.ToString());

            return iniread.ToString();
        }

        /// <summary>
        /// 설정 파일을 저장 하는 부분 
        /// </summary>
        /// <param name="skey">구분자</param>
        /// <param name="key">저장할 부분</param>
        /// <param name="values">입력 값</param>
        public void INIWrite(string FilePath, string skey, string key, string values)
        {
            string seedWrite = string.Empty;

            System.IO.FileInfo fi = new System.IO.FileInfo(FilePath);
            System.IO.FileAttributes fas = System.IO.File.GetAttributes(FilePath);
            if ((fas & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
            {
                System.IO.File.SetAttributes(FilePath, System.IO.FileAttributes.Normal);
            }

            //  seedWrite = Seed.Seed.Encrypt(values);
            WritePrivateProfileString(skey, key, values, FilePath);

            System.IO.File.SetAttributes(FilePath, System.IO.FileAttributes.ReadOnly);
        }
        #endregion

        #region Seed 암호화로 처리 부분
        /// <summary>
        /// 설정 파일 을 읽어 오는 부분 
        /// </summary>
        /// <param name="skey">구분자</param>
        /// <param name="key">읽어올 자료</param>
        /// <returns></returns>
        public string INIDecryptRead(string FilePath, string skey, string key)
        {
            StringBuilder iniread = new StringBuilder(255);
            int ret = GetPrivateProfileString(skey, key, "", iniread, 255, FilePath);

            string seedstring = string.Empty;
            seedstring = Seed.Seed.Decrypt(iniread.ToString());

            return seedstring;
        }

        /// <summary>
        /// 설정 파일을 저장 하는 부분 
        /// </summary>
        /// <param name="skey">구분자</param>
        /// <param name="key">저장할 부분</param>
        /// <param name="values">입력 값</param>
        public void INIEncryptWrite(string FilePath, string skey, string key, string values)
        {
            string seedWrite = string.Empty;
            System.IO.FileInfo fi = new System.IO.FileInfo(FilePath);
            System.IO.FileAttributes fas = System.IO.File.GetAttributes(FilePath);
            if ((fas & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
            {
                System.IO.File.SetAttributes(FilePath, System.IO.FileAttributes.Normal);
            }

            seedWrite = Seed.Seed.Encrypt(values);
            WritePrivateProfileString(skey, key, seedWrite, FilePath);

            System.IO.File.SetAttributes(FilePath, System.IO.FileAttributes.ReadOnly);
        }
        #endregion
    }
}