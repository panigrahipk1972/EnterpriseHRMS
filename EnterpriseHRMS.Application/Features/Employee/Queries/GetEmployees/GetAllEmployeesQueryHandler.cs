using AutoMapper;
using EnterpriseHRMS.Application.Features.Employee.DTOs;
using EnterpriseHRMS.Application.Features.Employee.Interfaces;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Employee.Queries.GetEmployees;

public sealed class GetAllEmployeesQueryHandler
    : IRequestHandler<GetAllEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetAllEmployeesQueryHandler(
        IEmployeeRepository employeeRepository,
        IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<EmployeeDto>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository
            .GetAllAsync(cancellationToken);

        return _mapper.Map<IReadOnlyList<EmployeeDto>>(employees);
    }
}