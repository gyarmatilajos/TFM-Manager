using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.CompanyEmployee.Requests;
using TFM_Manager.Shared.CompanyEmployee.Responses;

namespace TFM_Manager.Business.Interfaces
{
    public interface ICompanyEmployeeService
    {
        Task<AppResponse<List<CompanyEmployeeListItemResponse>>> GetAllAsync(
        int? companyId = null,
        int? employeeId = null,
        bool includeInactive = false);

        Task<AppResponse<CompanyEmployeeResponse>> GetByIdAsync(int id);

        Task<AppResponse<CompanyEmployeeResponse>> CreateAsync(
            CreateCompanyEmployeeRequest request);

        Task<AppResponse<CompanyEmployeeResponse>> UpdateAsync(
            int id,
            UpdateCompanyEmployeeRequest request);

        Task<AppResponse<bool>> CloseAsync(
            int id,
            CloseCompanyEmployeeRequest request);
    }
}
