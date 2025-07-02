using MediatR;

namespace Application.Commands.CreateItem
{
    public record CreateItemCommand(string Name, decimal Price) : IRequest<int>;
}