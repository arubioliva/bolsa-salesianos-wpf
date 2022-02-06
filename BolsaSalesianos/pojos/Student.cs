using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.pojos
{
    internal class Student
    {
        public string dni { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string license { get; set; }
        public string employed { get; set; }
        public string data_transf { get; set; }
        public string resume { get; set; }
        public string credential { get; set; }

        public Student(string dni)
        {
            this.dni = dni;
        }

        public Student(string dni, string name, string last_name, string phone, string email, string license, string employed, string data_transf, string credential) : this(dni)
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
    }
}
