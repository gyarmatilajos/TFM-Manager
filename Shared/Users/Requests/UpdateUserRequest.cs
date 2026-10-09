using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Users.Requests
{
    public sealed class UpdateUserRequest
    {
        public required string Email { get; init; }

        public int UserRoleId { get; init; }
    }
}
