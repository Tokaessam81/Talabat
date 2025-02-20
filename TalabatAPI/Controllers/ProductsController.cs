using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecific;
using TalabatAPI.DTO;
using TalabatAPI.Helpers;

namespace TalabatAPI.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IGenericRepository<ProductBrand>    _brand;
        private readonly IGenericRepository<ProductCategory> _category;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> genericRepository,IGenericRepository<ProductBrand> brand
            ,IGenericRepository<ProductCategory> category,IMapper mapper)
        {
            _genericRepository = genericRepository;
            _brand  =  brand;
            _category = category;
            _mapper = mapper;
        }
        [Cache(300)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProductsAsyncWithSpecification([FromQuery]ProductSpecParams spec)
        {
            var Specification = new ProductSpecification(spec);
            var Products=await _genericRepository.GetAllBySpecificationAsync(Specification);
            var Data= _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(Products);
            var CountSpec = new ProductSpecPaginationCount(spec);
            var Count= await _genericRepository.GetCountAsync(CountSpec);
            return Ok(new Pagination<ProductDTO>(spec.PageSize,spec.PageIndex,Count, Data));

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/api/Product/{Id}")]
        public async Task<ActionResult<ProductDTO>> GetProductAsyncWithSpecification(int Id)
        {

            var Specification = new ProductSpecification(Id);
            var Product=await _genericRepository.GetByIdSpecification(Specification);
            if(Product == null) return NotFound();
            return Ok(_mapper.Map<Product,ProductDTO>(Product));
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrandsAsync()
        {
            var Brands = await _brand.GetAll();
            return  Ok(Brands);
        } 
        [HttpGet("Category")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategoryAsync()
        {
            var categories = await _category.GetAll();
            return Ok(categories);
        }
    }
}
