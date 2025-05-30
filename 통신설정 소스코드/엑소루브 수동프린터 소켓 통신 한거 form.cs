using Dao.Common;
using Dao.Pop.Service;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using EXOLUBE.Common;
using EXOLUBE.reportLabel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using EXOLUBE.Main;
using IBatisNet.DataMapper;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraWaitForm;
using System.Drawing.Printing;

namespace EXOLUBE.Pop
{
    public partial class RusiaCodeManualLabelerForm : DevExpress.XtraEditors.XtraForm
    {
        CombineMgt cmgt = new CombineMgt();

        //private RusiaCodeManualLabelerForm mainForm;

        private Dictionary<string, bool> connectionResults = new Dictionary<string, bool>();

        public Dictionary<string, DeviceClient> Devices = new Dictionary<string, DeviceClient>();

        //private System.Windows.Forms.Timer statusTimer;
        private System.Windows.Forms.Timer EquipstatusTimer;
        public RusiaCodeManualLabelerForm()
        {
            InitializeComponent();
            //mainForm = this;
        }

        /// <summary>
        /// 라벨발행 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_label_print_Click(object sender, EventArgs e)
        {
            try
            {

                List<string> disconnectedDevices = new List<string>();
                string[] deviceIds = { "PRINT" };

                foreach (var deviceId in deviceIds)
                {
                    bool isConnected = false;

                    if (Devices.TryGetValue(deviceId, out var client) && client != null && client.IsConnected)
                    {
                        var socket = client.GetSocket();
                        isConnected = socket != null && !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
                    }
                    // 로그 기록
                    if (!isConnected)
                    {
                        disconnectedDevices.Add(deviceId);
                        XtraMessageBox.Show("프린터 연결 끊김", "통신오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AppendLog(deviceId, "연결 끊김 감지됨", Color.Red);
                        
                    }
                }
                // 전체 끊긴 장비를 요약해서 로그 출력
                if (disconnectedDevices.Count > 0)
                {
                    string summary = string.Join(", ", disconnectedDevices);
                    AppendLog("SYSTEM", $"[경고] 다음 장비 연결 끊김: {summary}", Color.OrangeRed);
                    return;
                }

                //발행할 수량
                int print_num = int.Parse(text_print_num.Text.Trim().ToString());
                //발행한 양호수량
                int good_qty = int.Parse(text_print_good_qty_get.Text.ToString());
                //계획수량
                int plan_qty = int.Parse(text_plan_qty_get.Text.ToString());
                //발행 가능 수량
                int posible_qty = plan_qty - good_qty;


                if (posible_qty >= print_num)//발행가능 수량
                {
                    DialogResult result = XtraMessageBox.Show("발행 하시겠습니까?", "발행", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                        PrintMultipleWithStatusCheck(print_num);  // 출력
                }
                else// 발행가능 수량 초과
                {
                    XtraMessageBox.Show("해당 기록서의 발행가능 수량 초과", "수량초과", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
                XtraMessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 리포트 라벨 출력
        /// </summary>
        /// <param name="count"></param>
        private void PrintMultipleWithStatusCheck(int count)
        {
            try
            {
                bool flag = false;
                XtraReport rusia = new RusiaLabel();
                int successCount = 0;
                int failCount = count;


                for (successCount = 0; successCount < count; successCount++)//발행 완료 수량 증가
                {
                    string barcodeId = "";
                    int rowHandle = 0;
                    for (rowHandle = 0; rowHandle < gv_barcode_mst.RowCount; rowHandle++)
                    {
                        string print_yn = gv_barcode_mst.GetRowCellValue(rowHandle, "PRINT_YN").ToString();
                        if (print_yn == "N")
                        {
                            barcodeId = gv_barcode_mst.GetRowCellValue(rowHandle, "BARCODEID").ToString();
                            break;
                        }
                    }
                    Dictionary<string, string> rusiaCode = new Dictionary<string, string>();
                    if (!string.IsNullOrWhiteSpace(barcodeId))
                        rusiaCode = ConvertToGS1BarcodeFormat(barcodeId);
                    else
                    {
                        flag = true;
                        break;
                    }
                    //프린터 상태 체크
                    if (!WaitForTscPrintComplete())
                    {
                        string logText = $"출력 실패: 프린터 응답 없음";
                        // 메시지 + 로그
                        XtraMessageBox.Show($"PRINT {logText}", "연결 상태", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RusiaCodeManualLabelerForm main = this;

                        main.AppendLog("PRINT", logText, Color.Red);
                        flag = true;
                        break;
                    }

                    //여기서 DB_RES,MST,INFO 업뎃 그리드뷰 업뎃

                    string order_no = gv_barcode_mst.GetRowCellValue(rowHandle, "ORDER_NO").ToString();
                    string serial_no = rusiaCode["serial"].ToString();
                    List<XMLParam> param = new List<XMLParam>();

                    param.Add(new XMLParam(nameof(order_no), order_no));
                    param.Add(new XMLParam(nameof(serial_no), serial_no));
                    param.Add(new XMLParam("user_id", RTCommon.USER_ID));
                    cmgt.beginTran();
                    int INFOResult = (int)cmgt.RequestDatabase("UpdateRusiaCodeInfo", CombineMgt.QueryType.UPDATE, param, RTCommon.USER_ID);
                    int MSTResult = (int)cmgt.RequestDatabase("UpdateRusiaCodeMST", CombineMgt.QueryType.UPDATE, param, RTCommon.USER_ID);
                    int RESResult = (int)cmgt.RequestDatabase("UpdateRusiaCodeRes", CombineMgt.QueryType.UPDATE, param, RTCommon.USER_ID);

                    Cursor.Current = Cursors.Default;
                    if (MSTResult > 0)
                    {
                        //// 처리 성공
                        //MessageBox.Show("제조번호 : " + lot_no + "이 등록 되었습니다.", "메세지", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("RusiaCodeMST 업데이트 실패", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // 처리 실패
                        flag = true;
                        break;
                    }
                    if (INFOResult > 0)
                    {
                        //// 처리 성공
                        //MessageBox.Show("제조번호 : " + lot_no + "이 등록 되었습니다.", "메세지", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("RusiaCodeInfo 업데이트 실패", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // 처리 실패
                        flag = true;
                        break;
                    }
                    if (RESResult > 0)
                    {
                        //// 처리 성공
                        //MessageBox.Show("제조번호 : " + lot_no + "이 등록 되었습니다.", "메세지", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("RusiaCodeRes 업데이트 실패", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // 처리 실패
                        flag = true;
                        break;
                    }
                    if (MSTResult > 0 && INFOResult > 0 && RESResult > 0)
                    {
                        cmgt.commitTran();//커밋
                        
                        gv_barcode_mst.SetRowCellValue(rowHandle, "PRINT_YN", "Y");

                        // 해당 오더에 마스터가 전체 발행완료 이면 info 작업완료
                        gv_product_info.GetFocusedRowCellValue("ORDER_NO").ToString();
                        gv_product_info.SetFocusedRowCellValue("QC_STATUS", "NONE");
                        gv_product_info.SetFocusedRowCellValue("ITEM_NAME", "작업중");
                        string goodNum = (int.Parse(txt_res_print_good_qty.Text.ToString()) + 1).ToString();
                        txt_res_print_good_qty.Text = goodNum;
                        text_print_good_qty_get.Text = goodNum;
                        gv_product_info.SetFocusedRowCellValue("PRINT_GOOD_QTY", goodNum);

                        //여기 코드 가져와서 발행
                        rusia.Parameters["GTIN"].Value = $"(01){rusiaCode["gtin"].ToString()}";
                        rusia.Parameters["Serial"].Value = $"(21){rusiaCode["serial"].ToString()}";
                        rusia.Parameters["BarCode"].Value = rusiaCode["rusiaCode"].ToString();
                        //라벨 초기화
                        rusia.CreateDocument();
                        // 프린트 상태 메시지 끄기
                        rusia.PrintingSystem.ShowPrintStatusDialog = false;
                        rusia.Print();
                        // 라벨 출력 속도 조절
                        Thread.Sleep(100);
                    }
                }
                if (flag == true)
                {
                    cmgt.rollback();//롤백
                    failCount -= successCount;
                    XtraMessageBox.Show($"{failCount}장 출력 실패", "출력실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    XtraMessageBox.Show($"{successCount}장 발행 완료", "출력완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
                RTCommon.ErrRecord(ex.Message);
            }

        }
        /// <summary>
        /// 바코드 발행 코드로 변환. 키 = "gtin", "serial", "rusiaCode"
        /// </summary>
        /// <param name="rawCode"></param>
        /// <returns></returns>
        private Dictionary<string, string> ConvertToGS1BarcodeFormat(string rawCode)
        {
            const char FNC1 = (char)232;
            const char GS = (char)29;

            // 파싱 기준 AI 식별자 위치 찾기
            // 예시: 01(14자리) + 21(~가변) + 91(~가변) + 92(~가변)
            string gtin = rawCode.Substring(2, 14); // 01 다음 14자리
            string serial = "";
            string vkey = "";
            string crypto = "";

            int pos21 = rawCode.IndexOf("21", 16);
            int pos91 = rawCode.IndexOf("91", pos21 + 15);
            int pos92 = rawCode.IndexOf("92", pos91 + 6);

            if (pos21 > -1 && pos91 > -1 && pos92 > -1)
            {
                serial = rawCode.Substring(pos21 + 2, pos91 - (pos21 + 2));
                vkey = rawCode.Substring(pos91 + 2, pos92 - (pos91 + 2));
                crypto = rawCode.Substring(pos92 + 2);
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            // GS 삽입 + 앞에 FNC1 삽입
            string rusiaCode = $"{FNC1}01{gtin}21{serial}{GS}91{vkey}{GS}92{crypto}";
            result.Add("gtin", gtin);
            result.Add("serial", serial);
            result.Add("rusiaCode", rusiaCode);
            return result;
        }
        /// <summary>
        /// 프린터 상태 체크
        /// </summary>
        /// <returns></returns>
        /// 
        private bool WaitForTscPrintComplete()
        {
            try
            {
                for (int i = 0; i < 5; i++) // 최대 5초 대기 (0.5초 * 5)
                {
                    var responseTask = Devices["PRINT"].SendAndWaitResponseAsync("~HS\r\n");
                    if (responseTask.Wait(1000))  // 1초 대기
                    {
                        string result = responseTask.Result;
                        string[] parts = result.Split(',');

                        if (parts.Length >= 8)
                        {
                            string paperOut = parts[1];         // paper out
                            string bufferFull = parts[5];       // buffer full
                            string partialFormat = parts[7];    // partial format

                            if (paperOut == "0" && bufferFull == "0" && partialFormat == "0")
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
                RTCommon.ErrRecord(ex.Message);
                return false;
            }
            return false;
        }

        //private bool WaitForTscPrintComplete()
        //{
        //    try
        //    {
        //        for (int i = 0; i < 5; i++) //최대 5초 대기
        //        {
        //            string result = SendCommandToPrinter("~HS\r\n").Trim();

        //            string[] parts = result.Split(',');

        //            if (parts.Length >= 8)
        //            {
        //                string paperOut = parts[1];         // paper out
        //                string bufferFull = parts[5];       // buffer full
        //                string partialFormat = parts[7];    // partial format

        //                if (paperOut == "0" && bufferFull == "0" && partialFormat == "0")
        //                {
        //                    return true; // 출력 완료 상태
        //                }
        //            }

        //            Thread.Sleep(500); // 0.5초 대기
        //        }
        //        return false; // 타임아웃
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false; // 타임아웃
        //    }
        //}

        //private string SendCommandToPrinter(string command)
        //{
        //    try
        //    {
        //        using (TcpClient client = new TcpClient(RTCommon.PRINT_IP, RTCommon.PRINT_PORT))
        //        using (NetworkStream stream = client.GetStream())
        //        {
        //            byte[] data = Encoding.ASCII.GetBytes(command);
        //            stream.Write(data, 0, data.Length);

        //            byte[] buffer = new byte[1024];
        //            Thread.Sleep(100); // 약간 대기
        //            int bytesRead = stream.Read(buffer, 0, buffer.Length);

        //            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return "";
        //    }

        //}
        /// <summary>
        /// 기록서 검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<XMLParam> param = new List<XMLParam>();
                string order_no = text_order_no.Text.ToString();
                string lot_no = text_lot_no.Text.ToString();
                string qc_status = combo_qc_status.Text.ToString();

                if (string.IsNullOrWhiteSpace(order_no))
                    order_no = "";
                if (string.IsNullOrWhiteSpace(lot_no))
                    lot_no = "";
                if (string.IsNullOrWhiteSpace(qc_status))
                    qc_status = "";

                param.Add(new XMLParam("user_id", RTCommon.USER_ID));
                param.Add(new XMLParam(nameof(order_no), order_no.Trim()));
                param.Add(new XMLParam(nameof(lot_no), lot_no.Trim()));
                param.Add(new XMLParam(nameof(qc_status), qc_status.Trim()));

                DataTable dt = (DataTable)cmgt.RequestDatabase("getAllBCDPRODUCTINFO", CombineMgt.QueryType.SELECT, param, RTCommon.USER_ID);

                Cursor.Current = Cursors.Default;

                if (dt.Rows.Count > 0)
                {
                    //DB 검색 데이터 그리드뷰에 보이기
                    gc_product_info.DataSource = dt;
                }
                else
                {
                    gc_product_info.DataSource = null;
                }

                String[] paramArr = new String[1];
                paramArr[0] = dt.Rows.Count.ToString();
                RTCommon.DispRtnMessage(Common.RTCommon.RTGetMessageParam(RTMessage.MSG_I001, paramArr), false);
                Cursor = System.Windows.Forms.Cursors.Default;
                GridViewOptionSet(); //그리드뷰 옵션
            }
            catch (Exception ex)
            {
                //this.Invoke(new txtLogDelegate(txtLogAppend), new object[] { "DB 처리실패" });
                RusiaCodeManualLabelerForm main = this;
                string logText = $"DB 처리실패";
                main.AppendLog("포장기록서", logText, Color.Red);
                XtraMessageBox.Show(logText, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RTCommon.ErrRecord(ex.Message);
            }
        }
        ////LOG
        //public delegate void txtLogDelegate(string str);
        //public void txtLogAppend(string str)
        //{
        //    if (me_log.Text.Length > 30000) me_log.Text = string.Empty;
        //    string nowTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //    string strText = nowTime + " " + str;
        //    //me_log.AppendText(strText + "\r\n");
        //    me_log.Text += strText + "\r\n";
        //    me_log.SelectionStart = me_log.Text.Length;
        //    me_log.ScrollToCaret();

        //    //20240409 JANG 추가
        //    HistLogFile.writeLogFile(strText + "\r\n");
        //}
        /// <summary>
        /// 그리드뷰 옵션
        /// </summary>
        private void GridViewOptionSet()
        {
            try
            {
                List<DevExpress.XtraGrid.Views.Grid.GridView> conList = FindGvControl(this);

                if (conList.Count <= 0)
                    return;
                foreach (DevExpress.XtraGrid.Views.Grid.GridView view in conList)
                {


                    view.OptionsView.ColumnAutoWidth = false; // 열 너비 자동 조정 해제
                    view.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;  //수평 스크롤바 활성화
                    view.BestFitColumns(); // 내용에 맞게 열 크기 자동 조정

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }

        }
        private List<DevExpress.XtraGrid.Views.Grid.GridView> FindGvControl(Control control)
        {
            List<DevExpress.XtraGrid.Views.Grid.GridView> gridList = new List<DevExpress.XtraGrid.Views.Grid.GridView>();
            try
            {


                if (control.GetType().Name == "GridControl")
                {
                    DevExpress.XtraGrid.GridControl gc = control as DevExpress.XtraGrid.GridControl;
                    DevExpress.XtraGrid.Views.Grid.GridView gv = (DevExpress.XtraGrid.Views.Grid.GridView)gc.Views[0];
                    gridList.Add(gv);
                }

                foreach (Control child in control.Controls)
                {
                    gridList.AddRange(FindGvControl(child)); // 재귀 결과를 누적
                }
                return gridList;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
                return gridList;
            }
            return gridList;
        }
        /// <summary>
        /// 타이머 시작
        /// </summary>
        private void InitConnectionStatusTimer()
        {
            try
            {
                EquipstatusTimer = new System.Windows.Forms.Timer();
                EquipstatusTimer.Interval = 5000; // 5초마다 검사
                EquipstatusTimer.Tick += (s, e) => CheckAllDeviceSocketConnections();
                EquipstatusTimer.Start();
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }
        //private bool InitConnectionStatusTimer()
        //{
        //    bool flag = false;
        //    try
        //    {

        //        flag = OneCeheckConnections("~HS\r\n");
        //        if (flag == false) return flag;
        //        statusTimer = new System.Windows.Forms.Timer();
        //        statusTimer.Interval = 5000; // 5초마다 검사
        //        statusTimer.Tick += (s, e) => CheckAllDeviceSocketConnections("~HS\r\n");
        //        statusTimer.Start();
        //        return flag;
        //    }
        //    catch (Exception ex)
        //    {
        //        RTCommon.ErrRecord(ex.Message);
        //        return flag;
        //    }
        //}
        /// <summary>
        /// 한번 연결 검사
        /// </summary>
        //public bool OneCeheckConnections(string command)
        //{
        //    try
        //    {
        //        using (TcpClient client = new TcpClient(RTCommon.PRINT_IP, RTCommon.PRINT_PORT))
        //        using (NetworkStream stream = client.GetStream())
        //        {
        //            byte[] data = Encoding.ASCII.GetBytes(command);
        //            stream.Write(data, 0, data.Length);

        //            byte[] buffer = new byte[1024];
        //            Thread.Sleep(100); // 약간 대기
        //            int bytesRead = stream.Read(buffer, 0, buffer.Length);

        //            string read = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        //            string[] parts = read.Split(',');

        //            if (parts.Length >= 8)
        //            {
        //                string paperOut = parts[1];         // paper out
        //                string bufferFull = parts[5];       // buffer full
        //                string partialFormat = parts[7];    // partial format

        //                if (paperOut == "0" && bufferFull == "0" && partialFormat == "0")
        //                {
        //                    SetDeviceStatusColor("PRINT", true);
        //                    return true;
        //                }
        //                else
        //                {
        //                    SetDeviceStatusColor("PRINT", false);
        //                    return false;
        //                }
        //            }
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //        MessageBox.Show("프린터 연결에 실패했습니다.\n\n" + ex.Message, "프린터 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //}
        /// <summary>
        /// 반복 연결 검사 장비연결상태 체크
        /// </summary>
        public void CheckAllDeviceSocketConnections()
        {
            try
            {
                List<string> disconnectedDevices = new List<string>();
                string[] deviceIds = { "PRINT" };

                foreach (var deviceId in deviceIds)
                {
                    bool isConnected = false;

                    if (Devices.TryGetValue(deviceId, out var client) && client != null && client.IsConnected)
                    {
                        var socket = client.GetSocket();
                        isConnected = socket != null && !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
                    }

                    // 상태 색상 표시
                    SetDeviceStatusColor(deviceId, isConnected);

                    // 로그 기록
                    if (!isConnected)
                    {
                        disconnectedDevices.Add(deviceId);
                        AppendLog(deviceId, "연결 끊김 감지됨", Color.Red);
                    }
                }

                // 전체 끊긴 장비를 요약해서 로그 출력
                if (disconnectedDevices.Count > 0)
                {
                    string summary = string.Join(", ", disconnectedDevices);
                    AppendLog("SYSTEM", $"[경고] 다음 장비 연결 끊김: {summary}", Color.OrangeRed);
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
                AppendLog("SYSTEM", $"[예외] 장비 연결 확인 오류: {ex.Message}", Color.Red);
            }
        }
        //public void CheckAllDeviceSocketConnections(string command)
        //{

        //    try
        //    {
        //        using (TcpClient client = new TcpClient(RTCommon.PRINT_IP, RTCommon.PRINT_PORT))
        //        using (NetworkStream stream = client.GetStream())
        //        {
        //            byte[] data = Encoding.ASCII.GetBytes(command);
        //            stream.Write(data, 0, data.Length);

        //            byte[] buffer = new byte[1024];
        //            Thread.Sleep(100); // 약간 대기
        //            int bytesRead = stream.Read(buffer, 0, buffer.Length);

        //            string read = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        //            string[] parts = read.Split(',');

        //            if (parts.Length >= 8)
        //            {
        //                string paperOut = parts[1];         // paper out
        //                string bufferFull = parts[5];       // buffer full
        //                string partialFormat = parts[7];    // partial format

        //                if (paperOut == "0" && bufferFull == "0" && partialFormat == "0")
        //                {
        //                    SetDeviceStatusColor("PRINT", true);
        //                }
        //                else
        //                {
        //                    SetDeviceStatusColor("PRINT", false);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        statusTimer.Stop();
        //        MessageBox.Show("프린터 연결에 실패했습니다.\n\n" + ex.Message, "프린터 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //}
        private void RusiaCodeManualLabelerForm_Load(object sender, EventArgs e)
        {
            //프린터 연결 타이머 시작
            InitConnectionStatusTimer();

            GridViewOptionSet(); //그리드뷰 옵션

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<XMLParam> param = new List<XMLParam>();



                param.Add(new XMLParam("user_id", RTCommon.USER_ID));
                DataTable dt = (DataTable)cmgt.RequestDatabase("getOrderSYSCODEITEM_MST", CombineMgt.QueryType.SELECT, param, RTCommon.USER_ID);

                Cursor.Current = Cursors.Default;

                if (dt.Rows.Count > 0)
                {
                    combo_qc_status.Properties.Items.Add("");
                    foreach (DataRow row in dt.Rows)
                        combo_qc_status.Properties.Items.Add(row["ITEM_NAME"].ToString());
                }
                else
                {
                    //gridControl2.DataSource = null;
                }

                String[] paramArr = new String[1];
                paramArr[0] = dt.Rows.Count.ToString();
                RTCommon.DispRtnMessage(Common.RTCommon.RTGetMessageParam(RTMessage.MSG_I001, paramArr), false);
                Cursor = System.Windows.Forms.Cursors.Default;
                GridViewOptionSet(); //그리드뷰 옵션
            }
            catch (Exception ex)
            {
                //this.Invoke(new txtLogDelegate(txtLogAppend), new object[] { "DB 처리실패" });
                RusiaCodeManualLabelerForm main = this;
                string logText = $"DB 처리실패";
                main.AppendLog("포장기록서 상태명 조회", logText, Color.Red);
                RTCommon.ErrRecord(ex.Message);
            }

        }

        private void gv_product_info_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                string order_no = this.gv_product_info.GetFocusedRowCellValue("ORDER_NO").ToString().Replace("^", "\r\n");

                Cursor.Current = Cursors.WaitCursor;
                List<XMLParam> param = new List<XMLParam>();

                param.Add(new XMLParam("user_id", RTCommon.USER_ID));
                param.Add(new XMLParam(nameof(order_no), order_no.Trim()));

                //DataTable dt = (DataTable)cmgt.RequestDatabase("getBCDPRODUCTRES", CombineMgt.QueryType.SELECT, param, RTCommon.USER_ID);
                DataTable dt = (DataTable)cmgt.RequestDatabase("getALLBARCODE_MST", CombineMgt.QueryType.SELECT, param, RTCommon.USER_ID);

                Cursor.Current = Cursors.Default;

                if (dt.Rows.Count > 0)
                {
                    gc_barcode_mst.DataSource = dt;

                    txt_res_order_no.Text = gv_product_info.GetFocusedRowCellValue("ORDER_NO").ToString();
                    txt_res_item_cd.Text = gv_product_info.GetFocusedRowCellValue("ITEM_CD").ToString();
                    txt_res_item_nm.Text = gv_product_info.GetFocusedRowCellValue("ITEM_NM").ToString();
                    txt_res_lot_no.Text = gv_product_info.GetFocusedRowCellValue("LOT_NO").ToString();
                    txt_res_upload_qty.Text = gv_product_info.GetFocusedRowCellValue("UPLOAD_QTY").ToString();
                    txt_res_print_good_qty.Text = gv_product_info.GetFocusedRowCellValue("PRINT_GOOD_QTY").ToString();
                    txt_res_print_fail_qty.Text = gv_product_info.GetFocusedRowCellValue("PRINT_FAIL_QTY").ToString();
                    txt_res_print_cancel_qty.Text = gv_product_info.GetFocusedRowCellValue("PRINT_CANCEL_QTY").ToString();
                    txt_res_expiration_code_qty.Text = gv_product_info.GetFocusedRowCellValue("EXPIRATION_CODE_QTY").ToString();
                    txt_res_inspect_good_qty.Text = gv_product_info.GetFocusedRowCellValue("INSPECT_GOOD_QTY").ToString();
                    txt_res_inspect_fail_qty.Text = gv_product_info.GetFocusedRowCellValue("INSPECT_FAIL_QTY").ToString();
                    txt_res_packing_cancel_qty.Text = gv_product_info.GetFocusedRowCellValue("PACKING_CANCEL_QTY").ToString();


                    //오른쪽 텍스트용 변수값 넣기

                    text_order_no_get.Text = order_no;
                    text_lot_no_get.Text = gv_product_info.GetFocusedRowCellValue("LOT_NO").ToString();
                    text_item_cd_get.Text = gv_product_info.GetFocusedRowCellValue("ITEM_CD").ToString();
                    text_item_name_get.Text = gv_product_info.GetFocusedRowCellValue("ITEM_NM").ToString();
                    text_plan_qty_get.Text = gv_product_info.GetFocusedRowCellValue("UPLOAD_QTY").ToString();
                    text_print_good_qty_get.Text = gv_product_info.GetFocusedRowCellValue("PRINT_GOOD_QTY").ToString();
                }
                else
                {
                    XtraMessageBox.Show("해당 작업지시의 상세항목이 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                String[] paramArr = new String[1];
                paramArr[0] = dt.Rows.Count.ToString();
                RTCommon.DispRtnMessage(Common.RTCommon.RTGetMessageParam(RTMessage.MSG_I001, paramArr), false);
                Cursor = System.Windows.Forms.Cursors.Default;

            }
            catch (Exception ex)
            {
                //this.Invoke(new txtLogDelegate(txtLogAppend), new object[] { "DB 처리실패" });
                RusiaCodeManualLabelerForm main = this;
                string logText = $"DB 처리실패";
                main.AppendLog("포장기록서 상세 조회", logText, Color.Red);
                RTCommon.ErrRecord(ex.Message);
            }
        }

        private void gv_product_info_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gv_product_info.FocusedRowHandle >= 0)
                {
                    gv_product_info_RowClick(null, null);
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }
        /// <summary>
        /// 프린터셋팅 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_print_setting_Click(object sender, EventArgs e)
        {
            try
            {
                //해제버튼 활성화
                btn_print_Unsetting.Enabled = true;

                bool result = TryConnect("PRINT", text_print_ip.Text, int.Parse(text_print_port.Text));
                connectionResults["PRINT"] = result;

                // 상태 색상 적용
                //btn_print_status.BackColor = result ? Color.LightGreen : Color.FromArgb(255, 192, 192);

                // 메인폼 로그 기록
                string logMsg = result ? "프린터 연결 성공" : "프린터 연결 실패";
                Color logColor = result ? Color.Black : Color.Red;

                // 연결 성공 시 INI 저장
                if (result)
                {
                    RTCommon.SetIniValue("EQUIP_SET", "PRINT_IP", text_print_ip.Text);
                    RTCommon.SetIniValue("EQUIP_SET", "PRINT_PORT", text_print_port.Text);
                }
                if (this != null && this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.AppendLog("PRINT", logMsg, logColor);
                    }));
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }

            //RTCommon.PRINT_IP = text_print_ip.Text.ToString().Trim();
            //RTCommon.PRINT_PORT = int.Parse(text_print_port.Text.ToString().Trim());

            ////bool result = TryConnect("PRINT", text_print_ip.Text.ToString().Trim(), int.Parse(text_print_port.Text.ToString().Trim()));
            ////connectionResults["PRINT"] = result;

            ////타이머 시작
            //bool result = InitConnectionStatusTimer();

            //// 메시지 + 로그
            //if (result)
            //    XtraMessageBox.Show($"PRINT 연결 성공", "연결 상태", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    XtraMessageBox.Show($"PRINT 연결 실패", "연결 상태", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //// 메인폼 로그 기록
            //string logMsg = result ? "프린터 연결 성공" : "프린터 연결 실패";
            //Color logColor = result ? Color.Black : Color.Red;
            //if (this != null && this.IsHandleCreated && !this.IsDisposed)
            //{
            //    this.BeginInvoke(new Action(() =>
            //    {
            //        this.AppendLog("PRINT", logMsg, logColor);
            //    }));
            //}
        }
        private bool TryConnect(string deviceId, string ip, int port)
        {

            var devices = this.Devices;

            // 기존 장비가 있을 경우
            if (devices.ContainsKey(deviceId))
            {
                var existingClient = devices[deviceId];
                var socket = existingClient.GetSocket();
                bool isStillConnected = false;

                try
                {
                    isStillConnected = socket != null && !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
                }
                catch
                {
                    isStillConnected = false;
                }

                if (isStillConnected)
                {
                    // 메시지 + 로그
                    XtraMessageBox.Show($"{deviceId}는 이미 연결되어 있습니다.", "연결 상태", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (this != null && this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.AppendLog(deviceId, "이미 연결되어 있음", Color.Orange);
                        }));
                    }

                    return true;
                }
                else
                {
                    existingClient.Disconnect();
                    devices.Remove(deviceId);
                    if (this != null && this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.AppendLog(deviceId, "기존 연결 끊어짐 → 재연결 시도", Color.DarkOrange);
                        }));
                    }
                }
            }

            // 새로 연결 시도
            var client = new DeviceClient(deviceId, ip, port);
            if (client.Connect())
            {
                client.OnDataReceived += data =>
                {
                    if (this != null && this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.AppendLog(deviceId, data, Color.Black);
                        }));
                    }
                };

                devices[deviceId] = client;

                // 메시지 + 로그
                XtraMessageBox.Show($"{deviceId} 연결 성공", "연결 상태", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.BeginInvoke(new Action(() =>
                {
                    this.AppendLog(deviceId, "연결 성공", Color.Black);
                }));

                return true;
            }
            else
            {
                // 메시지 + 로그
                XtraMessageBox.Show($"{deviceId} 연결 실패", "연결 상태", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (this != null && this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.AppendLog(deviceId, "연결 실패", Color.Red);
                    }));
                }

                return false;
            }
        }
        // 공통 버튼 색상 변경 함수
        public void SetDeviceStatusColor(string deviceId, bool isConnected)
        {
            try
            {
                Button targetButton = null;
                Color statusColor = isConnected ? Color.LightGreen : Color.FromArgb(255, 192, 192);
                string statusText = isConnected ? "연결성공" : "연결실패";


                switch (deviceId)
                {
                    //case "PLC":
                    //    targetButton = btn_plc_status;
                    //    break;
                    case "PRINT":
                        targetButton = btn_print_status;
                        break;
                        //case "INSPECTION":
                        //    targetButton = btn_inspection_status;
                        //    break;
                        //case "ATTACH":
                        //    targetButton = btn_attach_status;
                        //    break;
                }

                if (targetButton != null)
                {
                    targetButton.BackColor = statusColor;
                    targetButton.ForeColor = Color.White;
                    targetButton.Text = statusText;
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }

        public class LogEntry
        {
            public string Text { get; set; }
            public Color TextColor { get; set; } = Color.Black;

            public override string ToString()
            {
                return Text;
            }
        }
        const int MaxLogCount = 1000;

        private delegate void LogDelegate(string deviceId, string message, Color? color);
        /// <summary>
        /// 리스트 박스에 로그 남기기
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="message"></param>
        /// <param name="color"></param>
        public void AppendLog(string deviceId, string message, Color? color = null)
        {
            try
            {
                if (!this.IsHandleCreated || this.IsDisposed)
                    return;

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new LogDelegate(AppendLog), deviceId, message, color);
                    return;
                }

                if (list_log.Items.Count >= MaxLogCount)
                    list_log.Items.Clear();

                // [시간] + [장비] + 메시지를 화면에 표시
                string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                var entry = new LogEntry
                {
                    Text = $"[{timestamp}] [{deviceId}] {message}",
                    TextColor = color ?? Color.Black
                };

                list_log.Items.Add(entry);
                list_log.TopIndex = list_log.Items.Count - 1;
                list_log.SelectedIndex = list_log.Items.Count - 1;

                // 로그 파일에는 [시간] 없이 저장
                string plainLog = $"[{deviceId}] {message}";
                HistLogFile.writeLogFile(plainLog + "\r\n");
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }

        private void list_log_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0 || e.Index >= list_log.Items.Count) return;

                bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

                // 배경 색상 설정
                Color backColor = isSelected ? Color.DodgerBlue : e.BackColor;
                e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);

                // 로그 항목 텍스트 색상 설정
                Color textColor = Color.Black;
                string text = list_log.Items[e.Index].ToString();

                if (list_log.Items[e.Index] is LogEntry logEntry)
                {
                    text = logEntry.Text;
                    textColor = isSelected ? Color.White : logEntry.TextColor;
                }
                else
                {
                    textColor = isSelected ? Color.White : Color.Black;
                }

                // 텍스트 출력
                using (Brush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds);
                }

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }

