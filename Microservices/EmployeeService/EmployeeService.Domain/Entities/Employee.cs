namespace EmployeeService.Domain.Entities;

public class Employee
{
    public int Id { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public decimal Salary { get; private set; }

    public bool IsActive { get; private set; }

    private Employee()
    {
    }

    public Employee(
        string firstName,
        string lastName,
        string email,
        decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Salary = salary;
        IsActive = true;
    }

    public void UpdateSalary(decimal salary)
    {
        Salary = salary;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}