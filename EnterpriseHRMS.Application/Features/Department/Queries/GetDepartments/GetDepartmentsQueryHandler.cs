using EnterpriseHRMS.Application.Features.Department.Interfaces;
using MediatR;

namespace EnterpriseHRMS.Application.Features.Department.Queries.GetDepartments;

public sealed class GetDepartmentsQueryHandler
    : IRequestHandler<GetDepartmentsQuery, List<DepartmentDto>>
{
    private readonly IDepartmentRepository _repository;

    public GetDepartmentsQueryHandler(
        IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DepartmentDto>> Handle(
        GetDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var departments = await _repository.GetAllAsync(cancellationToken);

        return departments
            .Select(x => new DepartmentDto(
                x.Id,
                x.DepartmentCode,
                x.Name))
            .ToList();
    }
}