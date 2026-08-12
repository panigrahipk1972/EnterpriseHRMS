using EnterpriseHRMS.Application.Features.Employee.Commands.CreateEmployee;
using EnterpriseHRMS.Application.Features.Employee.Queries.GetEmployees;
using EnterpriseHRMS.Application.Features.Employee.Queries.GetEmployeeById;
using EnterpriseHRMS.Application.Features.Employee.Commands.UpdateEmployee;
using EnterpriseHRMS.Application.Features.Employee.Commands.DeleteEmployee;
using EnterpriseHRMS.API.Models.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseHRMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var employees = await _mediator.Send(
            new GetAllEmployeesQuery(),
            cancellationToken);

        return Ok(employees);
    }
    [HttpGet("{id:guid}")]
public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
{
    var employee = await _mediator.Send(
        new GetEmployeeByIdQuery(id),
        cancellationToken);

    if (employee is null)
    {
        return NotFound();
    }

    return Ok(employee);
}
[HttpPut("{id:guid}")]
public async Task<IActionResult> Update(
    Guid id,
    UpdateEmployeeRequest request,
    CancellationToken cancellationToken)
{
    var command = new UpdateEmployeeCommand(
        id,
        request.EmployeeCode,
        request.FirstName,
        request.LastName,
        request.Email,
        request.PhoneNumber,
        request.DateOfJoining,
        request.DepartmentId,
        request.Salary,
        request.IsActive);

    var result = await _mediator.Send(
        command,
        cancellationToken);

    if (!result)
    {
        return NotFound();
    }

    return NoContent();
}
[HttpDelete("{id:guid}")]
public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
{
    var result = await _mediator.Send(
        new DeleteEmployeeCommand(id),
        cancellationToken);

    if (!result)
    {
        return NotFound();
    }

    return NoContent();
}
}