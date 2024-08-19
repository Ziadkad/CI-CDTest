namespace CI_CD_Pipelines.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
}