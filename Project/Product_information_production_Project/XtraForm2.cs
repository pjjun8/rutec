using DevExpress.PivotGrid.QueryMode;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_information_production_Project
{
    public partial class XtraForm2 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// DB접속을 위한 전역지정
        /// </summary>
        public static string uid = "sa";
        public static string password = "fnxpr2020@)@)";
        public static string database = "SANGWON";
        public static string server = "61.100.180.71,14337";
        string connStr = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt = new DataTable();

        /// <summary>
        /// 통신을 위한 전역지정
        /// </summary>
        private TcpClient client;
        private NetworkStream stream;
        private CancellationTokenSource cts;
        private bool isConnected = false;
        private DateTime time = DateTime.Now;

        /// <summary>
        /// 스캐너 모드 변경을 위한 전역지정
        /// </summary>
        private bool toggleSwitch = false;
        public XtraForm2()
        {
            InitializeComponent();
        }
        /// <summary>
        /// UI 스레드에서 textBox 업데이트
        /// </summary>
        /// <param name="messge"></param>
        private void UpdateTextBox(string messge)
        {
            if (testTextEdit.InvokeRequired)
            {
                testTextEdit.Invoke(new Action<string>(UpdateTextBox), messge);
            }
            else
            {
                testTextEdit.Text += messge + Environment.NewLine;
            }
        }
        
        /// <summary>
        /// 서버에 메시지 송신(스캐너 실행 명령어 송신)
        /// </summary>
        private void SendMessages(string message)
        {
            try
            {
                if (!isConnected)
                {
                    UpdateTextBox($"{time.ToString("HH:mm")} 서버에 연결되어 있지 않습니다.");
                    return;
                }

                //스캐너 동작 명령어 송신
                //string message = "LON<CR>";
                if (string.IsNullOrWhiteSpace(message))
                {
                    UpdateTextBox($"{time.ToString("HH:mm")} 전송할 메시지를 입력하세요.");
                    return;
                }

                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                UpdateTextBox($"{time.ToString("HH:mm")} 송신: {message}");
                //SendTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 서버에서 메시지 수신(스캐너 종료 명령어 송신)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task ReceiveMessages(CancellationToken token)
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (!token.IsCancellationRequested)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else if (!(bytesRead == 0))
                    {   //스캐너로부터 문장 받으면 스캐너 종료 명령어 전송
                        //스캐너 종료 명령어 LOFF<CR> 전송, <CR> -> \r 캐리지리턴
                        //SendMessages("LOFF\r");

                        string message = Encoding.Default.GetString(buffer, 0, bytesRead);
                        // 정규식으로 제어 문자 제거
                        string cleanedMessage = Regex.Replace(message, @"[\u0000-\u001F]", "");
                        UpdateTextBox($"{time.ToString("HH:mm")} 수신: {cleanedMessage}");
                        //바코드 검수
                        CheckBarCode(cleanedMessage);
                        //DataTableReset();                
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                {
                    UpdateTextBox($"{time.ToString("HH:mm")} 서버 통신 오류: {ex.Message}");
                }
            }
            finally
            {
                isConnected = false;
                UpdateTextBox($"{time.ToString("HH: mm")} 서버와 연결 종료.");
                //ServerConnectLabel2.Text = "끊김";
            }
        }
        /// <summary>
        /// 바코드 검수 하고 라벨상태 업데이트
        /// </summary>
        /// <param name="barCode"></param>
        private void CheckBarCode(string barCode)
        {
            try
            {
                int num = 0;
                // 2. 모든 행을 순회하면서 셀 값 가져오기
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    string value = gridView1.GetRowCellValue(i, "barCodeField").ToString();
                    if (barCode.Equals(value))
                    {
                        string status = gridView1.GetRowCellValue(i, "labelStatusField").ToString();
                        if(status == "검수완료")
                        {
                            if(toggleSwitch)
                            {
                                string sql = $"update PInformation set labelStatus = @labelStatus where barCode = '{value}'";

                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@labelStatus", "라벨발행");

                                    num = cmd.ExecuteNonQuery();
                                    if (num != 0)
                                    {
                                        //MessageBox.Show("커밋 완료!");
                                        UpdateGridView();
                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show($"커밋 실패! {num}");
                                    }
                                }
                                break;
                            }
                            else
                            {
                                MessageBox.Show("이미 검수완료 상태입니다.");
                                break;
                            }
                        }
                        else
                        {
                            if(toggleSwitch)
                            {
                                MessageBox.Show("이미 라벨발행 상태입니다.");
                                break;
                            }
                            else
                            {
                                string sql = $"update PInformation set labelStatus = @labelStatus where barCode = '{value}'";

                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@labelStatus", "검수완료");

                                    num = cmd.ExecuteNonQuery();
                                    if (num != 0)
                                    {
                                        //MessageBox.Show("커밋 완료!");
                                        UpdateGridView();
                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show($"커밋 실패! {num}");
                                    }
                                }
                                break;
                            }
                        }
                    }
                    if ((num == 0) && (i + 1 == gridView1.RowCount))
                    {
                        MessageBox.Show("일치하는 바코드가 그리드뷰에 없습니다.");
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 그리드뷰 invoke
        /// </summary>
        /// <param name="action"></param>
        /////////////////////////////////test//////////////
        private void UpdateUI(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action);  // UI 스레드에서 실행
            }
            else
            {
                action();
            }
        }
        
        //UI 컨트롤을 변경하는 코드
        private void UpdateGridView()
        {
            UpdateUI(() =>
            {
                // UI 컨트롤 변경 코드 (그리드뷰 데이터 변경)
                DataTableReset();
                if (ProductionTimeStartDateEdit.EditValue is null && ProductionTimeEndDateEdit.EditValue is null)
                {
                    SelectTable("전체조회");
                }
                else
                {
                    SelectTable("날짜조회");
                }
                
            });
        }
        ///////////////////////////////////////////////////
        /// <summary>
        /// dataTable 초기화 함수
        /// </summary>
        private void DataTableReset()
        {
            try
            {
                dt.Clear();
                dt.Columns.Add("productCodeField");
                dt.Columns.Add("productDateField");
                dt.Columns.Add("expiryField");
                dt.Columns.Add("productionNumberField");
                dt.Columns.Add("lotNumField");
                dt.Columns.Add("barCodeField");
                dt.Columns.Add("productionTimeField"); 
                dt.Columns.Add("labelStatusField");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// DB테이블 조회
        /// </summary>
        private void SelectTable(string value)
        {
            try
            {
                string sql = "";
                if (value == "전체조회")
                {
                    sql = "select * from PInformation";   // * 수정
                }
                else if (value == "날짜조회")
                {
                    sql = "select * from [SANGWON].[dbo].[PInformation] where " +
                          $"productionTime between '{ProductionTimeStartDateEdit.Text}'/*변수*/ and dateadd(day, 1, '{ProductionTimeEndDateEdit.Text}')/*변수*/";   // * 수정

                }
                
                
                cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    string code, date, exDate, productionNumber, lotNum, barCode, productionTime, labelStatus;
                    while (reader.Read())
                    {
                        code = reader["productCode"].ToString();
                        date = reader["productionDate"].ToString();
                        exDate = reader["expiryDate"].ToString();
                        productionNumber = reader["productionNumber"].ToString();
                        lotNum = reader["lotNum"].ToString();
                        barCode = reader["barCode"].ToString();
                        productionTime = reader["productionTime"].ToString();
                        labelStatus = reader["labelStatus"].ToString();
                        
                        dt.Rows.Add(code, date, exDate, productionNumber, lotNum, barCode, productionTime, labelStatus);
                    }
                    gridControl1.DataSource = dt;
                }
                AutoGridViewCellSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 그리드뷰 셀 크기 조절
        /// </summary>
        private void AutoGridViewCellSize()
        {
            gridView1.OptionsView.ColumnAutoWidth = false; // 열 너비 자동 조정 해제
            gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;  //수평 스크롤바 활성화
            gridView1.BestFitColumns(); // 내용에 맞게 열 크기 자동 조정
        }
        private void XtraForm2_Load(object sender, EventArgs e)
        {
            
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                DataTableReset();
                ProductionTimeStartDateEdit.Text = $"{DateTime.Now.ToString("yyyy/MM/dd")}";
                ProductionTimeEndDateEdit.Text = $"{DateTime.Now.ToString("yyyy/MM/dd")}";
                SelectTable("날짜조회");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 선택한 날짜에서 조회 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSelectionButton_Click(object sender, EventArgs e)
        {
            DataTableReset();
            SelectTable("날짜조회");
        }

        /// <summary>
        /// 그리드뷰 엑셀로뽑기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.ExportToXls("XlsTest.xls");
        }
        /// <summary>
        /// 서버 연결 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TCPConnectButton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                UpdateTextBox($"{time.ToString("HH:mm")} 이미 서버 연결되어 있습니다.");
                return;
            }

            string serverIp = IPTextEdit.Text;
            int port = int.Parse(PortTextEdit.Text);

            try
            {
                client = new TcpClient();
                await client.ConnectAsync(serverIp, port);
                stream = client.GetStream();
                cts = new CancellationTokenSource();
                isConnected = true;
                UpdateTextBox($"{time.ToString("HH:mm")} 서버 연결됨: {serverIp}:{port}");
                //ServerConnectLabel2.Text = "연결";

                // 서버 메시지 수신 스레드 실행
                _ = Task.Run(() => ReceiveMessages(cts.Token));
            }
            catch (Exception ex)
            {
                UpdateTextBox($"{time.ToString("HH:mm")} 연결 실패: 스캐너와의 연결을 확인해 주세요.");//{ex.Message}
            }
        }
        /// <summary>
        /// 메세지 송신 버튼(스캐너 실행)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScannerOnButton_Click(object sender, EventArgs e)
        {
            //스캐너 실행 명령어"LON<CR>" 서버로 전송, <CR> -> \r 캐리지리턴
            SendMessages("LON\r");
        }
        /// <summary>
        /// 서버(스캐너) 연결해제 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TCPDisConnectButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                UpdateTextBox($"{time.ToString("HH: mm")} 서버에 연결되어 있지 않습니다.");
                return;
            }
            //서버(스캐너)연결해제시 스캐너 종료
            SendMessages("LOFF\r");
            Thread.Sleep(10);

            isConnected = false;
            cts.Cancel();
            client.Close();
            UpdateTextBox($"{time.ToString("HH: mm")}서버 연결 해제됨.");
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            //토글스위치가 ON일때 라벨상태 초기화(라벨발행상태로) 시키기
            if (toggleSwitch1.IsOn)
            {
                toggleSwitch = true;
                UpdateTextBox($"{time.ToString("HH: mm")} 검수취소 모드.");
            }
            else
            {
                toggleSwitch = false;
                UpdateTextBox($"{time.ToString("HH: mm")} 검수 모드.");
            }
        }

        private void HandScannerTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void HandScannerTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                CheckBarCode(HandScannerTextEdit.Text);
                HandScannerTextEdit.Text = "";
            }
        }
    }
}