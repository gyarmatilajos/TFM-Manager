using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Partners.Requests
{
    public sealed class UpdatePartnerLocationRequest
    {
        public required string Name { get; init; }

        public int LocationTypeId { get; init; }

        public string? Address { get; init; }

        public string? Notes { get; init; }
    }
}
