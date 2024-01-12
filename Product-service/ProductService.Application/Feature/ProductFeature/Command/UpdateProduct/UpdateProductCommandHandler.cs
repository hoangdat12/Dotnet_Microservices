using MediatR;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Exceptions;
using ProductService.Application.Response;


namespace ProductService.Application.Feature.ProductFeature.Command.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IProductFactory productFactory
    ) : IRequestHandler<UpdateProductCommand, BaseResponse>
    {
        private readonly IProductFactory _productFactory = productFactory;
        public async Task<BaseResponse> Handle(
            UpdateProductCommand request, 
            CancellationToken cancellationToken
        )
        {
            return await _productFactory.UpdateProduct(request);
        }
    }
}