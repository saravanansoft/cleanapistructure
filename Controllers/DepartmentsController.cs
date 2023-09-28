using Mapster;
using Microsoft.AspNetCore.Mvc;
using SERVICE_Layer.Interfaces;
using SERVICE_Layer.Models;
using System.Net;
using API_Layer.DTOs.Departments;
using API_Layer.Validators;
using SERVICE_Layer.Services;
using FluentValidation;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Get Department by Id
        /// </summary>
        /// <param name="id">Id of Department</param>
        /// <param name="cancellationToken">Token to cancel long running calls</param>
        /// <returns>Department</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ApiResponse> GetDepartmentByID(int id, CancellationToken cancellationToken)
        {
            return await _departmentService.GetDepartmentByID(id, cancellationToken);
        }

        /// <summary>
        /// Get Departments
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="isActiveOnly">Show Active Only</param>
        /// <returns>One or more Departments</returns>
        [HttpGet]
        public async Task<ApiResponse> GetDepartments(CancellationToken cancellationToken)
        {
            return await _departmentService.GetDepartments(cancellationToken);
        }

        /// <summary>
        /// Add Department
        /// </summary>
        /// <param name="departmentDTO">Department details</param>
        /// <returns>Status</returns>
        [HttpPost]
        public async Task<ApiResponse> AddDepartment(DepartmentDTO departmentDTO)
        {
            DepartmentDTOValidator validator = new DepartmentDTOValidator();
            var validationResult = await validator.ValidateAsync(departmentDTO);
            if (validationResult.IsValid)
                return await _departmentService.AddDepartment(departmentDTO.Adapt<DepartmentModel>());
            return new ApiResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = validationResult.Errors.ToArray()
            };
        }

        /// <summary>
        /// Update existing Department
        /// </summary>
        /// <param name="departmentDTO">Existing Department Details</param>
        /// <returns>Status</returns>
        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateDepartment(int id, [FromBody] DepartmentDTO departmentDTO)
        {
            DepartmentDTOValidator validator = new DepartmentDTOValidator();

            var validationResult = await validator.ValidateAsync(departmentDTO);
            
            
            if (validationResult.IsValid)
            {
                 return await _departmentService.UpdateDepartment(departmentDTO.Adapt<DepartmentModel>());
            }

            return new ApiResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = validationResult.Errors.ToArray()
            };
        }

        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="id">Id of Department</param>
        /// <returns>Status</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ApiResponse> DeleteDepartment(int id)
        {
            return await _departmentService.DeleteDepartment(id);
        }
         
    }
}
