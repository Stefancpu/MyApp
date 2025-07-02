using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;
using Application.Queries.GetItem;

namespace Application.Queries.ListItems
{
    public class ListItemsQueryHandler : IRequestHandler<ListItemsQuery, IEnumerable<ItemDto>>
    {
        private readonly IItemRepository _repository;

        public ListItemsQueryHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ItemDto>> Handle(ListItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync();
            return items.Select(item => new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedAt = item.CreatedAt
            });
        }
    }
}