using EnterpriseHRMS.Application.Features.Employee.DTOs;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Queries.GetEmployees;

public sealed record GetAllEmployeesQuery
    : IRequest<IReadOnlyList<EmployeeDto>>;