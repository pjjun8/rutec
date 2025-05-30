using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace EXOLUBE.Pop
{
    public class DeviceClient
    {
        private Socket socket;
        private Thread receiveThread;
        private bool isRunning;

        public string DeviceId { get; private set; }
        private string ip;
        private int port;


        public event Action<string> OnDataReceived;  // 문자열 (스캐너 등)
        public event Action<byte[]> OnBinaryReceived;   // 바이트 수신 (PLC 등)

        // DeviceId로부터 자동 판단
        public bool IsScanner => !DeviceId.Equals("PLC", StringComparison.OrdinalIgnoreCase);

        public bool IsConnected => socket != null && socket.Connected;

        public string Ip => ip;
        public int Port => port;

        public Socket GetSocket()
        {
            return socket;
        }

        public DeviceClient(string deviceId, string ip, int port)
        {
            DeviceId = deviceId;
            this.ip = ip;
            this.port = port;
        }

        public bool Connect()
        {
            try
            {
                if (IsConnected) return true;

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(IPAddress.Parse(ip), port);
                isRunning = true;

                receiveThread = new Thread(ReceiveLoop) { IsBackground = true };
                receiveThread.Start();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ReceiveLoop()
        {
            //try
            //{
            //    byte[] buffer = new byte[1024];
            //    while (isRunning)
            //    {
            //        int len = socket.Receive(buffer);
            //        if (len > 0)
            //        {
            //            string data = Encoding.UTF8.GetString(buffer, 0, len);
            //            OnDataReceived?.Invoke(data);
            //        }
            //        Thread.Sleep(10);
            //    }
            //}
            //catch
            //{
            //    // 연결 끊김 등
            //    Disconnect();
            //}
            try
            {
                byte[] buffer = new byte[1024];
                while (isRunning)
                {
                    int len = socket.Receive(buffer);
                    if (len > 0)
                    {
                        byte[] received = new byte[len];
                        Array.Copy(buffer, 0, received, 0, len);

                        if (IsScanner)
                        {
                            string text = Encoding.UTF8.GetString(received);
                            OnDataReceived?.Invoke(text);
                        }
                        else
                        {
                            OnBinaryReceived?.Invoke(received);
                        }

                        //20250529_PSW  대기 중인 응답 처리
                        string data = Encoding.UTF8.GetString(received, 0, len);
                        responseTcs?.TrySetResult(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DeviceId}] 수신 오류: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }

        public void Send(string msg)
        {
            if (IsConnected)
            {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                socket.Send(data);
            }
        }
        ////20250529_PSW 응답처리 대기용 Task 추가
        private TaskCompletionSource<string> responseTcs;
        public Task<string> SendAndWaitResponseAsync(string message, int timeoutMs = 2000)
        {
            responseTcs = new TaskCompletionSource<string>();

            Send(message); // ~HS\r\n

            var cancellation = new CancellationTokenSource(timeoutMs);
            cancellation.Token.Register(() => responseTcs.TrySetCanceled(), useSynchronizationContext: false);

            return responseTcs.Task;
        }

        public void Disconnect()
        {
            isRunning = false;
            try { socket?.Shutdown(SocketShutdown.Both); } catch { }
            socket?.Close();
            socket = null;
        }
        public bool IsPhysicallyConnected()
        {
            try
            {
                return socket != null && !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch
            {
                return false;
            }
        }
        public void ClearReceiveHandlers()
        {
            OnDataReceived = null; // 메인폼에서 안전하게 초기화할 수 있도록 제공
        }
    }
}
