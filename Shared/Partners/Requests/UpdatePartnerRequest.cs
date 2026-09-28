using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Partners.Requests
{
    public sealed class UpdatePartnerRequest
    {
        public required string Name { get; init; }

        public int PartnerTypeId { get; init; }

        public string? TaxNumber { get; init; }

        public string? Address { get; init; }

        public string? MailingAddress { get; init; }

        public string? BillingName { get; init; }

        public string? BillingAddress { get; init; }

        public string? BillingEmail { get; init; }

        public string? Phone { get; init; }

        public string? Email { get; init; }

        public string? Notes { get; init; }
    }
}
