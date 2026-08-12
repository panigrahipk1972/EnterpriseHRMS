namespace EnterpriseHRMS.API.Models.Employees;

public sealed record PatchEmployeeRequest(
    string? EmployeeCode,
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    DateOnly? DateOfJoining,
    Guid? DepartmentId,
    decimal? Salary,
    bool? IsActive
);