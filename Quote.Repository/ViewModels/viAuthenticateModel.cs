using System.ComponentModel.DataAnnotations;

namespace Quote.Repository.ViewModels
{
    public class viAuthenticateModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string DrugStoreId { get; set; }
    }
}
