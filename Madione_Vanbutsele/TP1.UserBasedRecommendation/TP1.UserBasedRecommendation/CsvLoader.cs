using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ITI.TP_UserBasedRecommendation
{
    public class CsvLoader<T>
    {
        public static IEnumerable<T> LoadCSV(string filename,  string delimiter = "|")
        {
            List<T> items;
            using (var reader = new StreamReader(Path(filename)))
            using (var csv = new CsvReader(reader, new CultureInfo("fr-FR")))
            {
                csv.Configuration.Delimiter = delimiter;
                csv.Configuration.HasHeaderRecord = false;

                //csv.Configuration.RegisterClassMap<T2>();

                var records = csv.GetRecords<T>();
                //words= new Dictionary<string,Word>() fast access with dictionnary
                items = new List<T>(records);
            }
            return items;
        }

        public static string Path(string filename)
        {
            return Environment.CurrentDirectory + "\\data\\"+ filename; 
        }

        public static bool FileExist(string filename)
        {
            return !File.Exists(Path(filename));
        }
    }
}
