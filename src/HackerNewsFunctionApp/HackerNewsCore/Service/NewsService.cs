﻿using HackerNewsFunctionApp.Domain;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsFunctionApp.Service
{
    public class NewsService
    {
        private string _url { get; set; }
        private RestClient _client { get; set; }
        public NewsService(string url)
        {
            _url = url;
            _client = new RestClient(_url);
        }
        public List<Story> GetLatestNews(int id, int page)
        {
            var story_ids = JsonConvert.DeserializeObject<int[]>(_client.Execute(new RestRequest("newstories.json", Method.GET)).Content);

            var stories = LoadObjectsFeedFast<Story>(story_ids.Where(x => x < id).Take(page).ToArray());

            return stories.OrderBy(x => x.Id).ToList();
        }

        public int GetLatestNewsCount()
        {
            var result = _client.Execute(new RestRequest("newstories.json", Method.GET)).Content;

            var story_ids = JsonConvert.DeserializeObject<int[]>(result);

            return story_ids.Length;
        }

        public int GetMaxId()
        {
            var result = _client.Execute(new RestRequest("maxitem.json", Method.GET)).Content;

            var max_id = int.Parse(result);

            return max_id;
        }

        public Story GetNewsByAuthorTitle(string SearchText)
        {
            var result = _client.Execute(new RestRequest("newstories.json", Method.GET)).Content;

            var story_ids = JsonConvert.DeserializeObject<int[]>(result).OrderByDescending(x=>x);

            while(story_ids.Count() > 0)
            {
                var stories = LoadObjectsFeedFast<Story>(story_ids.Take(10).ToArray());
                Story story = stories.Where(x => x.By.Contains(SearchText) 
                                                || x.Title.Contains(SearchText) 
                                                || x.Text.Contains(SearchText)).FirstOrDefault();
                if (story != null) return story;
                stories.RemoveRange(0, story_ids.Count() > 10 ? 10: story_ids.Count());
            }

            return null;
        }

        public Story GetNewsById(int id)
        {
            var response = _client.Execute(new RestRequest("item/" + id + ".json", Method.GET));

            Story story = JsonConvert.DeserializeObject<Story>(response.Content);

            return story;
        }

        private List<T> LoadObjectsFeedFast<T>(int[] ids)
        {
            List<Action> feedTasks = new List<Action>();

            ConcurrentStack<T> stack = new ConcurrentStack<T>();

            Parallel.ForEach(ids.AsEnumerable(), (id) =>
                    {
                        var response = _client.Execute(new RestRequest("item/" + id + ".json", Method.GET));
                        stack.Push(JsonConvert.DeserializeObject<T>(response.Content));
                    }
            );

            return new List<T>(stack.ToArray());
        }

    }
}
