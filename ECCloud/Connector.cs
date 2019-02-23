using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECCloud
{

    class Connector
    {
        public int hostPort { get; set; }
        public string hostName { get; set; }
        private IPAddress host;
        private IPEndPoint hostep;
        Socket sock;

        public Connector()
        {

        }


        public bool establishConnection()
        {
            host = IPAddress.Parse(hostName);
            hostep = new IPEndPoint(host, hostPort);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sock.Connect(hostep);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            
        }

        public string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }

        public int CheckUser(string command)
        {
            int sessionID = 0;
            string HTTPRequestHeaders_String = "GET ?q=fdgdfg HTTP/1.0" + command;
            byte[] HTTPRequestHeaders = System.Text.Encoding.ASCII.GetBytes(HTTPRequestHeaders_String);
            sessionID = sock.Send(HTTPRequestHeaders, SocketFlags.None);

            return sessionID;
        }

    }
}
