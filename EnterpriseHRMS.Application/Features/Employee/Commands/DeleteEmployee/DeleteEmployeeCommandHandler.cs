using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler
    : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(
        DeleteEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (employee is null)
        {
            return false;
        }

        _employeeRepository.Delete(employee);

        await _employeeRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}