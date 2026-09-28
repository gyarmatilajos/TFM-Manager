using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFM_Manager.Shared.Common
{
    public sealed class AppError
    {
    
    public required string Code { get; init; } = string.Empty;
    
    public required string Message { get; init; } = string.Empty;
    }
}
