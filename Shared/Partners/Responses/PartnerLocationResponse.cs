using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Partners.Responses
{
    public sealed class PartnerLocationResponse
    {
        public int Id { get; init; }

        public int PartnerId { get; init; }

        public required string Name { get; init; }

        public int LocationTypeId { get; init; }

        public string? Address { get; init; }

        public bool IsActive { get; init; }

        public string? Notes { get; init; }
        
    }
}
