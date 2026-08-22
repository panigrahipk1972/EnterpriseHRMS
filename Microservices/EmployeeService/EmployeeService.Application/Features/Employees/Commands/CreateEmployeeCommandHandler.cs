using EmployeeService.Application.Interfaces;
using EmployeeService.Domain.Entities;
using MediatR;

namespace EmployeeService.Application.Features.Employees.Commands;

public class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IEmployeeRepository _repository;

    public CreateEmployeeCommandHandler(
        IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = new Employee(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Salary);

        var createdEmployee =
            await _repository.CreateAsync(employee);

        return createdEmployee.Id;
    }
}