using System;
using Logging.API.Configuration;
using Logging.Domain.Commands.Create;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddMvcConfiguration();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatR(typeof(CreateLogCommand));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerSetup();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthConfiguration();
app.UseHealthChecksConfiguration();
app.MapControllers();
app.Run();