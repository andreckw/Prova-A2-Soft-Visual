namespace RenanYokoya.Models;

public class Folha
{
    public int Id { get; set; }
    public float Valor { get; set; }
    public int Quantidade { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int SalarioBruto { get; set; }
    public int ImpostoIrrf { get; set; }
    public int ImpostoInss { get; set; }
    public int ImpostoFgts { get; set; }
    public int SalarioLiquido { get; set; }
}
