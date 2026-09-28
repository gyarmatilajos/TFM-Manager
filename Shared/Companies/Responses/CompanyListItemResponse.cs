using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Companies.Responses
{
    public sealed class CompanyListItemResponse
    {
        public int Id { get; init; }

        public required string Name { get; init; }

        public required string TaxNumber { get; init; }

        public string? CompanyRegistrationNumber { get; init; }

        public bool IsActive { get; init; }
    }
}
