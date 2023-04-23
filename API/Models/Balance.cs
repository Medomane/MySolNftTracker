using MyLibrary;
using System;

namespace MySolNftTracker.Models
{
    public abstract class Balance
    {
        public object Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public void UpdateBalance() => _attr.Update<Balance>($"Amount = {Amount.ToSqlNumber()}", $"id = {Id}");
    }
    public class BalanceHistory : Balance
    {
        [My(MyAttribute.ActionType.ForeignKey)]
        public Wallet Wallet { get; set; }
    }
    public class BalanceLog : Balance
    {
        [My(MyAttribute.ActionType.ForeignKey)]
        public AppUser User { get; set; }
    }
}