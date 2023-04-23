using MyLibrary;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;

namespace MySolNftTracker.Models
{
    public class NonFungibleToken
    {
        [JsonIgnore]
        public object Id { get; set; }
        public string MintAddress { get; set; }
        public string Name { get; set; }
        public decimal SellerFeeBasisPoints { get; set; }

        [My(MyAttribute.ActionType.DbIgnore)]
        public string Image { get; set; }

        public string Rarity { get; set; }
        public int RarityRank { get; set; }

        [My(MyAttribute.ActionType.DbIgnore)]
        [JsonProperty(PropertyName = "collection")]
        public string CollectionSymbol { get; set; }

        [JsonIgnore]
        [My(MyAttribute.ActionType.ForeignKey)]
        public Collection Collection { get; set; }

        public override string ToString() => $"{Name}.";

        public static NonFungibleToken Get(string mintAddress)
        {
            Thread.Sleep(HelperFunctions.DurationBetweenCalls);
            using (var client = new HttpClient())
            {
                var res = client.GetAsync($"{HelperFunctions.BaseUrl}tokens/{mintAddress}");
                res.Wait();
                var data = res.Result;
                var content = data.Content.ReadAsStringAsync();
                content.Wait();
                if (data.IsSuccessStatusCode)
                {
                    var nft = JsonConvert.DeserializeObject<NonFungibleToken>(content.Result);
                    nft.SellerFeeBasisPoints /= 100;
                    return nft;
                }
            }
            return null;
        }
    }
}