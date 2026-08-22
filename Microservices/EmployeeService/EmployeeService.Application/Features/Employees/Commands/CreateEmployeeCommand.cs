using MediatR;

namespace EmployeeService.Application.Features.Employees.Commands;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    decimal Salary
) : IRequest<int>;