using BolsaSalesianos.database;
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

namespace BolsaSalesianos.pages.user.college
{
    /// <summary>
    /// Lógica de interacción para ShowEnterprisesPage.xaml
    /// </summary>
    public partial class ShowEnterprisesPage : Page
    {
        private EnterprisesService enterprisesServices;
        public ShowEnterprisesPage()
        {
            InitializeComponent();
            enterprisesServices = new EnterprisesService();
            enterprises_list.ItemsSource = enterprisesServices.FetchAll();

            List<string> enterprise_names = new List<string>();
            enterprise_names.AddRange(enterprisesServices.FetchAll().Select(o => o.cif).Distinct().ToList());
            enterprise_remove.ItemsSource = enterprise_names;
        }

        private void DeleteEnterprise(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(enterprise_remove.SelectedItem.ToString());
            enterprisesServices.Delete(new Enterprise { cif = enterprise_remove.SelectedItem.ToString() });
            enterprises_list.ItemsSource = enterprisesServices.FetchAll();
        }
    }
}
