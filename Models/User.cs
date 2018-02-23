using System;
using System.Collections.Generic;

namespace bank_proj.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        public int userid { get; set; }
        public string first { get; set; }
        public string last { get; set; }        
        public string email { get; set; }
        public string password { get; set; }
        public float balance { get;set; }
        public DateTime created { get;set; }
        public DateTime updated { get;set; }  

        public List<Transaction> transactions { get; set; }
 
        public User()
        {
            transactions = new List<Transaction>();
        }      
    }
}