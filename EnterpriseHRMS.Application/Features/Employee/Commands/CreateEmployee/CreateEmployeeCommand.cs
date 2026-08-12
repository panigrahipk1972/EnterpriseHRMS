using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Commands.CreateEmployee;

public sealed record CreateEmployeeCommand(
    string EmployeeCode,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateOnly DateOfJoining,
    decimal Salary,
    Guid DepartmentId
) : IRequest<Guid>;