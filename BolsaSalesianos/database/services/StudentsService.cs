using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BolsaSalesianos.database.pojos;
using Newtonsoft.Json;

namespace BolsaSalesianos.database
{
    internal class StudentsService : WebService<BolsaSalesianos.pojos.Student>
    {
        public StudentsService() : base("students")
        {

        }

        public List<BolsaSalesianos.pojos.Student> FetchAllByStudy(string filter)
        {
            try
            {
                string response = wc.UploadString(base_url + "?option=studies_vacants", filter);
                return JsonConvert.DeserializeObject<List<BolsaSalesianos.pojos.Student>>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

    }
}
