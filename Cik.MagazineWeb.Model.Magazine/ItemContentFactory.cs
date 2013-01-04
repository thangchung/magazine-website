namespace Cik.MagazineWeb.Model.Magazine
{
    public class ItemContentFactory
    {
        public static ItemContent CreateItemContent(string title, string shortDes, string content)
        {
            return CreateItemContent(title, shortDes, content, null, null, null);
        }

        public static ItemContent CreateItemContent(string title, string shortDes, string content, string smallImagePath, string mediumImagePath, string largeImagePath)
        {
            return new ItemContent
                {
                    Title = title,
                    SortDescription = shortDes,
                    Content = content,
                    SmallImage = smallImagePath,
                    MediumImage = mediumImagePath,
                    BigImage = largeImagePath
                };
        }
    }
}