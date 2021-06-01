using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {

            IPAddress IpAddress = IPAddress.Parse("127.0.0.1");
            int port = 1234;
            TcpListener serverListener = new TcpListener(IpAddress, port);
            serverListener.Start();
            Console.WriteLine("Start working...");

            while (true)
            {
                TcpClient currentConnectionClient = await serverListener.AcceptTcpClientAsync();

                NetworkStream networkStream = currentConnectionClient.GetStream();

                string responseLine = "HTTP/1.1 200 OK";
                string responseBody = @"<h2>Hello from *Василка*'s first custom server!<h2>";
                int responseBodyLenghtOfBytes = Encoding.UTF8.GetByteCount(responseBody);

                //text/html or text/plain ще знае какво да очаква
                string responce = $@"{responseLine}
Content-Length: {responseBodyLenghtOfBytes}
Content-Type: text/html; charset=UTF-8

{responseBody}";

                Byte[] responsAsBytes = Encoding.UTF8.GetBytes(responce);

                await networkStream.WriteAsync(responsAsBytes);
                currentConnectionClient.Close();
            }



        }
    }
}
