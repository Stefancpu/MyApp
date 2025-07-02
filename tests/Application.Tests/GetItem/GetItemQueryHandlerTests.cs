using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Queries.GetItem;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.GetItem
{
    public class GetItemQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ItemExists_ReturnsMappedDto()
        {
            var sample = new Item("X", 1m);
            sample.GetType().GetProperty("Id").SetValue(sample, 55);

            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(55)).ReturnsAsync(sample);

            var handler = new GetItemQueryHandler(mockRepo.Object);
            var dto = await handler.Handle(new GetItemQuery(55), CancellationToken.None);

            dto.Should().NotBeNull();
            dto.Id.Should().Be(55);
            dto.Name.Should().Be("X");
            dto.Price.Should().Be(1m);
        }

        [Fact]
        public async Task Handle_ItemMissing_ReturnsNull()
        {
            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Item)null);

            var handler = new GetItemQueryHandler(mockRepo.Object);
            var dto = await handler.Handle(new GetItemQuery(99), CancellationToken.None);

            dto.Should().BeNull();
        }
    }
}