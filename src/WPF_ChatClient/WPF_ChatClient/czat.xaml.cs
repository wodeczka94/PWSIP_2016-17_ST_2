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
    public partial class czat : Window
    {
        private Socket Server
        {
            get { return App.server; }
        }
        
        public bool Shown { get; set; }

        public czat()
        {
            InitializeComponent();

        }
        
        private void AppendText(string text, string side = null)
        {
            //richTextBox.Dispatcher.Invoke(DispatcherPriority.Normal,
            //    new Action(() =>
            //    {
            //        string message = DateTime.Now.ToShortTimeString();
            //        if (side != null)
            //            message += " [" + side + "] ";
            //        message += text + "\n";
            //        richTextBox.AppendText(message);
            //    }));
        }

        public new void Show()
        {
            Shown = true;
            base.Show();
        }

        public new void Close()
        {
            Shown = false;
            base.Close();
        }

    }
}
