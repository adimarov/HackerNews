using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsFunctionApp.Utils
{
    public class SearchRequest
    {
        [JsonProperty("search_text")]
        public string SearchText { get; set; }
    }
}
