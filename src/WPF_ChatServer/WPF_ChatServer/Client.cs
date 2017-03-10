using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ChatServer
{
    public class Client
    {
        private Socket SocketClient { get; set; }
        public string IP
        {
            get { return SocketClient.LocalEndPoint.ToString(); }
        }
        private MemoryStream Memory { get; set; }
        private BinaryReader Reader { get; set; }
        private Thread ThreadRequest { get; set; }
        private bool Wait { get; set; }

        public Client(Socket client)
        {
            SocketClient = client;
            
        }

        private void WaitForRequest()
        {
            while (true)
            {
                
            }
        }

        private bool Login()
        {
            return true;
        }
    }
}
