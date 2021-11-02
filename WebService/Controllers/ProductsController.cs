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
        public IActionResult GetProducts(int page = 0, int pageSize = 10)
        {
            var products = _dataService.GetProducts(page, pageSize);
            
            var items = products.Select(CreateProductListViewModel);
            
            var result = CreateResultModel(page, pageSize, _dataService.NumberOfProducts(), items);
            
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

        private object CreateResultModel(int page, int pageSize, int total, IEnumerable<ProductListViewModel> model)
        {
            return new
            {
                total,
                prev = CreateNextPageLink(page, pageSize),
                cur = CreateCurrentPageLink(page, pageSize),
                next = CreateNextPageLink(page, pageSize, total),
                items = model
            };
        }

        private string CreateNextPageLink(int page, int pageSize, int total)
        {
            var lastPage = GetLastPage(pageSize, total);
            return page >= lastPage ? null : GetProductsUrl(page + 1, pageSize);
        }

        
        private string CreateCurrentPageLink(int page, int pageSize)
        {
            return GetProductsUrl(page, pageSize);
        }

        private string CreateNextPageLink(int page, int pageSize)
        {
            return page <= 0 ? null : GetProductsUrl(page - 1, pageSize);
        }

        private string GetProductsUrl(int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetProducts),
                new { page, pageSize });
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
