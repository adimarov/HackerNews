using System;
using StackExchange.Redis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HackerNewsFunctionApp.Utils;
using System.Linq;
using HackerNewsFunctionApp.Service;
using HackerNewsFunctionApp.Domain;
using System.Collections.Generic;

namespace HackerNewsFunctionApp
{
    public static class GetLatestNewsApi
    {
        [FunctionName("GetLatestNews")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context
            )
        {
            List<Story> stories = new List<Story>();
            try
            {

                var config = Configuration.GetConfig(context);

                int id = int.Parse(req.Headers.Where(x => x.Key == "story_id").FirstOrDefault().Value);
                int size = int.Parse(req.Headers.Where(x => x.Key == "page_size").FirstOrDefault().Value);

                NewsService service = new NewsService(config["HackersNewsURL"], config["RedisCache"]);

                stories = service.GetLatestNews(id, size);
                return new OkObjectResult(stories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
