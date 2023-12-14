namespace EmployeeDrawings.Data;
public class Cache {
    public static IEnumerable<Microsoft.Graph.User> AllUsers { get; set; } = new List<Microsoft.Graph.User>();
}
