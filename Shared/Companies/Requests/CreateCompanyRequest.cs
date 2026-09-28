using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Companies.Requests
{
    public sealed class CreateCompanyRequest
    {
        public required string Name { get; init; }

        public required string TaxNumber { get; init; }

        public string? CompanyRegistrationNumber { get; init; }

        public required string RegisteredAddress { get; init; }

        public string? MailingAddress { get; init; }

        public string? BillingEmail { get; init; }

        public int? RepresentativeEmployeeId { get; init; }

        public string? Notes { get; init; }
    }
}
