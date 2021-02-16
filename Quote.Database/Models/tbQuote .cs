using Quote.Global;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quote.Database.Models
{
    /// <summary>
    /// table tbQuote
    /// </summary>

    public class tbQuote : BaseModel
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Text { get; set; }

        public int AuthorId { get; set; }
        public virtual tbAuthor Author { get; set; }
        public int CategoryId { get; set; }
        public virtual spCategory Category { get; set; }
    }
}
