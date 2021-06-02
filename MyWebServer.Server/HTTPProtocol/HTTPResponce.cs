using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.HTTPProtocol
{
   public class HTTPResponce
    {
        public HTTPStatusCode HTTPStatusCode { get;init; }

        public HTTPHeaderCollection ResponceHeaders { get; } = new HTTPHeaderCollection();

        public string Body { get; private set; }
    }
}
