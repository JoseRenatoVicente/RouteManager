using Logging.API;
using Logging.API.Configuration;
using RouteManager.Domain.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<ConfigureMongoDbIndexesService>();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddMvcConfiguration();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddDistributedCache(builder.Configuration);
builder.Services.AddDomainContext();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHttpsRedirection();

app.UseSwaggerSetup();
app.UseRouting();
app.UseAuthConfiguration();
app.UseHealthChecksConfiguration();
app.MapControllers();
app.Run();
