using BolsaSalesianos.database;
using BolsaSalesianos.database.pojos;
using BolsaSalesianos.database.services;
using BolsaSalesianos.pojos;
using MaterialDesignThemes.Wpf;
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
    /// Lógica de interacción para ShowStudiesPage.xaml
    /// </summary>
    public partial class ShowStudiesPage : Page
    {
        private IdiomsStudentService idiomsStudentService { get; set; }
        private StudiesStudentService studiesStudentService { get; set; }
        private StudentsService studentsServices { get; set; }
        private StudiesService studiesService { get; set; }
        private IdiomsService idiomsService { get; set; }
        private Student student { get; set; }
        private Credential credential { get; set; }
        public ShowStudiesPage(object userCredential)
        {
            InitializeComponent();
            idiomsService = new IdiomsService();
            idiomsStudentService = new IdiomsStudentService();
            studiesStudentService = new StudiesStudentService();
            studentsServices = new StudentsService();
            studiesService = new StudiesService();
            credential = (Credential)userCredential;
            GuardarEstudiante();
        }

        private void GuardarEstudiante()
        {
            student = studentsServices.FetchAllWithFilter(new Student { credential = credential.id })[0];
            ActualizaElementos();
        }

        private void ActualizaElementos()
        {
            List<Idiom> idiomsStudent = idiomsStudentService.GetIdiomsByStudent(student.dni);
            idioms_list.ItemsSource = idiomsStudent;
            student_idioms.ItemsSource = idiomsStudent.Select(o => $"{o.language}-{o.level}").Distinct().ToList();

            List<int> nums = new List<int>();
            for (int i = 1990; i < DateTime.Now.Year; i++) { nums.Add(i); }
            year_end.ItemsSource = nums;
            year_start.ItemsSource = nums;

            List<StudyStudent> studiesList = studiesStudentService.FetchAllWithFilter(new StudyStudent { student = student.dni });
            studies_list.ItemsSource = studiesList;
            student_studies.ItemsSource = studiesList.Select(o => o.study).Distinct().ToList();
            all_studies.ItemsSource = studiesService.FetchAll().Select(o => o.name).Distinct().ToList();

            List<Idiom> idioms = idiomsService.FetchAll();
            all_idioms.ItemsSource = idioms.Select(o => o.language).Distinct().ToList();
            all_levels.ItemsSource = idioms.Select(o => o.level).Distinct().ToList();

            student_dni.Text = student.dni;
            student_name.Text = student.name;
            student_last_name.Text = student.last_name;
            student_phone.Text = student.phone;
            student_email.Text = student.email;
            student_license.IsChecked = student.license == 1;
            student_employed.IsChecked = student.employed == 1;
            student_data_transf.IsChecked = student.data_transf == 1;

        }

        private void AddStudy(object sender, RoutedEventArgs e)
        {
            if (all_studies.SelectedItem != null && year_end.SelectedItem != null && year_start.SelectedItem != null)
            {
                int yearStart = int.Parse(year_start.SelectedItem.ToString());
                int yearEnd = int.Parse(year_end.SelectedItem.ToString());

                StudyStudent study = new StudyStudent
                {
                    study = all_studies.SelectedItem.ToString(),
                    student = student.dni
                };

                if (studiesStudentService.Fetch(study) != null)
                {
                    if (yearStart < yearEnd)
                    {
                        study.start = yearStart;
                        study.end = yearEnd;
                        studiesStudentService.Insert(study);
                        ActualizaElementos();
                    }
                    else
                    {
                        ShowError(studies_trigger, "Las fechas son incorrectas.");
                    }
                }
                else
                {
                    ShowError(studies_trigger, "Este alumno ya ha realizado este estudio.");
                }
            }
            else
            {
                ShowError(studies_trigger, "Rellene todos los campos por favor.");
            }
        }

        private void RemoveStudy(object sender, RoutedEventArgs e)
        {
            if (student_studies.SelectedItem != null)
            {
                StudyStudent studyStudent = new StudyStudent { study = student_studies.SelectedItem.ToString(), student = student.dni };
                studiesStudentService.Delete(studyStudent);
                ActualizaElementos();
            }
            else
            {
                ShowError(studies_trigger, "Rellene todos los campos por favor.");
            }
        }

        private void ShowError(Snackbar snackbar, string error)
        {
            snackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            snackbar.MessageQueue.Enqueue(error);
            snackbar.IsActive = true;
        }

        private void RemoveIdiom(object sender, RoutedEventArgs e)
        {
            if (student_idioms.SelectedItem != null)
            {
                string[] languaje = student_idioms.SelectedItem.ToString().Split('-');
                Console.WriteLine(languaje[0] + "-" + languaje[1]);

                Idiom newIdiom = new Idiom { language = languaje[0], level = languaje[1] };
                Idiom idiom = idiomsService.Fetch(newIdiom);
                if (idiom != null)
                {
                    IdiomStudent idiomStudent = new IdiomStudent { idiom = idiom.id, student = student.dni };
                    idiomsStudentService.Delete(idiomStudent);
                    ActualizaElementos();
                }
            }
            else
            {
                ShowError(studies_trigger, "Rellene todos los campos por favor.");
            }
        }

        private void AddIdiom(object sender, RoutedEventArgs e)
        {
            if (all_idioms.SelectedItem != null && all_levels.SelectedItem != null)
            {
                Idiom newIdiom = new Idiom { language = all_idioms.SelectedItem.ToString(), level = all_levels.SelectedItem.ToString() };
                Idiom idiom = idiomsService.Fetch(newIdiom);

                IdiomStudent idiomStudent = new IdiomStudent { idiom = idiom.id, student = student.dni };
                idiomsStudentService.Insert(idiomStudent);
                ActualizaElementos();
            }
        }

        private void UpdateValues(object sender, RoutedEventArgs e)
        {
            Validator validator = new Validator(update_trigger);
            Dictionary<string, string> data = validator.getValues(student_update, "student");
            if (validator.IsValid(data))
            {
                Student student = new Student();
                student.dni = data["dni"];
                student.name = data["name"];
                student.last_name = data["last_name"];
                student.phone = data["phone"];
                student.email = data["email"];
                student.license = (bool)student_license.IsChecked ? 1 : 0;
                student.employed = (bool)student_employed.IsChecked ? 1 : 0;
                student.data_transf = (bool)student_data_transf.IsChecked ? 1 : 0;
                student.credential = credential.id;
                studentsServices.Update(student);
                GuardarEstudiante();
            }
        }
    }
}
