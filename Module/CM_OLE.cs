using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Module
{
    //SQL 관련 함수..
    class CM_OLE
    {
        public static SqlConnection _conn = null;
        public static SqlConnection _SCMconn = null;


        public static void SCMCreateConnection()
        {
            try
            {
                if (_SCMconn == null)
                {
                    _SCMconn = new SqlConnection(@"server = 218.151.213.84; database = HDS_S302; uid = s302; pwd = s3021");

                }
            }
            catch (SqlException e)
            {
                string errors = "";

                foreach (SqlError error in e.Errors)
                {
                    errors += "\r\n" + error.Message;
                }
                _SCMconn.Close();
                MessageBox.Show(e.Message, "error! CreateConnection", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }


        public static void CreateConnection()
        {
            try
            {
                if (_conn == null)
                {
                    _conn = new SqlConnection(@"server = " + CM_Main.ADDRESS.Trim() + "; database = " + CM_Main.DATABASE.Trim() + "; uid = " + CM_Main.S_UID.Trim() + "; pwd = " + CM_Main.S_PWD.Trim());

                }
            }
            catch (SqlException e)
            {
                string errors = "";

                foreach (SqlError error in e.Errors)
                {
                    errors += "\r\n" + error.Message;
                }
                _conn.Close();
                MessageBox.Show(e.Message, "error! CreateConnection", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        // 완전 강제 소멸..
        public static void Close()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
                GC.Collect();
            }
        }

        //디비 클로우즈
        public static void WorkClose()
        {

            if (_conn != null)
            {
                _conn.Close();
                GC.Collect();
            }
        }

        public static DataSet SelectQuery(string strSQL, string strtableName)
        {
            DataSet ds;

            try
            {
                //CreateConnection();
                ds = new DataSet(strtableName);
                SqlDataAdapter adapter = new SqlDataAdapter(strSQL, _conn);
                adapter.Fill(ds);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "error! SelectQuery", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return null;
            }
            finally
            {
                _conn.Close();
            }

            return ds;
        }


        public static bool StoredQuery(string strSQL)
        {
            try
            {
                //CreateConnection();
                SqlCommand com = new SqlCommand(strSQL, _conn);

                int result = com.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "error! StoredQuery", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                return false;
            }
            finally
            {
                _conn.Close();
            }

            return true;
        }

        public static string[] GetRowData(DataSet ds, string TableName, int RowIndex)
        {
            try
            {
                DataRow dr = ds.Tables[0].Rows[RowIndex];

                string[] strResult = new string[dr.Table.Columns.Count];

                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {

                    strResult[i] = dr[i].ToString();
                }

                dr.Delete();

                return strResult;
            }
            catch
            {
                return null;
            }
        }

    }
}