using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Регистрация MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Регистрация DataContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация PasswordHasher
builder.Services.AddScoped<TRKApp.Services.IPasswordHasher, TRKApp.Services.PasswordHasher>();

// Регистрация UserService
builder.Services.AddScoped<IUserService, UserService>();

// Поддержка контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

// Добавляем логирование для отладки
builder.Services.AddLogging(builder => builder.AddConsole());

var app = builder.Build();

// Применение миграций при запуске
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Логирование всех HTTP запросов
app.Use(async (context, next) =>
{
    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {context.Request.Method} {context.Request.Path}");
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        Console.WriteLine($"API запрос: {context.Request.Method} {context.Request.Path}");
        if (context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;
            Console.WriteLine($"Body: {body}");
        }
    }
    await next();
    Console.WriteLine($"Ответ: {context.Response.StatusCode}");
});

// Настройка статических файлов из разных папок
app.UseStaticFiles(); // Обслуживает wwwroot по умолчанию

// Дополнительные статические файлы из корня проекта
var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "images");
if (Directory.Exists(imagesPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(imagesPath),
        RequestPath = "/images"
    });
}

var stylesPath = Path.Combine(builder.Environment.ContentRootPath, "styles");
if (Directory.Exists(stylesPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(stylesPath),
        RequestPath = "/styles"
    });
}

var scriptsPath = Path.Combine(builder.Environment.ContentRootPath, "scripts");
if (Directory.Exists(scriptsPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(scriptsPath),
        RequestPath = "/scripts"
    });
}

// Middleware для обработки .shtml файлов с SSI
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;
    
    if (path != null && path.EndsWith(".shtml", StringComparison.OrdinalIgnoreCase))
    {
        // Убираем /pages/ из пути и оставляем только имя файла
        var fileName = Path.GetFileName(path);
        var filePath = Path.Combine(builder.Environment.ContentRootPath, "Pages", fileName);
        
        if (File.Exists(filePath))
        {
            var content = await File.ReadAllTextAsync(filePath);
            
            // Обработка SSI директив <!--#include virtual="..." -->
            var includePattern = @"<!--#include virtual=""([^""]+)"" -->";
            content = Regex.Replace(content, includePattern, match =>
            {
                var includePath = match.Groups[1].Value;
                // Обрабатываем относительные пути типа "../base/header.html"
                var cleanPath = includePath.Replace("../", "").TrimStart('/');
                var includeFilePath = Path.Combine(builder.Environment.ContentRootPath, cleanPath);
                
                if (File.Exists(includeFilePath))
                {
                    return File.ReadAllText(includeFilePath);
                }
                return $"<!-- Include not found: {includePath} -->";
            });
            
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(content);
            return;
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync($"File not found: {filePath}");
            return;
        }
    }
    
    await next();
});

// Перенаправление с корня на index.shtml
app.MapGet("/", () => Results.Redirect("/pages/index.shtml"));

app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // Добавляем маршруты для контроллеров
app.MapRazorPages();

app.Run();
