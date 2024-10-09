namespace Chirp.Razor
{
    using Microsoft.Data.Sqlite;
    using System;
    using System.Collections.Generic;

    public class DbFacade
    {
        private string _sqlDbFilePath;

        public DbFacade()
        {
            try
            {
                _sqlDbFilePath = Environment.GetEnvironmentVariable("CHIRPDBPATH") ?? "./data/chirp.db";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            
        }
            
        private SqliteConnection GetConnection()
        {
            return new SqliteConnection($"Data Source={_sqlDbFilePath};");
        }

        public List<CheepViewModel> GetCheepsFromAuthor(string author)
        {
            var cheeps = new List<CheepViewModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var sqlQuery = @"SELECT message.text, user.username, message.pub_date 
                                 FROM message 
                                 JOIN user ON message.author_id = author_id 
                                 WHERE user.username = @Author 
                                 ORDER BY message.pub_date DESC";

                using var command = new SqliteCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Author", author);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var message = reader.GetString(0); 
                    var timestamp = UnixTimeStampToDateTimeString(reader.GetInt64(2)); 

                    cheeps.Add(new CheepViewModel(author, message, timestamp));
                }
            }
            return cheeps;
        }

        public List<CheepViewModel> GetCheeps()
        {
            var cheeps = new List<CheepViewModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var sqlQuery = @"SELECT message.text, user.username, message.pub_date 
                                 FROM message 
                                 JOIN user ON message.author_id = author_id 
                                 ORDER BY message.pub_date DESC";

                using var command = new SqliteCommand(sqlQuery, connection);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var author = reader.GetString(1); 
                    var message = reader.GetString(0); 
                    var timestamp = UnixTimeStampToDateTimeString(reader.GetInt64(2)); 

                    cheeps.Add(new CheepViewModel(author, message, timestamp));
                }
            }
            return cheeps;
        }

        private string UnixTimeStampToDateTimeString(long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);
            return dateTime.ToString("MM/dd/yy H:mm:ss");
        }
    }
}
