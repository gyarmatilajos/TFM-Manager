using Microsoft.AspNetCore.Mvc;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Business.Services;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.Companies.Requests;
using TFM_Manager.Shared.Companies.Responses;

namespace TFM_Manager.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public sealed class CompanyController : ControllerBase
    {
        private readonly IcompanyService _companyService;

        public CompanyController(IcompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyListItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
        {
            var result = await _companyService.GetAllAsync(includeInactive);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _companyService.GetByIdAsync(id);
           
            if(!result.IsSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(
        typeof(AppResponse<CompanyResponse>),
        StatusCodes.Status201Created)]
        [ProducesResponseType(
        typeof(AppResponse<CompanyResponse>),
        StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
        typeof(AppResponse<CompanyResponse>),
        StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
        [FromBody] CreateCompanyRequest request)
        {
            var result = await _companyService.CreateAsync(request);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Company.AlreadyExists => Conflict(result),
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
        typeof(AppResponse<CompanyResponse>),
        StatusCodes.Status200OK)]
        [ProducesResponseType(
        StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
        StatusCodes.Status404NotFound)]
        [ProducesResponseType(
        StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyRequest request)
        {
            var result = await _companyService.UpdateAsync(id, request);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Company.NotFound => NotFound(result),
                    ErrorCodes.Company.AlreadyExists => Conflict(result),
                    ErrorCodes.Company.ValidationError => BadRequest(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }


        [HttpPatch("{id:int}/deactivate")]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _companyService.DeactivateAsync(id);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Company.NotFound => NotFound(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }


        [HttpPatch("{id:int}/activate")]
        [ProducesResponseType(
            typeof(AppResponse<bool>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Activate(int id)
        {
            var result = await _companyService.ActivateAsync(id);

            if (!result.IsSuccess)
            {
                return result.Error?.Code switch
                {
                    ErrorCodes.Company.NotFound => NotFound(result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }


    }
}
