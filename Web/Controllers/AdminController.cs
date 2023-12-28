using System.Text;
using Web.Attributes;
using Web.ORM.Entities;
using Web.ORM.Repository;

namespace Web.Controllers;

[HttpController(name: "User")]
public class AdminController
{
    private static UsersRepository _repo = new UsersRepository();
    
    [Get("getUsers")]
    public string GetUsers()
    {
        var users = _repo.Select().Result;

        var html = new StringBuilder();

        foreach (var user in users)
        {
            html.Append($"<p>{user.user_name}</p>\n");
        }

        return html.ToString();
    }
}