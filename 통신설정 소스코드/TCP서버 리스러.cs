using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace TCPServerTest01_Winform
{
    public partial class Form1 : Form
    {
        // UI 업데이트를 위한 델리게이트
        delegate void SafeCallDelegate(string text);
        private TcpListener server; // TCP 리스너
        private bool isRunning = false; // 서버 실행 상태
        private CancellationTokenSource cts; // 서버 종료용 토큰
        private List<TcpClient> clients = new List<TcpClient>(); // 연결된 클라이언트 목록
        private int count = 0;
        private DateTime time = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
        }

        // UI 스레드에서 textBox6 업데이트
        private void UpdateTextBox(string text)
        {
            if (textBox3.InvokeRequired)
            {
                textBox3.Invoke(new SafeCallDelegate(UpdateTextBox), text);
            }
            else
            {
                textBox3.AppendText(text + Environment.NewLine); // 줄바꿈 추가하여 가독성 향상
            }
        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                textBox1.AppendText($"서버가 이미 실행 중입니다.\r\n");
                return;
            }

            int port = int.Parse(ServerPortTextBox.Text);
            server = new TcpListener(IPAddress.Any, port);
            cts = new CancellationTokenSource();
            isRunning = true;

            ServerStateLabel2.Text = "가동";
            textBox1.AppendText($"서버 시작: 포트 {port}에서 대기 중... {time.ToString("HH:mm")}\r\n");
            await Task.Run(() => StartServer(cts.Token));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                textBox1.AppendText($"서버가 실행 중이 아닙니다.\r\n");
                return;
            }

            ServerStateLabel2.Text = "정지";
            count = 0;
            ClientNumTextBox.Text = $"{count}";
            isRunning = false;
            cts.Cancel();
            server.Stop();

            lock (clients)
            {
                foreach (var client in clients)
                {
                    client.Close();
                }
                clients.Clear();
            }

            textBox1.AppendText($"서버 종료됨.\r\n");
        }
        // 🔹 TCP 서버 실행
        private async Task StartServer(CancellationToken token)
        {
            try
            {
                server.Start();

                while (!token.IsCancellationRequested)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    lock (clients)
                    {
                        clients.Add(client);
                    }

                    textBox1.AppendText($"클라이언트 연결됨 {time.ToString("HH:mm")}: {((IPEndPoint)client.Client.RemoteEndPoint).ToString()}\r\n");
                    count++;
                    ClientNumTextBox.Text = $"{count}";
                    Task.Run(() => HandleClient(client));
                }
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                {
                    textBox1.AppendText($"서버 오류: {ex.Message}\r\n");
                }
            }
        }
        // 🔹 클라이언트 메시지 처리
        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    textBox1.AppendText($"수신 {time.ToString("HH:mm")}: {message}\r\n");
                }
            }
            catch (Exception ex)
            {
                textBox1.AppendText($"클라이언트 오류: {ex.Message}\r\n");
            }
            finally
            {
                lock (clients)
                {
                    clients.Remove(client);
                }
                client.Close();
                if(count > 0)
                {
                    count--;
                }
                ClientNumTextBox.Text = $"{count}";
                textBox1.AppendText($"클라이언트 연결 종료 {time.ToString("HH:mm")}\r\n");

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 🔹 서버에서 클라이언트로 메시지 전송
        private void button3_Click(object sender, EventArgs e)
        {
            string message = textBox3.Text;
            if (string.IsNullOrWhiteSpace(message))
            {
                textBox1.AppendText($"전송할 메시지를 입력하세요.\r\n");
                return;
            }

            SendMessageToClients(message);
            textBox3.Clear();
        }

        // 🔹 연결된 모든 클라이언트에게 메시지 전송
        private void SendMessageToClients(string message)
        {
            byte[] responseData = Encoding.Default.GetBytes(message);

            lock (clients)
            {
                foreach (var client in clients)
                {
                    if (client.Connected)
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(responseData, 0, responseData.Length);
                    }
                }
            }

            textBox1.AppendText($"송신 {time.ToString("HH:mm")}: {message}\r\n");
        }

    }
}
