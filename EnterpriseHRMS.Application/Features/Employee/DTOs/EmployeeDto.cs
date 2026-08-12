namespace EnterpriseHRMS.Application.Features.Employee.DTOs;

public sealed record EmployeeDto
{
    public Guid Id { get; init; }
    public string EmployeeCode { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public DateOnly  DateOfJoining { get; init; }
    public Guid DepartmentId { get; init; }
    public decimal Salary { get; init; }
    public bool IsActive { get; init; }
    public string FullName { get; init; } = string.Empty;
}