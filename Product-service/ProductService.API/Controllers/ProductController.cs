using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dto;
using ProductService.Application.Feature.ProductFeature.Command.DeleteProduct;
using ProductService.Application.Feature.ProductFeature.Command.DraftProduct;
using ProductService.Application.Feature.ProductFeature.Command.PublicProduct;
using ProductService.Application.Feature.ProductFeature.Command.UpdateProduct;
using ProductService.Application.Feature.ProductFeature.Query.GetListProduct;
using ProductService.Application.Feature.ProductFeature.Query.GetProduct;
using ProductService.Domain.Entity;
using ProductService.API.Annotation;
using ProductService.Application.Response;
using ProductService.Application.Dto.Product;
using ProductService.Application.Feature.ProductFeature.Command.CreateProduct;
using System.Security.Claims;
using AutoMapper;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController(
        IMediator mediator,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ProductDto> Get(Guid id)
        {
                ProductDto productDto = await _mediator.Send(request: new GetProductQuery(id));
                return productDto;
        }

        [HttpGet]
        [Cache]
        public async Task<List<ProductDto>> Gets(
            string? keyword = null, 
            int page = 1, 
            int limit = 20
        ) {
            Pagination pagination = new()
            {
                SortBy = keyword,
                Page = page,
                Limit = limit
            };
            return await _mediator.Send(request: new GetListProductQuery(pagination));
        }

        [HttpPost]
        [UserAuthorization]
        public async Task<Product> Create(CreateProductReq createProductReq)
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            CreateProductCommand request = new() 
            {
                CreateProductReq = createProductReq,
                User = user
            };

            Product newProduct = await _mediator.Send(request);
            return newProduct;
        }

        [HttpPatch]
        [UserAuthorization]
        public async Task<BaseResponse> Update(UpdateProductReq payload) 
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            Console.WriteLine(JsonSerializer.Serialize(user));

            UpdateProductCommand request = new() 
            {
                UpdateProductReq = payload,
                User = user
            };

            return await _mediator.Send(request);
        }

        [HttpDelete]
        [UserAuthorization]
        public async Task<BaseResponse> Delete(DeleteProductReq payload) 
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            Console.WriteLine(JsonSerializer.Serialize(user));

            DeleteProductCommand request = new() 
            {
                DeleteProductReq = payload,
                User = user
            };
            return await _mediator.Send(request);
        }

        [HttpPost("public/{productId}")]
        [UserAuthorization]
        public async Task<Product> PublicProduct(Guid productId) 
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            Console.WriteLine(JsonSerializer.Serialize(user));

            PublicProductCommand request = new() 
            {
                Id = productId,
                User = user
            };
            return await _mediator.Send(request);
        }

        [HttpPost("draft/{productId}")]
        [UserAuthorization]
        public async Task<Product> DraftProduct(Guid productId) 
        {
            ClaimsPrincipal principal = HttpContext.User;
            UserDecode user = _mapper.Map<UserDecode>(principal);

            Console.WriteLine(JsonSerializer.Serialize(user));

            DraftProductCommand request = new() 
            {
                Id = productId,
                User = user
            };
            return await _mediator.Send(request);
        }
    }
}
