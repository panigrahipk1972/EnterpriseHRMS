using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Commands.PatchEmployee;

public sealed class PatchEmployeeCommandHandler
    : IRequestHandler<PatchEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public PatchEmployeeCommandHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(
        PatchEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (employee is null)
        {
            return false;
        }

        // Basic details
        if (request.EmployeeCode is not null ||
            request.FirstName is not null ||
            request.LastName is not null ||
            request.DateOfJoining.HasValue)
        {
            employee.UpdateBasicDetails(
                request.EmployeeCode ?? employee.EmployeeCode,
                request.FirstName ?? employee.FirstName,
                request.LastName ?? employee.LastName,
                request.DateOfJoining ?? employee.DateOfJoining);
        }

        // Contact details
        if (request.Email is not null ||
            request.PhoneNumber is not null)
        {
            employee.UpdateContactDetails(
                request.Email ?? employee.Email,
                request.PhoneNumber ?? employee.PhoneNumber);
        }

        // Department
        if (request.DepartmentId.HasValue)
        {
            employee.TransferDepartment(
                request.DepartmentId.Value);
        }

        // Salary
        if (request.Salary.HasValue)
        {
            employee.ChangeSalary(
                request.Salary.Value);
        }

        // Active status
        if (request.IsActive.HasValue)
        {
            if (request.IsActive.Value)
            {
                employee.Activate();
            }
            else
            {
                employee.Deactivate();
            }
        }

        _employeeRepository.Update(employee);

        await _employeeRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}