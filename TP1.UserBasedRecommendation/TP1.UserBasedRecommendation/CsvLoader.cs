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
        /// <summary>
        /// Load a CSV, in this case, the french lexic
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> LoadCSV(string filename,  string delimiter = "|")
        {
            List<T> items;
            //CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);
            //config.Delimiter = ";";

            //TODO add path helper
            //new CultureInfo("fr-FR")
            //CultureInfo.InvariantCulture
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
    }
}
