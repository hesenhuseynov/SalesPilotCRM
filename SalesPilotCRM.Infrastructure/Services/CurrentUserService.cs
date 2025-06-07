using Microsoft.AspNetCore.Http;
using SalesPilotCRM.Application.Interfaces;
using System.Security.Claims;


namespace SalesPilotCRM.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;


        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
