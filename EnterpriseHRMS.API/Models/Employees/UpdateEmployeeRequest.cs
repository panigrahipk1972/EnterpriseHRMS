namespace EnterpriseHRMS.API.Models.Employees;

public sealed record UpdateEmployeeRequest(
    string EmployeeCode,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateOnly DateOfJoining,
    Guid DepartmentId,
    decimal Salary,
    bool IsActive
);