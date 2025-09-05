using System.Text.Json;
using System.Xml.Linq;

/// <summary>
/// Filters employees based on specific criteria and returns results as JSON.
/// Criteria: Age 25-40 (inclusive), Department IT or Finance, Salary 5000-9000 (inclusive), HireDate after 2017
/// </summary>
/// <param name="employees">Collection of employee tuples</param>
/// <returns>JSON string with filtered results including names sorted by length desc then alphabetically, salary statistics, and count</returns>
static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
{
    // Filter employees based on criteria
    var filteredEmployees = employees.Where(emp => 
        emp.Age >= 25 && emp.Age <= 40 &&
        (emp.Department == "IT" || emp.Department == "Finance") &&
        emp.Salary >= 5000 && emp.Salary <= 9000 &&
        emp.HireDate > new DateTime(2016, 12, 31)
    ).ToList();

    // Sort names by length descending, then alphabetically
    var sortedNames = filteredEmployees
        .Select(emp => emp.Name)
        .OrderByDescending(name => name.Length)
        .ThenBy(name => name)
        .ToList();

    // Calculate statistics
    var totalSalary = filteredEmployees.Sum(emp => emp.Salary);
    var averageSalary = filteredEmployees.Count > 0 ? Math.Round(filteredEmployees.Average(emp => emp.Salary), 2) : 0;
    var minSalary = filteredEmployees.Count > 0 ? filteredEmployees.Min(emp => emp.Salary) : 0;
    var maxSalary = filteredEmployees.Count > 0 ? filteredEmployees.Max(emp => emp.Salary) : 0;
    var count = filteredEmployees.Count;

    // Create result object
    var result = new
    {
        Names = sortedNames,
        TotalSalary = totalSalary,
        AverageSalary = averageSalary,
        MinSalary = minSalary,
        MaxSalary = maxSalary,
        Count = count
    };

    return JsonSerializer.Serialize(result);
}