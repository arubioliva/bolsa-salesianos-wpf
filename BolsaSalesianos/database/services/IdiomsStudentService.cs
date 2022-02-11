using BolsaSalesianos.database.pojos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database
{
    internal class IdiomsStudentService : WebService<pojos.IdiomStudent>
    {
        public IdiomsStudentService() : base("idioms_student")
        {

        }
        public List<IdiomStudent> GetIdiomsByStudent(string dni_usuario)
        {
            try
            {
                string response = wc.UploadString(base_url + "?option=idioms_by_student", JsonFromPojo(new {dni=dni_usuario}));
                return JsonConvert.DeserializeObject<List<IdiomStudent>>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}

