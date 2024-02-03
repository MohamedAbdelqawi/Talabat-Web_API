namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
        public const int MaxPageSize = 10;
        public int pageSize = 5;
        public int PageSize { get { return pageSize; } set { pageSize = value > MaxPageSize ? MaxPageSize : value; } }

        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? Typeid { get; set; }
        public string? search;
        public string? Search { get { return search; } set { search = value.ToLower(); } }
    }
}
