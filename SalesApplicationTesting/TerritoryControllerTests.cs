using Microsoft.AspNetCore.Mvc;
using Moq;
using Sales_Application.Controllers;
using Sales_Application.DTO;
using Sales_Application.Repository.Contract;
using SprintSalesTesting.Mock;

namespace SprintSalesTesting
{
    [TestFixture]
    public class TerritoryControllerTests
    {
        private Mock<ITerritoryRepository> territoryRepositoryMock;
        private territoryController controller;

        [SetUp]
        public void SetUp()
        {
            territoryRepositoryMock = new Mock<ITerritoryRepository>();
            controller = new territoryController(territoryRepositoryMock.Object);
        }

        [Test]
        public async Task AddNewTerritory_ShouldReturnOkResult_WithExpectedResult()
        {
            // Arrange: create a sample TerritoryDto and expected result
            var territoryDto = MockData.GetTerritoryDto();
            var expectedResult = "Record Created Successfully";

            territoryRepositoryMock.Setup(repo => repo.addNewTerritory(It.IsAny<TerritoryDto>())).ReturnsAsync(expectedResult);

            // Act: call the method
            var result = await controller.AddNewTerritory(territoryDto);

            // Assert: verify the result
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedResult, okResult.Value);

        }

        [Test]
        public async Task GetAllTerritory_WhenResultIsNull_ShouldReturnNotFound()
        {
            // Arrange: Set up the mock repository to return null
            territoryRepositoryMock
        .Setup(repo => repo.getAllTerritory())
        .ReturnsAsync((List<TerritoryDto>)null);

            // Act: Call the method
            var result = await controller.GetAllTerritory();

            // Assert: Verify the result
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetAllTerritory_WhenResultIsNotNull_ShouldReturnOk()
        {
            // Arrange: Sample list of territories and set up the mock repository to return it
            var territories = MockData.GetTerritoryDtoList();

            territoryRepositoryMock
                .Setup(repo => repo.getAllTerritory())
                .ReturnsAsync(territories);

            // Act: Call the method
            var result = await controller.GetAllTerritory();

            // Assert: Verify the result
            Assert.NotNull(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

        }

        [Test]
        public async Task UpdateTheTerritory_WhenResultIsNull_ShouldReturnNotfound()
        {
            // Arrange
            var territoryId = "101";
            var territoryDto = MockData.GetTerritoryDto();
            territoryRepositoryMock.Setup(repo => repo.updateTheTerritory(territoryId, territoryDto))
                .ReturnsAsync((string)null); 

            // Act
            var result = await controller.UpdateTheTerritory(territoryId, territoryDto);

            // Assert
            var okResult = result as NotFoundObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public async Task UpdateTheTerritory_WhenResultIsNotNull_ShouldReturnOk()
        {
            // Arrange
            var territoryId = "101";
            var territoryDto = MockData.GetTerritoryDto();

            var expectedResult = "Updated Successfully"; // Replace with actual result type
            territoryRepositoryMock
                .Setup(repo => repo.updateTheTerritory(territoryId, territoryDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.UpdateTheTerritory(territoryId, territoryDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedResult, okResult.Value);
        }

        [Test]
        public async Task DeleteTheTerritory_WhenResultIsNull_ShouldReturnNotFound()
        {
            // Arrange: Set up the mock repository to return null
            var territorydto = MockData.GetTerritoryDto();
            var territoryId = "1020";
            territoryRepositoryMock.Setup(repo => repo.deleteTheTerritory(territoryId)).ReturnsAsync((string)null); // Adjust with actual result type

            // Act: Call the method
            var result = await controller.DeleteTheTerritory(territoryId);

            // Assert: Verify the result
             var okResult = result as NotFoundObjectResult;
              Assert.IsNotNull(okResult);
             Assert.AreEqual(404, okResult.StatusCode);

        }

        [Test]
        public async Task DeleteTheTerritory_WhenResultIsNotNull_ShouldReturnOk()
        {
            // Arrange: Create a sample result and set up the mock repository to return it
            var territoryId = "101";
            var expectedResult = "Deleted Successfully"; // Replace with actual result type
            territoryRepositoryMock
                .Setup(repo => repo.deleteTheTerritory(territoryId))
                .ReturnsAsync(expectedResult);

            // Act: Call the method
            var result = await controller.DeleteTheTerritory(territoryId);

            // Assert: Verify the result
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedResult, okResult.Value);
        }

    }
}
