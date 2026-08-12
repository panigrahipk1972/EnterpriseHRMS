using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using EnterpriseHRMS.Domain.Entities;
using EnterpriseHRMS.Domain.Exceptions;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Guid> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        // Business Rule 1
        if (await _employeeRepository.ExistsByEmailAsync(
                request.Email,
                cancellationToken))
        {
            throw new DomainException("An employee with the same email already exists.");
        }

        // Create Domain Entity
        var employee = new Domain.Entities.Employee(
            request.EmployeeCode,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.DateOfJoining,
            request.Salary,
            request.DepartmentId);

        // Save
        await _employeeRepository.AddAsync(employee, cancellationToken);

        await _employeeRepository.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}