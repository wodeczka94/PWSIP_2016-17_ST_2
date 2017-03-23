using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class Contact : UserControl
    {
        #region NickProperty
        private static readonly DependencyProperty NickProperty = DependencyProperty.Register("Nick",
            typeof(string),
            typeof(Contact),
            new PropertyMetadata(
                string.Empty)
            );
        
        private string Nick
        {
            get { return (string)GetValue(NickProperty); }
            set { SetValue(NickProperty, value); }
        }
        #endregion

        private int ID;

        private czat oknoCzatu;

        public Contact(int id, string name)
        {
            InitializeComponent();

            Nick = name;
            ID = id;
        }

        private void _this_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (oknoCzatu == null)
                oknoCzatu = new czat() { Title = Nick };

            //if (!oknoCzatu.Shown)
            //    oknoCzatu.Show();

            //if (!oknoCzatu.IsActive)
            //    oknoCzatu.Activate();

            if (!oknoCzatu.IsVisible)
                oknoCzatu.Show();

            if (oknoCzatu.WindowState == WindowState.Minimized)
                oknoCzatu.WindowState = WindowState.Normal;

            oknoCzatu.Activate();
            oknoCzatu.Topmost = true;  // important
            oknoCzatu.Topmost = false; // important
            oknoCzatu.Focus();
        }
    }
}
