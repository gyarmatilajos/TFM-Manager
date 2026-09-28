using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFM_Manager.Shared.Common
{
    public sealed class AppError
    {
    [Required]
    public string Code { get; init; } = string.Empty;
    [Required]
    public string Message { get; set; } = string.Empty;
    }
}
