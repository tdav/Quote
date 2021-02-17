using QuoteServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Senders
{
    public interface ISender : INamedService
    {
        Task<bool> SendAsync(object value);
    }
}
