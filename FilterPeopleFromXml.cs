using System.Text.Json;
using System.Xml.Linq;

/// <summary>
/// Filters people from XML data based on specific criteria and returns results as JSON.
/// Criteria: Age > 30, Department = "IT", Salary > 5000, HireDate < 2019
/// </summary>
/// <param name="xmlData">XML string containing Person data</param>
/// <returns>JSON string with filtered results including names, salary statistics, and count</returns>
static string FilterPeopleFromXml(string xmlData)
{
    try
    {
        // Parse the XML data
        var xmlDoc = XDocument.Parse(xmlData);
        var people = xmlDoc.Descendants("Person");
        
        var filteredPeople = new List<(string Name, decimal Salary)>();

        foreach (var person in people)
        {
            var name = person.Element("Name")?.Value ?? "";
            var ageStr = person.Element("Age")?.Value ?? "0";
            var department = person.Element("Department")?.Value ?? "";
            var salaryStr = person.Element("Salary")?.Value ?? "0";
            var hireDateStr = person.Element("HireDate")?.Value ?? "";

            // Parse values
            if (int.TryParse(ageStr, out int age) &&
                decimal.TryParse(salaryStr, out decimal salary) &&
                DateTime.TryParse(hireDateStr, out DateTime hireDate))
            {
                // Apply filters: Age > 30, Department = "IT", Salary > 5000, HireDate < 2019
                if (age > 30 && 
                    department == "IT" && 
                    salary > 5000 && 
                    hireDate.Year < 2019)
                {
                    filteredPeople.Add((name, salary));
                }
            }
        }

        // Sort names alphabetically
        var sortedNames = filteredPeople.Select(p => p.Name).OrderBy(n => n).ToList();
        
        // Calculate statistics
        var totalSalary = filteredPeople.Sum(p => p.Salary);
        var averageSalary = filteredPeople.Count > 0 ? filteredPeople.Average(p => p.Salary) : 0;
        var maxSalary = filteredPeople.Count > 0 ? filteredPeople.Max(p => p.Salary) : 0;
        var count = filteredPeople.Count;

        // Create result object
        var result = new
        {
            Names = sortedNames,
            TotalSalary = (int)totalSalary,
            AverageSalary = (int)averageSalary,
            MaxSalary = (int)maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
    catch
    {
        // Return empty result in case of any parsing errors
        var emptyResult = new
        {
            Names = new List<string>(),
            TotalSalary = 0,
            AverageSalary = 0,
            MaxSalary = 0,
            Count = 0
        };
        return JsonSerializer.Serialize(emptyResult);
    }
}