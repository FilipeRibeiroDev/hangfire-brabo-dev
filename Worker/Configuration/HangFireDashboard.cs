using Hangfire.Dashboard.BasicAuthorization;
using System.Diagnostics.CodeAnalysis;

namespace Worker.Configuration
{
    public class HangFireDashboard
    {
        public static BasicAuthAuthorizationFilter[] AuthAuthorizationFilters()
        {
            return [
                new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    SslRedirect = false,
                    RequireSsl = false,
                    LoginCaseSensitive = true,
                    Users = new[]
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = "admin",
                            PasswordClear = "admin"
                        }
                    }
                })
            ];
        }
    }
}
