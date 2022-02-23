using BolsaSalesianos.database;
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
        private Student student { get; set; }
        private StudentsService studentsService { get; set; }
        public StudentPage(object credential)
        {
            InitializeComponent();
            this.credential = (Credential)credential;
            studentsService = new StudentsService();
            student = new Student();
            student.credential = this.credential.id;
            student = studentsService.Fetch(student);

            student_user.Text = this.credential.user;
            student_dni.Text = student.dni;
            student_name.Text = student.name;
            student_email.Text = student.email;
            student_phone.Text = student.phone;
            student_pass.Password = this.credential.pass;
            student_pass_2.Password = this.credential.pass;
            student_last_name.Text = student.last_name;
            student_resume.Text = student.resume;
            student_employed.IsChecked = student.employed == 1;
            student_data_transf.IsChecked = student.data_transf == 1;
            student_license.IsChecked = student.license == 1;
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

        private void Profile(object sender, RoutedEventArgs e)
        {
            modal_user.IsOpen = true;
        }

        private void Updates(object sender, RoutedEventArgs e)
        {
            Validator validator = new Validator(trigger);
            if (validator.IsValid(validator.getValues(student_form, "student")))
            {
                studentsService.Update(new Student
                {
                    dni = student_dni.Text,
                    name = student_name.Text,
                    email = student_email.Text,
                    phone = student_phone.Text,
                    last_name = student_last_name.Text,
                    resume = student_resume.Text,
                    employed = (bool)student_employed.IsChecked ? 1 : 0,
                    data_transf = (bool)student_data_transf.IsChecked ? 1 : 0,
                    license = (bool)student_license.IsChecked ? 1 : 0
                });
                CredentialsService credentialsService = new CredentialsService();
                credentialsService.Update(new Credential
                {
                    id = credential.id,
                    user = student_user.Text,
                    pass = student_pass.Password,
                    type = "Estudiante",
                    last_connection = DateTime.Now.ToString("yyyy-MM-dd")
                });
                modal_user.IsOpen = false;
            }

        }
    }

}
