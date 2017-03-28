using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Czat
{
    public sealed partial class RozmowaPage : Page
    {
        public RozmowaPage()
        {
            this.InitializeComponent();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void wyslijButton_Click(object sender, RoutedEventArgs e)
        {
            //streamWriter.WriteLine(wiadomoscTextBox.Text);
            //streamWriter.Flush();
            //wiadomosciStackPanel.Children.Add(new TextBlock { Text = DateTime.Now.ToString("hh.mm.ss - ") + wiadomoscTextBox.Text, FontSize = 20 });
            //wiadomoscTextBox.Text = string.Empty;
        }

        private async void Odbierz()
        {
            while (true)
            {
                //wiadomosciStackPanel.Children.Add(new TextBlock { Text = DateTime.Now.ToString("hh.mm.ss - ") + await streamReader.ReadLineAsync(), FontSize = 20 });
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Odbierz();
        }
    }
}
