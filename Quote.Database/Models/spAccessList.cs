using Quote.Global;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quote.Database.Models
{
    /// <summary>
    /// таблица Доступ пользователя
    /// </summary>
    /// 

    public partial class spAccessList : BaseModel 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}


