using CsvHelper.Configuration;
using CsvHelper;
using Faker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleCsv.Data;
using System.Globalization;
using System.Text;
using System.Net;
using PeopleCsv.Web.ViewModels;

namespace PeopleCsv.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _connectionString;
        public FileController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet("generate")]
        public IActionResult Generate(int amount)
        {
            List<Person> people = GeneratePeople(amount);
            var csv = GenerateCsv(people);
            var csvBytes = Encoding.UTF8.GetBytes(csv);
            return File(csvBytes, "text/csv", "people.csv");

        }
     
        [HttpPost("upload")]
        public void Upload(UploadViewModel vm)
        {
            int indexOfComma = vm.Base64Data.IndexOf(',');
            string base64 = vm.Base64Data.Substring(indexOfComma + 1);
            byte[] bytes = Convert.FromBase64String(base64);
            List<Person> people = GetFromCsvBytes(bytes);
            var repo = new PeopleRepo(_connectionString);
            repo.Add(people);
        }
        [HttpGet("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepo(_connectionString);
            return repo.GetPeople();
        }
        [HttpPost("deleteall")]
        public void Delete()
        {
            var repo = new PeopleRepo(_connectionString);
            repo.DeleteAll();
        }
        public string GenerateCsv(List<Person> people)
        {
            var writer = new StringWriter();
            var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(people);

            return writer.ToString();
        }

        static List<Person> GeneratePeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Age = RandomNumber.Next(1, 100),
                Address = Address.StreetAddress(),
                Email = Internet.Email()
            }).ToList();

        }
        static List<Person> GetFromCsvBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<Person>().ToList();
        }
    }
}
