using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.HTTPProtocol
{
    public class HTTPRequest
    {
        private const string NewLine = "\r\n";
        public HTTPRequestMethod Method { get; private set; }

        public string Url { get; private set; }
        public HTTPHeaderCollection RequestHeaders { get; private set; } = new HTTPHeaderCollection();

        public string Body { get; private set; }

        public static HTTPRequest HttpParse (string request)
        {

            return new HTTPRequest();
        }

        public static HTTPRequest Parse(string request)
        {
            var lines = request.Split(NewLine);

            var startLine = lines.First().Split(" ");

            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];

            var headers = ParseHttpHeaders(lines.Skip(1));

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(NewLine, bodyLines);

            return new HTTPRequest
            {
                Method = method,
                Url = url,
                RequestHeaders = headers,
                Body = body
            };
        }

        private static HTTPRequestMethod ParseHttpMethod(string method)
           => method.ToUpper() switch
           {
               "GET" => HTTPRequestMethod.Get,
               "POST" => HTTPRequestMethod.Post,
               "PUT" => HTTPRequestMethod.Put,
               "DELETE" => HTTPRequestMethod.Delete,
               _ => throw new InvalidOperationException($"Method '{method}' is not supported."),
           };


        private static HTTPHeaderCollection ParseHttpHeaders(IEnumerable<string> headerLines)
        {
            var headerCollection = new HTTPHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");
                }

                var header = new HTTPHeader { 
                Name = headerParts[0],
                Value = headerParts[1].Trim()
            };

                headerCollection.Add(header);
            }

            return headerCollection;
        }


    }
}
