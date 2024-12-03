using NUnit.Framework;
using Chirp.Core;
using Chirp.Infrastructure.Test.Mock;

namespace Chirp.Tests
{
    public class CheepServiceTests
    {
        private CheepService _cheepService;
        private MockCheepRepository _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockCheepRepository();
            _cheepService = new CheepService(_mockRepository);
        }

        [Test]
        public void AddCheep_ShouldAddCheep()
        {
            var author = new Author { UserName = "TestAuthor" , Cheeps = new List<Cheep>()};
            var text = "Test Cheep";

           
            _cheepService.AddCheep(author, text);

            
            var cheeps = _mockRepository.ReadCheepDTO(1).Result;
            Assert.AreEqual(1, cheeps.Count);
            Assert.AreEqual("TestAuthor", cheeps[0].Author);
            Assert.AreEqual("Test Cheep", cheeps[0].Text);
        }
        
    }
}