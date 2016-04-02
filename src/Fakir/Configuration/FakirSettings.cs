namespace Fakir.Configuration
{
    public static class FakirSettings
    {
        public static class General
        {
            public const string WebSiteRootAddress = "App.General.WebSiteRootAddress";
        }

        public static class UserManagement
        {
            public const string AllowSelfRegistration = "App.UserManagement.AllowSelfRegistration";
            public const string IsNewRegisteredUserActiveByDefault = "App.UserManagement.IsNewRegisteredUserActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.UserManagement.UseCaptchaOnRegistration";
        }
    }
}
