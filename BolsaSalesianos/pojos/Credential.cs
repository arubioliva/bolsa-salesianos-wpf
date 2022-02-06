using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.pojos
{
    class Credential
    {
        public string user { get; set; }
        public string pass { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string last_connection { get; set; }

        public Credential(string user, string pass)
        {
            this.user = user;
            this.pass = pass;
        }

        public Credential(string user)
        {
            this.user = user;
        }

        public Credential(string user, string pass, string type, string last_connection)
        {
            this.user = user;
            this.pass = pass;
            this.type = type;
            this.last_connection = last_connection;
        }

        public Credential()
        {

        }
    }
}
