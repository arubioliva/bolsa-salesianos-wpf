using BolsaSalesianos.database.pojos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database
{
    internal class IdiomsService : WebService<Idiom>
    {
        public IdiomsService() : base("idioms")
        {

        }
    }
}
