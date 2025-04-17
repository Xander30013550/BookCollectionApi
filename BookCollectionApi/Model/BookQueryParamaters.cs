namespace BookCollectionApi.Model
{
    public class BookQueryParamaters : QueryParameters
    {
        public string title { get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public string language { get; set; } = string.Empty;
        public string genre { get; set; } = string.Empty;

    }
}
