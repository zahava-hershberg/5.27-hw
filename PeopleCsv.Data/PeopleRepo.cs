using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCsv.Data
{
    public class PeopleRepo
    {
        private readonly string _connectionString;
        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(List<Person>person)
        {
            using var context = new PeopleCsvDataContext(_connectionString);
            foreach(var p in person)
            {
                context.CsvPeople.Add(p);
            }
          
            context.SaveChanges();
        }
        public List<Person> GetPeople()
        {
            using var context = new PeopleCsvDataContext(_connectionString);
            return context.CsvPeople.ToList();
        }
        public void DeleteAll()
        {
            using var context = new PeopleCsvDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM CsvPeople");
        }
     

    }
}
