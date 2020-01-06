namespace Website.Models.Pages
{
    /// <summary>
    /// Used primarily for publishing news articles on the website
    /// </summary>
    [SiteContentType(
        GroupName = Global.GroupNames.News, 
        DisplayName = "Article",
        GUID = "AEECADF2-3E89-4117-ADEB-F8D43565D2F4")]
    //[SiteImageUrl(Global.StaticGraphicsFolderPath + "page-type-thumbnail-article.png")]
    public class ArticlePage : StandardPage
    {

    }
}
