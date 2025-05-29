using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EXOLUBE.Pop
{
    public class DeviceClient
    {
        public string DeviceId { get; private set; }
        private string ip;
        private int port;
        private Socket socket;
        private Thread receiveThread;
        private bool isRunning;

        public event Action<string> OnDataReceived;
        public bool IsConnected => socket != null && socket.Connected;

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
            try
            {
                byte[] buffer = new byte[1024];
                while (isRunning)
                {
                    int len = socket.Receive(buffer);
                    if (len > 0)
                    {
                        string data = Encoding.UTF8.GetString(buffer, 0, len);
                        OnDataReceived?.Invoke(data);
                    }
                    Thread.Sleep(10);
                }
            }
            catch
            {
                // 연결 끊김 등
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

    }
}
