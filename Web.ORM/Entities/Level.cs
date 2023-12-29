namespace Web.ORM.Entities;

public class Level : IEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;

    public string? Picture { get; set; }
}