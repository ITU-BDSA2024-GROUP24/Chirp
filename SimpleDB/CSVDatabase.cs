using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public class CSVDatabase<T> : IDatabaseRepository<T> {
    string recordDatabasePath = ".\\data\\chirp_cli_db.csv";
    

    public IEnumerable<T> Read(int? limit = null)
    {
        using (StreamReader sr = new StreamReader(recordDatabasePath))
        using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<T>().ToList();
        }
    }

    public void Store(T record) {
        using (StreamWriter sw = new StreamWriter(recordDatabasePath, append: true))
        using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
        {
            csv.WriteRecord(record);
            csv.NextRecord();
        }
    } 
}



