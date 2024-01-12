using MediatR;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Exceptions;
using ProductService.Application.Response;
using ProductService.Domain.Entity;

namespace ProductService.Application.Feature.ProductFeature.Command.DeleteProduct
{
    public class DeleteProductCommandHandler(
        IProductFactory productFactory
    ) : IRequestHandler<DeleteProductCommand, BaseResponse>
    {
        private readonly IProductFactory _productFactory = productFactory;
        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productFactory.DeleteProduct(request);
        }
    }
}