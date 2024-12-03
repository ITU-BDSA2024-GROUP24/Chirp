using Chirp.Core;

namespace Chirp.Infrastructure.Test.Mock
{
    public class MockCheepRepository : ICheepRepository
    {
        private readonly List<Cheep> _cheeps = new();
        private readonly List<Author> _authors = new();

        public MockCheepRepository()
        {
            var author1 = new Author()
            {
                UserName = "Author1",
                Email = "author1@example.com" ,
                Cheeps = new List<Cheep>()
            };

            var author2 = new Author()
            {
                UserName = "Author2",
                Email = "author2@example.com" ,
                Cheeps = new List<Cheep>()
            };

            _authors.AddRange(new[] { author1, author2 });
            _cheeps.AddRange(new[]
            
            {
                new Cheep
                {
                    Author = author1,
                    Text = "This is the first mock cheep.",
                    TimeStamp = DateTime.UtcNow.AddMinutes(-10)
                },
                new Cheep
                {
                    Author = author1,
                    Text = "This is another cheep from Author1.",
                    TimeStamp = DateTime.UtcNow.AddMinutes(-5)
                },
                new Cheep
                {
                    Author = author2,
                    Text = "Author2's first cheep is here!",
                    TimeStamp = DateTime.UtcNow
                }
            });
        }

        
        
        
        
        

        
        
        
        
        

        
        
        

        
        
        
        
        
        
        
        
        
        
        

        
        

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        
        

        
        
        
        
        
        
        
        
        

        
        
        
        
        

        
        
        
        
        
    }
}
