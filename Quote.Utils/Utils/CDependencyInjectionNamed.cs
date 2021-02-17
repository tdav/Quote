using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QuoteServer.Extensions
{
    public interface INamedService
    {
        string Name { get; }
    }

    public static class NamedServiceExtensions
    {
        public static T GetServiceByName<T>(this IServiceProvider provider, string serviceName) where T : INamedService
        {
            var candidates = provider.GetServices<T>();
            return candidates.FirstOrDefault(s => s.Name == serviceName);
        }
    }
}
