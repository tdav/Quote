using Quote.Global;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quote.Database.Models
{
    /// <summary>
    /// table tbQuote
    /// </summary>
    public class tbAuthor : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [StringLength(100)]
        public string Patronymic { get; set; }
    }
}
