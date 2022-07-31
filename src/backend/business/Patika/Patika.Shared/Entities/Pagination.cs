namespace Patika.Shared.Entities
{
    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int Count { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
        public Pagination()
        {
            Page = 1;
            Count = 1000;
        }
        public Pagination(int page, int pageSize)
        {
            Page = page < 1 ? 1 : page + 1;
            Count = pageSize < 0 ? 20 : pageSize;
        }
    }
}