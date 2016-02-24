using AuraRT.Serializer;
using AuraRT.Utilities;
using Newtonsoft.Json;
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
            string content = null;

            try
            {
                //avvio la connessione
                HttpClient client = new HttpClient();
                HttpResponseMessage response = null;

                switch(type)
                {
                    case RequestType.GET:
                        response = await client.GetAsync(url + HttpUtilities.ParametersToString(parameters));
                        break;
                    case RequestType.POST:
                        response = await client.PostAsync(url, HttpUtilities.ParametersToHttpContent(parameters));
                        break;
                    case RequestType.PUT:
                        response = await client.PutAsync(url, HttpUtilities.ParametersToHttpContent(parameters));
                        break;
                    case RequestType.DELETE:
                        response = await client.DeleteAsync(url + HttpUtilities.ParametersToString(parameters));
                        break;
                }

                //leggi risposta
                if(response.IsSuccessStatusCode)
                {
                    //leggi risultato
                    content = await response.Content.ReadAsStringAsync();

                    if(!Json.isValidJson(content))
                    {
                        apiresult.SetError("INVALID_CONTENT", "Url content is not json.");
                    }
                    else if(!Json.isValid<AuraAPIResult<T>>(content))
                    {
                        apiresult.SetError("INVALID_JSON", "Json content is not valid.");
                    }
                    else
                    {
                        apiresult = Json.Deserialize<AuraAPIResult<T>>(content);
                    }
                }
                else if(HttpUtilities.IsConnectedToInternet() == false)
                {
                    apiresult.SetError("INTERNET_NOT_FOUND", "Your internet connection is not found.");
                }
                else
                {
                    apiresult.SetError("API_NOT_FOUND", "Api url not found.");
                }

                apiresult.content = content;

            }
            catch(Exception ex)
            {
                apiresult.SetError("EXCEPTION",Json.Serialize(new AuraAPIException(ex.HResult.ToString(),ex.Message,ex.StackTrace,content)));
            }

            return apiresult;
        }
    }

    public class AuraAPIResult<T>
    {
        public bool status { get; set; }
        public AuraAPIError error { get; set; }
        public T output { get; set; }

        [JsonIgnore]
        public string content { get; set; }

        public AuraAPIResult()
        {
            this.status = false;
            this.error = null;
            this.output = default(T);
            this.content = null;
        }
        
        internal void SetError(string code, string message)
        {
            this.status = false;
            this.error = new AuraAPIError(code, message);
            this.output = default(T);
        }
                
    }

    public enum RequestType
    {
        GET,
        POST,
        PUT,
        DELETE
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

    public class AuraAPIException
    {
        public string hresult { get; set; }
        public string message { get; set; }
        public string stacktrace { get; set; }
        public string urlcontent { get; set; }

        public AuraAPIException(string hres, string mes, string st, string con)
        {
            this.hresult = hres;
            this.message = mes;
            this.stacktrace = st;
            this.urlcontent = con;
        }
    }

}
