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
using System.Collections.ObjectModel;
using WPF_ChatClient;

namespace WPF_ChatClient
{
    /// <summary>
    /// Interaction logic for kontakty.xaml
    /// </summary>
    public partial class kontakty : Window
    {
        private Socket Server
        {
            get { return App.server; }
        }
        private int ID
        {
            get { return App.Index; }
        }
        private List<czat> oknaCzatu;

        public kontakty()
        {

            InitializeComponent();

            oknaCzatu = new List<czat>();

            WczytajKontakty();
        }

        private void WczytajKontakty()
        {
            //wyslij zapytanie
            string msg = "list";
            SocketDataTransfer.Send(Server, msg);

            //odbierz kontakty
            msg = SocketDataTransfer.Recive(Server);

            //wstaw kontakty do listy
            string[] msgs = msg.Split('|');

            spKontakty.Children.Clear();

            for (int i = 1; i < msgs.Length; i++)
            {
                string[] r = msgs[i].Split(':');
                string name = r[1];
                int id = int.Parse(r[0]);

                Contact c = new Contact(id, name);
                spKontakty.Children.Add(c);
            }
        }

        private void buttonDodajKontakt_Click(object sender, RoutedEventArgs e)
        {
            DodawanieKontaku d = new DodawanieKontaku();
            d.ShowDialog();
            if (d.DialogResult.HasValue && d.DialogResult.Value)
                WczytajKontakty();
        }
    }
}