        private void list_log_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    int index = list_log.IndexFromPoint(e.Location);
                    if (index != ListBox.NoMatches)
                    {
                        list_log.SelectedIndex = index;  // 우클릭 위치 항목 선택
                    }
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }

        private void list_log_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    if (list_log.SelectedItem != null)
                    {
                        string text = list_log.SelectedItem.ToString();
                        Clipboard.SetText(text);
                    }

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }
        /// <summary>
        /// 작업시작 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                btn_end.Enabled = true;
                btn_start.Enabled = false;
                btn_search.Enabled = false;
                gc_product_info.Enabled = false;
                btn_print_setting.Enabled = true;
                btn_print_Unsetting.Enabled = true;
                btn_label_print.Enabled = true;
                text_print_ip.BackColor = Color.AliceBlue;
                text_print_port.BackColor = Color.AliceBlue;
                text_print_num.BackColor = Color.AliceBlue;
                text_print_ip.ReadOnly = false;
                text_print_port.ReadOnly = false;
                text_print_num.ReadOnly = false;
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }
        }
        /// <summary>
        /// 작업종료 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_end_Click(object sender, EventArgs e)
        {
            try
            {
                //statusTimer.Stop();
                //SetDeviceStatusColor("PRINT", false);
                // 메인폼 로그 기록
                //string logMsg = "프린터 연결 해제";
                //Color logColor = Color.Black;
                //if (this != null && this.IsHandleCreated && !this.IsDisposed)
                //{
                //    this.BeginInvoke(new Action(() =>
                //    {
                //        this.AppendLog("PRINT", logMsg, logColor);
                //    }));
                //}

                btn_end.Enabled = false;
                btn_start.Enabled = true;
                btn_search.Enabled = true;
                gc_product_info.Enabled = true;
                btn_print_setting.Enabled = false;
                btn_print_Unsetting.Enabled = false;
                btn_label_print.Enabled = false;
                text_print_ip.BackColor = Color.WhiteSmoke;
                text_print_port.BackColor = Color.WhiteSmoke;
                text_print_num.BackColor = Color.WhiteSmoke;
                text_print_ip.ReadOnly = true;
                text_print_port.ReadOnly = true;
                text_print_num.ReadOnly = true;
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }

        }

        private void gv_barcode_mst_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return; // 그룹/헤더 등 무시

                GridView view = sender as GridView;
                string print_yn = view.GetRowCellValue(e.RowHandle, "PRINT_YN")?.ToString();
                //string okValue = view.GetRowCellValue(e.RowHandle, "RUSIACODE_OK")?.ToString();
                //string noValue = view.GetRowCellValue(e.RowHandle, "RUSIACODE_NO")?.ToString();

                if (print_yn == "Y")
                {
                    e.Appearance.BackColor = Color.Orange;   // 배경색
                    e.Appearance.ForeColor = Color.Black;    // 글자색 (선택)
                }
                //if (okValue == "Y")
                //{
                //    e.Appearance.BackColor = Color.LightGreen;   // 배경색
                //    e.Appearance.ForeColor = Color.Black;       // 글자색 (선택)
                //}
                //if (noValue == "Y")
                //{
                //    e.Appearance.BackColor = Color.Red;   // 배경색
                //    e.Appearance.ForeColor = Color.Black;       // 글자색 (선택)
                //}
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }

        }

        private void btn_print_Unsetting_Click(object sender, EventArgs e)
        {
            try
            {
                var devices = this.Devices;
                devices["PRINT"].Disconnect();

                string logMsg = "프린터 연결 해제";
                Color logColor = Color.Black;
                if (this != null && this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.AppendLog("PRINT", logMsg, logColor);
                    }));
                }
            }
            catch (Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }

        }

        private void gv_barcode_mst_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return; // 그룹/헤더 등 무시
                // 특정 행 스타일 다시 적용
                //gv_barcode_mst.InvalidateRow(e.RowHandle); // 값 무효화 (스타일 포함)
                gv_barcode_mst.PostEditor();               // 에디터 값 확정
                gv_barcode_mst.UpdateCurrentRow();         // 바뀐 값 반영 강제 커밋
                gv_barcode_mst.RefreshRow(e.RowHandle);    // 다시 그리기 요청
            }
            catch(Exception ex)
            {
                RTCommon.ErrRecord(ex.Message);
            }

        }
    }
}
