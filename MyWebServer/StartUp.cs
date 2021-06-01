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

                //  REQUEST
                int bufferLenght = 1024;
                byte[] buffer = new byte[bufferLenght];
                StringBuilder streamReaderOfRequest = new StringBuilder();

                //Read information from stream reqest
                var totalBitesRead = 0;
                while (networkStream.DataAvailable)
                {
                    var bitesRead = await networkStream.ReadAsync(buffer, 0, bufferLenght);
                    totalBitesRead += bitesRead;
                    if(totalBitesRead > 10 * 1024)
                    {
                        // throw...
                        currentConnectionClient.Close(); 
                    }
                    streamReaderOfRequest.Append(Encoding.UTF8.GetString(buffer, 0, bitesRead));
                }

                Console.WriteLine(streamReaderOfRequest.ToString());

                //RESPONSE
                string responseLine = "HTTP/1.1 200 OK";
                string responseBody = @"
<html> <head>
<link rel=""icon"" href=""data:,"">
</head> <body>Hello from *Василка*'s first custom server</body> </html>";
                int responseBodyLenghtOfBytes = Encoding.UTF8.GetByteCount(responseBody);

                //text/html or text/plain ще знае какво да очаква
                string responce = $@"{responseLine}
Server: My Custom Web Server for demo purposes
Date: {DateTime.UtcNow.ToString("r")}
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
