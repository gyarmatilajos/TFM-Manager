using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Employees.Requests
{
    public sealed class UpdateEmployeeRequest
    {
        public required string FullName { get; init; }

        public int EmployeeTypeId { get; init; }

        public string? Phone { get; init; }

        public string? Email { get; init; }

        public string? Address { get; init; }

        public required string TaxIdentificationNumber { get; init; }

        public string? SocialSecurityNumber { get; init; }

        public DateOnly? DateOfBirth { get; init; }

        public string? PlaceOfBirth { get; init; }

        public string? MotherName { get; init; }

        public string? Notes { get; init; }
    }
}
