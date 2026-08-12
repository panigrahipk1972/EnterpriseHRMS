using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler
    : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(
        UpdateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (employee is null)
        {
            return false;
        }

        /*employee.EmployeeCode = request.EmployeeCode;
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.PhoneNumber = request.PhoneNumber;
        employee.DateOfJoining = request.DateOfJoining;
        employee.DepartmentId = request.DepartmentId;
        employee.Salary = request.Salary;
        employee.IsActive = request.IsActive;*/
        employee.UpdateBasicDetails(
    request.EmployeeCode,
    request.FirstName,
    request.LastName,
    request.DateOfJoining);

employee.UpdateContactDetails(
    request.Email,
    request.PhoneNumber);

employee.TransferDepartment(
    request.DepartmentId);

employee.ChangeSalary(
    request.Salary);

if (request.IsActive)
{
    employee.Activate();
}
else
{
    employee.Deactivate();
}

        _employeeRepository.Update(employee);

        await _employeeRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}