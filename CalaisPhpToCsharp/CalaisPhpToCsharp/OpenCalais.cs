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
            client.DefaultRequestHeaders.Add("outputFormat", "application/json");   //xml/rdf

            var content = new StringContent("Open Calais is a sophisticated Thomson Reuters web service that attaches intelligent metadata-tags to your unstructured content enabling powerful text analytics. The Calais natural language processing engine automatically analyzes and tags your input files in such a way that your consuming application can both easily pinpoint relevant data, and effectively leverage the invaluable intelligence and insights contained within the text." +
                "Open Calais analyzes the semantic content of your input files using a combination of statistical, machine-learning, and custom pattern - based methods.The developed by the Text Metadata Services(TMS) group at Thomson Reuters output highly accurate and detailed metadata." +
                "Open Calais also maps your metadata-tags to Thomson Reuters unique IDs.This supports disambiguation(and linking) of data across all documents processed by Calais, and also offers you the opportunity to further enrich your data with related information from the Thomson Reuters datasets.");

            var respons = client.PostAsync("", content).Result;
            var responseResult = respons.Content.ReadAsStringAsync().Result;
            //responseResult = JsonConvert.SerializeObject(responseResult.ToString(), Formatting.Indented);

            return responseResult;
        }

    }
}
