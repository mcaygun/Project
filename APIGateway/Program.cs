using Ocelot.DependencyInjection;
using Ocelot.Middleware;
//using BookStore.Service.Extensions;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
//builder.Services.LoadServiceLayerExtension();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Swagger UI �zerinde downstream servislerin swagger d�k�manlar�na eri�im sa�layacak endpointler eklenir
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API Gateway", Version = "v1" });
});

// Ocelot yap�land�rmas�n� ekleyin
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Ocelot servisini ekleyin
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // API Gateway'in kendi swagger dok�mantasyonu
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");

        // Burada her bir downstream servisin swagger endpoint'ini de UI'a ekliyoruz
        c.SwaggerEndpoint("http://localhost:5077/swagger/v1/swagger.json", "Category Service");
        c.SwaggerEndpoint("http://localhost:5110/swagger/v1/swagger.json", "Image Service");
        c.SwaggerEndpoint("http://localhost:5036/swagger/v1/swagger.json", "BookStore Service");
    });
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ocelot middleware'ini ekleyin
await app.UseOcelot();

app.Run();
