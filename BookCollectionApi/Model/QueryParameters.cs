namespace BookCollectionApi.Model
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _pageSize = 50;
        public int Page { get; set; } = 1;
        public int Size
        {
            get { return _pageSize; }
            set
            {
                _pageSize = Math.Min(_maxSize, value);
            }
        }

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



    }
}
