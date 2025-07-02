using System.Collections.Generic;
using MediatR;
using Application.Queries.GetItem;

namespace Application.Queries.ListItems
{
    public record ListItemsQuery() : IRequest<IEnumerable<ItemDto>>;
}