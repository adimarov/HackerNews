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
using System.Collections.Generic;
using HackerNewsFunctionApp.Domain;

namespace HackerNewsFunctionApp
{
    public static class SearchNewsApi
    {
        [FunctionName("SearchNews")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var config = Configuration.GetConfig(context);

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            SearchRequest request = JsonConvert.DeserializeObject<SearchRequest>(content);

            NewsService service = new NewsService(config["HackersNewsURL"], config["RedisCache"]);

            var stories = service.GetNewsByAuthorTitle(request.SearchText);

            return new OkObjectResult(stories);
        }
    }
}
