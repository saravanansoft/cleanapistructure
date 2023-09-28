using FluentValidation;

namespace API_Layer.DTOs.Departments;

public class DepartmentDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

