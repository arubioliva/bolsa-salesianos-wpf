using BolsaSalesianos.pages.user.student;
using BolsaSalesianos.pojos;
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

namespace BolsaSalesianos.pages.user
{
    /// <summary>
    /// Lógica de interacción para StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        private Credential credential { get; set; }
        public StudentPage(object credential)
        {
            this.credential = (Credential)credential;
            InitializeComponent();
        }

        private void CloseSession(object sender, RoutedEventArgs e)
        {
            Switcher.window.Close();
        }

        private void Studies(object sender, RoutedEventArgs e)
        {
            content.Content = new ShowStudiesPage(credential);
        }
        private void Vacants(object sender, RoutedEventArgs e)
        {
            content.Content = new InscriptionsPage(credential);
        }
    }

}
