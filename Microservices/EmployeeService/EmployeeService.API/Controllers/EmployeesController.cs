using EmployeeService.API.Models;
using EmployeeService.Application.Features.Employees.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.API.Controllers;

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
    public async Task<IActionResult> CreateEmployee(
        CreateEmployeeRequest request)
    {
        var command = new CreateEmployeeCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Salary);

        var employeeId = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(CreateEmployee),
            new { id = employeeId },
            new { id = employeeId });
    }
}