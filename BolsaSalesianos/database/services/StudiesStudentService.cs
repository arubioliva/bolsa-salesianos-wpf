using BolsaSalesianos.database.pojos;
using BolsaSalesianos.pojos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database
{
    internal class StudiesStudentService : WebService<StudyStudent>
    {
        public StudiesStudentService() : base("studies_student")
        {

        }
        
    }
}
