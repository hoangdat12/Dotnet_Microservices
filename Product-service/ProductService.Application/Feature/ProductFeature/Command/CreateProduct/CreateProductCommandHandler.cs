using MediatR;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Domain.Entity;


namespace ProductService.Application.Feature.ProductFeature.Command.CreateProduct
{
    public class CreateProductCommandHandler(
        IProductFactory productFactory
    ) : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductFactory _productFactory = productFactory;

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productFactory.CreateProduct(request);
        }
    }
}
