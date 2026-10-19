using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.CompanyEmployee.Responses
{
    public sealed class CompanyEmployeeResponse
    {
        public int Id { get; init; }

        public int CompanyId { get; init; }
        public string CompanyName { get; init; } = string.Empty;

        public int EmployeeId { get; init; }
        public string EmployeeName { get; init; } = string.Empty;

        public int EmploymentTypeId { get; init; }
        public string EmploymentTypeName { get; init; } = string.Empty;

        public string Position { get; init; } = string.Empty;

        public DateOnly StartDate { get; init; }
        public DateOnly? EndDate { get; init; }

        public bool IsActive { get; init; }

        public string? Notes { get; init; }
    }
}
