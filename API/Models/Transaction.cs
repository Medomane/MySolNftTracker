using MyLibrary;
using System;

namespace MySolNftTracker.Models
{
    public class Transaction
    {
        public object Id { get; set; }
        public string Signature { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Tool { get; set; }
        public bool IncludeFee { get; set; }
        public decimal Profit { get; set; }
        public decimal Fee { get; set; }
        public TransactionMode Mode { get; set; }
        public string Comment { get; set; }

        [My(MyAttribute.ActionType.ForeignKey)]
        public NonFungibleToken NonFungibleToken { get; set; }

        [My(MyAttribute.ActionType.ForeignKey)]
        public Wallet Wallet { get; set; }

        [My(MyAttribute.ActionType.ForeignKey)]
        public Transaction BuyTransaction { get; set; }

        public override string ToString() => "#";
        
    }

    public class ApiTransaction
    {
        public string Signature { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string TokenMint { get; set; }
        public string CollectionSymbol { get; set; }
        public long BlockTime { get; set; }
        public string Buyer { get; set; }
        public string Seller { get; set; }
        public decimal Price { get; set; }

        public bool IsBuy(Wallet wallet) => Buyer == wallet.Address;

        /*public (Transaction, string) GetTransaction(Wallet wallet)
        {
            var transaction = new Transaction
            {
                Wallet = wallet,
                Date = DateTimeOffset.FromUnixTimeSeconds(BlockTime).DateTime,
                Price = Price,
                Signature = Signature,
                IncludeFee = false,
                Mode = Buyer == wallet.Address ? TransactionMode.Buy : TransactionMode.Sell
            };
            var nftRes = _attr.Get<NonFungibleToken>($"MintAddress = {TokenMint.ToSqlString()}");
            if (nftRes.Count > 0) transaction.NonFungibleToken = nftRes[0];
            else
            {
                var (nft, msg) = NonFungibleToken.Get(TokenMint);
                if (nft != null) transaction.NonFungibleToken = nft;
                else return (null, msg);
            }
            if (transaction.NonFungibleToken != null) return (transaction, null);
            return (null, "No result is found !!!");
        }*/
    }

    public enum TransactionMode
    {
        Buy,
        Sell
    }
}