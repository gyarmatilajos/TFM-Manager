using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Partners.Responses
{
    public sealed class PartnerListItemResponse
    {
        public int Id { get; init; }

        public required string Name { get; init; }

        public int PartnerTypeId { get; init; }

        public string? TaxNumber { get; init; }

        public string? Phone { get; init; }

        public string? Email { get; init; }

        public bool IsActive { get; init; }
    }
}
