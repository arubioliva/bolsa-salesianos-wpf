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
using BolsaSalesianos.database;
using BolsaSalesianos.pojos;
using MaterialDesignThemes.Wpf;

namespace BolsaSalesianos
{
    /// <summary>
    /// Lógica de interacción para PruebaLoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private CredentialsService credentialsService;

        public LoginPage()
        {
            InitializeComponent();
            credentialsService = new CredentialsService();
        }

        private void TryLogin(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hola?");
            Credential credential = credentialsService.Fetch(new Credential(user_field.Text, pass_field.Password));
            if (credential != null)
            {
                Switcher.SwitchWindow(new UserWindow(credential));
            }
            else
            {
                trigger.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
                trigger.MessageQueue.Enqueue("Los credenciales son icorrectos");
                trigger.IsActive = true;
            }
        }

        /* 
        * Comprueba que las contraseñas son iguales y muestra un mensaje de error en
        * caso de que estas no lo sean.
        */
        private void Register(object sender, RoutedEventArgs e)
        {
            Switcher.SwitchPage(new RegisterPage());
        }

    }

}
