using System.Text.Json;
using AutoMapper;
using Grpc.Core;
using ProductService.Application.Contract.Persistant;
using ProductService.Domain.Entity;

namespace ProductService.Infrustructure.Service
{
    public class ProductGRPCServer(
        IProductRepository productRepository,
        IMapper mapper
    ): ProductGRPC.ProductGRPCBase
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public override async Task<GetProductRes> GetProduct(GetProductReq request, ServerCallContext context)
        {
            Product product = await _productRepository.GetByIdAsync(new Guid(request.ProductId));
            return _mapper.Map<GetProductRes>(product);
        }
        public async override Task<GetPriceRes> GetPrices(GetProductsReq request, ServerCallContext context)
        {
            List<Guid> productGuids = request.ProductIds.Ids.Select(Guid.Parse).ToList();

            if (!Guid.TryParse(request.ShopId, out Guid shopId))
                throw new ArgumentException("Invalid ShopId format");

            List<Product> products = await _productRepository.GetProductsOfShop(productGuids, shopId);

            double totalPrice = 0;
            Products lProduct = new();
            GetPriceRes res = new();

            products.ForEach(p => {
                GetProductRes productRes = new()
                {
                    Id = p.Id.ToString(),
                    ProductName = p.ProductName,
                    ProductThumb = p.ProductThumb,
                    ProductPrice = p.ProductPrice,
                    ProductType = p.ProductType.ToString(),
                    ProductShop = p.ProductShop.ToString()
                };
                totalPrice += p.ProductPrice;
                lProduct.Products_.Add(productRes);
            });
            
            res.Price = totalPrice;
            res.Products = lProduct;
            return res;
        }
        public override async Task<Products> GetProductByIds(ProductIds request, ServerCallContext context)
        {
            List<Guid> productGuids = request.Ids.Select(Guid.Parse).ToList();
            List<Product> products = await _productRepository.GetProducts(productGuids);

            Products res = new();
            products.ForEach(p => {
                GetProductRes productRes = new()
                {
                    Id = p.Id.ToString(),
                    ProductName = p.ProductName,
                    ProductThumb = p.ProductThumb,
                    ProductPrice = p.ProductPrice,
                    ProductType = p.ProductType.ToString(),
                    ProductShop = p.ProductShop.ToString()
                };
                res.Products_.Add(productRes);
            });

            return res;
        }
    }
}