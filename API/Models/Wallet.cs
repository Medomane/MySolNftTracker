using MyLibrary;

namespace MySolNftTracker.Models
{
    [My(MyAttribute.ActionType.Duplicate)]
    public class Wallet
    {
        public object Id { get; set; }
        public string Caption { get; set; }

        [My(MyAttribute.ActionType.Duplicate)]
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public bool Activated { get; set; }
        public int Order { get; set; }
        public string Comment { get; set; }

        [My(MyAttribute.ActionType.ForeignKey, MyAttribute.ActionType.Duplicate)]
        public AppUser User { get; set; }

        public override string ToString() => $"{Caption}.";

        /*public (decimal oldBalance, decimal newBalance) CheckBalance(DateTime now)
        {
            var oldBalance = Balance;
            UpdateBalance();
            var newBalance = Balance;
            if (Balance == newBalance) return (oldBalance, newBalance);
            var balanceToDay = _attr.Get<BalanceHistory>($"YEAR([Date]) = {now.Year} AND MONTH([Date]) = {now.Month} AND DAY([Date]) = {now.Day} AND WalletId = {Id}");
            if (balanceToDay != null && balanceToDay.Count > 0)
            {
                var obj = balanceToDay[0];
                if (obj.Amount != Balance)
                {
                    obj.Amount = Balance;
                    obj.UpdateBalance();
                }
            }
            else
            {
                new BalanceHistory
                {
                    Amount = Balance,
                    Date = now,
                    Wallet = this
                }.Save();
            }
            return (oldBalance, newBalance);
        }

        public void UpdateBalance()
        {
            try
            {
                Thread.Sleep(Setting.DurationBetweenCalls);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", _func.GetAppSetting("token"));
                    var res = client.GetAsync($"https://public-api.solscan.io/account/{Address}");
                    res.Wait();
                    var data = res.Result;
                    var content = data.Content.ReadAsStringAsync();
                    content.Wait();
                    if (data.IsSuccessStatusCode)
                    {
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(content.Result);
                        Balance = json.ContainsKey("lamports") ? decimal.Divide(json["lamports"].ToDecimal(), 1000000000) : 0;
                        _attr.Update<Wallet>($"[Balance] = {Balance.ToSqlNumber()}", $"Id = {Id}");
                    }
                    else if (content.Result.Has("Too Many Request"))
                    {
                        Thread.Sleep(70000);
                        UpdateBalance();
                    }
                    /*else
                    {
                        Balance = 0;
                        _attr.Update<Wallet>($"Id = {Id}", "[Balance] = 0");
                    }/
                }
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }
        public List<ApiTransaction> GetActivities(bool checkExistence)
        {
            var list = new List<ApiTransaction>();
            const int limit = 500;
            var offset = 0;
            while (true)
            {
                Thread.Sleep(Setting.DurationBetweenCalls);
                using (var client = new HttpClient())
                {
                    var res = client.GetAsync($"{Helpers.ApiBaseUrl}wallets/{Address}/activities?offset={offset}&limit={limit}");
                    res.Wait();
                    var data = res.Result;
                    var content = data.Content.ReadAsStringAsync();
                    content.Wait();
                    if (data.IsSuccessStatusCode)
                    {
                        var dt = JsonConvert.DeserializeObject<List<ApiTransaction>>(content.Result);
                        if (dt.Count > 0)
                        {
                            IEnumerable<ApiTransaction> listRes;
                            if (checkExistence)
                            {
                                var transactions = _db.Where<Transaction>($"WalletId = {Id}").Rows.Cast<DataRow>();
                                listRes = dt.Where(trx => trx.Type == "buyNow"
                                                          /*&& DateTimeOffset.FromUnixTimeSeconds(trx.BlockTime).DateTime >= Setting.TransactionsMaxDate /
                                                          && transactions.All(dr => dr.StringValueOf("TransactionId") != trx.Signature));
                            }
                            else listRes = dt.Where(trx => trx.Type == "buyNow");
                            if (!listRes.Any()) break;
                            list.AddRange(listRes);
                        }
                        else break;
                    }
                }
                offset += limit;
            }
            return list;
        }


        public static DataTable Find(bool cb = false, string cond = null) => _db.Query($@"SELECT Id,{(cb ? "LEFT([Address], 5) + '...' + RIGHT([Address], 5) + ' - ' + [Caption]" : "")} [Caption], Address, Balance, Activated, Comment FROM {_attr.GetName<Wallet>()} WHERE UserId = {AppUser.Current.Id} {(cond.IsNotNull() ? $" AND {cond}" : "")} ORDER BY [Order]");
        public static DataTable All() => Find();*/
    }
}