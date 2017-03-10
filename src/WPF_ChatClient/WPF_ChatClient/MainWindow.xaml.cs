using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPAddress IP { get; set; }
        private string IPString { get; set; }
        private IPEndPoint IPEP { get; set; }
        private Socket SocketServer { get; set; }
        private int Port { get; set; }
        private bool Wait { get; set; }
        private Thread ThreadRequest { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            IPString = "82.139.159.145";
            IP = IPAddress.Parse(IPString);
            Port = 1024;
            IPEP = new IPEndPoint(IP, Port);

            
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                SocketServer.Connect(IPEP);
                AppendText("Połączono z serwerem.", "Client");
            }
            catch (SocketException)
            {
                SocketServer.Close();
            }
        }

        private void AppendText(string text, string side = null)
        {
            richTextBox.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    string message = DateTime.Now.ToShortTimeString();
                    if (side != null)
                        message += " [" + side + "] ";
                    message += text + "\n";
                    richTextBox.AppendText(message);
                }));
        }
    }
}
