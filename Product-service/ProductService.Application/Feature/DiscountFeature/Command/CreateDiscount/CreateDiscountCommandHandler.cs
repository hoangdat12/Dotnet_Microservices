
using AutoMapper;
using MediatR;
using ProductService.Application.Constant;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Exceptions;
using ProductService.Domain.Entity;
using ShopGRPCService;

namespace ProductService.Application.Feature.DiscountFeature.Command.CreateDiscount
{
    public class CreateDiscountCommandHandler(
        IDiscountRepository discountRepository,
        IProductRepository productRepository,
        IShopGRPCClient shopGRPCClient,
        IMapper mapper
    ) : IRequestHandler<CreateDiscountCommand, Discount>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IShopGRPCClient _shopGRPCClient = shopGRPCClient;
        private readonly IMapper _mapper = mapper;

        public async Task<Discount> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {   
            List<Guid> discountProductIds = request.CreateDiscountReq.DiscountProductIds;
            Guid discountShopId = request.CreateDiscountReq.DiscountShopId;
            GetShopRes foundShop = null;
            
            if (discountProductIds.Count != 0) {
                List<Product> products = await _productRepository
                    .GetProducts(discountProductIds);

                if (products.Count != discountProductIds.Count)
                    throw new NotFoundException("Some product not found!");
            }

            if (!discountShopId.Equals(null)) {
                foundShop = await _shopGRPCClient.GetShopAsync(discountShopId.ToString());

                if (foundShop.Id.Equals(null))
                    throw new NotFoundException("Shop not found!");
            }

            _ = await _discountRepository.GetByDiscountCodeAsync(
                request.CreateDiscountReq.DiscountCode
            ) ?? throw new BadRequestException("Discount already exist, please change Discount Code!");

            if (
                foundShop != null && !foundShop.ShopName.Contains(request.User.UserId.ToString())
            )
                throw new ForbiddenException("Not permission!");

            if (foundShop.Equals(null) && request.User.Role == Role.USER.ToString())
                throw new ForbiddenException("Not permission!");

            Discount newDiscount = _mapper.Map<Discount>(request.CreateDiscountReq);

            return await _discountRepository.CreateAsync(newDiscount);
        }
    }
}