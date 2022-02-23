using BolsaSalesianos.database;
using BolsaSalesianos.database.pojos;
using BolsaSalesianos.database.services;
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

namespace BolsaSalesianos.pages.user.student
{
    /// <summary>
    /// Lógica de interacción para SubscribePage.xaml
    /// </summary>
    public partial class InscriptionsPage : Page
    {
        private StudentsService studentsService;
        private Credential credential;
        private VacantsService vacantsService;
        private SelectionsService selectionsService;
        private Student student;
        public InscriptionsPage(object Credential)
        {
            selectionsService = new SelectionsService();
            vacantsService = new VacantsService();
            studentsService = new StudentsService();
            credential = (Credential)Credential;
            InitializeComponent();

            student = studentsService.Fetch(new Student { credential = credential.id });
            vacants_list.ItemsSource = vacantsService.GetVacantsByStudent(student.dni);
        }

        private void vacants_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            modal.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vacant selectedVacant = (Vacant)vacants_list.SelectedItem;
            if (selectionsService.FetchAllWithFilter(new Selection { student = student.dni, vacant = selectedVacant.id }).Count == 0)
            {
                Selection selection = new Selection
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd"),
                    student = student.dni,
                    vacant = selectedVacant.id
                };
                selectionsService.Insert(selection);
                modal.IsOpen = false;
            }
            else
            {
                

            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            modal.IsOpen = false;
        }
    }
}
