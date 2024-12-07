using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chirp.Core;
using Chirp.Infrastructure;
using Moq;
using Xunit;


namespace Chirp.Tests
{
    public class CheepServiceTests
    {
        private readonly Mock<ICheepRepository> _mockRepository;
        private readonly CheepService _cheepService;

        public CheepServiceTests()
        {
            _mockRepository = new Mock<ICheepRepository>();
            _cheepService = new CheepService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetCheeps_ReturnsCorrectCheepViewModels()
        {
            // Arrange
            var cheepDTOs = new List<CheepDTO>
            {
                new CheepDTO
                {
                    Author = new Author { UserName = "Author1" },
                    Text = "Cheep1",
                    Timestamp = 1672531200 // Unix time for 2023-01-01 00:00:00 UTC
                },
                new CheepDTO
                {
                    Author = new Author { UserName = "Author2" },
                    Text = "Cheep2",
                    Timestamp = 1672617600 // Unix time for 2023-01-02 00:00:00 UTC
                }
            };

            _mockRepository.Setup(repo => repo.ReadCheepDTO(It.IsAny<int>()))
                .ReturnsAsync(cheepDTOs);
            
            var result = _cheepService.GetCheeps(1);

            Assert.Equal(2, result.Count);
            Assert.Equal("Author1", result[0].Author);
            Assert.Equal("Cheep1", result[0].Message);
            Assert.Equal("01/01/23 0:00:00", result[0].Timestamp);
        }

        [Fact]
        public async Task AddCheep_CallsRepositoryWithCorrectData()
        {
            // Arrange
            var author = new Author { UserName = "TestAuthor" };
            var text = "TestCheep";

            // Act
            await _cheepService.AddCheep(author, text);

            // Assert
            _mockRepository.Verify(repo => repo.CreateCheep(It.Is<Cheep>(c =>
                c.Author == author &&
                c.Text == text &&
                c.TimeStamp.Date == DateTime.Now.Date)), Times.Once);
        }

        [Fact]
        public async Task GetAuthorByName_ReturnsCorrectAuthor()
        {
            // Arrange
            var author = new Author { UserName = "TestAuthor" };
            _mockRepository.Setup(repo => repo.GetAuthorByName("TestAuthor"))
                .ReturnsAsync(author);

            // Act
            var result = await _cheepService.GetAuthorByName("TestAuthor");

            // Assert
            Assert.Equal(author, result);
        }

        [Fact]
        public async Task AddAuthor_CallsRepositoryWithCorrectData()
        {
            // Arrange
            var author = new Author { UserName = "NewAuthor" };

            // Act
            await _cheepService.AddAuthor(author);

            // Assert
            _mockRepository.Verify(repo => repo.CreateAuthor(author), Times.Once);
        }

        [Fact]
        public void GetCheepsFromAuthor_ReturnsCorrectCheepViewModels()
        {
            // Arrange
            var cheepDTOs = new List<CheepDTO>
            {
                new CheepDTO
                {
                    Author = new Author { UserName = "Author1" },
                    Text = "Cheep1",
                    Timestamp = 1672531200 // Unix time for 2023-01-01 00:00:00 UTC
                }
            };

            _mockRepository.Setup(repo => repo.ReadCheepDTOFromAuthor(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(cheepDTOs);

            // Act
            var result = _cheepService.GetCheepsFromAuthor(1, "Author1");

            // Assert
            Assert.Single(result);
            Assert.Equal("Author1", result[0].Author);
            Assert.Equal("Cheep1", result[0].Message);
            Assert.Equal("01/01/23 0:00:00", result[0].Timestamp);
        }
    }
}
