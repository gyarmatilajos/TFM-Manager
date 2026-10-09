using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Business.Interfaces;
using TFM_Manager.Data.DbContext;
using TFM_Manager.Shared.Common;
using TFM_Manager.Shared.Companies.Requests;
using TFM_Manager.Shared.Companies.Responses;

namespace TFM_Manager.Business.Services
{
    public sealed class CompanyService : IcompanyService
    {
        private readonly TfmManagerDbContext _context;

        public CompanyService(TfmManagerDbContext context)
        {
            _context = context;
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
    }
}
