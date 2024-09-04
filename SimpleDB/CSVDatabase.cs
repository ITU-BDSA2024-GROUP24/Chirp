namespace SimpleDB;

public class CSVDatabase<T> implements IDatabaseRepository<T>
{
    string recordDatabasePath = ".\\data\\chirp_cli_db.csv"
    public IEnumerable<T> Read(int limit)
    {
        using (StreamReader sr = new StreamReader(recordDatabasePath))
        using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
        {
            return records = csv.GetRecords<T>();    
    }
    public void Store(T record)
    {
        using (StreamWriter sw = new StreamWriter(recordDatabasePath, append: true))
        using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
        {
            csv.WriteRecord(record);
            csv.NextRecord();
        }
    }
}


