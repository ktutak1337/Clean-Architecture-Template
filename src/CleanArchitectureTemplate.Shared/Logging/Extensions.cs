using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitectureTemplate.Shared.Contexts;
using CleanArchitectureTemplate.Shared.Logging.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace CleanArchitectureTemplate.Shared.Logging
{
    public static class Extensions
    {
        private const string ConsoleOutputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";
        private const string FileOutputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}";
        private const string AppSectionName = "app";
        private const string LoggerSectionName = "logger";

        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                var logger = ctx.RequestServices.GetRequiredService<ILogger<IContext>>();
                var context = ctx.RequestServices.GetRequiredService<IContext>();
                logger.LogInformation("Started processing a request [Request ID: '{RequestId}', Correlation ID: '{CorrelationId}', Trace ID: '{TraceId}', User ID: '{UserId}']...",
                    context.RequestId, context.CorrelationId, context.TraceId, context.Identity.IsAuthenticated ? context.Identity.Id : string.Empty);

                await next();

                logger.LogInformation("Finished processing a request with status code: {StatusCode} [Request ID: '{RequestId}', Correlation ID: '{CorrelationId}', Trace ID: '{TraceId}', User ID: '{UserId}']",
                ctx.Response.StatusCode, context.RequestId, context.CorrelationId, context.TraceId, context.Identity.IsAuthenticated ? context.Identity.Id : string.Empty);
            });

            return app;
        }

        public static IHostBuilder UseLogging(this IHostBuilder builder, Action<LoggerConfiguration> configure = null,
            string loggerSectionName = LoggerSectionName,
            string appSectionName = AppSectionName)
            => builder.UseSerilog((context, loggerConfiguration) =>
            {
                if (string.IsNullOrWhiteSpace(loggerSectionName))
                {
                    loggerSectionName = LoggerSectionName;
                }

                if (string.IsNullOrWhiteSpace(appSectionName))
                {
                    appSectionName = AppSectionName;
                }

                var appSettings = context.Configuration.GetOptions<AppSettings>(appSectionName);
                var loggerSettings = context.Configuration.GetOptions<LoggerSettings>(loggerSectionName);

                MapOptions(loggerSettings, appSettings, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
                configure?.Invoke(loggerConfiguration);
            });

        private static void MapOptions(LoggerSettings loggerSettings, AppSettings appSettings,
            LoggerConfiguration loggerConfiguration, string environmentName)
        {
            var level = GetLogEventLevel(loggerSettings.Level);

            loggerConfiguration.Enrich.FromLogContext()
                .MinimumLevel.Is(level)
                .Enrich.WithProperty("Environment", environmentName)
                .Enrich.WithProperty("Application", appSettings.Name)
                .Enrich.WithProperty("Instance", appSettings.Instance)
                .Enrich.WithProperty("Version", appSettings.Version);

            foreach (var (key, value) in loggerSettings.Tags ?? new Dictionary<string, object>())
            {
                loggerConfiguration.Enrich.WithProperty(key, value);
            }

            foreach (var (key, value) in loggerSettings.Overrides ?? new Dictionary<string, string>())
            {
                var logLevel = GetLogEventLevel(value);
                loggerConfiguration.MinimumLevel.Override(key, logLevel);
            }

            loggerSettings.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
                .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p))));

            loggerSettings.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
                .ByExcluding(Matching.WithProperty(p)));

            Configure(loggerConfiguration, loggerSettings);
        }

        private static void Configure(LoggerConfiguration loggerConfiguration, LoggerSettings settings)
        {
            var consoleSettings = settings.Console ?? new ConsoleSettings();
            var fileSettings = settings.File ?? new FileSettings();
            var seqOptions = settings.Seq ?? new SeqSettings();

            if (consoleSettings.Enabled)
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: ConsoleOutputTemplate);
            }

            if (fileSettings.Enabled)
            {
                var path = string.IsNullOrWhiteSpace(fileSettings.Path) ? "logs/logs.txt" : fileSettings.Path;
                if (!Enum.TryParse<RollingInterval>(fileSettings.Interval, true, out var interval))
                {
                    interval = RollingInterval.Day;
                }

                loggerConfiguration.WriteTo.File(path, rollingInterval: interval, outputTemplate: FileOutputTemplate);
            }

            if (seqOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);
            }
        }

        private static LogEventLevel GetLogEventLevel(string level)
            => Enum.TryParse<LogEventLevel>(level, true, out var logLevel)
                ? logLevel
                : LogEventLevel.Information;
    }
}
