using System.Text;
using Web.Attributes;
using Web.Constants;
using Web.ORM.Entities;
using Web.ORM.Repository;

namespace Web.Controllers;

[HttpController(name: "home")]
public class MainPageController
{
    private static BossesRepository _bosses = new BossesRepository();
    private static LevelsRepository _levels = new LevelsRepository();
    private static FriendsRepository _friends = new FriendsRepository();

    [Get("GetMainPage")]
    public string GetMainPage()
    {
        var bossesHtml = GetBossesHtml();
        var levelsHtml = GetLevelsHtml();
        var friendHtml = GetFriendsHtml();
        
        var globalPlaceholders = new Dictionary<string, string>()
        {
            {"{bosses}", bossesHtml},
            {"{levels}", levelsHtml},
            {"{friends}", friendHtml},
        };
        
        var htmlPage = HtmlFileManager.GetHtmlFileBody(StaticFilePaths.HomeFilesPath, StaticFilePaths.MainPageName);

        foreach (var placeholder in globalPlaceholders)
        {
            htmlPage = htmlPage.Replace(placeholder.Key, placeholder.Value);
        }

        return htmlPage;
    }

    [Get("AddBoss")]
    public void AddBoss(Boss boss)
    {
        // Console.WriteLine(boss.Name);
        _bosses.Add(boss);
    }
    
    [Get("DeleteBoss")]
    public async Task DeleteBoss(string id)
    {
        // Console.WriteLine(id);
        _bosses.Delete(id);
    }
    
    [Get("EditBoss")]
    public void EditBoss(Boss boss)
    {
        // Console.WriteLine(boss.Name);
        _bosses.Edit(boss);
    }


    private string GetBossesHtml()
    {
        var bosses = _bosses.Select().Result;

        var html = new StringBuilder();
        
        foreach (var boss in bosses.OrderBy(b => b.Id))
        {
            var bossTemplate = 
                HtmlFileManager.GetHtmlFileBody(StaticFilePaths.HomeTemplatesPath,
                    StaticFilePaths.MainPageBossTemplate);

            var placeholders = new Dictionary<string, string>();

            placeholders["{bossId}"] = boss.Id.ToString();
            placeholders["{bossName}"] = boss.Name;
            placeholders["{bossPicture}"] = boss.Picture;

            foreach (var placeholder in placeholders)
            {
                bossTemplate = bossTemplate.Replace(placeholder.Key, placeholder.Value);
            }

            html.Append(bossTemplate);
        }

        return html.ToString();
    }

    private string GetLevelsHtml()
    {
        var levels = _levels.Select().Result;
        
        var html = new StringBuilder();
        
        foreach (var level in levels.OrderBy(b => b.Id))
        {
            var levelTemplate = 
                HtmlFileManager.GetHtmlFileBody(StaticFilePaths.HomeTemplatesPath,
                    StaticFilePaths.MainPageLevelTemplate);

            var placeholders = new Dictionary<string, string>();

            placeholders["{levelName}"] = level.Name;
            placeholders["{levelPicture}"] = level.Picture;

            foreach (var placeholder in placeholders)
            {
               levelTemplate = levelTemplate.Replace(placeholder.Key, placeholder.Value);
            }

            html.Append(levelTemplate);
        }

        return html.ToString();
    }

    private string GetFriendsHtml()
    {
        var friends = _friends.Select().Result;
        
        var html = new StringBuilder();
        
        foreach (var friend in friends.OrderBy(b => b.Id))
        {
            var friendTemplate = 
                HtmlFileManager.GetHtmlFileBody(StaticFilePaths.HomeTemplatesPath,
                    StaticFilePaths.MainPageFriendTemplate);

            var placeholders = new Dictionary<string, string>();

            placeholders["{friendPicture}"] = friend.Picture;
            placeholders["{friendLink}"] = friend.Link;

            foreach (var placeholder in placeholders)
            {
                friendTemplate = friendTemplate.Replace(placeholder.Key, placeholder.Value);
            }

            html.Append(friendTemplate);
        }

        return html.ToString();
    }
}