using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CalaisPhpToCsharp
{
    public class OpenCalais
    {
        string outputformat = "application/json";
        string contentType = "text/html";

        string api_url = "https://api.thomsonreuters.com/permid/calais";
        string api_token = "JMN42A8jCAidt38AMELgyqu3dq2Lf7J0";
        string[] entities = null;

        public OpenCalais()
        {
            if (string.IsNullOrEmpty(api_token))
            {
                throw new Exception("An OpenCalais API token is required to use this class.");
            }       
        }

        public string getEntities()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(api_url);
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            client.DefaultRequestHeaders.Add("X-AG-Access-Token", api_token);            
            client.DefaultRequestHeaders.Add("outputFormat", "xml/rdf");   //xml/rdf

            var content = new StringContent("Trees are one of the most important things in the world. They give us shade. " +
                "When we are going somewhere in the sun, when we are tired, we sit under a tree to rest. We choose a tree because it is shady. " +
                "Trees give us food. We take fruits, vegetables, grains and green leaves from trees. We also eat roots like carrots, sweet potatoes, " +
                "manioc and beetroot. Trees give us wood to make houses, buildings, furniture etc. The most important things is that they give us " +
                "oxygen to breathe. Trees make our earth clean and beautiful.We must not cut trees.It takes only a few minutes to cut a tree but it " +
                "takes years to grow.");

            var respons = client.PostAsync("", content).Result;
            var responseResult = respons.Content.ReadAsStringAsync().Result;
            //responseResult = JsonConvert.SerializeObject(responseResult.ToString(), Formatting.Indented);

            return responseResult;
        }

    }
}
