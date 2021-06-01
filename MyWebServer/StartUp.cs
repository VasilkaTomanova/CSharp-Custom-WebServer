using System;
using System.Net;
using System.Net.Sockets;

namespace MyWebServer
{
   public class StartUp
    {
        public static void Main(string[] args)
        {
 
                IPAddress IpAddress = IPAddress.Parse("121.0.0.1");
                int port = 1234;
                TcpListener servetListener = new TcpListener(IpAddress, port);
                servetListener.Start();
            while (true)
            {

            }

           


        }
    }
}
