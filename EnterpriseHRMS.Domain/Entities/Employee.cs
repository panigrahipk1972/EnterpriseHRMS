using EnterpriseHRMS.Domain.Common;
using EnterpriseHRMS.Domain.Exceptions;

namespace EnterpriseHRMS.Domain.Entities;

public class Employee : BaseAuditableEntity
{
    private Employee()
    {
        // Required by EF Core
    }

    public Employee(
        string employeeCode,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        DateOnly dateOfJoining,
        decimal salary,
        Guid departmentId)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
            throw new DomainException("Employee code is required.");

        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required.");

        if (salary <= 0)
            throw new DomainException("Salary must be greater than zero.");

        if (departmentId == Guid.Empty)
            throw new DomainException("Department is required.");

        EmployeeCode = employeeCode;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfJoining = dateOfJoining;
        Salary = salary;
        DepartmentId = departmentId;
        IsActive = true;
    }

    public string EmployeeCode { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public DateOnly DateOfJoining { get; private set; }

   // public decimal Salary { get; private set; }

    public Guid DepartmentId { get; private set; }

    public virtual Department Department { get; private set; } = null!;

    public decimal Salary { get; private set; }

    public bool IsActive { get; private set; }

    public void ChangeSalary(decimal salary)
    {
        if (salary <= 0)
            throw new DomainException("Salary must be greater than zero.");

        Salary = salary;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void TransferDepartment(Guid departmentId)
    {
        if (departmentId == Guid.Empty)
            throw new DomainException("Department is required.");

        DepartmentId = departmentId;
    }

    public void UpdateContactDetails(string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required.");

        Email = email;
        PhoneNumber = phoneNumber;
    }
    public void UpdateBasicDetails(
    string employeeCode,
    string firstName,
    string lastName,
    DateOnly dateOfJoining)
{
    if (string.IsNullOrWhiteSpace(employeeCode))
        throw new DomainException("Employee code is required.");

    if (string.IsNullOrWhiteSpace(firstName))
        throw new DomainException("First name is required.");

    if (string.IsNullOrWhiteSpace(lastName))
        throw new DomainException("Last name is required.");

    EmployeeCode = employeeCode;
    FirstName = firstName;
    LastName = lastName;
    DateOfJoining = dateOfJoining;
}

    public string FullName => $"{FirstName} {LastName}";
}