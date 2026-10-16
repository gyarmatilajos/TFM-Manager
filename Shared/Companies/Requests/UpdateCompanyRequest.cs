using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFM_Manager.Shared.Companies.Requests
{
    public sealed class UpdateCompanyRequest
    {
        [Required]
        [MaxLength(200)]
        public required string Name { get; init; }

        [Required]
        [MaxLength(20)]
        public required string TaxNumber { get; init; }

        [MaxLength(20)]
        public string? CompanyRegistrationNumber { get; init; }

        [Required]
        [MaxLength(500)]
        public required string RegisteredAddress { get; init; }

        [MaxLength(500)]
        public string? MailingAddress { get; init; }

        [MaxLength(256)]
        [EmailAddress]
        public string? BillingEmail { get; init; }

        public int? RepresentativeEmployeeId { get; init; }

        [MaxLength(2000)]
        public string? Notes { get; init; }
    }
}
