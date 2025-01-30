namespace OnlineExamSystem.API.Infrastructure
{
    public static class HttpContextExtensions
    {
        public static bool IsUserLoggedIn(this HttpContext context)
        {
            if (context?.Session is null)
            {
                return false;
            }

            var token = GetAuthToken(context);

            return !string.IsNullOrWhiteSpace(token);
        }

        public static string GetAuthToken(this HttpContext context)
        {
            if (context?.Session is null)
            {
                return string.Empty;
            }

            var token = context.Session.GetString("AuthToken");

            return string.IsNullOrWhiteSpace(token) ? string.Empty : token;
        }

        public static string GetUserRole(this HttpContext context)
        {
            if (context?.Session is null)
            {
                return string.Empty;
            }

            var role = context.Session.GetString("UserRole");

            return string.IsNullOrWhiteSpace(role) ? string.Empty : role;
        }
    }
}