namespace EmployeeDrawings.Models;
using Microsoft.Graph.Models;

public class Employee
{
    public User UserInfo { get; }

    public Employee(User user) { UserInfo = user; }
}
