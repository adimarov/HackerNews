using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using HackerNewsFunctionApp.Utils;
using RestSharp;
using System.Linq;
using HackerNewsFunctionApp.Service;

namespace HackerNewsFunctionApp
{
    public static class SearchNewsApi
    {
        [FunctionName("SearchNews")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var config = Configuration.GetConfig(context);
            string search_text = req.Headers.Where(x => x.Key == "search_text").FirstOrDefault().Value;

            NewsService service = new NewsService(config["HackersNewsURL"]);

            var story = service.GetNewsByAuthorTitle(search_text);

            return new OkObjectResult(story);
        }
    }
}
