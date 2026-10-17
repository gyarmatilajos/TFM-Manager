using Microsoft.AspNetCore.Mvc;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.Employees.Requests;
using TFM_Manager.Shared.Employees.Responses;

namespace TFM_Manager.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(
            typeof(AppResponse<List<EmployeeListItemResponse>>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            typeof(AppResponse<object>),
            StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(
            [FromQuery] bool includeInactive = false)
        {
            var result = await _employeeService.GetAllAsync(includeInactive);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status201Created)]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            [FromBody] CreateEmployeeRequest request)
        {
            var result = await _employeeService.CreateAsync(request);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Employee.ValidationError => BadRequest(result),
                    ErrorCodes.Employee.AlreadyExists => Conflict(result),
                    _ => BadRequest(result)
                };
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data!.Id },
                result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status404NotFound)]
        [ProducesResponseType(
            typeof(AppResponse<EmployeeResponse>),
            StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateEmployeeRequest request)
        {
            var result = await _employeeService.UpdateAsync(id, request);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Employee.ValidationError => BadRequest(result),
                    ErrorCodes.Employee.NotFound => NotFound(result),
                    ErrorCodes.Employee.AlreadyExists => Conflict(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }

        [HttpPatch("{id:int}/deactivate")]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _employeeService.DeactivateAsync(id);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Employee.NotFound => NotFound(result),
                    ErrorCodes.Employee.ValidationError => BadRequest(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }

        [HttpPatch("{id:int}/activate")]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Activate(int id)
        {
            var result = await _employeeService.ActivateAsync(id);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Employee.NotFound => NotFound(result),
                    ErrorCodes.Employee.ValidationError => BadRequest(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }
    }
}
