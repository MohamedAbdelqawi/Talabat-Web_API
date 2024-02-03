using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Apis.Helpers;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Apis.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOFWork _unitOFWork;

        //private readonly IGenericRepositry<Product> _productsRepo;
        //private readonly IGenericRepositry<ProductBrand> _brandsRepo;
        //private readonly IGenericRepositry<ProductType> _typesRepo;

        public ProductsController(IMapper mapper, IUnitOFWork unitOFWork/* IGenericRepositry<Product> ProductsRepo, IGenericRepositry<ProductBrand> brandsRepo, IGenericRepositry<ProductType> typesRepo*/)
        {
            _mapper = mapper;
            _unitOFWork = unitOFWork;
            //_productsRepo = ProductsRepo;
            //_brandsRepo = brandsRepo;
            //_typesRepo = typesRepo;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAll([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndTypeSpecification(specParams);


            var products = await _unitOFWork.Repository<Product>().GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countspec = new ProductWithFilterationForCountSpecification(specParams);
            var count = await _unitOFWork.Repository<Product>().GetCountWithSpecAsync(countspec);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.pageSize, count, data));
        }

        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);

            // var product = await _productsRepo.GetByIdAsync(id);
            var product = await _unitOFWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            return (product is null) ? NotFound(new ApiResponse(404)) : Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOFWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _unitOFWork.Repository<ProductType>().GetAllAsync();
            return Ok(Types);
        }
    }

}
