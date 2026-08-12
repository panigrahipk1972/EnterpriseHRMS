using EnterpriseHRMS.Application.Features.Employee.DTOs;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Queries.GetEmployeeById;

public sealed record GetEmployeeByIdQuery(Guid Id)
    : IRequest<EmployeeDto?>;