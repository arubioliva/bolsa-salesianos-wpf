using BolsaSalesianos.database.pojos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database.services
{
    internal class VacantsService : WebService<Vacant>
    {
        public VacantsService() : base("vacants")
        {

        }

        public List<Vacant> GetVacantsByStudent(string dni_usuario)
        {
            try
            {
                string response = wc.UploadString(base_url + "?option=vacants_for_student", JsonFromPojo(new { dni = dni_usuario }));
                return JsonConvert.DeserializeObject<List<Vacant>>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
