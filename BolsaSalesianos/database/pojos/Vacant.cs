using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database.pojos
{
    internal class Vacant
    {
        public int id { get; set; }
        public string workstation { get; set; }
        public int vehicle { get; set; }
        public int work_experience { get; set; }
        public int salary { get; set; }
        public string study { get; set; }
        public string working_day { get; set; }
        public int hours { get; set; }
    }
}
