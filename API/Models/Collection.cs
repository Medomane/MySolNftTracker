using Newtonsoft.Json;
using System.Net.Http;
using MyLibrary;

namespace MySolNftTracker.Models
{
    public class Collection
    {
        [JsonIgnore]
        public object Id { get; set; }
        public string Symbol { get; set; }

        [My(MyAttribute.ActionType.DbIgnore)]
        public string Name { get; set; }
        [My(MyAttribute.ActionType.DbIgnore)]
        public string Description { get; set; }
        [My(MyAttribute.ActionType.DbIgnore)]
        public string Image { get; set; }
        [My(MyAttribute.ActionType.DbIgnore)]
        public string Twitter { get; set; }
        [My(MyAttribute.ActionType.DbIgnore)]
        public string Discord { get; set; }
        [My(MyAttribute.ActionType.DbIgnore)]
        public string Website { get; set; }
        [My(MyAttribute.ActionType.DbIgnore)]
        public decimal FloorPrice { get; set; }
        [My(MyAttribute.ActionType.DbIgnore)]
        public bool IsFlagged { get; set; }
        public override string ToString() => $"{Name}.";

        public static Collection Get(string symbol)
        {
            System.Threading.Thread.Sleep(HelperFunctions.DurationBetweenCalls);
            using (var client = new HttpClient())
            {
                var res = client.GetAsync($"{HelperFunctions.BaseUrl}collections/{symbol}");
                res.Wait();
                var data = res.Result;
                var content = data.Content.ReadAsStringAsync();
                content.Wait();
                if (data.IsSuccessStatusCode) return JsonConvert.DeserializeObject<Collection>(content.Result);
            }
            return null;
        }
    }
}