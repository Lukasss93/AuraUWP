using AuraRT.Serializer;
using AuraRT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lukasss93
{
    public class AuraAPI<T>
    {
        public bool status { get; private set; }
        public AuraAPIError error { get; private set; }
        public T output { get; private set; }

        public AuraAPI()
        {
            this.status = false;
            this.error = null;
            this.output = default(T);
        }

        public static async Task SendRequest<T>(string url, RequestType type, Dictionary<string, string> parameters = null)
        {
            AuraAPI<T> apiresult = new AuraAPI<T>();

            parameters = (parameters == null) ? new Dictionary<string, string>() : parameters;

            //inizializza risultato
            string content = null;
            AuraAPI<T> result = null;

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
                    result = Json.Deserialize<AuraAPI<T>>(content);
                }
                catch(Exception ex)
                {
                    apiresult.SetError(
                        "EXCEPTION-" + ex.HResult,
                        "Message: " + ex.Message + "\n\n" +
                        "Content: " + content);
                }

                apiresult.status = result.status;
                apiresult.error = result.error;
                apiresult.output = result.output;
            }
            else if(HttpUtilities.IsConnectedToInternet() == false)
            {
                apiresult.SetError("CONNECTION_NOTFOUND", "Your internet connection is not found.");
            }
            else
            {
                apiresult.SetError("NOTFOUND", "Api url not found.");
            }
        }

        private void SetError(string code, string message)
        {
            this.status = false;
            this.error = new AuraAPIError(code, message);
            this.output = default(T);
        }


        public enum RequestType
        {
            GET,
            POST
        }
        
    }

    public class AuraAPIError
    {

        public string code { get; private set; }
        public string message { get; private set; }

        public AuraAPIError(string code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }

}
