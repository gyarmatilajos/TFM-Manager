using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Users.Responses
{
    public sealed class UserListItemResponse
    {
        public int Id { get; init; }

        public required string EmployeeName { get; init; }

        public required string Email { get; init; }

        public required string UserRoleName { get; init; }

        public bool IsActive { get; init; }
    }
}
