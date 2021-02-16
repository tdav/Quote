using Quote.Global;
using System;
using System.ComponentModel.DataAnnotations;

namespace Quote.Database.Models
{
    public partial class spRole : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string UserAccess { get; set; }
    }
}
