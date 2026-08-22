namespace EmployeeService.API.Models;

public record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    decimal Salary
);