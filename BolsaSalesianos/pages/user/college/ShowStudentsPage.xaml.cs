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
using BolsaSalesianos.pojos;
using BolsaSalesianos.database;

namespace BolsaSalesianos.pages.user.college
{
    /// <summary>
    /// Lógica de interacción para ShowStudentsPage.xaml
    /// </summary>
    public partial class ShowStudentsPage : Page
    {
        private List<Student> students { get; set; }
        private StudentsServices studentsServices { get; set; }
        private CredentialsService credentialsService { get; set; }


        public ShowStudentsPage()
        {
            InitializeComponent();
            studentsServices = new StudentsServices();
            credentialsService = new CredentialsService();
            students = studentsServices.FetchAll();
            students_list.ItemsSource = students;

            List<int> credentials = credentialsService.FetchAll().Select(o => o.id).ToList();
            credential_selection.ItemsSource = credentials;
        }

    }
}
