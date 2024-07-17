using Journal.Database;
using Journal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Journal.Repository.Tests;

public class InputRepositoryTests
{
    private readonly IRepository<Input> _inputRepository;
    private readonly IQueryExecutor _queryExecutorSubstitute;
    private readonly InputContext _inputContext;

    public InputRepositoryTests()
    {
        _queryExecutorSubstitute = Substitute.For<IQueryExecutor>();
        _inputContext = new InputContext(new DbContextOptionsBuilder<InputContext>().UseInMemoryDatabase("Journal").Options);
        _inputRepository =
            new InputRepository(
                _queryExecutorSubstitute,
                Substitute.For<ILogger<InputRepository>>(),
                _inputContext);
    }


    [Fact]
    public async Task Create_ShouldReturnNewInput()
    {
        // Arrange
        var newInput = Input.BuildNewInput($"{nameof(Create_ShouldReturnNewInput)}_{DateTime.Now.ToString("u")}");

        // Act
        var createdInput = await _inputRepository.Insert(newInput);

        // Assert
        Assert.NotNull(createdInput);
        Assert.True(createdInput.Id > 0);
        Assert.Equal(newInput.InputText, createdInput.InputText);
    }
}