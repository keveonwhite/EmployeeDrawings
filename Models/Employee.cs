namespace EmployeeDrawings.Models;

public class Employee {
    public Microsoft.Graph.User UserInfo { get; }

    public Employee(Microsoft.Graph.User user) { UserInfo = user; }
}
