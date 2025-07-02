using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Queries.GetItem;
using Application.Queries.ListItems;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.ListItems
{
    public class ListItemsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsAllItemsAsDto()
        {
            var items = new List<Item>
            {
                new Item("A", 1m),
                new Item("B", 2m)
            };
            items[0].GetType().GetProperty("Id").SetValue(items[0], 1);
            items[1].GetType().GetProperty("Id").SetValue(items[1], 2);

            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(r => r.ListAsync()).ReturnsAsync(items);

            var handler = new ListItemsQueryHandler(mockRepo.Object);
            var result = await handler.Handle(new ListItemsQuery(), CancellationToken.None);

            result.Should().HaveCount(2);
            result.Should().ContainSingle(x => x.Id == 1 && x.Name == "A");
            result.Should().ContainSingle(x => x.Id == 2 && x.Name == "B");
        }
    }
}