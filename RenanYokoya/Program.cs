using RenanYokoya.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapPost("api/funcionario/cadastrar",([FromBody] Funcionario funcionario, [FromServices] AppDbContext Ctx) =>
{
    Ctx.Funcionarios.Add(funcionario);
    Ctx.SaveChanges();
    return Results.Created();
}
);

app.MapGet("api/funcionario/listar", ([FromServices] AppDbContext Ctx) =>
{
    var funcionarios = Ctx.Funcionarios.ToList();
    return Results.Ok(funcionarios);
}
);

app.Run();
