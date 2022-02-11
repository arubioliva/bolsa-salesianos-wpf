using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BolsaSalesianos.pojos;

namespace BolsaSalesianos.database
{
    internal class EnterprisesService : WebService<Enterprise>
    {
        public EnterprisesService() : base("enterprises")
        {

        }
    }
}
