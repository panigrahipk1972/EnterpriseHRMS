using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Commands.DeleteEmployee;

public sealed record DeleteEmployeeCommand(Guid Id)
    : IRequest<bool>;