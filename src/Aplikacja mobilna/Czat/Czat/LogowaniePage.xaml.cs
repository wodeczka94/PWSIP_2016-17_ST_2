using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Czat
{
    public sealed partial class LogowaniePage : Page
    {
        public LogowaniePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await PolaczenieClass.streamSocket.ConnectAsync(new HostName("10.33.64.64"), "1024");
            }
            catch (Exception)
            {
                await new MessageDialog("Nie można połączyć się z serwerem.").ShowAsync();
                Application.Current.Exit();
            }
        }

        private void zalogujButton_Click(object sender, RoutedEventArgs e)
        {
            PolaczenieClass.streamWriter.WriteLine("login|" + identyfikatorTextBox.Text + "|" + hasloPasswordBox.Password);
            PolaczenieClass.streamWriter.Flush();

            string odpowiedz = PolaczenieClass.streamReader.ReadLine();

            if (odpowiedz == "true")
            {
                Frame.Navigate(typeof(RozmowaPage));
            }
            else if (odpowiedz == "false")
            {
                new MessageDialog("Niepoprawne dane.").ShowAsync();
            }
            else if (odpowiedz == "null")
            {
                new MessageDialog("Użytkownik jest zalogowany.").ShowAsync();
            }
        }

        private void zarejestrujTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }
    }
}
