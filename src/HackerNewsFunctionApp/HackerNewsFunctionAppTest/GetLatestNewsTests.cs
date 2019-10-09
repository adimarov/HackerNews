using HackerNewsFunctionApp.Service;
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
            NewsService service = new NewsService("https://hacker-news.firebaseio.com/v0/");
            int count = service.GetLatestNewsCount();
            Assert.AreEqual(count, 500);
        }

        [Test]
        public void TestMaxId()
        {
            NewsService service = new NewsService("https://hacker-news.firebaseio.com/v0/");
            int max_id = service.GetMaxId();
            Assert.IsTrue(max_id > 0);
        }

        [Test]
        public void TestLatestNewsPage()
        {
            NewsService service = new NewsService("https://hacker-news.firebaseio.com/v0/");
            int max_id = service.GetMaxId();
            var news = service.GetLatestNews(max_id, 10);
            Assert.AreEqual(news.Count, 10);
        }

        [Test]
        public void TestSearchByText()
        {
            NewsService service = new NewsService("https://hacker-news.firebaseio.com/v0/");
            int max_id = service.GetMaxId();
            var story = service.GetNewsById(max_id);
            while(story.Type !="story")
            {
                max_id -= 1;
                story = service.GetNewsById(max_id);
            }

            string search_string = (new List<string> { story.Text, story.By, story.Title }).Where(x => !string.IsNullOrEmpty(x)).FirstOrDefault();

            var search_story = service.GetNewsByAuthorTitle(search_string.Substring(0, search_string.Length > 5 ? 5 : search_string.Length));

            Assert.IsNotNull(search_story);
        }

    }
}