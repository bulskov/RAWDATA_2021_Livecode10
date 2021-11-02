using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebService.ViewModels;

namespace WebService.Controllers
{
    
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public ProductsController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetProducts))]
        public IActionResult GetProducts([FromQuery] QueryString queryString)
        {
            var products = _dataService.GetProducts(queryString);
            
            var items = products.Select(CreateProductListViewModel);
            
            var result = CreateResultModel(queryString, _dataService.NumberOfProducts(), items);
            
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetProduct))]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = CreateProductViewModel(product);

            return Ok(model);
        }

        /*
         *
         * Helper methods
         *
         */

        private object CreateResultModel(QueryString queryString, int total, IEnumerable<ProductListViewModel> model)
        {
            return new
            {
                total,
                prev = CreateNextPageLink(queryString),
                cur = CreateCurrentPageLink(queryString),
                next = CreateNextPageLink(queryString, total),
                items = model
            };
        }

        private string CreateNextPageLink(QueryString queryString, int total)
        {
            var lastPage = GetLastPage(queryString.PageSize, total);
            return queryString.Page >= lastPage ? null : GetProductsUrl(queryString.Page + 1, queryString.PageSize, queryString.OrderBy);
        }

        
        private string CreateCurrentPageLink(QueryString queryString)
        {
            return GetProductsUrl(queryString.Page, queryString.PageSize, queryString.OrderBy);
        }

        private string CreateNextPageLink(QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetProductsUrl(queryString.Page - 1, queryString.PageSize, queryString.OrderBy);
        }

        private string GetProductsUrl(int page, int pageSize, string orderBy)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetProducts),
                new { page, pageSize, orderBy });
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }

        private ProductViewModel CreateProductViewModel(Product product)
        {
            var model = _mapper.Map<ProductViewModel>(product);
            model.Url = GetProductUrl(product);
            return model;
        }

        private ProductListViewModel CreateProductListViewModel(Product product)
        {
            var model = _mapper.Map<ProductListViewModel>(product);
            model.Url = GetProductUrl(product);
            return model;
        }

        private string GetProductUrl(Product product)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { product.Id });
        }
    }
}
