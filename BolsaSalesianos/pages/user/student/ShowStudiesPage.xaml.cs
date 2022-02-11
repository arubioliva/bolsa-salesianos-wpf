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
        public ShowStudiesPage(object credential)
        {
            InitializeComponent();
            idiomsService = new IdiomsService();
            idiomsStudentService = new IdiomsStudentService();
            studiesStudentService = new StudiesStudentService();
            studentsServices = new StudentsService();
            studiesService = new StudiesService();

            int id = ((Credential)credential).id;
            student = studentsServices.FetchAllWithFilter(new Student { credential = id })[0];

            List<IdiomStudent> idiomsStudent = idiomsStudentService.GetIdiomsByStudent(student.dni);
            idioms_list.ItemsSource = idiomsStudent;
            student_idioms.ItemsSource = idiomsStudent.Select(o => $"{o.language} - {o.level}").Distinct().ToList();

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
        }
    }
}
