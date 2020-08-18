using System;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using VB = Microsoft.VisualBasic;
using System.Data.SqlClient;


namespace Module
{
    sealed class CM_Function
    {
        ////문자를 숫자로 리턴한다
        public static double GetNum(object textSring)
        {
            double returnValue = default(double);

            try
            {
                if (Information.IsDBNull(textSring))
                {
                    returnValue = (double)0;
                }
                else if (Strings.Trim(textSring.ToString()).Length == 0)
                {
                    returnValue = (double)0;
                }
                else
                {
                    returnValue = (double)(Conversion.Val(GetNumber(Strings.Trim(textSring.ToString())))); //getnumber함수는 spdsort모듈에 있음
                }

                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n Function Error!", "확인");
                return 0;
            }
            finally
            {
            }
        }

        private static object GetNumber(string numberString)
        {
            object returnValue = default(object);

            int i = 0;
            string character = "";

            try
            {
                for (i = 1; i <= numberString.Length; i++)
                {
                    character = VB.Strings.Mid(numberString, i, 1);
                    if (character.Trim().Length != 0 && character.Trim() != ",")
                    {
                        returnValue = returnValue + character;
                    }
                }

                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n Function Error!", "확인");
                return 0;
            }
            finally
            {
            }
        }


        public static string CheckNull(object textString)
        {
            string returnValue = default(string);

            try
            {
                if (textString == null)
                {
                    returnValue = "";
                }
                else if (Information.IsDBNull(textString))
                {
                    returnValue = "";
                }
                else
                {
                    returnValue = Strings.Trim(textString.ToString());
                }

                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n Function Error!", "확인");
                return "";
            }
            finally
            {
            }
        }


