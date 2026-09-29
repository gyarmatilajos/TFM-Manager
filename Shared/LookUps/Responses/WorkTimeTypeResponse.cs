using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.LookUps.Responses
{
    public sealed class WorkTimeTypeResponse
    {
        public int Id { get; init; }

        public required string Name { get; init; }

        public decimal DailyHours { get; init; }

        public bool IsActive { get; init; }
    }
}
