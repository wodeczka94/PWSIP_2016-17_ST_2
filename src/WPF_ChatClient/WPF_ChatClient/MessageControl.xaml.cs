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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_ChatClient
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        #region DateTime Property
        private static readonly DependencyProperty DateTimeProperty = DependencyProperty.Register("DateTime",
            typeof(string),
            typeof(MessageControl),
            new PropertyMetadata(
                string.Empty)
            );

        private string DateTime
        {
            get { return (string)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }
        #endregion
        #region Message Property
        private static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message",
            typeof(string),
            typeof(MessageControl),
            new PropertyMetadata(
                string.Empty)
            );

        private string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        #endregion
        #region Nick Property
        private static readonly DependencyProperty NickProperty = DependencyProperty.Register("Nick",
            typeof(string),
            typeof(MessageControl),
            new PropertyMetadata(
                string.Empty)
            );

        public string Nick
        {
            get { return (string)GetValue(NickProperty); }
            set { SetValue(NickProperty, value); }
        }
        #endregion
        #region Left Property
        private static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left",
            typeof(bool),
            typeof(MessageControl),
            new PropertyMetadata(
                false)
            );

        private bool Left
        {
            get { return (bool)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }
        #endregion
        public MessageControl()
        {
            
            InitializeComponent();
        }
    }
}
