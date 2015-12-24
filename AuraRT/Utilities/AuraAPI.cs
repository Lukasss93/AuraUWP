using AuraRT.Serializer;
using AuraRT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AuraRT.Utilities
{
    public class AuraAPI
    {
        public static async Task<AuraAPIResult<T>> SendRequest<T>(string url, RequestType type, Dictionary<string, string> parameters = null)
        {
            AuraAPIResult<T> apiresult = new AuraAPIResult<T>();

            parameters = (parameters == null) ? new Dictionary<string, string>() : parameters;

            //inizializza risultato
            string content = null;

            //avvio la connessione
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;

            if(type == RequestType.GET)
            {
                response = await client.GetAsync(url + "?" + HttpUtilities.ParametersToString(parameters));
            }
            else
            {
                HttpContent postdata = new FormUrlEncodedContent(parameters);
                response = await client.PostAsync(url, postdata);
            }

            //leggi risposta
            if(response.IsSuccessStatusCode)
            {
                //leggi risultato
                content = await response.Content.ReadAsStringAsync();

                try
                {
                    apiresult = Json.Deserialize<AuraAPIResult<T>>(content);
                }
                catch(Exception ex)
                {
                    apiresult.SetError("EXCEPTION",ex.HResult + "\n\n" +ex.Message + "\n\n" + content);
                }
            }
            else if(HttpUtilities.IsConnectedToInternet() == false)
            {
                apiresult.SetError("INTERNET_NOTFOUND", "Your internet connection is not found.");
            }
            else
            {
                apiresult.SetError("HOST_NOTFOUND", "Api url not found.");
            }

            return apiresult;
        }
    }

    public class AuraAPIResult<T>
    {
        public bool status { get; set; }
        public AuraAPIError error { get; set; }
        public T output { get; set; }

        public AuraAPIResult()
        {
            this.status = false;
            this.error = null;
            this.output = default(T);
        }
        
        public void SetError(string code, string message)
        {
            this.status = false;
            this.error = new AuraAPIError(code, message);
            this.output = default(T);
        }
                
    }

    public enum RequestType
    {
        GET,
        POST
    }

    public class AuraAPIError
    {

        public string code { get; set; }
        public string message { get; set; }

        public AuraAPIError(string code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }

}
