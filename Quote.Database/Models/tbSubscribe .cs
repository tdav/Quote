using Quote.Global;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quote.Database.Models
{
    /// <summary>
    /// table tbQuote
    /// </summary>
    public class tbSubscribe : BaseModel
    {
        public int Id { get; set; }
        public int SubscribeUserId { get; set; }
        public virtual tbUser SubscribeUser { get; set; }
        public int SenderId { get; set; }
        public virtual spSender Sender { get; set; }
    }
}
