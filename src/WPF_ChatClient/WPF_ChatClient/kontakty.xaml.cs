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
    /// Interaction logic for kontakty.xaml
    /// </summary>
    public partial class kontakty : Window
    {
        private Socket Server
        {
            get { return App.server; }
            set { App.server = value; }
        }

        public kontakty()
        {

            InitializeComponent();

            WczytajKontakty();
        }

        private void WczytajKontakty()
        {
            //wyslij zapytanie 
        }
    }
}
