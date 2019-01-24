using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

namespace SkyriseRecruitWebAplication.Models
{
    public class HTMLAnalizator
    {
        private const String API_URL = "https://mercury.postlight.com/parser?url=";
        private readonly String X_API_KEY;


        public HTMLAnalizator()
        {         
            X_API_KEY = ConfigurationManager.ConnectionStrings["x-api-key"].ConnectionString;
        }

        public List<KeyValuePair<String, int>> GetListOfOccurence(String html)
        {
            //return list of nodes value of instances as Key,Value Pairs         

            Dictionary<string, int> nodesOccurenceCounts = new Dictionary<string, int>();
            List<HtmlNode> listOfNodes = GetListOfNodes(html);
            foreach (var item in listOfNodes
                                    .GroupBy(a => a.Name)
                                    .Select(x => x.First()))
            {
                nodesOccurenceCounts.Add(item.Name, listOfNodes.Count(a => a.Name == item.Name));                
            }
            return  nodesOccurenceCounts.OrderByDescending(o => o.Value).ToList(); ;
        }

        private List<HtmlNode> GetListOfNodes(String html)
        {
            //return list every node that has occured in html string
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode.SelectNodes("//*");
            return htmlNodes.ToList();
        }

        public RequestResult GetAPIResult(String url)
        {
            RequestResult requestResult;
            //getting response from API and return as RequestResult type.
            HttpWebRequest request = PrepareRequest(url);
            try
            {
                WebResponse response = request.GetResponse();
                requestResult = DecodeResponse(response);
            }
            catch (WebException ex)
            {
                throw ex;
            }
            return requestResult;
        }

        private RequestResult DecodeResponse(WebResponse response)
        {
            //convert API respone to RequestResult type.
            RequestResult requestResult;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                requestResult = JsonConvert.DeserializeObject<RequestResult>(reader.ReadToEnd());
            }
            return requestResult;
        }

        private HttpWebRequest PrepareRequest(string url)
        {
            //prepare request for API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_URL + url);
            request.Headers["x-api-key"] = X_API_KEY;
            // Because System.Net.Mime,MediaTypeNames doesn't contain "application/json" value
            request.ContentType = "application/json"; 
            return request;
        }
    }
}