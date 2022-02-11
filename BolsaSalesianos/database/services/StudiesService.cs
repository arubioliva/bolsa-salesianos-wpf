using BolsaSalesianos.database.pojos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database.services
{
    internal class StudiesService : WebService<Study>
    {
        public StudiesService() : base("studies")
        {

        }
    }
}
