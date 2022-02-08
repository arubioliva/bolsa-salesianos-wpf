using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.pojos
{
    internal class Enterprise
    {
        public string cif { get; set; }
        public string name { get; set; }
        public string contact_person { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int credential { get; set; }

        public Enterprise(string cif, string name, string contact_person, string phone, string email, int credential) : this(cif)
        {
            this.name = name;
            this.contact_person = contact_person;
            this.phone = phone;
            this.email = email;
            this.credential = credential;
        }

        public Enterprise()
        {
        }

        public Enterprise(string cif)
        {
            this.cif = cif;
        }

    }

    
}
