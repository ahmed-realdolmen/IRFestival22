using Newtonsoft.Json;
using System;

namespace IRFestival.Api.Domain
{
    public class Article
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("tag")]
        public string Tag { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public enum Status
    {
        Published,
        Unpublished
    }
}