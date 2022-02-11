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
using MaterialDesignThemes.Wpf;

namespace BolsaSalesianos
{
    /// <summary>
    /// Lógica de interacción para RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {

        private readonly CredentialsService credentialsService;
        private readonly EnterprisesService enterprisesService;
        private readonly StudentsService studentsService;
        private readonly Validator validator;

        public RegisterPage()
        {
            InitializeComponent();
            radio_student.IsChecked = true;

            validator = new Validator(trigger);
            credentialsService = new CredentialsService();
            studentsService = new StudentsService();
            enterprisesService = new EnterprisesService();
        }

        /* 
        * Método que realiza el cambio de registro entre el del usuario y el
        * de la empresa.
        */
        private void ChangeRegister(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(radio_student))
            {
                enterprise_form.Visibility = Visibility.Collapsed;
                student_form.Visibility = Visibility.Visible;
            }
            else
            {
                enterprise_form.Visibility = Visibility.Visible;
                student_form.Visibility = Visibility.Collapsed;
            }
        }

        /* 
        * Método que realiza el cambio de página que corresponde a la MainWindow
        */
        private void BackButton(object sender, RoutedEventArgs e)
        {
            Switcher.SwitchPage(new LoginPage());
        }

        /* 
        * Método que evalua en que formulario se encuentra el usuario actualmente,
        * tras la comprobación realiza el correspondiente método para ingresar los
        * datos.
        */
        private void Register(object sender, RoutedEventArgs e)
        {
            if ((bool)radio_student.IsChecked) StudentRegister();
            else EnterpriseRegister();
        }

        /* 
        * Método que intenta realizar un registro de una empresa, en caso de que el intento sea fallido mostrará
        * el correspondiente mensaje de error.
        */
        private void EnterpriseRegister()
        {
            Dictionary<String, String> values = validator.getValues(enterprise_form, "enterprise");
            bool valid = validator.IsValid(values) & PassValid(values["pass"], values["pass_2"]);
            if (valid)
            {
                Credential credential = credentialsService.Fetch(new Credential(values["user"]));
                Enterprise enterpriseCif = enterprisesService.Fetch(new Enterprise(values["cif"]));
                Enterprise enterpriseName = enterprisesService.Fetch(new Enterprise { name = values["name"] });

                if (IsNull(credential, "El nombre de usuario está ocupado.")
                    && IsNull(enterpriseCif, "El cif de la empresa está ocupado.")
                    && IsNull(enterpriseName, "El nombre de la empresa está ocupado."))
                {
                    Credential newCredentiela = new Credential(values["user"], values["pass"], "Empresa", "0000-00-00");
                    Credential insertedCredential = credentialsService.InsertAndGetCredential(newCredentiela);
                    if (insertedCredential != null)
                    {
                        Enterprise newEnterprise = new Enterprise(values["cif"], values["name"], values["contact_person"],
                            values["phone"], values["email"], insertedCredential.id);
                        enterprisesService.Insert(newEnterprise);
                        Switcher.SwitchWindow(new UserWindow(insertedCredential));
                    }
                }
            }
            else
            {
                trigger.IsActive = true;
            }
        }

        /* 
        * Método que intenta realizar un registro de un estudiante, en caso de que el intento sea fallido mostrará
        * el correspondiente mensaje de error.
        */
        private void StudentRegister()
        {
            Dictionary<String, String> values = validator.getValues(student_form, "student");
            bool valid = validator.IsValid(values) & PassValid(values["pass"], values["pass_2"]);

            if (valid)
            {
                Credential credential = credentialsService.Fetch(new Credential(values["user"]));
                Student student = studentsService.Fetch(new Student(values["dni"]));
                if (IsNull(credential, "El nombre de usuario está ocupado.")
                    && IsNull(student, "El dni de usuario está ocupado."))
                {
                    Credential insertedCredential = credentialsService.InsertAndGetCredential(new Credential(values["user"],
                        values["pass"], "Estudiante", "0000-00-00"));
                    if (insertedCredential != null)
                    {
                        Student newStudent = new Student(values["dni"], values["name"], values["last_name"], values["phone"],
                            values["email"], (bool)student_license.IsChecked ? 1 : 0, (bool)student_employed.IsChecked ? 1 : 0, (bool)student_data_transf.IsChecked ? 1 : 0, insertedCredential.id);
                        studentsService.Insert(newStudent);
                        Switcher.SwitchWindow(new UserWindow(insertedCredential));
                    }
                }
            }
            else
            {
                trigger.IsActive = true;
            }
        }

        /* 
        * Comprueba que las contraseñas son iguales y muestra un mensaje de error en
        * caso de que estas no lo sean.
        */
        private bool PassValid(string pass, string pass_2)
        {
            if (!pass.Equals(pass_2)) trigger.MessageQueue.Enqueue("Las contraseñas no coinciden");
            return pass.Equals(pass_2);
        }

        /* 
         * Metodo que recibe un objeto y un mensaje de posible error,
         * en caso de recibir un objeto nulo devolvera false y mues-
         * tra el mensaje de error previamente recogido.
         */
        private bool IsNull(object item, string errorMsg)
        {
            if (item != null)
            {
                trigger.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
                trigger.MessageQueue.Enqueue(errorMsg);
                trigger.IsActive = true;
            }
            return item == null;
        }
    }
}
