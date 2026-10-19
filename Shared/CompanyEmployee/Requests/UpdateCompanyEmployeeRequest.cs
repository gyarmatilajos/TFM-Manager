using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFM_Manager.Shared.CompanyEmployee.Requests
{
    public sealed class UpdateCompanyEmployeeRequest
    {
        [Range(1, int.MaxValue)]
        public int EmploymentTypeId { get; init; }

        [Required]
        [MaxLength(150)]
        public required string Position { get; init; }

        [Required]
        public DateOnly? StartDate { get; init; }

        public DateOnly? EndDate { get; init; }

        [MaxLength(2000)]
        public string? Notes { get; init; }
    }
}
