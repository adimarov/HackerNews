using HackerNewsFunctionApp.Service;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HackerNewsFunctionAppTest
{
    [TestFixture]
    public class GetLatestNewsTests
    {
        [Test]
        public void TestNewsCount()
        {
            
            int count = GetNewsService().GetLatestNewsCount();
            Assert.AreEqual(count, 500);
        }

        [Test]
        public void TestMaxId()
        {
            int max_id = GetNewsService().GetMaxId();
            Assert.IsTrue(max_id > 0);
        }

        [Test]
        public void TestLatestNewsPage()
        {
            int max_id = GetNewsService().GetMaxId();
            var news = GetNewsService().GetLatestNews(max_id, 10);
            Assert.AreEqual(news.Count, 10);
        }

        [Test]
        public void TestSearchByText()
        {
            int max_id = GetNewsService().GetMaxId();
            var story = GetNewsService().GetNewsById(max_id);
            while(story.Type !="story")
            {
                max_id -= 1;
                story = GetNewsService().GetNewsById(max_id);
            }

            string search_string = (new List<string> { story.Text, story.By, story.Title }).Where(x => !string.IsNullOrEmpty(x)).FirstOrDefault();

            var search_story = GetNewsService().GetNewsByAuthorTitle(search_string.Substring(0, search_string.Length > 5 ? 5 : search_string.Length));

            Assert.IsNotNull(search_story);
        }

        private NewsService GetNewsService()
        {
            var config = new ConfigurationBuilder()
                               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                               .AddEnvironmentVariables()
                               .Build();

            NewsService service = new NewsService(config.GetSection("Values")["HackersNewsURL"], config.GetSection("Values")["RedisCache"]);
            return service;
        }

    }
}