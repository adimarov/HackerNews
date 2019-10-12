using HackerNewsFunctionApp.Domain;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace HackerNewsCore.Service
{
    public static class CacheService
    {
        private static string _url;
        public static void Initialize(string url)
        {
            _url = url;

        }
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(_url);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public static Story GetStory(int id)
        {
            RedisValue val = Connection.GetDatabase().StringGet(id.ToString());
            if (val == RedisValue.Null) { return null;}
            else return JsonConvert.DeserializeObject<Story>(val);
        }

        public static bool SetStory(Story story)
        {
            return Connection.GetDatabase().StringSet(story.Id.ToString(), JsonConvert.SerializeObject(story));
        }
    }
}
