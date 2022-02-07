using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BolsaSalesianos.database;

namespace BolsaSalesianos.pojos
{
    internal class Student : IEditableObject
    {
        public string dni { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string resume { get; set; }
        public int credential { get; set; }
        public int license { get; set; }
        public int employed { get; set; }
        public int data_transf { get; set; }

        public Student(string dni)
        {
            this.dni = dni;
        }
        public Student()
        {

        }

        public Student(string dni, string name, string last_name, string phone, string email, int license, int employed, int data_transf, int credential) : this(dni)
        {
            this.name = name;
            this.last_name = last_name;
            this.phone = phone;
            this.email = email;
            this.license = license;
            this.employed = employed;
            this.data_transf = data_transf;
            this.credential = credential;
        }

        public void BeginEdit()
        {

        }

        public void EndEdit()
        {
            StudentsServices studentsServices = new StudentsServices();
            studentsServices.Update(this);

        }

        public void CancelEdit()
        {
        }
    }
}
