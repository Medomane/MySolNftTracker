using MyLibrary;
using System;

namespace MySolNftTracker.Models
{
    public class OtherOperation
    {
        public object Id { get; set; }
        public string Caption { get; set; }
        public string Link { get; set; }
        public decimal Profit { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        [My(MyAttribute.ActionType.ForeignKey)]
        public Wallet Wallet { get; set; }

        [My(MyAttribute.ActionType.ForeignKey)]
        public AppUser User { get; set; }

        public override string ToString() => $"{Caption}.";
    }
}