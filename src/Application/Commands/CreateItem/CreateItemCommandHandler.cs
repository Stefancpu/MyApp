using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
    {
        private readonly IItemRepository _repository;

        public CreateItemCommandHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item(request.Name, request.Price);
            await _repository.AddAsync(item);
            return item.Id;
        }
    }
}