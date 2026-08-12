using AutoMapper;
using EnterpriseHRMS.Application.Features.Employee.DTOs;

namespace EnterpriseHRMS.Application.Features.Employee.Mappings;

public sealed class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EnterpriseHRMS.Domain.Entities.Employee, EmployeeDto>();
    }
}