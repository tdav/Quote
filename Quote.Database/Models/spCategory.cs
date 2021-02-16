using Quote.Global;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quote.Database.Models
{
    /// <summary>
    /// table spCategory
    /// </summary>
    public class spCategory : BaseModel
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
    }
}
