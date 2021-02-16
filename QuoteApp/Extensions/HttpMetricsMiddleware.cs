using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuoteServer.Extensions
{
    //https://medium.com/@hedgic/monitoring-and-load-testing-asp-net-core-application-94256c9d0be7
    //https://gist.github.com/SlyNet/5cdb3f2b2f3a1b279c161f0b08f6594a
    public class HttpMetricsMiddleware
    {
        private readonly RequestDelegate next;
        private static readonly Gauge httpInProgress = Prometheus.Metrics.CreateGauge("http_requests_in_progress", "Number or requests in progress", "system");
        private static readonly Histogram httpRequestsDuration = Prometheus.Metrics.CreateHistogram("http_requests_duration_seconds", "Duration of http requests per tracking system", "system");

        public HttpMetricsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string[] subsystem = new string[1];
            var path = context.Request.Path;

            // will not track request to diagnostics endpoints
            if (path.StartsWithSegments("/metrics"))
            {
                await next(context);
                return;
            }

            using var inprogress = httpInProgress.TrackInProgress();
            using var timer = httpRequestsDuration.NewTimer();

            try
            {
                await next(context);
            }
            catch (Exception)
            {
                CommonMetrics.ExceptionsOccur.Inc();
                throw;
            }
        }
    }


    internal sealed class NpgsqlMetricsCollectionService : EventListener, IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventName != "EventCounters"
                || eventData.Payload.Count <= 0
                || !(eventData.Payload[0] is IDictionary<string, object> data)
            )
                return;

            WriteCounters(data);
        }

        private void WriteCounters(IDictionary<string, object> eventPayload)
        {
            switch (eventPayload["Name"])
            {
                case "idle-connections":
                    CommonMetrics.NpgsqlConnections.Labels("idle").Set(Convert.ToSingle(eventPayload["Mean"]));
                    break;
                case "busy-connections":
                    CommonMetrics.NpgsqlConnections.Labels("busy").Set(Convert.ToSingle(eventPayload["Mean"]));
                    break;
                case "connection-pools":
                    CommonMetrics.NpgsqlConnectionsPoolCount.Set(Convert.ToSingle(eventPayload["Mean"]));
                    break;
                case "bytes-written-per-second":
                    var written = Convert.ToSingle(eventPayload["Increment"]);
                    if (written > 0) CommonMetrics.NpgsqlDataCounter.Labels("write").Inc(written);
                    break;
                case "bytes-read-per-second":
                    var read = Convert.ToSingle(eventPayload["Increment"]);
                    if (read > 0) CommonMetrics.NpgsqlDataCounter.Labels("read").Inc(read);
                    break;
            }
        }

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name.Equals("Npgsql", StringComparison.OrdinalIgnoreCase))
            {
                EnableEvents(eventSource, EventLevel.Verbose, EventKeywords.None, new Dictionary<string, string>
                {
                    {"EventCounterIntervalSec", "1"}
                });
            }
        }
    }


    public static class CommonMetrics
    {
        public static Gauge NpgsqlConnections = Metrics.CreateGauge("npgsql_connections_current", "Number of connections managed by Npgsql (idle/busy)", "type");
        public static Gauge NpgsqlConnectionsPoolCount = Metrics.CreateGauge("npgsql_connection_pools_current", "Number of connection pools managed by Npgsql");
        public static Counter NpgsqlDataCounter = Metrics.CreateCounter("npgsql_data_bytes_total", "Amount of byte read/write by Npgsql", "direction");
        public static Counter ExceptionsOccur = Metrics.CreateCounter("exceptions_total", "Total number of exceptions happen on site during it's life time");
    }
}
