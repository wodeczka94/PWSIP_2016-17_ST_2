using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SocketDataTransfer.Send("dc");
            server.Shutdown(SocketShutdown.Send);
            string s = SocketDataTransfer.Recive();
            MessageBox.Show("Od serwera: "+s);
            server.Shutdown(SocketShutdown.Receive);
            server.Close();
        }
    }
}
