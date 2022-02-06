using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BolsaSalesianos.pojos;

namespace BolsaSalesianos.database
{
    internal class EnterprisesServices : WebService<Enterprise>
    {
        public EnterprisesServices() : base("enterprises")
        {

        }
    }
}
