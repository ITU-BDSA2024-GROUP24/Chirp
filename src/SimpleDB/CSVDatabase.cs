using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public sealed class CSVDatabase<T> : IDatabaseRepository<T> {
    private static string recordDatabasePath = ".\\data\\chirp_cli_db.csv";

    private static CSVDatabase<T> instance = null;
    private static readonly object singletonLock = new object();

    public static CSVDatabase<T> Instance
    {
        get
        {
            if (instance == null)
            {
                lock (singletonLock)
                {
                    if (instance == null)
                    {
                        instance = new CSVDatabase<T>();
                    }
                }
            }
            return instance;
        }
    }
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



