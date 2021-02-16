using System;

namespace QuoteServer
{
    public class viUser 
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public int Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
