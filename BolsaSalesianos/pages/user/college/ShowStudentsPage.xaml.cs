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
using System.Collections.ObjectModel;
using BolsaSalesianos.database.pojos;
using Newtonsoft.Json;

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
        private StudiesStudentService studiesService { get; set; }
        private SelectionsService selectionsService { get; set; }
        private IdiomsService idiomsService { get; set; }

        public ShowStudentsPage()
        {
            InitializeComponent();
            studentsServices = new StudentsServices();
            credentialsService = new CredentialsService();
            studiesService = new StudiesStudentService();
            selectionsService = new SelectionsService();
            idiomsService = new IdiomsService();

            students = studentsServices.FetchAll();
            students_list.ItemsSource = students;

            credential_selection.ItemsSource = credentialsService.FetchAll().Select(o => o.id).ToList();

            List<string> studies_names = new List<string> { "Todos" };
            studies_names.AddRange(studiesService.FetchAll().Select(o => o.study).Distinct().ToList());
            studies.ItemsSource = studies_names;
            studies.SelectedIndex = 0;

            List<int> studies_years = new List<int> { 0000 };
            studies_years.AddRange(studiesService.FetchAll().Select(o => o.end).Distinct().ToList());
            studies_year.ItemsSource = studies_years;
            studies_year.SelectedIndex = 0;

            List<int> selectionsIds = new List<int> { 0000 };
            selectionsIds.AddRange(selectionsService.FetchAll().Select(o => o.id).Distinct().ToList());
            selection_id.ItemsSource = selectionsIds;
            selection_id.SelectedIndex = 0;


            List<Idiom> idioms = idiomsService.FetchAll();
            List<string> idiomsLanguage = new List<string> { "Todos" };
            List<string> idiomsLevel = new List<string> { "Todos" };
            idiomsLanguage.AddRange(idioms.Select(o => o.language).Distinct());
            idiomsLevel.AddRange(idioms.Select(o => o.level).Distinct());


            idiom_language.ItemsSource = idiomsLanguage;
            idiom_language.SelectedIndex = 0;

            idiom_level.ItemsSource = idiomsLevel;
            idiom_level.SelectedIndex = 0;


        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            students_list.ItemsSource = students.Where(student => (student.name + student.last_name + student.dni + student.email + student.resume).ToLower().Contains(((TextBox)sender).Text.Replace(" ", "").ToLower()));
        }

        private void studies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("aaaaaaaaaaa");
            string study = studies.SelectedIndex != 0 ? studies.SelectedItem.ToString() : null;

            int? year = null;
            if (studies_year.SelectedItem != null && studies_year.SelectedIndex > 0)
                year = int.Parse(studies_year.SelectedItem.ToString());

            int? selectionId = null;
            if (selection_id.SelectedIndex > 0)
                selectionId = int.Parse(selection_id.SelectedItem.ToString());

            int? selected = (bool)selection_selected.IsChecked ? 1 : 0;

            string idiomLanguage = idiom_language.SelectedIndex > 0 ? idiom_language.SelectedItem.ToString() : null;
            string idiomLevel = idiom_level.SelectedIndex > 0 ? idiom_level.SelectedItem.ToString() : null;

            if (study == null && selectionId == null && idiomLanguage == null && idiomLevel == null && year == null)
            {
                students = studentsServices.FetchAll();
            }
            else
            {
                object item = new
                {
                    study_student = new { study = study, end = year },
                    selection = new { id = selectionId, selected = selected },
                    idiom = new { language = idiomLanguage, level = idiomLevel }
                };
                string filter = JsonConvert.SerializeObject(item, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                students = studentsServices.FetchAllByStudy(filter);
                students_list.ItemsSource = students;
            }

        }
    }
}
