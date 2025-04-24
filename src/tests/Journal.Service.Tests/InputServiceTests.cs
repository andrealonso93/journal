using Journal.Domain;
using Journal.Notification;
using Journal.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Journal.Service.Tests
{
    public class InputServiceTests
    {
        private readonly IInputService _inputService;
        private readonly IRepository<Input> _inputRepository = Substitute.For<IRepository<Input>>();

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
            
            var updatedText = "Updated test";
            _inputRepository.Update(Arg.Any<Input>()).Returns(Input.BuildNewInput(updatedText));
            
            // Act
            var response = await _inputService.UpdateInputAsync(existingInput.Id, updatedText);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(response.InputText, updatedText);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenInputTextIsTooLong()
        {
            // Arrange
            var bigText = new string('X', 600);
            
            // Act
            var response = await _inputService.CreateInputAsync(bigText);

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public async Task Should_ReturnNull_WhenRecord_DoesntExist()
        {
            // Arrange


            // Act


            // Assert

        }
    }
}