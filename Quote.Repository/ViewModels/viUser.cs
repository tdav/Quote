using System;

namespace Quote.Repository.ViewModels
{
    public class viUser : viStatusMessage
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
