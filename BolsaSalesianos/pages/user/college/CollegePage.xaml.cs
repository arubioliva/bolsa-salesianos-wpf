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
using BolsaSalesianos.pages.user.college;

namespace BolsaSalesianos.pages.user
{
    /// <summary>
    /// Lógica de interacción para CollegePage.xaml
    /// </summary>
    public partial class CollegePage : Page
    {
        public CollegePage(object credential)
        {
            InitializeComponent();
        }

        private void CloseSession(object sender, RoutedEventArgs e)
        {
            Switcher.SwitchOwner();
        }

        private void StudentsPage(object sender, RoutedEventArgs e)
        {
            content.Content = new ShowStudentsPage();
        }

        private void EnterprisesPage(object sender, RoutedEventArgs e)
        {
            content.Content = new ShowEnterprisesPage();
        }
    }
}
