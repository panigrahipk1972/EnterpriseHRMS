using EnterpriseHRMS.Application.Features.Department.Queries.GetDepartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseHRMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetDepartmentsQuery(),
            cancellationToken);

        return Ok(result);
    }
}