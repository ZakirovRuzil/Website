namespace Web.ORM.Entities;

public class User
{
    public string user_name { get; set; } = default!;

    public string user_password { get; set; } = default!;
    
    public bool is_admin { get; set; } = default!;
}