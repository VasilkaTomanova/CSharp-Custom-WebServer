using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server;
namespace MyWebServer
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {
            CustomHttpServer server = new CustomHttpServer("127.0.0.1", 1234);
            await server.Start();


        }
    }
}
