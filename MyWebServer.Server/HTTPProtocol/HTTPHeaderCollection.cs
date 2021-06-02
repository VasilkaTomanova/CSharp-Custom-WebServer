using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.HTTPProtocol
{
    public class HTTPHeaderCollection
    {
        private readonly Dictionary<string, HTTPHeader> headers;
        public HTTPHeaderCollection()
        {
            this.headers = new Dictionary<string, HTTPHeader>();
        }
    }
}
