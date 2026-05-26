using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os Controllers e previne loop de referência no JSON (Crucial)
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddOpenApi(); // Nativo do .NET 8+

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Gera o JSON do .NET em: /openapi/v1.json

    // Configura a interface gráfica antiga para ler o JSON novo
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API Ocorrências v1");
    });
}

app.MapControllers();
app.Run();
