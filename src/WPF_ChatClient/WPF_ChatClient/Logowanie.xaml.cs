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
                    MessageBox.Show(d.ToString());
                    SocketServer.Close();
                    Thread.Sleep(3000);
                }
            }

            string msg;
            //byte[] msga;
            //byte[] bytes = new byte[256];

            //odebranie prosby o dane logowania
            //SocketServer.Receive(bytes);
            //msg = Encoding.UTF8.GetString(bytes);
            //msg = new string( msg.ToCharArray().Distinct().ToArray());
            msg = SocketDataTransfer.Recive(SocketServer);
            MessageBox.Show(msg);

            //wyslanie danych logowania
            string login = tbLogin.Text;
            string haslo = tbHaslo.Text;
            msg = "dane|" + login + "|" + haslo;
            //msga = Encoding.UTF8.GetBytes(msg);
            //SocketServer.Send(msga);
            SocketDataTransfer.Send(SocketServer, msg);

            //otrzymanie poprawnosci o danych logowania
            //SocketServer.Receive(bytes);
            //msg = Encoding.UTF8.GetString(bytes);
            msg = SocketDataTransfer.Recive(SocketServer);
            if (msg == "polaczono")
            {
               //jesli poprawne to przejdz do okna

            }
            else
            {
                //inaczej wiadomosc i niepoprawnosci danych
                MessageBox.Show("Podane dane są nie poprawne.");

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
