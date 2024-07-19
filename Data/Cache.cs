namespace EmployeeDrawings.Data;
using EmployeeDrawings.Models;
using Microsoft.Graph.Models;

public class Cache
{
    public static IEnumerable<User> AllUsers { get; set; } = new List<User>();
    public static IEnumerable<Employee>? AllEmps { get; set; }
}

