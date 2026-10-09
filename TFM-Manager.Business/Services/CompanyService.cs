using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.Companies.Requests;
using TFM_Manager.Shared.Companies.Responses;

namespace TFM_Manager.Business.Services
{
    public interface CompanyService
    {
        Task<AppResponse<List<CompanyListItemResponse>>> GetAllAsync(bool includeInactive = false);
        Task<AppResponse<CompanyResponse>> GetByIdAsync(int id);
        Task<AppResponse<CompanyResponse>> CreateAsync(CreateCompanyRequest request);
        Task<AppResponse<CompanyResponse>> UpdateAsync(int id, UpdateCompanyRequest request);
        Task<AppResponse<bool>> DeactivateAsync(int id);
        Task<AppResponse<bool>> ActivateAsync(int id);
    }
}
