using Microsoft.AspNetCore.Mvc;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.CompanyEmployee.Requests;
using TFM_Manager.Shared.CompanyEmployee.Responses;

namespace TFM_Manager.Api.Controllers
{
    [ApiController]
    [Route("api/company-employees")]
    public sealed class CompanyEmployeeController : ControllerBase
    {
        private readonly ICompanyEmployeeService _companyEmployeeService;

        public CompanyEmployeeController(ICompanyEmployeeService companyEmployeeService)
        {
            _companyEmployeeService = companyEmployeeService;
        }

        [HttpGet]
        [ProducesResponseType(
            typeof(AppResponse<List<CompanyEmployeeListItemResponse>>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int? companyId = null,
            [FromQuery] int? employeeId = null,
            [FromQuery] bool includeInactive = false)
        {
            var result = await _companyEmployeeService.GetAllAsync(
                companyId,
                employeeId,
                includeInactive);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(
            typeof(AppResponse<CompanyEmployeeResponse>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            typeof(AppResponse<CompanyEmployeeResponse>),
            StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _companyEmployeeService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(
        typeof(AppResponse<CompanyEmployeeResponse>),
        StatusCodes.Status201Created)]
        [ProducesResponseType(
        typeof(AppResponse<CompanyEmployeeResponse>),
        StatusCodes.Status201Created)]
        [ProducesResponseType(
        typeof(AppResponse<CompanyEmployeeResponse>),
        StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
        typeof(AppResponse<CompanyEmployeeResponse>),
        StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateCompanyEmployeeRequest request)
        {
            var result = await _companyEmployeeService.CreateAsync(request);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.CompanyEmployee.AlreadyExists => Conflict(result),
                    ErrorCodes.CompanyEmployee.ValidationError => BadRequest(result),
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
        typeof(AppResponse<CompanyEmployeeResponse>),
        StatusCodes.Status200OK)]
        [ProducesResponseType(
        typeof(AppResponse<CompanyEmployeeResponse>),
        StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
        typeof(AppResponse<CompanyEmployeeResponse>),
        StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyEmployeeRequest request)
        {
            var result = await _companyEmployeeService.UpdateAsync(id, request);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.CompanyEmployee.NotFound => NotFound(result),
                    ErrorCodes.CompanyEmployee.ValidationError => BadRequest(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }

        [HttpPatch("{id:int}/close")]
        [ProducesResponseType(
        typeof(AppResponse<bool>),
        StatusCodes.Status200OK)]
        [ProducesResponseType(
        typeof(AppResponse<bool>),
        StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
        typeof(AppResponse<bool>),
        StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Close(
        int id,
        [FromBody] CloseCompanyEmployeeRequest request)
        {
            var result = await _companyEmployeeService.CloseAsync(id, request);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.CompanyEmployee.NotFound => NotFound(result),
                    ErrorCodes.CompanyEmployee.ValidationError => BadRequest(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }
    }
}
