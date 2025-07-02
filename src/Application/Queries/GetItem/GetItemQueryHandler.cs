using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;
using Application.Queries.GetItem;

namespace Application.Queries.GetItem
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IItemRepository _repository;

        public GetItemQueryHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(request.Id);
            if (item == null) return null;

            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedAt = item.CreatedAt
            };
        }
    }
}