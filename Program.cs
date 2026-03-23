var builder = WebApplication.CreateBuilder(args);

// --- 1. РЕГИСТРАЦИЯ СЕРВИСОВ (builder.Services) ---
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Пробник"
    });
});

// Собираем приложение (этот вызов должен быть ОДИН раз)
var app = builder.Build();

// --- 2. НАСТРОЙКА КОНФИГУРАЦИИ (app.Use...) ---

// Swagger должен быть в начале, чтобы он работал
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Пробник");
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

// Настройка путей для контроллеров и страниц
app.MapControllers();
app.MapRazorPages();

// Запуск приложения (этот вызов должен быть ОДИН раз в самом конце)
app.Run();