using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
    /// Interaction logic for rejestracja.xaml
    /// </summary>
    public partial class rejestracja : Window
    {
        private Socket Server
        {
            get { return App.server; }
            set { App.server = value; }
        }


        public rejestracja()
        {
            InitializeComponent();
        }

        private void ButtonZarejestruj_Click(object sender, RoutedEventArgs e)
        {
            //sciaga login z textboxa
            string login = textBox.Text;

            //sciaga haslo z passwordboxa
            string haslo = passwordBox.Password;

            //sciaga email
            string email = textBox1.Text;

            //generuje komende do utworzenia nowego uzytkownika
            string msg = "reg|" + login + "|" + haslo + "|" + email;

            //wysyla komende do serwera
            SocketDataTransfer.Send(Server, msg);

            //odbiera komende o pozytywnym utworzeniu 
            MessageBox.Show(SocketDataTransfer.Recive(Server));

            //zamyka okno
            Close();
        }

        private void textBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            //bierze wartosc
            string login = textBox.Text;

            //wysyla zapytanie
            SocketDataTransfer.Send(Server, "check|login|" + login);


            //odbiera odpowiedz
            string b = SocketDataTransfer.Recive(Server);


            //wyswietla wiadomosc
            if (b == true.ToString())
                label.Content = "Podany login istnieje";
            else
                label.Content = "";
        }

        private void textBoxMail_LostFocus(object sender, RoutedEventArgs e)
        {
            //bierze wartosc
            string mail = textBox1.Text;

            //wysyla zapytanie
            SocketDataTransfer.Send(Server, "check|mail|" + mail);


            //odbiera odpowiedz
            string b = SocketDataTransfer.Recive(Server);


            //wyswietla wiadomosc
            if (b == true.ToString())
                label1.Content = "Podany mail istnieje";
            else
                label1.Content = "";
        }
    }
}
