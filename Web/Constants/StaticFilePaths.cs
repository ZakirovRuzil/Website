namespace Web.Constants;

public class StaticFilePaths
{
    //Путь до статичных файлов
    public const string StaticFilesPath = "Web.static";
    
    //Путь до домашней страницы
    public const string HomeFilesPath = $"{StaticFilesPath}.home";
    
    //Путь до страницы босса
    public const string BossFilesPath = $"{StaticFilesPath}.boss";
    
    //Путь до шаблонов домашней страницы
    public const string HomeTemplatesPath = $"{HomeFilesPath}.templates";
    
    // Название домашней страницы
    public const string MainPageName = "index.html";
    
    // Название шаблона босса
    public const string MainPageBossTemplate = "BossTemplate.html";
    
    // Название шаблона уровня
    public const string MainPageLevelTemplate = "LevelTemplate.html";
    
    // Название шаблона уровня
    public const string MainPageFriendTemplate = "FriendTemplate.html";
}