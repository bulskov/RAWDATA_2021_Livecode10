using System;
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
            var model = products.Select(CreateProductListViewModel);
            var total = _dataService.NumberOfProducts();

            var lastPage = (int) Math.Ceiling(total / (double)pageSize) - 1;

            var prev = page <= 0
                ? null
                : _linkGenerator.GetUriByName(
                  HttpContext, 
                  nameof(GetProducts), 
                  new { page = page - 1, pageSize });

            var cur = _linkGenerator.GetUriByName(HttpContext, nameof(GetProducts), new { page, pageSize });

            var next = page >= lastPage
                ? null
                : _linkGenerator.GetUriByName(
                HttpContext, 
                nameof(GetProducts), 
                new { page = page + 1, pageSize });

            var result = new
            {
                total,
                prev,
                cur,
                next,
                items = model
            };

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

        private ProductViewModel CreateProductViewModel(Product product)
        {
            var model = _mapper.Map<ProductViewModel>(product);
            model.Url = GetUrl(product);
            return model;
        }

        private ProductListViewModel CreateProductListViewModel(Product product)
        {
            var model = _mapper.Map<ProductListViewModel>(product);
            model.Url = GetUrl(product);
            return model;
        }

        private string GetUrl(Product product)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { product.Id });
        }
    }
}