        //텍스트 박스 SetFocus
        public static void TextSetFocus(ref System.Windows.Forms.TextBox obj)
        {
            try
            {
                obj.Focus();
                obj.SelectionStart = 0;
                obj.SelectionLength = Strings.Len(obj.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n Function Error!", "확인");
                return;
            }
            finally
            {
            }
        }


        /////****************************************************************************************************/
        /////*    설명 : 문자열이 Null이거나 빈문자열일때 공백(Space) 하나를 반환하는 함수
        /////*    인자 : Find - 문자열

        /////****************************************************************************************************/
        public static string rns(string Find)
        {
            string returnValue = default(string);

            if (Information.IsDBNull(Find) || Find.Trim() == "")
            {
                returnValue = " ";
            }
            else
            {
                returnValue = Find.Trim();
            }

            return returnValue;
        }

        // 간단한 암호화 B
        // CoreGW/defaultM.asp용 암호화 체계 ^^;
        public static string EncryptString(string str_Renamed, bool encUse)
        {
            string returnValue = default(string);

            //On Error Resume Next VBConversions Warning: On Error Resume Next not supported in C#

            short i = default(short);
            string result = "";
            string tmp;
            try
            {
                tmp = str_Renamed.Trim();

                if (tmp == "")
                {
                    result = "";
                }
                else
                {
                    for (i = 1; i <= tmp.Length; i++)
                    {
                        result = result + VB.Strings.Format(Strings.Asc(tmp.Substring(i - 1, 1)), "00#");
                    }

                    if (encUse == true)
                    {
                        result = GetRand999() + result + GetRand999();
                    }
                }

                returnValue = result;
                return returnValue;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n EncryptString Function Error !", "확인");
                return "";
            }
            finally
            {

            }

        }

        // 간단한 복호화 B
        public static string DecryptString(string inData, bool decUse)
        {
            string returnValue = default(string);

            //On Error Resume Next VBConversions Warning: On Error Resume Next not supported in C#

            short i = default(short);
            string tmp = default(string);
            string tmpVal = default(string);
            string result = "";

            try
            {
                tmp = inData.Trim();

                if (tmp == "")
                {
                    result = "";
                }
                else
                {
                    if (decUse == true)
                    {
                        tmp = tmp.Substring(3, tmp.Length - 6);
                    }

                    for (i = 1; i <= tmp.Length; i += 3)
                    {
                        tmpVal = (int.Parse(tmp.Substring(i - 1, 3))).ToString();
                        result = result + Strings.Chr(int.Parse(tmpVal));
                    }
                }

                returnValue = result;
                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n DecryptString Function Error !", "확인");
                return "";
            }
            finally
            {
            }
        }

        // 100~999 사이의 난수를 반환합니다.
        private static short GetRand999()
        {
            try
            {
                short returnValue = default(short);
                //Int((상한값 - 하한값 + 1) * Rnd + 하한값)
                returnValue = (short)(Conversion.Int((999 - 100 + 1) * VBMath.Rnd() + 100));
                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n SetSpreadColID Function Error !", "확인");
                return 0;
            }
            finally
            {

            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        //DATE 포맷 함수
        public static string gSub_DateFormat(string pStr_Date)
        {
            string returnValue = default(string);

            string nReturn = "";
            try
            {

                if (pStr_Date.Trim() != "")
                {
                    nReturn = pStr_Date.Replace("-", "").Replace(" ", "").Replace(":", "");

                    //nReturn = VB.Format(nReturn, "####-##-##")

                    if (gFun_LenB(nReturn) == 8)
                    {
                        nReturn = gFun_LeftB(nReturn, 4) + "-" + gFun_MidB(nReturn, 5, 2) + "-" + gFun_RightB(nReturn, 2);
                    }
                }

                returnValue = nReturn;

                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n gSub_DateFormat Function Error !", "확인");
                return "";
            }
            finally
            {

            }
        }

        //-----------------------------------------------------------------------------------------------------------------------
        //TIME 포맷 함수
        public static string gSub_TimeFormat(string pStr_Time)
        {
            string returnValue = default(string);

            string nReturn = "";
            try
            {
                if (pStr_Time.Trim() != "")
                {
                    nReturn = pStr_Time.Replace("-", "").Replace(" ", "").Replace(":", "");

                    if (gFun_LenB(nReturn) == 4)
                    {
                        nReturn = VB.Strings.Format(double.Parse(nReturn), "00:00");
                    }
                    else if (gFun_LenB(nReturn) == 6)
                    {
                        nReturn = VB.Strings.Format(double.Parse(nReturn), "00:00:00");
                    }
                }

                returnValue = nReturn;

                return returnValue;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n gSub_TimeFormat Function Error !", "확인");
                return "";
            }
            finally
            {

            }
        }


        public static string gFun_MidB(string pStr_String, int pInt_Start, int pInt_Len = 0)
        {
            int nInt_Pos = default(int);
            System.Text.Encoding nObj_Encod = System.Text.Encoding.GetEncoding(949);
            byte[] nByt_Temp = null;
            string nStr_Return = "";

            try
            {
                nInt_Pos = nObj_Encod.GetByteCount(pStr_String) - pInt_Start + 1;
                pInt_Len = pInt_Len == 0 ? nInt_Pos : pInt_Len;
                pInt_Len = pInt_Len > nInt_Pos ? nInt_Pos : pInt_Len;

                nByt_Temp = nObj_Encod.GetBytes(pStr_String);
                nStr_Return = (string)(nObj_Encod.GetString(nByt_Temp, pInt_Start - 1, pInt_Len));

                return nStr_Return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n gFun_MidB Function Error !", "확인");
                return "";
            }
            finally
            {
            }

        }

        public static string gFun_LeftB(string pStr_String, int pInt_Len)
        {
            int nInt_Pos = default(int);
            System.Text.Encoding nObj_Encod = System.Text.Encoding.GetEncoding(949);
            byte[] nByt_Temp = null;
            string nStr_Return = "";

            try
            {
                nInt_Pos = System.Convert.ToInt32(nObj_Encod.GetByteCount(pStr_String));
                pInt_Len = pInt_Len > nInt_Pos ? nInt_Pos : pInt_Len;
                nByt_Temp = nObj_Encod.GetBytes(pStr_String);
                nStr_Return = (string)(nObj_Encod.GetString(nByt_Temp, 0, pInt_Len));

                return nStr_Return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n gFun_LeftB Function Error !", "확인");
                return "";
            }
            finally
            {
            }

        }

        public static string gFun_RightB(string pStr_String, int pInt_Len)
        {
            int nInt_Pos = default(int);
            byte[] nByt_Temp = null;
            System.Text.Encoding nObj_Encod = System.Text.Encoding.GetEncoding(949);
            //string nStr_Return = "";

            try
            {
                nInt_Pos = System.Convert.ToInt32(nObj_Encod.GetByteCount(pStr_String));
                pInt_Len = pInt_Len > nInt_Pos ? nInt_Pos : pInt_Len;

                nByt_Temp = nObj_Encod.GetBytes(pStr_String);
                return nObj_Encod.GetString(nByt_Temp, nByt_Temp.Length - pInt_Len, pInt_Len);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n gFun_RightB Function Error !", "확인");
                return "";
            }
            finally
            {
            }
        }

        public static int gFun_LenB(string pStr_String)
        {
            System.Text.Encoding nObj_Encod = System.Text.Encoding.GetEncoding(949);
            int nInt_Len = 0;
            try
            {
                nInt_Len = System.Convert.ToInt32(nObj_Encod.GetByteCount(pStr_String));
                return nInt_Len;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n gFun_LenB Function Error !", "확인");
                return 0;
            }
            finally
            {
            }
        }

        //이거 안먹는다..ㅜㅜ
        public static String Format(object Expression, string style)
        {
            string Str = "";
            try
            {
                switch (VB.Information.TypeName(Expression))
                {
                    case "DATE":
                        Str = VB.Strings.Format(Expression, style);
                        break;

                    default:
                        Str = VB.Strings.Format(GetNum(Expression), style);
                        break;
                }
                return Str;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n Format Function Error !", "확인");
                return "";
            }
            finally
            {
            }
        }

        public static string DateFormat(string tDate)
        {
            string Rt_STR = "";
            //2010-08-06 08:37:10

            try
            {
                if (tDate.Length == 14)
                {
                    Rt_STR = tDate.Substring(0, 4) + "-" + tDate.Substring(4, 2) + "-" + tDate.Substring(6, 2) + " " +
                             tDate.Substring(8, 2) + ":" + tDate.Substring(10, 2) + ":" + tDate.Substring(12, 2);
                    return Rt_STR;
                }
                return "";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n Format Function Error !", "확인");
                return "";
            }
        }
    }
}