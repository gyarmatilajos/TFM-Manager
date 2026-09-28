using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Employees.Responses
{
    public sealed class EmployeeListItemResponse
    {
        public int Id { get; init; }

        public required string FullName { get; init; }

        public int EmployeeTypeId { get; init; }

        public string? Phone { get; init; }

        public string? Email { get; init; }

        public bool IsActive { get; init; }
    }
}
