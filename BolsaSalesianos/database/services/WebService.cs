using BolsaSalesianos.pojos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BolsaSalesianos.database
{
    internal class WebService<T>
    {
        public string base_url { get; set; }
        public WebClient wc;

        /* 
         * Constructor que recibira como parametro el complementario de la url que será el nombre de
         * la tabla a la que se quiere acceder.
         * 
         * También recibirá un tipo de dato con el que se crearan los metodos.
         */

        public WebService(string name)
        {
            base_url = "https://c4ytrsshj.gclientes.com/api_rubio/" + name;
            wc = new WebClient();
        }

        /* 
         * Realiza una consulta la cuál retorna un único objeto.
         */
        public T Fetch(T data)
        {
            try
            {
                string response = wc.UploadString(base_url + "?petition=one_object", JsonFromPojo(data));
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /* 
         * Realiza una consulta la cuál retorna una lista de datos filtrando los datos por un objeto del mismo tipo.
         */
        public List<T> FetchAllWithFilter(T data)
        {
            try
            {
                string response = wc.UploadString(base_url, JsonFromPojo(data));
                return JsonConvert.DeserializeObject<List<T>>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /* 
        * Realiza una consulta la cuál retorna una lista de datos.
        */
        public List<T> FetchAll()
        {
            try
            {
                string response = wc.UploadString(base_url, JsonFromPojo(new { }));
                return JsonConvert.DeserializeObject<List<T>>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /* 
        * Inserta un objeto del tipo indicado devolviendo un objeto de tipo status, que indicará si el ingreso
        * ha sido realizado correctamente o no.
        */
        public Status Insert(T data)
        {
            try
            {
                string response = wc.UploadString(base_url + "?petition=insert", JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                return JsonConvert.DeserializeObject<Status>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public Status Update(T data)
        {
            try
            {
                string response = wc.UploadString(base_url, "PUT", JsonConvert.SerializeObject(data));
                return JsonConvert.DeserializeObject<Status>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public Status Delete(T data)
        {
            try
            {
                string response = wc.UploadString(base_url, "DELETE", JsonFromPojo(data));
                return JsonConvert.DeserializeObject<Status>(response);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /* 
        * Convierte un json en un pojo ignorando de este aquellos valores con valores nulos.
        */
        public static string JsonFromPojo(object data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
        }
    }
}
