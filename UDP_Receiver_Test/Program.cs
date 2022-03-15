using System;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace UDP_Receiver_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PUdp.Create();

            while (true)
            {
                PUdp.BeginReceive();
                Console.ReadKey();
            }
        }

        internal static class PUdp
        {
            private static UdpClient socket;
            public static void Create()
            {
                if (socket == null)
                    socket = new UdpClient(8080);
            }

            public static void BeginReceive()
            {
                socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
            }

            private static void OnUdpData(IAsyncResult result)
            {
                UdpClient socket = result.AsyncState as UdpClient;
                IPEndPoint source = new IPEndPoint(0, 0);
                byte[] message = socket.EndReceive(result, ref source);
                string returnData = Encoding.ASCII.GetString(message);

                Console.WriteLine("Data: " + returnData.ToString() + " from " + source);
            }
        }
    }
}
