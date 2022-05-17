using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.UsePathBase("/gateway");
app.UseSwaggerForOcelotUI(opt =>
{
    opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("API GATEWAY FUNCIONANDO");
    });
});

await app.UseOcelot();
app.Run();