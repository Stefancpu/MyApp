using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateItem;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.CreateItem
{
    public class CreateItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_AddsItemAndReturnsId()
        {
            // Arrange
            var mockRepo = new Mock<IItemRepository>();
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<Item>()))
                .Returns(Task.CompletedTask)
                .Callback<Item>(i => i.GetType().GetProperty("Id").SetValue(i, 100));

            var handler = new CreateItemCommandHandler(mockRepo.Object);
            var command = new CreateItemCommand("TestItem", 12.34m);

            // Act
            var id = await handler.Handle(command, CancellationToken.None);

            // Assert
            id.Should().Be(100);
            mockRepo.Verify(r => r.AddAsync(It.Is<Item>(i =>
                i.Name == "TestItem" && i.Price == 12.34m
            )), Times.Once);
        }
    }
}