using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.Employees.Requests;
using TFM_Manager.Shared.Employees.Responses;

namespace TFM_Manager.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<AppResponse<List<EmployeeListItemResponse>>> GetAllAsync(bool includeInactive = false);
        Task<AppResponse<EmployeeResponse>> GetByIdAsync(int id);
        Task<AppResponse<EmployeeResponse>> CreateAsync(CreateEmployeeRequest request);
        Task<AppResponse<EmployeeResponse>> UpdateAsync(int id, UpdateEmployeeRequest request);
        Task<AppResponse<bool>> ActivateAsync(int id);
        Task<AppResponse<bool>> DeactivateAsync(int id);
    }
}
