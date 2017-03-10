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

namespace WPF_ChatServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Client> Clients { get; set; }

        private Socket SocketServer { get; set; }
        private Thread ThreadWaitForClients { get; set; }
        private IPAddress IP { get; set; }
        private IPEndPoint IPEP { get; set; }
        private int Port { get; set; }
        private bool Wait { get; set; }
        private int QueueLength { get; set; }

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
            IP = IPAddress.Parse("82.139.159.145");
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
                Socket client = SocketServer.Accept();
                AppendText("Użytkownik połączony - autoryzacja");

                AddItem(new Client(client));
            }
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
    }
}
