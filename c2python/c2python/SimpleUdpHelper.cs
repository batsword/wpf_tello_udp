/*

* ==============================================================================
*
* Filename: UdpHelper
* Description: 
*
* Version: 1.0
* Created: 2018.10.27
* Compiler: Visual Studio 2015
*
* Author: zyj
* Company: nined
*
* ==============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace c2python
{
    public class SimpleUdpHelper
    {
        private const int dataLength = 102400;
        //服务器端Socket对象
        private Socket serverSocket;
        //接收数据的字符数组
        private byte[] receiveData = new byte[dataLength];

        public event Action<byte[]> OnSocketReceive;

        /// <summary>
        /// 如需第一次往指定ip发送
        /// </summary>
        private const string firstSendIP = "127.0.0.1";
        private const int firstSendPort = 20000;

        private const int MyPort = 19999;

        //客户端的IP和端口，端口 0 表示任意端口
        private static IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);
        //实例化客户端 终点
        private EndPoint epSender = (EndPoint)client;

        public void Start(int port = MyPort)
        {
            //实例化服务器端Socket对象
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //服务器端的IP和端口，IPAddress.Any实际是：0.0.0.0，表示任意，基本上表示本机IP
            IPEndPoint server = new IPEndPoint(IPAddress.Any, port);
            //IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            uint IOC_IN = 0x80000000;
            uint IOC_VENDOR = 0x18000000;
            uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
            serverSocket.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);

            //Socket对象跟服务器端的IP和端口绑定
            serverSocket.Bind(server);
            //客户端的IP和端口，端口 0 表示任意端口
            IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);
            //实例化客户端 终点
            EndPoint epSender = (EndPoint)clients;
            //开始异步接收消息  接收后，epSender存储的是发送方的IP和端口
            serverSocket.BeginReceiveFrom(receiveData, 0, receiveData.Length, SocketFlags.None,
                ref epSender, new AsyncCallback(ReceiveData), epSender);
            Console.WriteLine("Listening...");
            Console.ReadLine();
        }



        private void ReceiveData(IAsyncResult iar)
        {

            //结束异步接收消息  recv 表示接收到的字符数
            int recv = serverSocket.EndReceiveFrom(iar, ref epSender);
            ////将接收到的数据打印出来，发送方采用什么编码方式，此处就采用什么编码方式 转换成字符串
            //Console.WriteLine("Client:" + Encoding.ASCII.GetString(receiveData, 0, recv));
            ////定义要发送回客户端的消息，采用ASCII编码，
            ////如果要发送汉字或其他特殊符号，可以采用UTF-8            
            //byte[] sendData = Encoding.ASCII.GetBytes("hello");
            ////开始异步发送消息  epSender是上次接收消息时的客户端IP和端口信息
            //serverSocket.BeginSendTo(sendData, 0, sendData.Length, SocketFlags.None,
            //    epSender, new AsyncCallback(SendData), epSender);

            byte[] bytes = new byte[recv];
            Array.Copy(receiveData, 0, bytes, 0, bytes.Length);
            OnSocketReceive(bytes);

            //重新实例化接收数据字节数组
            receiveData = new byte[1024];
            //开始异步接收消息，此处的委托函数是这个函数本身，递归
            serverSocket.BeginReceiveFrom(receiveData, 0, receiveData.Length, SocketFlags.None,
                ref epSender, new AsyncCallback(ReceiveData), epSender);
        }

        public void Send(byte[] sendData)
        {
            serverSocket.BeginSendTo(sendData, 0, sendData.Length, SocketFlags.None,
                epSender, new AsyncCallback(SendData), epSender);
        }

        public void SendTo(byte[] sendData, string ip = firstSendIP, int port = firstSendPort)
        {
            //客户端的IP和端口，端口 0 表示任意端口
            //string strHostName = Dns.GetHostName();
            //IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            //string LocalHostIP = ipEntry.AddressList[0].ToString();
            IPEndPoint epSenderx = new IPEndPoint(IPAddress.Parse(ip), port);

            //EndPoint epSenderx = (EndPoint)client;
            //serverSocket.BeginSendTo(sendData, 0, sendData.Length, SocketFlags.None,
            //    epSenderx, new AsyncCallback(SendData), epSenderx);
            serverSocket.SendTo(sendData, sendData.Length, SocketFlags.None, epSenderx);
        }

        private void SendData(IAsyncResult iar)
        {
            serverSocket.EndSend(iar);
        }

        /// <summary>
        /// 累加校验和 2位
        /// </summary>
        /// <param name="memorySpage">需要校验的数据</param>
        /// <returns>返回校验和结果</returns>
        public Int16 Fill(byte[] memorySpage)
        {
            int num = 0;
            for (int i = 0; i < memorySpage.Length; i++)
            {
                num = (num + memorySpage[i]) % 0xffff;
            }
            //实际上num 这里已经是结果了，如果只是取int 可以直接返回了
            memorySpage = BitConverter.GetBytes(num);
            //返回累加校验和
            return BitConverter.ToInt16(new byte[] { memorySpage[0], memorySpage[1] }, 0);
        }

        /// <summary>
        /// 累加和校验 1位
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public byte SumCheck(byte[] bs)
        {
            int num = 0;
            //所有字节累加
            for (int i = 0; i < bs.Length; i++)
            {
                num = num + bs[i];
            }
            byte ret = (byte)(num & 0xff);//只要最后一个字节
            return ret;
        }


        public static byte[] String2ByteArray(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);
        }

        public static byte[] String2ASCII_ByteArray(string str)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);
        }

        public static string ByteArray2String(byte[] byteArray)
        {
            return System.Text.Encoding.Default.GetString(byteArray);
        }

        public static string ASCIIByteArray2String(byte[] byteArray)
        {
            return System.Text.Encoding.ASCII.GetString(byteArray);
        }

    }
}
