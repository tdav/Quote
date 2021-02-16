using System.ComponentModel.DataAnnotations;

namespace QuoteServer
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
