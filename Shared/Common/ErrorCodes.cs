using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Shared.Common
{
    public static class ErrorCodes
{
    public static class General
    {
        public const string UnexpectedError = "General.UnexpectedError";
        public const string NotFound = "General.NotFound";
        public const string InvalidInput = "General.InvalidInput";
    }
    public static class User
    {
        public const string NotFound = "User.NotFound";
        public const string InvalidCredentials = "User.InvalidCredentials";
        public const string AlreadyExists = "User.AlreadyExists";
    }
    public static class Partner
    {
        public const string NotFound = "Partner.NotFound";
        public const string AlreadyExists = "Partner.AlreadyExists";
    }
}
}
