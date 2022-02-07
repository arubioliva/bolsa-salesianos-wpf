using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BolsaSalesianos.pojos;
using Newtonsoft.Json;

namespace BolsaSalesianos.database
{
    internal class StudentsServices : WebService<Student>
    {
        public StudentsServices() : base("students")
        {

        }
    }
}
