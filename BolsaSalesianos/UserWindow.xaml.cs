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
using System.Windows.Shapes;
using BolsaSalesianos.pages.user;
using BolsaSalesianos.pojos;

namespace BolsaSalesianos
{
    /// <summary>
    /// Lógica de interacción para UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private Credential credential;
        public UserWindow(object credential)
        {
            this.credential = (Credential)credential;
            InitializeComponent();
            InitializePage();

        }

        public void InitializePage()
        {
            switch (credential.type)
            {
                case "Estudiante":
                    Content = new StudentPage(credential);
                    break;
                case "Centro":
                    Content = new CollegePage(credential);
                    break;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Close();
        }
    }
}
