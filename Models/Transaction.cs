using System;

namespace bank_proj.Models
{
    public class Transaction : BaseEntity
    {
        public int transactionid { get;set; }
        public float amount { get;set; }
        public int userid { get; set; }
        public User User { get; set; }
        public DateTime date { get;set; }
    }
}