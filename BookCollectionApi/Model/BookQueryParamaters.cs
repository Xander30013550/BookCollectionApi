namespace BookCollectionApi.Model
{
    public class BookQueryParamaters : QueryParameters
    {
        public string? title { get; set; } = String.Empty;
        public string? author { get; set; } = String.Empty;
        public string? language { get; set; } = String.Empty;
        public string? genre { get; set; } = String.Empty;

    }
}
