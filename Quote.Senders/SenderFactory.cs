using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Senders
{
    public interface ISenderFactory
    {
        ISender GetSender<T>() where T : class;
    }

    public class SenderFactory : ISenderFactory
    {
        private readonly IConfiguration conf;

        public SenderFactory(IConfiguration configuration)
        {
            conf = configuration;
        }

        public ISender GetSender<T>() where T : class
        {
            return new T();
        }
    }
}
