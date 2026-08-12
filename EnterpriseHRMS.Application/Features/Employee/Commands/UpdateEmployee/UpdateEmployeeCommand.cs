using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Commands.UpdateEmployee;

public sealed record UpdateEmployeeCommand(
    Guid Id,
    string EmployeeCode,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateOnly DateOfJoining,
    Guid DepartmentId,
    decimal Salary,
    bool IsActive
) : IRequest<bool>;