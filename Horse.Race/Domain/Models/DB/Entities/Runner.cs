namespace Domain.Models.DB.Entities;

public class Runner
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Barrier { get; set; }
    public string? Name { get; set; }
    public decimal WinPrice { get; set; }
    public string? Jockey { get; set; }
    public string? Trainer { get; set; }

    public int RaceId { get; set; }
}