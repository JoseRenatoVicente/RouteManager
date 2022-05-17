using Identity.API.Configurations;
using Identity.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddMvcConfiguration();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();
var seederService = app.Services.GetService<SeederService>();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHttpsRedirection();

seederService?.Seed();
app.UseSwaggerSetup();
app.UseRouting();
app.UseAuthConfiguration();
app.UseHealthChecksConfiguration();
app.MapControllers();
app.Run();