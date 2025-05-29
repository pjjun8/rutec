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

namespace TCPClientTest01_Winform
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private CancellationTokenSource cts;
        private bool isConnected = false;
        private DateTime time = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
        }

        // UI 스레드에서 textBox 업데이트
        private void UpdateTextBox(string messge)
        {
            if (textBox3.InvokeRequired)
            {
                textBox3.Invoke(new Action<string>(UpdateTextBox), messge);
            }
            else
            {
                textBox3.AppendText(messge + Environment.NewLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //서버 연결 버튼
        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                UpdateTextBox($"{time.ToString("HH:mm")} 이미 서버 연결되어 있습니다.");
                return;
            }

            string serverIp = ServerIPtextBox.Text;
            int port = int.Parse(ServerPortTextBox.Text);

            try
            {
                client = new TcpClient();
                await client.ConnectAsync(serverIp, port);
                stream = client.GetStream();
                cts = new CancellationTokenSource();
                isConnected = true;
                UpdateTextBox($"{time.ToString("HH:mm")} 서버 연결됨: {serverIp}:{port}");
                ServerConnectLabel2.Text = "연결";

                // 서버 메시지 수신 스레드 실행
                _ = Task.Run(() => ReceiveMessages(cts.Token));
            }
            catch (Exception ex)
            {
                UpdateTextBox($"{time.ToString("HH:mm")} 연결 실패: {ex.Message}");
            }
        }
        // 서버 연결해제 버튼
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                UpdateTextBox($"{time.ToString("HH: mm")} 서버에 연결되어 있지 않습니다.");
                return;
            }

            isConnected = false;
            cts.Cancel();
            client.Close();
            UpdateTextBox($"{time.ToString("HH: mm")}서버 연결 해제됨.");
            ServerConnectLabel2.Text = "끊김";
        }
        // 서버에서 메시지 수신
        private async Task ReceiveMessages(CancellationToken token)
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (!token.IsCancellationRequested)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    if (bytesRead == 0) break;

                    string message = Encoding.Default.GetString(buffer, 0, bytesRead);
                    UpdateTextBox($"{time.ToString("HH:mm")} 수신: {message}");
                }
            }
            catch (Exception ex)
            {
                UpdateTextBox($"{time.ToString("HH:mm")} 서버 통신 오류: {ex.Message}");
            }
            finally
            {
                isConnected = false;
                UpdateTextBox($"{time.ToString("HH: mm")} 서버와 연결 종료.");
                ServerConnectLabel2.Text = "끊김";
            }
        }

        private void SendTextButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                UpdateTextBox($"{time.ToString("HH:mm")} 서버에 연결되어 있지 않습니다.");
                return;
            }

            string message = SendTextBox.Text;
            if (string.IsNullOrWhiteSpace(message))
            {
                UpdateTextBox($"{time.ToString("HH:mm")} 전송할 메시지를 입력하세요.");
                return;
            }

            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            UpdateTextBox($"{time.ToString("HH:mm")} 송신: {message}");
            SendTextBox.Clear();
        }
    }
}
