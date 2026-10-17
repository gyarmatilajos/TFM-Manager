using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFM_Manager.Shared.Employees.Requests
{
    public sealed class UpdateEmployeeRequest
    {
        [Required]
        [MaxLength(200)]
        public required string FullName { get; init; }
        [Range(1, int.MaxValue)]
        public int EmployeeTypeId { get; init; }
        [MaxLength(30)]
        public string? Phone { get; init; }

        [EmailAddress]
        [MaxLength(256)]
        public string? Email { get; init; }

        [MaxLength(500)]                
        public string? Address { get; init; }
        [MaxLength(20)]
        public required string TaxIdentificationNumber { get; init; }

        [MaxLength(20)]
        public string? SocialSecurityNumber { get; init; }

        public DateOnly? DateOfBirth { get; init; }
        [MaxLength(100)]
        public string? PlaceOfBirth { get; init; }

        [MaxLength(200)]
        public string? MotherName { get; init; }
        [MaxLength(2000)]
        public string? Notes { get; init; }
    }
}
