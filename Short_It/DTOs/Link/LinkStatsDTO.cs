namespace Short_It.DTOs.Link
{
    public class LinkStatsDTO
    {
        public string LinkTitle { get; set; }
        public string FullLink { get; set; }
        public string ShortLink { get; set; }
        public int TimesVisited { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
