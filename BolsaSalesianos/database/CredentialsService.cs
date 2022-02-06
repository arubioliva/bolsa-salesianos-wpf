using BolsaSalesianos.pojos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database
{
    internal class CredentialsService : WebService<Credential>
    {

        public CredentialsService() : base("credentials")
        {

        }

        public Credential InsertAndGetCredential(Credential credential)
        {
            Insert(credential);
            return Fetch(new Credential { user = credential.user });
        }
    }
}
