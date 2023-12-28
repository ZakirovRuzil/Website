using System.Text;
using Web.Attributes;
using Web.ORM.Entities;
using Web.ORM.Repository;

namespace Web.Controllers;

[HttpController(name: "Boss")]
public class BossController
{
    private static BossesRepository _repo = new BossesRepository();

    [Get("GetBosses")]
    public string GetBosses()
    {
        var bosses = _repo.Select().Result;

        var html = new StringBuilder();

        foreach (var boss in bosses.OrderBy(b => b.Id))
        {
            html.Append($"<p> Boss name: {boss.Name}</p>" +
                        $"<p style=\"display: flex;\"> " +
                        $"Boss picture:\n " +
                        $"<div style=\"overflow: hidden;width:70px;height: 70px;border-radius: 50%;\">" +
                        $"<img style=\"width: 100%;height: 100%;object-fit: cover;\" src = \"{boss.Picture}\">" +
                        $"</div>" +
                        $"</p>" +
                        $"Boss Id: {boss.Id}" +
                        $"<hr>");
        }

        return html.ToString();
    }

    [Get("AddBoss")]
    public void AddBoss(Boss boss)
    {
        // Console.WriteLine(boss.Name);
        _repo.Add(boss);
    }
    
    [Get("DeleteBoss")]
    public async Task DeleteBoss(string id)
    {
        // Console.WriteLine(id);
        _repo.Delete(id);
    }
    
    [Get("EditBoss")]
    public void EditBoss(Boss boss)
    {
        // Console.WriteLine(boss.Name);
        _repo.Edit(boss);
    }
}