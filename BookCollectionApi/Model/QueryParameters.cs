namespace BookCollectionApi.Model
{
    public class QueryParameters
    {
        const int MaxSize = 100;
        private int _pageSize = 50;
        public int Page { get; set; }
        public string sortBy { get; set; } = "id";
        private string sortOrder = "asc";
        public string SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    sortOrder = value;
                }
            }
        }
        public int Size
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = Math.Min(_pageSize, value);
            }
        }


    }
}
