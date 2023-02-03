using System;
namespace Pronia.Contracts.Email
{
    public static class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Activation of account";
            public const string ORDER_ACTIVATION_MESSAGE = $"Your account is active";
        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Your activation URL : {EmailMessageKeywords.ACTIVATION_URL}";
        }
    }
}