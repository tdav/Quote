using Microsoft.AspNetCore.Builder;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteServer.Extensions
{
    /// <summary>
    /// https://prometheus.io/  Power your metrics and alerting with a leading open-source monitoring solution.
    /// https://grafana.com/    The analytics platform for all your metrics
    /// </summary>
    public static class PrometheusMetricService
    {
        public static void UseMyMetricServer(this IApplicationBuilder app)
        {
            // prometheus-net - /metrics
            app.UseMetricServer();
            app.UseHttpMetrics();
        }
    }
}
