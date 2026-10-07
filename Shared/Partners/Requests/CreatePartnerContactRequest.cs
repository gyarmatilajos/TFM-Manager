using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Partners.Requests
{
    public sealed class CreatePartnerContactRequest
    {
        public required string Name { get; init; }

        public int ContactTypeId { get; init; }

        public string? Phone { get; init; }

        public string? Email { get; init; }

        public string? Position { get; init; }

        public bool IsPrimary { get; init; }

        public string? Notes { get; init; }
    }
}
