using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for DodawanieKontaku.xaml
    /// </summary>
    public partial class DodawanieKontaku : Window
    {
        private Socket Server
        {
            get { return App.server; }
        }

        public DodawanieKontaku()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //swtorzyc zapytanie
            string msg = "check|login|" + textBox.Text;

            //wyslac zaytanie
            SocketDataTransfer.Send(Server, msg);

            //
            msg = SocketDataTransfer.Recive(Server);

            bool a = bool.Parse(msg);

            if (a)
            {
                DialogResult = true;

                msg=""

                Close();
            }
            else
            {
                MessageBox.Show("Podany użytkownik nie istnieje.");
            }
            

        }
    }
}
