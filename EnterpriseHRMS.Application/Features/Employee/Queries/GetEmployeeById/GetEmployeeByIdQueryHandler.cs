using AutoMapper;
using EnterpriseHRMS.Application.Features.Employee.DTOs;
using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Queries.GetEmployeeById;

public sealed class GetEmployeeByIdQueryHandler
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(
        IEmployeeRepository employeeRepository,
        IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeDto?> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (employee is null)
        {
            return null;
        }

        return _mapper.Map<EmployeeDto>(employee);
    }
}