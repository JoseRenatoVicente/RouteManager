using Identity.API.Configurations;
using Identity.API.Services;
using Identity.Domain.Commands.Roles.Create;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddMvcConfiguration();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatR(typeof(CreateRoleCommand));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
var seederService = app.Services.GetService<SeederService>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerSetup();
}

seederService?.Seed();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthConfiguration();
app.UseHealthChecksConfiguration();
app.MapControllers();
app.Run();