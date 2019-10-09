using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using HackerNewsFunctionApp.Utils;
using System.Linq;
using HackerNewsFunctionApp.Service;

namespace HackerNewsFunctionApp
{
    public static class GetLatestNewsApi
    {
        [FunctionName("GetLatestNews")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {

            var config = Configuration.GetConfig(context);
            int id = int.Parse(req.Headers.Where(x => x.Key == "id").FirstOrDefault().Value);
            int page = int.Parse(req.Headers.Where(x => x.Key == "page").FirstOrDefault().Value);

            NewsService service = new NewsService(config["HackersNewsURL"]);

            var stories = service.GetLatestNews(id, page);

            return new OkObjectResult(stories);

        }
    }
}
