using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Data.DbContext;
using TFM_Manager.Data.Entities.Employees;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.CompanyEmployee.Requests;
using TFM_Manager.Shared.CompanyEmployee.Responses;

namespace TFM_Manager.Business.Services
{
    public sealed class CompanyEmployeeService : ICompanyEmployeeService
    {
        private readonly TfmManagerDbContext _context;

        public CompanyEmployeeService(TfmManagerDbContext context)
        {
            _context = context;
        }
        public async Task<AppResponse<bool>> CloseAsync(int id, CloseCompanyEmployeeRequest request)
        {
            var companyEmployee = await _context.CompanyEmployees
        .FirstOrDefaultAsync(x => x.Id == id);

            if (companyEmployee is null)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.CompanyEmployee.NotFound,
                    "A lezárandó céges jogviszony nem található.");
            }

            if (!companyEmployee.IsActive)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A céges jogviszony már le van zárva.");
            }

            if (request is null || !request.EndDate.HasValue)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A lezárás dátumának megadása kötelező.");
            }

            if (request.EndDate.Value < companyEmployee.StartDate)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A lezárás dátuma nem lehet korábbi a jogviszony kezdeténél.");
            }

            companyEmployee.EndDate = request.EndDate.Value;
            companyEmployee.IsActive = false;

            await _context.SaveChangesAsync();

            return AppResponse<bool>.Success(
                "A céges jogviszony sikeresen lezárva.",
                true);
        }

        public async Task<AppResponse<CompanyEmployeeResponse>> CreateAsync(CreateCompanyEmployeeRequest request)
        {
            if (request is null ||
        request.StartDate is null ||
        string.IsNullOrWhiteSpace(request.Position))
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A kezdődátum és a munkakör megadása kötelező.");
            }

            if (request.EndDate.HasValue &&
                request.EndDate.Value < request.StartDate.Value)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A záródátum nem lehet korábbi a kezdődátumnál.");
            }

            var company = await _context.Companies
                .AsNoTracking()
                .Where(x => x.Id == request.CompanyId)
                .Select(x => new { x.Name, x.IsActive })
                .FirstOrDefaultAsync();

            if (company is null || !company.IsActive)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A megadott cég nem létezik vagy inaktív.");
            }

            var employee = await _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == request.EmployeeId)
                .Select(x => new { x.FullName, x.IsActive })
                .FirstOrDefaultAsync();

            if (employee is null || !employee.IsActive)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A megadott dolgozó nem létezik vagy inaktív.");
            }

            var employmentType = await _context.EmploymentTypes
                .AsNoTracking()
                .Where(x => x.Id == request.EmploymentTypeId)
                .Select(x => new { x.Name })
                .FirstOrDefaultAsync();

            if (employmentType is null)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A megadott foglalkoztatási típus nem található.");
            }

            var alreadyExists = await _context.CompanyEmployees
                .AnyAsync(x =>
                    x.CompanyId == request.CompanyId &&
                    x.EmployeeId == request.EmployeeId &&
                    x.IsActive);

            if (alreadyExists)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.AlreadyExists,
                    "A dolgozónak már van aktív jogviszonya ennél a cégnél.");
            }

            var companyEmployee = new CompanyEmployee
            {
                CompanyId = request.CompanyId,
                EmployeeId = request.EmployeeId,
                EmploymentTypeId = request.EmploymentTypeId,
                Position = request.Position.Trim(),
                StartDate = request.StartDate.Value,
                EndDate = request.EndDate,
                Notes = request.Notes,
                IsActive = true
            };

            _context.CompanyEmployees.Add(companyEmployee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx &&
                      (sqlEx.Number == 2601 || sqlEx.Number == 2627) &&
                      sqlEx.Message.Contains(
                          "IX_CompanyEmployees_CompanyId_EmployeeId",
                          StringComparison.OrdinalIgnoreCase))
            {
                _context.Entry(companyEmployee).State = EntityState.Detached;

                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.AlreadyExists,
                    "A dolgozónak már van aktív jogviszonya ennél a cégnél.");
            }

            var response = new CompanyEmployeeResponse
            {
                Id = companyEmployee.Id,
                CompanyId = companyEmployee.CompanyId,
                CompanyName = company.Name,
                EmployeeId = companyEmployee.EmployeeId,
                EmployeeName = employee.FullName,
                EmploymentTypeId = companyEmployee.EmploymentTypeId,
                EmploymentTypeName = employmentType.Name,
                Position = companyEmployee.Position,
                StartDate = companyEmployee.StartDate,
                EndDate = companyEmployee.EndDate,
                IsActive = companyEmployee.IsActive,
                Notes = companyEmployee.Notes
            };

            return AppResponse<CompanyEmployeeResponse>.Success(
                "A céges jogviszony sikeresen létrehozva.",
                response);
        }

        public async Task<AppResponse<List<CompanyEmployeeListItemResponse>>> GetAllAsync(int? companyId = null, int? employeeId = null, bool includeInactive = false)
        {
            var query = _context.CompanyEmployees.AsNoTracking();

            if (companyId.HasValue)
            {
                query = query.Where(x => x.CompanyId == companyId.Value);
            }

            if (employeeId.HasValue)
            {
                query = query.Where(x => x.EmployeeId == employeeId.Value);
            }

            if (!includeInactive)
            {
                query = query.Where(x => x.IsActive);
            }

            var companyEmployees = await query
                .OrderBy(x => x.Company.Name)
                .ThenBy(x => x.Employee.FullName)
                .Select(x => new CompanyEmployeeListItemResponse
                {
                    Id = x.Id,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FullName,
                    EmploymentTypeId = x.EmploymentTypeId,
                    EmploymentTypeName = x.EmploymentType.Name,
                    Position = x.Position,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return AppResponse<List<CompanyEmployeeListItemResponse>>.Success(
                "A céges jogviszonyok lekérdezése sikeres.",
                companyEmployees);
        }

        public async Task<AppResponse<CompanyEmployeeResponse>> GetByIdAsync(int id)
        {
            var companyEmployee = await _context.CompanyEmployees
        .AsNoTracking()
        .Where(x => x.Id == id)
        .Select(x => new CompanyEmployeeResponse
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            CompanyName = x.Company.Name,
            EmployeeId = x.EmployeeId,
            EmployeeName = x.Employee.FullName,
            EmploymentTypeId = x.EmploymentTypeId,
            EmploymentTypeName = x.EmploymentType.Name,
            Position = x.Position,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            IsActive = x.IsActive,
            Notes = x.Notes
        })
        .FirstOrDefaultAsync();

            if (companyEmployee is null)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.NotFound,
                    "A keresett céges jogviszony nem található.");
            }

            return AppResponse<CompanyEmployeeResponse>.Success(
                "A céges jogviszony lekérdezése sikeres.",
                companyEmployee);
        }

        public async Task<AppResponse<CompanyEmployeeResponse>> UpdateAsync(int id, UpdateCompanyEmployeeRequest request)
        {
            var companyEmployee = await _context.CompanyEmployees
       .FirstOrDefaultAsync(x => x.Id == id);

            if (companyEmployee is null)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.NotFound,
                    "A módosítandó céges jogviszony nem található.");
            }

            if (!companyEmployee.IsActive)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "Lezárt céges jogviszony nem módosítható.");
            }

            if (request is null ||
                request.StartDate is null ||
                string.IsNullOrWhiteSpace(request.Position))
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A kezdődátum és a munkakör megadása kötelező.");
            }

            if (request.EndDate.HasValue &&
                request.EndDate.Value < request.StartDate.Value)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A záródátum nem lehet korábbi a kezdődátumnál.");
            }

            var employmentType = await _context.EmploymentTypes
                .AsNoTracking()
                .Where(x => x.Id == request.EmploymentTypeId)
                .Select(x => new { x.Name })
                .FirstOrDefaultAsync();

            if (employmentType is null)
            {
                return AppResponse<CompanyEmployeeResponse>.Failure(
                    ErrorCodes.CompanyEmployee.ValidationError,
                    "A megadott foglalkoztatási típus nem található.");
            }

            companyEmployee.EmploymentTypeId = request.EmploymentTypeId;
            companyEmployee.Position = request.Position.Trim();
            companyEmployee.StartDate = request.StartDate.Value;
            companyEmployee.EndDate = request.EndDate;
            companyEmployee.Notes = request.Notes;

            await _context.SaveChangesAsync();

            var response = await _context.CompanyEmployees
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new CompanyEmployeeResponse
                {
                    Id = x.Id,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company.Name,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FullName,
                    EmploymentTypeId = x.EmploymentTypeId,
                    EmploymentTypeName = x.EmploymentType.Name,
                    Position = x.Position,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsActive = x.IsActive,
                    Notes = x.Notes
                })
                .FirstAsync();

            return AppResponse<CompanyEmployeeResponse>.Success(
                "A céges jogviszony sikeresen módosítva.",
                response);
        }
    }
}
