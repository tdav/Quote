using Quote.Global;
using System;
using System.ComponentModel.DataAnnotations;

namespace Quote.Database.Models
{
    /// <summary>
    /// таблица Пользователи
    /// </summary>

    public partial class tbUser : BaseModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
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

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [Required]
        public Guid RoleId { get; set; }
        public spRole Role { get; set; }

        /// <summary>
        /// Tel номери
        /// </summary>
        [StringLength(20)]
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{LastName} {Name} {Patronymic}";
        }
    }
}
