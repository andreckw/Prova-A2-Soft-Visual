using RenanYokoya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

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

app.MapPost("api/folha/cadastrar", ([FromBody] Folha folha, [FromServices] AppDbContext Ctx) =>
{
    var funcionario = Ctx.Funcionarios.FirstOrDefault(f => f.Id == folha.FuncionarioId);

    if (funcionario == null) {
        return Results.NotFound();
    }

    folha.SalarioBruto = folha.Valor * folha.Quantidade;
    folha.ImpostoFgts = folha.SalarioBruto * 0.08f;

    if (folha.SalarioBruto >= 1903.99 && folha.SalarioBruto <= 2826.65) {
        folha.ImpostoIrrf = folha.SalarioBruto * (7.5f / 100) + 142.80f;
    } else if (folha.SalarioBruto >= 2826.66 && folha.SalarioBruto <= 3751.05) {
        folha.ImpostoIrrf = folha.SalarioBruto * 0.15f + 354.80f;
    } else if (folha.SalarioBruto >= 3751.06 && folha.SalarioBruto <= 4664.68) {
        folha.ImpostoIrrf = folha.SalarioBruto * (22.5f / 100) + 636.13f;
    } else if (folha.SalarioBruto > 4664.68) {
        folha.ImpostoIrrf = folha.SalarioBruto * (27.5f / 100) + 869.36f;
    } else {
        folha.ImpostoIrrf = folha.SalarioBruto;
    }

    if (folha.SalarioBruto >= 1693.73 && folha.SalarioBruto <= 2822.90) {
        folha.ImpostoInss = folha.SalarioBruto * 0.09f;
    } else if (folha.SalarioBruto >= 2822.91 && folha.SalarioBruto <= 5645.80) {
        folha.ImpostoInss = folha.SalarioBruto * 0.11f;
    } else if (folha.SalarioBruto > 5645.81) {
        folha.ImpostoInss = 621.03f;
    } else {
        folha.ImpostoInss = folha.SalarioBruto * 0.08f;
    }

    folha.SalarioLiquido = folha.SalarioBruto - folha.ImpostoIrrf - folha.ImpostoInss;

    Ctx.Folhas.Add(folha);
    Ctx.SaveChanges();
    return Results.Created();
});

app.MapGet("api/folha/listar", ([FromServices] AppDbContext Ctx) =>
{
    var folhas = Ctx.Folhas.ToList();
    if (folhas.Count == 0) {
        return Results.NotFound();
    }

    return Results.Ok(folhas);
});

app.MapGet("api/folha/buscar/{cpf}/{mes}/{ano}", (String cpf, int mes, int ano, 
    [FromServices] AppDbContext Ctx) =>
{
    var funcionario = Ctx.Funcionarios.FirstOrDefault(f => f.Cpf == cpf);
    if (funcionario == null) {
        return Results.NotFound();
    }

    var folha = Ctx.Folhas.FirstOrDefault(f => f.Mes == mes && f.Ano == ano 
        && f.FuncionarioId == funcionario.Id);

    if (folha == null) {
        return Results.NotFound();
    }

    return Results.Ok(folha);
});

app.Run();
