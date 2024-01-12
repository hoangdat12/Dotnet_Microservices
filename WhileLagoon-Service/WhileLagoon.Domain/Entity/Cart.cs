using System.Text.Json.Serialization;
using WhileLagoon.Domain.Common;

namespace WhileLagoon.Domain.Entity
{
    public class Cart: BaseEntity
    {
        public List<CartProduct> Products {get; set;} = [];
        public Guid UserId {get; set;}
        [JsonIgnore]
        public User User {get; set;}
    }
}