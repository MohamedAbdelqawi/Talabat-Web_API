using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams specParams) : base(p =>
        (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&

        (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId) && (!specParams.Typeid.HasValue || p.ProductTypeId == specParams.Typeid)

        )
        {
        }
    }
}
