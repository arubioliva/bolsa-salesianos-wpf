using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database.pojos
{
    internal class Selection
    {
        public int id { get; set; }
        public string student { get; set; }
        public string date { get; set; }
        public int selected { get; set; }
        public int vacant { get; set; }
    }
}
