using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using System.Xml;
using System.Windows.Forms;


namespace EXOLUBE.Common
{
    /// <summary>
    /// 파일을 관리한다.
    /// </summary>
    public class HistLogFile
    {
        private const String _DEF_HIST_FILE_ = "H";

        private static String makeLogPath()
        {
            //String strLogPath = Directory.GetCurrentDirectory();
            String strLogPath = @"C:\\RUTEC\\ExLog";
      

            String strNewPath = strLogPath + "\\hist";
            DirectoryInfo di = new DirectoryInfo(strNewPath);
            if (di.Exists == false)
                di.Create();
            return strNewPath;
        }
        /// <summary>
        /// 라벨폴더 추가 2024-04-05 김선엽
        /// </summary>
        /// <returns></returns>


        private static String makeIniPath()
        {

            //String strLogPath = Directory.GetCurrentDirectory();
            String strLogPath = @"C:\\RUTEC\\ExLog";

            String strNewPath = strLogPath;
            DirectoryInfo di = new DirectoryInfo(strNewPath);
            if (di.Exists == false)
                di.Create();
            return strNewPath;
        }

        private static String makeFullFileName(String sFileName)
        {
            if (sFileName == "ini.ini")
            {
                return makeIniPath() + "\\" + sFileName;
            }
            else if(sFileName == "test.ini")
            {
                return makeIniPath() + "\\" + sFileName;
            }
            else
            {
                return makeLogPath() + "\\" + sFileName;
            }
        }

        /// <summary>
        /// 생성시 지정한 루트에 텍스트 파일를 입력한 파일 이름(이름중[.txt]제외)으로 생성
        /// 생성완료시 true, 실패시 false 반환한다.
        /// </summary>
        /// <param name="strFileName">파일 이름(String)</param>
        /// <returns>생성완료시 true, 실패시 false</returns>
        public static bool createTextFile(string sFileName)
        {
            FileStream fsFile = null;
            bool nResult = false;

            try
            {
                String sFileFullName = makeFullFileName(sFileName);
                fsFile = new FileStream(sFileFullName, FileMode.Create);
                nResult = true;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message; //이벤트 추가 ㅋ
                nResult = false;
            }
            finally
            {
                fsFile.Close();
            }
            return nResult;
        }

        public static bool clearFile()
        {
            return clearTextFile(_DEF_HIST_FILE_ + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        }

        /// <summary>
        /// 생성시 지정한 루트에 텍스트 파일를 입력한 파일 이름(이름중[.txt]제외)으로 생성
        /// 생성완료시 true, 실패시 false 반환한다.
        /// </summary>
        /// <param name="strFileName">파일 이름(String)</param>
        /// <returns>생성완료시 true, 실패시 false</returns>
        public static bool clearTextFile(string sFileName)
        {
            FileStream fsFile = null;
            bool nResult = false;

            try
            {
                String sFileFullName = makeFullFileName(sFileName);
                fsFile = new FileStream(sFileFullName, FileMode.Truncate);
                nResult = true;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message; //이벤트 추가 ㅋ
                nResult = false;
            }
            finally
            {
                if (fsFile != null)
                    fsFile.Close();
            }
            return nResult;
        }

        public static bool writeLogFile(String sWriteString)
        {
            return writeTextFile(_DEF_HIST_FILE_ + DateTime.Now.ToString("yyyyMMdd") + ".txt", sWriteString);
        }

        public static bool writeLogFilePLC(String sWriteString)
        {
            return writeTextFile(_DEF_HIST_FILE_ + DateTime.Now.ToString("yyyyMMdd") + "_PLC.txt", sWriteString);
        }

        public static bool writePrintLogFile(String sWriteString)
        {
            return writeTextFile(_DEF_HIST_FILE_ + DateTime.Now.ToString("yyyyMMdd") + "_PRINT.txt", sWriteString);
        }

        public static bool writeFileIni(String sWriteString)
        {
            return writeIniFile("ini.ini", sWriteString);
        }

        public static bool writeFileTest(String sWriteString)
        {
            return writeIniFile("test.ini", sWriteString);
        }

        /// <summary>
        /// 입력한 파일(이름중[.txt])에 입력한 문자열을 추가한다.
        /// 기존 파일에 내용 추가. 파일이 없을 경우 생성
        /// 입력 완료시 true, 실패시 false 반환한다.
        /// </summary>
        /// <param name="sFileName">파일명(String)</param>
        /// <param name="sWriteString">입력(추가)할 문자열(String)</param>
        /// <returns>입력 완료시 true, 실패시 false</returns>
        public static bool writeTextFile(String sFileName, String sWriteString)
        {
            FileStream fsFile = null;
            StreamWriter nSW = null;
            bool nResult = false;
            try
            {
                String sFileFullName = makeFullFileName(sFileName);
                fsFile = new FileStream(sFileFullName, FileMode.Append, FileAccess.Write);

                nSW = new StreamWriter(fsFile);
                long ltick = DateTime.Now.Ticks;
                //string strData = DateTime.Now.ToString("yyyyMMddHHmmss") + String.Format("{0:d3}", DateTime.Now.Millisecond);
                string strData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                nSW.Write(strData + " : " + sWriteString);
                nResult = true;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
                nResult = false;
            }
            finally
            {
                nSW.Close();
                fsFile.Close();
            }
            return nResult;
        }

        /// <summary>
        /// INI 파일 생성
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="sWriteString"></param>
        /// <returns></returns>
        public static bool writeIniFile(String sFileName, String sWriteString)
        {
            FileStream fsFile = null;
            StreamWriter nSW = null;
            bool nResult = false;
            try
            {
                String sFileFullName = makeFullFileName(sFileName);
                fsFile = new FileStream(sFileFullName, FileMode.Append, FileAccess.Write);

                // Endording Ansi

                nSW = new StreamWriter(fsFile, Encoding.Default);
                long ltick = DateTime.Now.Ticks;
                string strData = DateTime.Now.ToString("yyyyMMddHHmmss") + String.Format("{0:d3}", DateTime.Now.Millisecond);
                nSW.Write(sWriteString);
                nResult = true;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
                nResult = false;
            }
            finally
            {
                nSW.Close();
                fsFile.Close();
            }
            return nResult;
        }

        public static bool readLogFile(TextBox txtLog, string fileName)
        {
            return readTextFile(txtLog, fileName);
        }
        /// <summary>
        /// 입력한 파일(이름중[.txt]제외)에서 파일의 내용을 읽어들인다.
        /// </summary>
        /// <param name="sFileName">파일명(String)</param>
        /// <returns></returns>
        public static bool readTextFile(TextBox txtLog, String sFileName)
        {
            bool bRet = false;
            StreamReader sr = null;
            String strReadText = "";
            try
            {
                String sFileFullName = makeFullFileName(sFileName);

                FileStream fsFile = new FileStream(sFileFullName, FileMode.OpenOrCreate, FileAccess.Read);
                sr = new StreamReader(fsFile, System.Text.Encoding.Default);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                while (sr.Peek() > -1)
                {
                    strReadText = sr.ReadLine();
                    string[] temp = strReadText.Split(',');
                    if(temp.Length == 2
                        && temp[0].Length == 17
                        && temp[1].Length == 4)
                        txtLog.AppendText(temp[0] + "/" + temp[1] + "\r\n");
                }
                bRet = true;
            }
            catch (Exception ex)
            {
                strReadText = "";
                string strMsg = ex.Message;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
            return bRet;
        }

    }

    
}
