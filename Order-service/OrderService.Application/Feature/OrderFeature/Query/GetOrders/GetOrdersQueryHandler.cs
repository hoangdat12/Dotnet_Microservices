

using MediatR;
using OrderService.Application.Contract.Persistence;
using OrderService.Domain.Entity;

namespace OrderService.Application.Feature.OrderFeature.Query.GetOrders
{
    public class GetOrdersQueryHandler
    (
        IOrderRepository orderRepository
    ) : IRequestHandler<GetOrdersQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetByUserIdAsync(request.User.UserId, request.Pagination);
        }
    }
}