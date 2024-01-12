using MediatR;
using System.ComponentModel.DataAnnotations;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.ShopAddressFeature.Command.CreateShopAddress
{
    public record CreateShopAddressCommand: IRequest<ShopAddress>
    {

        [Required]
        public string AddressUserName { get; set; }

        [Required]
        public string AddressPhone { get; set; }

        [Required]
        public string AddressCountry { get; set; }

        [Required]
        public string AddressCity { get; set; }
        [Required]
        public string AddresState { get; set; }

        [Required]
        public string AddressDetail { get; set; }
    }
}
