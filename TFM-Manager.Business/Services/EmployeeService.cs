using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Data.DbContext;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.Employees.Requests;
using TFM_Manager.Shared.Employees.Responses;

namespace TFM_Manager.Business.Services
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly TfmManagerDbContext _context;

        public EmployeeService(TfmManagerDbContext context)
        {
            _context = context;
        }

        public async Task<AppResponse<bool>> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppResponse<EmployeeResponse>> CreateAsync(CreateEmployeeRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AppResponse<bool>> DeactivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppResponse<List<EmployeeListItemResponse>>> GetAllAsync(bool includeInactive = false)
        {
            var query = _context.Employees.AsNoTracking();

            if (!includeInactive)
            {
                query = query.Where(x => x.IsActive);
            }

            var employees = await query
                .OrderBy(x => x.FullName)
                .Select(x => new EmployeeListItemResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    EmployeeTypeId = x.EmployeeTypeId,
                    EmployeeTypeName = x.EmployeeType.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return AppResponse<List<EmployeeListItemResponse>>.Success(
                "A dolgozók lekérdezése sikeres",
                employees);
        }

        public async Task<AppResponse<EmployeeResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppResponse<EmployeeResponse>> UpdateAsync(int id, UpdateEmployeeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
