using Microsoft.Web.WebView2.Wpf;
using Supabase;
using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using static Supabase.Gotrue.Constants;

namespace SupaBaseExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent ();
        }

        Client supabase;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://gjcigvsjjlqrgumafqzn.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImdqY2lndnNqamxxcmd1bWFmcXpuIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTA0MTk4ODMsImV4cCI6MjAwNTk5NTg4M30.rY9pAyO3_abQGYUb1VHYM6FeXYTJh1J2ghQ-tJhoN3o";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            supabase = new Supabase.Client (url, key, options);
            await supabase.InitializeAsync ();
            var session = supabase.Auth.CurrentSession;
            supabase.Auth.AddStateChangedListener ((sender, changed) =>
            {
                switch (changed)
                {
                    case AuthState.SignedIn:
                        break;
                    case AuthState.SignedOut:
                        break;
                    case AuthState.UserUpdated:
                        break;
                    case AuthState.PasswordRecovery:
                        break;
                    case AuthState.TokenRefreshed:
                        break;
                }
            });
            var signInUrl = await supabase.Auth.SignIn (Provider.Github);
           
            Debug.WriteLine (signInUrl);
            GoWeb (signInUrl.Uri.AbsoluteUri);
        }

        private async void SignOut(object sender, RoutedEventArgs e)
        {
            if (supabase == null)
                return;

            var signOutUrl = supabase.Auth.SignOut ();
        }

        private void GoWeb(string url)
        {
            Debug.WriteLine (url);
            WebView2 web = new ();
            Grid.SetRow (web, 0);
            Grid.SetRowSpan (web, 2);
            web.Source = new Uri(url);
            grd.Children.Add (web);
        }
    }
}
