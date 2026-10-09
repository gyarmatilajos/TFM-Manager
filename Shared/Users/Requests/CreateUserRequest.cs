using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Users.Requests
{
    public sealed class CreateUserRequest
    {
        public int EmployeeId { get; init; }

        public required string Email { get; init; }

        public int UserRoleId { get; init; }

        public required string Password { get; init; }
    }
}
