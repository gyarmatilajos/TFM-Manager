using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Data.DbContext;
using TFM_Manager.Data.Entities.Companies;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.Companies.Requests;
using TFM_Manager.Shared.Companies.Responses;
using Microsoft.Data.SqlClient;

namespace TFM_Manager.Business.Services
{
    public sealed class CompanyService : IcompanyService
    {
        private readonly TfmManagerDbContext _context;

        public CompanyService(TfmManagerDbContext context)
        {
            _context = context;
        }

        public async Task<AppResponse<bool>> ActivateAsync(int id)
        {
            var company = await _context.Companies
        .FirstOrDefaultAsync(x => x.Id == id);

            if (company is null)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.Company.NotFound,
                    "A megadott cég nem található.");
            }

            if (company.IsActive)
            {
                return AppResponse<bool>.Success(
                    "A cég már aktív.",
                    true);
            }

            company.IsActive = true;

            await _context.SaveChangesAsync();

            return AppResponse<bool>.Success(
                "A cég sikeresen aktiválva.",
                true);
        }

        public async Task<AppResponse<CompanyResponse>> CreateAsync(CreateCompanyRequest request)
        {
            // Kötelező mezők ellenőrzése
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.TaxNumber) ||
                string.IsNullOrWhiteSpace(request.RegisteredAddress))
            {
                return AppResponse<CompanyResponse>.Failure(
                    ErrorCodes.Company.ValidationError,
                    "A cégnév, az adószám és a székhely megadása kötelező.");
            }

            // Adószám egyediségének ellenőrzése
            var taxnumber = request.TaxNumber.Trim();
            var taxNumberExists = await _context.Companies
                .AnyAsync(x => x.TaxNumber == taxnumber);

            if (taxNumberExists)
            {
                return AppResponse<CompanyResponse>.Failure(
                    ErrorCodes.Company.AlreadyExists,
                    "A megadott adószámmal már létezik cég.");
            }

            // Képviselő ellenőrzése
            if (request.RepresentativeEmployeeId.HasValue)
            {
                var employeeExists = await _context.Employees
                    .AnyAsync(x =>
                        x.Id == request.RepresentativeEmployeeId.Value);

                if (!employeeExists)
                {
                    return AppResponse<CompanyResponse>.Failure(
                        ErrorCodes.Company.ValidationError,
                        "A megadott képviselő nem található.");
                }
            }

            // Új cég létrehozása
            var company = new Company
            {
                Name = request.Name.Trim(),
                TaxNumber = taxnumber,
                CompanyRegistrationNumber = request.CompanyRegistrationNumber,
                RegisteredAddress = request.RegisteredAddress.Trim(),
                MailingAddress = request.MailingAddress,
                BillingEmail = request.BillingEmail,
                RepresentativeEmployeeId = request.RepresentativeEmployeeId,
                Notes = request.Notes,
                IsActive = true
            };

            _context.Companies.Add(company);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx &&
                      (sqlEx.Number == 2601 || sqlEx.Number == 2627) &&
                      sqlEx.Message.Contains(
                          "IX_Companies_TaxNumber",
                          StringComparison.OrdinalIgnoreCase))
            {
                return AppResponse<CompanyResponse>.Failure(
                    ErrorCodes.Company.AlreadyExists,
                    "A megadott adószámmal már létezik cég.");
            }
            

            // Válasz DTO összeállítása
            var response = new CompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                TaxNumber = company.TaxNumber,
                CompanyRegistrationNumber = company.CompanyRegistrationNumber,
                RegisteredAddress = company.RegisteredAddress,
                MailingAddress = company.MailingAddress,
                BillingEmail = company.BillingEmail,
                RepresentativeEmployeeId = company.RepresentativeEmployeeId,
                Notes = company.Notes,
                IsActive = company.IsActive
            };

            return AppResponse<CompanyResponse>.Success(
                "A cég sikeresen létrehozva.",
                response);
        }

        public async Task<AppResponse<bool>> DeactivateAsync(int id)
        {
            var company = await _context.Companies
         .FirstOrDefaultAsync(x => x.Id == id);

            if (company is null)
            {
                return AppResponse<bool>.Failure(
                    ErrorCodes.Company.NotFound,
                    "A megadott cég nem található.");
            }

            if (!company.IsActive)
            {
                return AppResponse<bool>.Success(
                    "A cég már inaktív.",
                    true);
            }

            company.IsActive = false;

            await _context.SaveChangesAsync();

            return AppResponse<bool>.Success(
                "A cég sikeresen inaktiválva.",
                true);
        }

        public async Task<AppResponse<List<CompanyListItemResponse>>> GetAllAsync(bool includeInactive = false)
        {
            var query = _context.Companies.AsNoTracking();

            if(!includeInactive)
            {
                query = query.Where(c => c.IsActive);
            }
            var companies = await query
                .OrderBy(c => c.Name)
                .Select(x => new CompanyListItemResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    TaxNumber = x.TaxNumber,
                    CompanyRegistrationNumber = x.CompanyRegistrationNumber,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return AppResponse<List<CompanyListItemResponse>>.Success("A cégek lekérdezése sikeres",companies);
        }

        public async Task<AppResponse<CompanyResponse>> GetByIdAsync(int id)
        {
            var company = await _context.Companies
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new CompanyResponse
                {
                    Id = x.Id,
                    Name=x.Name,
                    TaxNumber = x.TaxNumber,
                    CompanyRegistrationNumber = x.CompanyRegistrationNumber,
                    RegisteredAddress = x.RegisteredAddress,
                    MailingAddress = x.MailingAddress,
                    BillingEmail = x.BillingEmail,
                    RepresentativeEmployeeId = x.RepresentativeEmployeeId,
                    IsActive = x.IsActive,
                    Notes = x.Notes
                }).FirstOrDefaultAsync();

            if(company is null)
            {
                return AppResponse<CompanyResponse>.Failure(ErrorCodes.Company.NotFound,"A keresett cég nem található");
            }

            return AppResponse<CompanyResponse>.Success("A cég lekérdezése sikeres", company);
        }

        public async Task<AppResponse<CompanyResponse>> UpdateAsync(int id, UpdateCompanyRequest request)
        {
            // Cég keresése
            var company = await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == id);

            if (company is null)
            {
                return AppResponse<CompanyResponse>.Failure(
                    ErrorCodes.Company.NotFound,
                    "A módosítandó cég nem található.");
            }

            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.TaxNumber) ||
                string.IsNullOrWhiteSpace(request.RegisteredAddress))
            {
                return AppResponse<CompanyResponse>.Failure(
                    ErrorCodes.Company.ValidationError,
                    "A cégnév, az adószám és a székhely megadása kötelező.");
            }

            // Adószám normalizálása
            var taxNumber = request.TaxNumber.Trim();

            // Adószám egyediségének ellenőrzése
            var taxNumberExists = await _context.Companies
                .AnyAsync(x =>
                    x.TaxNumber == taxNumber &&
                    x.Id != id);

            if (taxNumberExists)
            {
                return AppResponse<CompanyResponse>.Failure(
                    ErrorCodes.Company.AlreadyExists,
                    "A megadott adószámmal már létezik másik cég.");
            }

            if (request.RepresentativeEmployeeId.HasValue)
            {
                var employeeExists = await _context.Employees
                    .AnyAsync(x =>
                        x.Id == request.RepresentativeEmployeeId.Value);

                if (!employeeExists)
                {
                    return AppResponse<CompanyResponse>.Failure(
                        ErrorCodes.Company.ValidationError,
                        "A megadott képviselő nem található.");
                }
            }

            // Cég adatainak frissítése
            company.Name = request.Name.Trim();
            company.TaxNumber = taxNumber;
            company.CompanyRegistrationNumber =
                request.CompanyRegistrationNumber;
            company.RegisteredAddress = request.RegisteredAddress.Trim();
            company.MailingAddress = request.MailingAddress;
            company.BillingEmail = request.BillingEmail;
            company.RepresentativeEmployeeId =
                request.RepresentativeEmployeeId;
            company.Notes = request.Notes;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx &&
                      (sqlEx.Number == 2601 || sqlEx.Number == 2627) &&
                      sqlEx.Message.Contains(
                          "IX_Companies_TaxNumber",
                          StringComparison.OrdinalIgnoreCase))
            {
                return AppResponse<CompanyResponse>.Failure(
                    ErrorCodes.Company.AlreadyExists,
                    "A megadott adószámmal már létezik másik cég.");
            }

            // Response DTO összeállítása
            var response = new CompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                TaxNumber = company.TaxNumber,
                CompanyRegistrationNumber =
                    company.CompanyRegistrationNumber,
                RegisteredAddress = company.RegisteredAddress,
                MailingAddress = company.MailingAddress,
                BillingEmail = company.BillingEmail,
                RepresentativeEmployeeId =
                    company.RepresentativeEmployeeId,
                IsActive = company.IsActive,
                Notes = company.Notes
            };

            return AppResponse<CompanyResponse>.Success(
                "A cég adatai sikeresen módosítva.",
                response);
        }
    }
}
