using Journal.Domain;
using Journal.Notification;
using Journal.Repository;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Journal.Service.Tests
{
    public class InputServiceTests
    {
        private readonly IInputService _inputService;
        private readonly InputRepository _inputRepository = Substitute.For<InputRepository>();

        public InputServiceTests()
        {
            _inputService = new InputService(_inputRepository, Substitute.For<ILogger<InputService>>(), Substitute.For<NotificationContext>());
        }

        [Fact]
        public async Task Should_UpdateExistingRecord()
        {
            // Arrange
            var existingInput = new Input { Id = 1, InputText = "Test" };
            _inputRepository.Find(1).Returns(existingInput);
            var updatedInput = existingInput;
            updatedInput.InputText = "Updated Test";

            // Act
            var response = await _inputRepository.Update(updatedInput);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(response.InputText, updatedInput.InputText);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenInputTextIsTooLong()
        {
            // Arrange


            // Act


            // Assert

        }


        [Fact]
        public async Task Should_RetunNull_WhenRecord_DoesntExist()
        {
            // Arrange


            // Act


            // Assert

        }
    }
}