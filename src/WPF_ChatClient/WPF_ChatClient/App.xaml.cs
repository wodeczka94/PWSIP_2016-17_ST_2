using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_ChatClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Socket server;
        public static StreamWriter Writer;
        public static StreamReader Reader;  
        public static int Index;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SocketDataTransfer.Send(server, "dc");
            server.Shutdown(SocketShutdown.Send);
            string s = SocketDataTransfer.Recive(server);
            server.Shutdown(SocketShutdown.Receive);
            server.Close();
        }
    }
}
