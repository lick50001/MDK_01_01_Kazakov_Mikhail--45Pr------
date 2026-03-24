using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Руководство для использования запросов",
        Description = "Полное руководство для использования запросов находящихся в проекте"
    });

    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "Руководство для использования запросов",
        Description = "Полное руководство для использования запросов находящихся в проекте"
    });

    c.SwaggerDoc("v3", new OpenApiInfo
    {
        Version = "v3",
        Title = "Руководство для использования запросов",
        Description = "Полное руководство для использования запросов находящихся в проекте"
    });

    c.SwaggerDoc("v4", new OpenApiInfo
    {
        Version = "v4",
        Title = "Руководство для использования запросов",
        Description = "Полное руководство для использования запросов находящихся в проекте"
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Настройка middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Запросы GET");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Запросы POST");
        c.SwaggerEndpoint("/swagger/v3/swagger.json", "Запросы PUT");
        c.SwaggerEndpoint("/swagger/v4/swagger.json", "Запросы DELETE");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();