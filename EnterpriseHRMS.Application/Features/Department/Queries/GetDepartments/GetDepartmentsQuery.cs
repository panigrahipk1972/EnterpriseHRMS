using MediatR;

namespace EnterpriseHRMS.Application.Features.Department.Queries.GetDepartments;

public record GetDepartmentsQuery() : IRequest<List<DepartmentDto>>;

public record DepartmentDto(
    Guid Id,
    string DepartmentCode,
    string Name);