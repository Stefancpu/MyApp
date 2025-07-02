using MediatR;

namespace Application.Queries.GetItem
{
    public record GetItemQuery(int Id) : IRequest<ItemDto>;
}