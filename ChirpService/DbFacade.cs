namespace Chirp.Razor

{
    using Microsoft.Data.Sqlite;
    using System;
    using System.Collections.Generic;

    public class DbFacade
    {
        /*private string? _sqlDbFilePath;


        public DbFacade()
        {
            try
            {
                
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

        public List<CheepViewModel> GetCheepsFromAuthor(string author, int skip)
        {
            var cheeps = new List<CheepViewModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var sqlQuery = @"SELECT message.text, user.username, message.pub_date 
                                 FROM message 
                                 JOIN user ON message.author_id = author_id 
                                 WHERE user.username = @Author
                                 ORDER BY message.pub_date DESC
                                 LIMIT 32 OFFSET @skip";

                using var command = new SqliteCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Author", author);
                command.Parameters.AddWithValue("@skip", skip);

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

        public List<CheepViewModel> GetCheeps(int skip)
        {
            var cheeps = new List<CheepViewModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var sqlQuery = @"SELECT message.text, user.username, message.pub_date 
                                 FROM message 
                                 JOIN user ON message.author_id = author_id 
                                 ORDER BY message.pub_date DESC
                                 LIMIT 32 OFFSET @skip";

                using var command = new SqliteCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@skip", skip);
                
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

     */   
    }
}
