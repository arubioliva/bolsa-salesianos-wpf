using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.pojos
{
    internal class Status
    {

        public int status { get; set; }
        public Error error { get; set; }

        public Status()
        {
        }

        public Status(int status, Error error)
        {
            this.status = status;
            this.error = error;
        }
    }

    internal class Error
    {

        public List<String> errorInfo { get; set; }

        public Error()
        {
        }

        public Error(List<String> errorInfo)
        {
            this.errorInfo = errorInfo;
        }


    }
}
