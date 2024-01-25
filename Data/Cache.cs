namespace EmployeeDrawings.Data;

using EmployeeDrawings.Models;

public class Cache {
    public static IEnumerable<Microsoft.Graph.User> AllUsers { get; set; } = new List<Microsoft.Graph.User>();
    public static IEnumerable<Employee>? AllEmps { get; set; }
}

