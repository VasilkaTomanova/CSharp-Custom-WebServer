using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.HTTPProtocol
{
    public class HTTPRequest
    {
        public HTTPRequestMethod Method { get; private set; }

        public string Url { get; private set; }
        public HTTPHeaderCollection Headers { get; } = new HTTPHeaderCollection();

        public string Body { get; private set; }
    }
}
