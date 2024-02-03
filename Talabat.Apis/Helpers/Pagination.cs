namespace Talabat.Apis.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> date)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Date = date;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }

        public IReadOnlyList<T> Date { get; set; }
    }
}
