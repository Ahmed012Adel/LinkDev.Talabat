using LinkDev.Talabat.Core.Application.Abstraction;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Services
{
    public class LoggedUserInService : ILoggedUserInService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public string UserId { get; }

        public LoggedUserInService(/*IHttpContextAccessor? httpContextAccessor*/)
        {
            //_httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid)!;
        }
    }
}
