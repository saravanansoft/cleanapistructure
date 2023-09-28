using API_Layer.DTOs.Departments;
using FluentValidation;

namespace API_Layer.Validators;
 
public class DepartmentDTOValidator : AbstractValidator<DepartmentDTO>
{
    public DepartmentDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}