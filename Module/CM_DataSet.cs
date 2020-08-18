using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Module
{
    class CM_DataSet
    {

        public static DataTable Exec_Select(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string[] Arry_Para;
            string[] Arry_Valu;

            System.Data.DataTable dtData = new System.Data.DataTable();

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');

            try
            {
                if (Arry_Para.Length == Arry_Valu.Length)
                {
                    CM_OLE.CreateConnection();
                    SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._conn);
                    scData.CommandType = CommandType.StoredProcedure;
                    scData.CommandTimeout = Convert.ToInt32(1000);

                    for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
                    {
                        scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
                    }

                    SqlDataAdapter adtData = new SqlDataAdapter(scData);

                    CM_OLE._conn.Open();
                    adtData.Fill(dtData);
                    CM_OLE._conn.Close();
                }
                else
                {
                    DataRow drData = dtData.NewRow();
                    dtData.Rows.Add(drData);
                }
            }
            catch (Exception Ex)
            {
                CM_OLE._conn.Close();
                DataRow drData = dtData.NewRow();
                dtData.Rows.Add(drData);
            }

            return dtData;
        }

        public static SqlDataAdapter Exec_DataAdapter(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string[] Arry_Para;
            string[] Arry_Valu;

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');


            CM_OLE.CreateConnection();
            SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._conn);
            scData.CommandType = CommandType.StoredProcedure;
            scData.CommandTimeout = Convert.ToInt32(1000);

            for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
            {
                scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
            }

            SqlDataAdapter adtData = new SqlDataAdapter(scData);

            return adtData;

        }

        public static string Exec_Save(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string Rtn_Message = string.Empty;
            string[] Arry_Para;
            string[] Arry_Valu;

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');

            try
            {
                if (Arry_Para.Length == Arry_Valu.Length)
                {
                    CM_OLE.CreateConnection();
                    SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._conn);
                    scData.CommandType = CommandType.StoredProcedure;
                    scData.CommandTimeout = Convert.ToInt32(1000);

                    for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
                    {
                        scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
                    }

                    CM_OLE._conn.Open();
                    scData.ExecuteNonQuery();
                    CM_OLE._conn.Close();

                    Rtn_Message = "저장 하였습니다.";
                }
                else
                {
                    Rtn_Message = "저장에 실패하였습니다.";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());

                Rtn_Message = "저장에 실패하였습니다.";
            }

            return Rtn_Message;
        }

        #region ▣▣ 저장 [Retutn :  DataTable]
        public static DataTable Exec_SaveSelect(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string[] Arry_Para;
            string[] Arry_Valu;

            System.Data.DataTable dtData = new System.Data.DataTable();

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');

            try
            {
                if (Arry_Para.Length == Arry_Valu.Length)
                {
                    CM_OLE.CreateConnection();
                    SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._conn);
                    scData.CommandType = CommandType.StoredProcedure;
                    scData.CommandTimeout = Convert.ToInt32(1000);

                    for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
                    {
                        scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
                    }

                    SqlDataAdapter adtData = new SqlDataAdapter(scData);

                    CM_OLE._conn.Open();
                    adtData.Fill(dtData);
                    CM_OLE._conn.Close();
                }
                else
                {
                    DataRow drData = dtData.NewRow();
                    dtData.Rows.Add(drData);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());

                CM_OLE._conn.Close();
                DataRow drData = dtData.NewRow();
                dtData.Rows.Add(drData);
            }

            return dtData;
        }
        #endregion

        public static string Exec_Delete(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string Rtn_Message = string.Empty;
            string[] Arry_Para;
            string[] Arry_Valu;

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');

            try
            {
                if (Arry_Para.Length == Arry_Valu.Length)
                {
                    CM_OLE.CreateConnection();
                    SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._conn);
                    scData.CommandType = CommandType.StoredProcedure;
                    scData.CommandTimeout = Convert.ToInt32(1000);

                    for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
                    {
                        scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
                    }

                    CM_OLE._conn.Open();
                    scData.ExecuteNonQuery();
                    CM_OLE._conn.Close();

                    Rtn_Message = "삭제 하였습니다.";
                }
                else
                {
                    Rtn_Message = "삭제에 실패하였습니다.";
                }
            }
            catch
            {
                Rtn_Message = "삭제에 실패하였습니다.";
            }

            return Rtn_Message;
        }

        #region ▣▣ 삭제 [Retutn :  DataTable]
        public static DataTable Exec_DeleteSelect(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string Rtn_Message = string.Empty;
            string[] Arry_Para;
            string[] Arry_Valu;

            System.Data.DataTable dtData = new System.Data.DataTable();

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');

            try
            {
                if (Arry_Para.Length == Arry_Valu.Length)
                {
                    CM_OLE.CreateConnection();
                    SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._conn);
                    scData.CommandType = CommandType.StoredProcedure;
                    scData.CommandTimeout = Convert.ToInt32(1000);

                    for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
                    {
                        scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
                    }

                    SqlDataAdapter adtData = new SqlDataAdapter(scData);

                    CM_OLE._conn.Open();
                    adtData.Fill(dtData);
                    CM_OLE._conn.Close();
                }
                else
                {
                    DataRow drData = dtData.NewRow();
                    dtData.Rows.Add(drData);
                }
            }
            catch
            {
                CM_OLE._conn.Close();
                DataRow drData = dtData.NewRow();
                dtData.Rows.Add(drData);
            }

            return dtData;
        }
        #endregion

        /// <summary>
        /// 이 함수는 SP 실행(테이블 반환)  함수입니다.
        /// </summary>
        public static DataTable fnSpExecuteTable(string sspname, params string[] pparams)
        {
            CM_OLE.CreateConnection();
            SqlCommand scData = new SqlCommand(sspname, CM_OLE._conn);
            scData.CommandType = CommandType.StoredProcedure;
            scData.CommandTimeout = Convert.ToInt32(1000);
            scData.Parameters.Clear();

            int i = 0;
            int k, j;
            while (i < pparams.Length)
            {
                //":"의 갯수로 파라미터의 갯수를 반환
                k = pparams[i].IndexOf(":");
                j = pparams[i].Length;

                if (k + 1 == j)
                {
                    scData.Parameters.AddWithValue(pparams[i].Substring(0, k), null);
                }
                else
                {
                    scData.Parameters.AddWithValue(pparams[i].Substring(0, k), pparams[i].Substring(k + 1, j - k - 1));
                }

                i++;
            }

            SqlDataAdapter adtData = new SqlDataAdapter(scData);
            DataTable nDataTable = new DataTable();

            try
            {
                CM_OLE._conn.Open();
                adtData.Fill(nDataTable);
                CM_OLE._conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());

                try
                {
                    nDataTable = new DataTable();
                    CM_OLE._conn.Close();

                    return nDataTable;
                }
                catch (Exception es)
                {
                    nDataTable = new DataTable();
                    CM_OLE._conn.Close();
                    return nDataTable;
                }
            }
            return nDataTable;
        }

        public static DataTable Exec_SCMSelect(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string[] Arry_Para;
            string[] Arry_Valu;

            System.Data.DataTable dtData = new System.Data.DataTable();

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');

            try
            {
                if (Arry_Para.Length == Arry_Valu.Length)
                {
                    CM_OLE.SCMCreateConnection();
                    SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._SCMconn);
                    scData.CommandType = CommandType.StoredProcedure;
                    scData.CommandTimeout = Convert.ToInt32(1000);

                    for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
                    {
                        scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
                    }

                    SqlDataAdapter adtData = new SqlDataAdapter(scData);

                    CM_OLE._SCMconn.Open();
                    adtData.Fill(dtData);
                    CM_OLE._SCMconn.Close();
                }
                else
                {
                    DataRow drData = dtData.NewRow();
                    dtData.Rows.Add(drData);
                }
            }
            catch (Exception Ex)
            {
                CM_OLE._SCMconn.Close();

                MessageBox.Show(Ex.Message.ToString());

                DataRow drData = dtData.NewRow();
                dtData.Rows.Add(drData);
            }
            return dtData;
        }

        public static SqlDataAdapter Exec_SCMDataAdapter(string Proc_Name, string Para_Name, string Valu_Data)
        {
            string[] Arry_Para;
            string[] Arry_Valu;

            Arry_Para = Para_Name.Split('^');
            Arry_Valu = Valu_Data.Split('^');

            CM_OLE.SCMCreateConnection();
            SqlCommand scData = new SqlCommand(Proc_Name, CM_OLE._SCMconn);
            scData.CommandType = CommandType.StoredProcedure;
            scData.CommandTimeout = Convert.ToInt32(1000);

            for (int arrCnt = 0; arrCnt < Arry_Para.Length; arrCnt++)
            {
                scData.Parameters.Add("@" + Arry_Para[arrCnt].Trim(), Arry_Valu[arrCnt].Trim());
            }

            SqlDataAdapter adtData = new SqlDataAdapter(scData);

            return adtData;
        }

    }
}