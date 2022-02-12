using BolsaSalesianos.database.pojos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database
{
    internal class IdiomsStudentService : WebService<IdiomStudent>
    {
        public IdiomsStudentService() : base("idioms_student")
        {

        }
        public List<Idiom> GetIdiomsByStudent(string dni_usuario)
        {
            try
            {
                string response = wc.UploadString(base_url + "?option=idioms_by_student", JsonFromPojo(new {dni=dni_usuario}));
                return JsonConvert.DeserializeObject<List<Idiom>>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}

