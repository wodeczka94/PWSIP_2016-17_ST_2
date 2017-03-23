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
using System.Collections.ObjectModel;
using System.IO;

namespace WPF_ChatServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Client> Clients { get; set; }
        private Socket SocketServer;
        private Thread ThreadWaitForClients;
        private IPAddress IP;
        private IPEndPoint IPEP;
        private int Port;
        private bool Wait;
        private int QueueLength;

        public MainWindow()
        {
            InitializeComponent();
            Clients = new ObservableCollection<Client>();
            listView.ItemsSource = Clients;
            InitializeServer();
        }

        private void InitializeServer()
        {
            Port = 1024;
            QueueLength = 4;
            IP = IPAddress.Loopback;
            //IP = IPAddress.Parse("10.33.61.10");
            IPEP = new IPEndPoint(IP, Port);

            SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketServer.Bind(IPEP);
            SocketServer.Listen(QueueLength);

            Wait = true;
            ThreadWaitForClients = new Thread(new ThreadStart(WaitForClients));
            ThreadWaitForClients.Start();
            AppendText("Uruchomiono serwer", "Server");
        }

        private void WaitForClients()
        {
            while (Wait)
            {
                if (!Wait) break;
                Socket socketclient = SocketServer.Accept();
                if (!Wait) break;
                Client client = new Client(socketclient, this);
                AddItem(client);
            }
        }

        public void DisconnectClient(Client client)
        {
            RemoveItem(client);
            client.Dispose();
        }


        private void AppendText(string text, string side = null)
        {
            richTextBox.Dispatcher.Invoke(DispatcherPriority.Normal, 
                new Action(() => 
                {
                    string message = DateTime.Now.ToLongTimeString()+" ";
                    if (side != null)
                        message += "[" + side + "] ";
                    message += text + "\n";
                    richTextBox.AppendText(message);
                }));
        }

        private void AddItem(Client item)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => Clients.Add(item)));
        }

        private void RemoveItem(Client item)
        {
            Application.Current.Dispatcher.BeginInvoke(new Func<bool>(() => Clients.Remove(item)));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Wait = false;
            SocketServer.Close();

            List<Client> ms = Clients.ToList();

            //foreach (var item in ms)
            //{
            //    item.Disconnect();
            //}

            //SocketServer.Shutdown(SocketShutdown.Both);
        }
    }
}
