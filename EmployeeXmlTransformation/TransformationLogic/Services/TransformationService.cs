using System.Xml.Serialization;
using System.Xml;
using TransformationLogic.Models;
using TransformationLogic.Interfaces;
using CsvHelper;
using System.Globalization;
using log4net;

namespace TransformationLogic.Services
{
    public class TransformationService : ITransformationService
    {
        private const string OutputFileName = "output.csv";

        private readonly ILog _logger;

        public TransformationService(ILog logger)
        {
            _logger = logger;
        }

        public void Transform(InputDataDto inputData)
        {
            _logger.Info($"Transformation started.");
            var transformedEmployees = DeserializeEmployeeXmlFiles(inputData.InputFolder);
            SerializeEmployeesToCsv(transformedEmployees, inputData.OutputFolder);

            _logger.Info($"Transformation finished. Csv file saved to {Path.Combine(inputData.OutputFolder, OutputFileName)}");
            Console.WriteLine($"Csv file saved to {Path.Combine(inputData.OutputFolder, OutputFileName)}");
        }

        private IEnumerable<TransformedEmployee> DeserializeEmployeeXmlFiles(string folder)
        {
            var inputFiles = Directory.GetFiles(folder, "*.xml");

            List<TransformedEmployee> result = new List<TransformedEmployee>();

            foreach (var file in inputFiles)
            {
                result.AddRange(DeserializeEmployeeXmlFile(file));
            }

            _logger.Info($"{result.Count} records created.");
            return result;
        }

        private void SerializeEmployeesToCsv(IEnumerable<TransformedEmployee> transformedEmployees, string outputFolder)
        {
            using (TextWriter writer = new StreamWriter(Path.Combine(outputFolder, OutputFileName), false, System.Text.Encoding.UTF8))
            {
                var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(transformedEmployees);
            }
        }

        private IEnumerable<TransformedEmployee> DeserializeEmployeeXmlFile(string input)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Employer));
            using (XmlReader reader = XmlReader.Create(input))
            {
                var employer = (Employer)ser.Deserialize(reader);

                if (employer == null)
                {
                    _logger.Error($"File {input} could not be deserialized.");
                    throw new NullReferenceException($"File {input} could not be deserialized.");
                }
                
                return Map(employer);
            }
        }

        private IEnumerable<TransformedEmployee> Map(Employer employer)
        {
            return employer.Employees.Select(x => Map(x, employer.CompanyName)).ToList();
        }

        private TransformedEmployee Map(Employee employee, string companyName)
        {
            return new TransformedEmployee()
            {
                CompanyName = companyName,
                EmployeeName = $"{FirstLetterToUpperCase(employee.FirstName)} {FirstLetterToUpperCase(employee.LastName)}",
                EmployeeNumber = employee.Id,
                EmployeeAddress = employee.Address == null ? null : 
                employee.Address.StreetNo == null ? $"{employee.Address.Street}, {employee.Address.City}" :
                    $"{employee.Address.Street} {employee.Address.StreetNo}, {employee.Address.City}",
                EmployedSince = employee.EmployedSince == null ? "NA" : employee.EmployedSince?.ToString("yyyy-MM-dd")
            };
        }

        private string FirstLetterToUpperCase(string name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
        }
    }
}
