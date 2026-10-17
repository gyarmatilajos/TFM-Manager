using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Data.DbContext;
using TFM_Manager.Data.Entities.Employees;
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
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.Employee.NotFound,
                    "A megadott dolgozó nem található.");
            }

            if (employee.IsActive == true)
            {
                return AppResponse<bool>.Success("A dolgozó már aktív.", true);
            }

            employee.IsActive = true;
            await _context.SaveChangesAsync();

            return AppResponse<bool>.Success("A dolgozó sikeresen aktiválva.", true);
        }

        public async Task<AppResponse<EmployeeResponse>> CreateAsync(CreateEmployeeRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.FullName))
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.ValidationError,
                    "A dolgozó nevének megadása kötelező.");
            }

            var taxIdentificationNumber = string.IsNullOrWhiteSpace(request.TaxIdentificationNumber)
                ? null
                : request.TaxIdentificationNumber.Trim();

            if (taxIdentificationNumber is not null &&
                await _context.Employees.AnyAsync(x =>
                    x.TaxIdentificationNumber == taxIdentificationNumber))
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.AlreadyExists,
                    "A megadott adóazonosítóval már létezik dolgozó.");
            }

            var employeeType = await _context.EmployeeTypes
                .AsNoTracking()
                .Where(x => x.Id == request.EmployeeTypeId)
                .Select(x => new { x.Name })
                .FirstOrDefaultAsync();

            if (request.EmployeeTypeId <= 0 || employeeType is null)
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.ValidationError,
                    "A megadott dolgozótípus nem található.");
            }

            var employee = new Employee
            {
                FullName = request.FullName.Trim(),
                EmployeeTypeId = request.EmployeeTypeId,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                TaxIdentificationNumber = taxIdentificationNumber,
                SocialSecurityNumber = request.SocialSecurityNumber,
                DateOfBirth = request.DateOfBirth,
                PlaceOfBirth = request.PlaceOfBirth,
                MotherName = request.MotherName,
                Notes = request.Notes,
                IsActive = true
            };

            _context.Employees.Add(employee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx &&
                      (sqlEx.Number == 2601 || sqlEx.Number == 2627) &&
                      sqlEx.Message.Contains(
                          "IX_Employees_TaxIdentificationNumber",
                          StringComparison.OrdinalIgnoreCase))
            {
                _context.Entry(employee).State = EntityState.Detached;

                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.AlreadyExists,
                    "A megadott adóazonosítóval már létezik dolgozó.");
            }

            var response = new EmployeeResponse
            {
                Id = employee.Id,
                FullName = employee.FullName,
                EmployeeTypeId = employee.EmployeeTypeId,
                Phone = employee.Phone,
                Email = employee.Email,
                Address = employee.Address,
                TaxIdentificationNumber = employee.TaxIdentificationNumber,
                SocialSecurityNumber = employee.SocialSecurityNumber,
                DateOfBirth = employee.DateOfBirth,
                PlaceOfBirth = employee.PlaceOfBirth,
                MotherName = employee.MotherName,
                Notes = employee.Notes,
                IsActive = employee.IsActive,
                EmployeeTypeName = employeeType.Name
            };

            return AppResponse<EmployeeResponse>.Success(
                "A dolgozó sikeresen létrehozva.", response);
        }

        public async Task<AppResponse<bool>> DeactivateAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.Employee.NotFound,
                    "A megadott dolgozó nem található.");
            }

            if (employee.IsActive == false)
            {
                return AppResponse<bool>.Success("A dolgozó már inaktív.", true);
            }

            employee.IsActive = false;
            await _context.SaveChangesAsync();

            return AppResponse<bool>.Success("A dolgozó sikeresen inaktiválva.", true);
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
            var employee = await _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new EmployeeResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    EmployeeTypeId = x.EmployeeTypeId,
                    Phone = x.Phone,
                    Email = x.Email,
                    Address = x.Address,
                    TaxIdentificationNumber = x.TaxIdentificationNumber,
                    SocialSecurityNumber = x.SocialSecurityNumber,
                    DateOfBirth = x.DateOfBirth,
                    PlaceOfBirth = x.PlaceOfBirth,
                    MotherName = x.MotherName,
                    Notes = x.Notes,
                    IsActive = x.IsActive,
                    EmployeeTypeName = x.EmployeeType.Name
                })
                .FirstOrDefaultAsync();

            if (employee is null)
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.NotFound,
                    "A keresett dolgozó nem található.");
            }

            return AppResponse<EmployeeResponse>.Success(
                "A dolgozó lekérdezése sikeres.", employee);
        }

        public async Task<AppResponse<EmployeeResponse>> UpdateAsync(int id, UpdateEmployeeRequest request)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.NotFound,
                    "A módosítandó dolgozó nem található.");
            }

            if (request is null || string.IsNullOrWhiteSpace(request.FullName))
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.ValidationError,
                    "A dolgozó nevének megadása kötelező.");
            }

            var taxIdentificationNumber = string.IsNullOrWhiteSpace(request.TaxIdentificationNumber)
                ? null
                : request.TaxIdentificationNumber.Trim();

            if (taxIdentificationNumber is not null &&
                await _context.Employees.AnyAsync(x =>
                    x.TaxIdentificationNumber == taxIdentificationNumber && x.Id != id))
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.AlreadyExists,
                    "A megadott adóazonosítóval már létezik másik dolgozó.");
            }

            var employeeType = await _context.EmployeeTypes
                .AsNoTracking()
                .Where(x => x.Id == request.EmployeeTypeId)
                .Select(x => new { x.Name })
                .FirstOrDefaultAsync();

            if (request.EmployeeTypeId <= 0 || employeeType is null)
            {
                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.ValidationError,
                    "A megadott dolgozótípus nem található.");
            }

            employee.FullName = request.FullName.Trim();
            employee.EmployeeTypeId = request.EmployeeTypeId;
            employee.Phone = request.Phone;
            employee.Email = request.Email;
            employee.Address = request.Address;
            employee.TaxIdentificationNumber = taxIdentificationNumber;
            employee.SocialSecurityNumber = request.SocialSecurityNumber;
            employee.DateOfBirth = request.DateOfBirth;
            employee.PlaceOfBirth = request.PlaceOfBirth;
            employee.MotherName = request.MotherName;
            employee.Notes = request.Notes;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx &&
                      (sqlEx.Number == 2601 || sqlEx.Number == 2627) &&
                      sqlEx.Message.Contains(
                          "IX_Employees_TaxIdentificationNumber",
                          StringComparison.OrdinalIgnoreCase))
            {
                var entry = _context.Entry(employee);
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;

                return AppResponse<EmployeeResponse>.Failure(
                    ErrorCodes.Employee.AlreadyExists,
                    "A megadott adóazonosítóval már létezik másik dolgozó.");
            }

            var response = new EmployeeResponse
            {
                Id = employee.Id,
                FullName = employee.FullName,
                EmployeeTypeId = employee.EmployeeTypeId,
                Phone = employee.Phone,
                Email = employee.Email,
                Address = employee.Address,
                TaxIdentificationNumber = employee.TaxIdentificationNumber,
                SocialSecurityNumber = employee.SocialSecurityNumber,
                DateOfBirth = employee.DateOfBirth,
                PlaceOfBirth = employee.PlaceOfBirth,
                MotherName = employee.MotherName,
                Notes = employee.Notes,
                IsActive = employee.IsActive,
                EmployeeTypeName = employeeType.Name
            };

            return AppResponse<EmployeeResponse>.Success(
                "A dolgozó adatai sikeresen módosítva.", response);
        }
    }
}
