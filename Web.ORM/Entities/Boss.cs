namespace Web.ORM.Entities;

public class Boss: IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Picture { get; set; }
    
    public string? Discription { get; set; }
}