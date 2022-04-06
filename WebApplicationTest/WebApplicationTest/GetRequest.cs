namespace WebApplicationTest
{
    public class GetRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public SortOptions? SortOption { get; set; }
    }

    public enum SortOptions
    {
        None,
        Acsending,
        Decsending
    }
}
