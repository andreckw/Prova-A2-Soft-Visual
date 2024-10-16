using RenanYokoya.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("api/folha/cadastrar", ([FromBody] Folha folha) =>
{
    
});


app.Run();
