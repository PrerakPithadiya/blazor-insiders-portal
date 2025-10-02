using Xunit;
using Moq;
using Blazor_In_Insiders.Services;
using Blazor_In_Insiders.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Blazor_In_Insiders.Tests
{
    public class UserLoginServiceTests
    {
        [Fact]
        public async Task FindUserLoginAsync_ShouldReturnNull_WhenCredentialsDoNotMatch()
        {
            // Arrange
            var mockMongoDbService = new Mock<MongoDbService>(new Blazor_In_Insiders.Data.MongoDbSettings { ConnectionString = "mongodb://localhost:27017", DatabaseName = "TestDb" });
            var mockCollection = new Mock<IMongoCollection<UserLogin>>();
            var mockCursor = new Mock<IAsyncCursor<UserLogin>>();
            var users = new List<UserLogin>(); // Empty list

            mockCursor.Setup(_ => _.Current).Returns(users);
            mockCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(false);
            mockCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            mockCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<UserLogin>>(),
                It.IsAny<FindOptions<UserLogin, UserLogin>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            mockMongoDbService.Setup(s => s.GetCollection<UserLogin>("UserLogins")).Returns(mockCollection.Object);
            mockMongoDbService.Setup(s => s.GetCollection<UserLogin>("UserLogins")).Returns(mockCollection.Object);

            // Act
            var result = await UserLoginService.FindUserLoginAsync(mockMongoDbService.Object, "wrong@email.com", "wrongpassword");

            // Assert
            Assert.Null(result);
        }
    }
}