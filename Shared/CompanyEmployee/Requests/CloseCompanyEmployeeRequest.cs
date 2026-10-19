using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFM_Manager.Shared.CompanyEmployee.Requests
{
    public sealed class CloseCompanyEmployeeRequest
    {
        [Required]
        public DateOnly? EndDate { get; init; }
    }
}
