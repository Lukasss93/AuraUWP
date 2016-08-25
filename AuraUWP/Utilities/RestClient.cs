using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AuraUWP.Utilities
{
    public class RestClient
    {
        private HttpClient client = new HttpClient();

        public RestClient()
        {
            Parameters = new Dictionary<string, string>();
            Headers = new Dictionary<string, string>();
        }

        private Dictionary<string, string> parameters;
        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        private Dictionary<string,string> headers;

        public Dictionary<string,string> Headers
        {
            get { return headers; }
            set { headers = value; }
        }


        public async Task<RestResponse> SendRequest(string url, RequestType type)
        {
            //add headers
            foreach(var header in Headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
                        
            //start request + set parameters
            HttpResponseMessage response = null;
            switch(type)
            {
                case RequestType.GET:
                    response = await client.GetAsync(url + HttpUtilities.ParametersToString(Parameters));
                    break;
                case RequestType.POST:
                    response = await client.PostAsync(url, HttpUtilities.ParametersToHttpContent(Parameters));
                    break;
                case RequestType.PUT:
                    response = await client.PutAsync(url, HttpUtilities.ParametersToHttpContent(Parameters));
                    break;
                case RequestType.DELETE:
                    response = await client.DeleteAsync(url + HttpUtilities.ParametersToString(Parameters));
                    break;
            }
                        
            //initialize restclientresponse
            RestResponse restresponse = new RestResponse()
            {
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                Content = await response.Content.ReadAsStringAsync(),
                Headers=response.Headers,
                StatusCode=response.StatusCode
            };    
            
            return restresponse;
        }

        public async Task<RestResponseJson<T>> SendRequest<T>(string url, RequestType type)
        {
            var response = await SendRequest(url, type);

            RestResponseJson<T> output = new RestResponseJson<T>();
            output.Response = response;


            if(response.IsSuccessStatusCode)
            {
                output.JsonObject = JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                output.JsonObject = default(T);
            }

            return output;
        }
    }

    public class RestResponse
    {
        public bool IsSuccessStatusCode { get; internal set; }
        public string Content { get; internal set; }        
        public HttpResponseHeaders Headers { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
    }

    public class RestResponseJson<T>
    {
        public RestResponse Response { get; internal set; }
        public T JsonObject { get; internal set; }
    }

    public enum RequestType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
