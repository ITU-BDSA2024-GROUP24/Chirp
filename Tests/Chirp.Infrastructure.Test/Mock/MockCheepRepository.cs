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

        public Task CreateCheep(Cheep newCheep)
        {
            _cheeps.Add(newCheep);
            return Task.CompletedTask;
        }

        public Task CreateAuthor(Author newAuthor)
        {
            _authors.Add(newAuthor);
            return Task.CompletedTask;
        }

        public Task<List<CheepDTO>> ReadCheepDTO(int page)
        {
            const int CheepsPerPage = 32;

            var mockCheeps = _cheeps
                .OrderByDescending(cheep => cheep.TimeStamp)
                .Skip((page - 1) * CheepsPerPage)
                .Take(CheepsPerPage)
                .Select(cheep => new CheepDTO
                {
                    Author = cheep.Author.UserName,
                    Text = cheep.Text,
                    Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds
                })
                .ToList();

            return Task.FromResult(mockCheeps);
        }

        public Task<List<CheepDTO>> ReadCheepDTOFromAuthor(int page, string authorName)
        {
            const int CheepsPerPage = 32;
            var mockCheeps = _cheeps
                .Where(cheep => cheep.Author.UserName == authorName)
                .OrderByDescending(cheep => cheep.TimeStamp)
                .Skip((page - 1) * CheepsPerPage)
                .Take(CheepsPerPage)
                .Select(cheep => new CheepDTO
                {
                    Author = cheep.Author.UserName,
                    Text = cheep.Text,
                    Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds
                })
                .ToList();

            return Task.FromResult(mockCheeps);
        }

        public Task UpdateCheep(CheepDTO alteredCheep)
        {
            var cheep = _cheeps.FirstOrDefault(c => c.Text == alteredCheep.Text);
            if (cheep != null)
            {
                cheep.Text = alteredCheep.Text;
            }
            return Task.CompletedTask;
        }

        public Task<Author> GetAuthorByName(string name)
        {
            var author = _authors.FirstOrDefault(a => a.UserName == name);
            return Task.FromResult(author);
        }

        public Task<Author> GetAuthorByEmail(string email)
        {
            var author = _authors.FirstOrDefault(a => a.Email == email);
            return Task.FromResult(author);
        }
    }
}
