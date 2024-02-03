using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams specParams) : base(p =>
        (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
        (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId) && (!specParams.Typeid.HasValue || p.ProductTypeId == specParams.Typeid)

        )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            AddOrderBy(p => p.Name);
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDsc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }

            }

            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        }

        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
