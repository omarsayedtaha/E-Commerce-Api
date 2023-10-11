using AutoMapper;
using CoreLayer.Entities;
using CoreLayer.Repository;
using CoreLayer.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using RepositoryLayer.Data;
using TalabatApi.Dtos;
using TalabatApi.Errors;
using TalabatApi.Helper;
using CoreLayer.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TalabatApi.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<ProductType> _typeRepo;
        //private readonly IGenericRepository<ProductBrand> _brandRepo;

        private readonly IMapper _mapper;

        public ProductController(//IGenericRepository<Product> productRepo,
        //    IGenericRepository<ProductBrand> brandRepo,
        //    IGenericRepository<ProductType> typeRepo,
         IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_productRepo = productRepo;
            // _typeRepo = typeRepo;
            //_brandRepo = brandRepo;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]
        //[CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetAllProducts([FromQuery]ProductSpecParams specparams)
        {
            var spec = new ProductwithBrandandTypeSpecification(specparams); 

            var products = await _unitOfWork.Repository<Product>().GetAllWihtSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var count = data.Count();
            return Ok(new Pagination<ProductToReturnDto>(specparams.PageIndex, specparams.PageSize,count ,data));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProdutById(int id)
        {
            var spec = new ProductwithBrandandTypeSpecification(id); 
            var product = await _unitOfWork.Repository<Product>().GetByIdWihtSpecAsync(spec); 
            var mappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);

            return Ok(mappedProduct);
        }

        [HttpGet("Brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetAllBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

            return Ok(brands);
        }
        [HttpGet("Types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Types = await _unitOfWork.Repository<ProductType>().GetAllAsync();

            return Ok(Types);
        }
    }
}
