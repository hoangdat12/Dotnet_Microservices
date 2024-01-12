using MediatR;
using ProductService.Application.Dto;
using ProductService.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Feature.ProductFeature.Command.DraftProduct
{
    public record DraftProductCommand: IRequest<Product>
    {
        public UserDecode User {get; set;}
        [Required]
        public Guid Id {get; set;}
    }
}