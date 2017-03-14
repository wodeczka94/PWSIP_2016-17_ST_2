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
using System.Windows.Shapes;

namespace WPF_ChatClient
{
    /// <summary>
    /// Interaction logic for Logowanie.xaml
    /// </summary>
    public partial class Logowanie : Window
    {
        private Socket SocketServer { get; set; }
        private IPAddress IP { get; set; }
        private IPEndPoint IPEP { get; set; }
        private int Port { get; set; }


        public Logowanie()
        {
            InitializeComponent();

            Port = 1024;
            IP = IPAddress.Loopback;
            //IP = IPAddress.Parse("192.168.40.100");
            IPEP = new IPEndPoint(IP, Port);

            tbLogin.Focus();
            //while (SocketServer == null || !SocketServer.Connected)
            //{
            //    try
            //    {
            //SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //        SocketServer.Connect(IPEP);
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.ToString());
            //        Thread.Sleep(3000);
            //    }
            //}
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            //polaczenie z serverem
            while (SocketServer == null || !SocketServer.Connected)
            {
                try
                {
                    SocketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    SocketServer.Connect(IPEP);
                }
                catch (Exception d)
                {
                    if (i == 3)
                    {
                        MessageBox.Show("Nie można połączyć się z serwerm");
                        return;
                    }
                    i++;
                    MessageBox.Show(d.ToString());
                    SocketServer.Close();
                    Thread.Sleep(3000);
                }
            }

            string msg;
            //byte[] msga;
            //byte[] bytes = new byte[256];

            //odebranie prosby o dane logowania
            msg = SocketDataTransfer.Recive(SocketServer);
            //MessageBox.Show(msg);

            //wyslanie danych logowania
            string login = tbLogin.Text;
            string haslo = tbHaslo.Password;
            msg = "dane|" + login + "|" + haslo;
            SocketDataTransfer.Send(SocketServer, msg);

            //otrzymanie poprawnosci o danych logowania
            msg = SocketDataTransfer.Recive(SocketServer);
            if (msg == "polaczono")
            {
                //jesli poprawne to przejdz do okna
                MainWindow m = new MainWindow();
                Close();
                m.ShowDialog();
            }
            else
            {
                //inaczej wiadomosc i niepoprawnosci danych
                MessageBox.Show("Podane dane są nie poprawne.");
                tbLogin.Focus();
                SocketServer.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.SelectAll();
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox tb = sender as  PasswordBox;
            tb.SelectAll();
        }
    }
}
