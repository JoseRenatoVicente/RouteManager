using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Mime;
using System.Text.Json;


namespace RouteManager.WebAPI.Core.Configuration
{
    public static class HealthChecksConfig
    {

        public static void UseHealthChecksConfiguration(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/status-text");
            app.UseHealthChecks("/status-json",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                statusApplication = report.Status.ToString(),
                            });

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
        }
    }
}
