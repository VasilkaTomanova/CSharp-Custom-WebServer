using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server
{
    public class CustomHttpServer
    {
        private readonly IPAddress ipAdreess;
        private readonly int port;
        private readonly TcpListener serverListener;

        public CustomHttpServer(string ipAdreess, int port)
        {
            this.ipAdreess = IPAddress.Parse(ipAdreess);
            this.port = 1234;
            this.serverListener = new TcpListener(this.ipAdreess, this.port);
        }





        public async Task Start()
        {
            this.serverListener.Start();
            Console.WriteLine("Start working...");

            while (true)
            {
                //Използваме await ... AcceptTcpClientAsync, защото е по - бързо и обработваме повече
                // и се възползваме от това, че компютътр има повече ядра, все пак е за сървър и ще има повече ядра и
                // така му използваме възможностите
                TcpClient currentConnectionClient = await this.serverListener.AcceptTcpClientAsync();
                NetworkStream networkStream = currentConnectionClient.GetStream();

                string request = await ReadRequest(networkStream);
                Console.WriteLine(request);

                await WriteResponce(networkStream);

                currentConnectionClient.Close();
            }


        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            int bufferLenght = 1024;
            byte[] buffer = new byte[bufferLenght];
            StringBuilder streamReaderOfRequest = new StringBuilder();

            //Read information from stream reqest
            var totalBitesRead = 0;
            while (networkStream.DataAvailable)
            {
                var bitesRead = await networkStream.ReadAsync(buffer, 0, bufferLenght);
                totalBitesRead += bitesRead;
                if (totalBitesRead > 10 * 1024)
                {
                    // throw...
                    // currentConnectionClient.Close();
                }
                streamReaderOfRequest.Append(Encoding.UTF8.GetString(buffer, 0, bitesRead));
            }
            return streamReaderOfRequest.ToString();
        }

        private async Task WriteResponce(NetworkStream networkStream)
        {
            string responseLine = "HTTP/1.1 200 OK";
            string responseBody = @"
<html> <head>
<link rel=""icon"" href=""data:,"">
</head> <body>Hello from *Василка*'s first custom server</body> </html>";
            //we need to know the length of bytes of the real content of the page = response body or responce content
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
        }



    }
}
