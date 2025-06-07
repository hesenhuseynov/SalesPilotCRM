using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesPilotCRM.Application.Common.Constants;
using SalesPilotCRM.Application.Common.Constants.Messages;
using SalesPilotCRM.Application.Common.Enums;
using SalesPilotCRM.Application.Common.Models;
using SalesPilotCRM.Application.Features.Auth.Login;
using SalesPilotCRM.Application.Features.Auth.RefreshToken;
using SalesPilotCRM.Application.Features.Auth.Register;
using SalesPilotCRM.Application.Interfaces;
using SalesPilotCRM.Application.Interfaces.Auth;
using SalesPilotCRM.Domain.Entities;

namespace SalesPilotCRM.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAppWriteDbContext _writeDbContext;
        private readonly IAppReadDbContext _readDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IAppWriteDbContext writeDbContext,
            IAppReadDbContext readDbContext,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
             ILogger<AuthService> logger
            )
        {
            _writeDbContext = writeDbContext;
            _readDbContext = readDbContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {

            _logger.LogInformation("📥 Registering new user: {Email}", request.Email);

            var existingUser = await _readDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser is not null)
            {
                _logger.LogWarning("📛 Registration failed: Email already exists. Email: {Email}", request.Email);
                return Result<RegisterResponse>.Fail(AuthMessages.EmailAlreadyExists, ResultStatus.Conflict);
            }

            var role = await _readDbContext.Roles
                .FirstOrDefaultAsync(r => r.Name == RoleConstants.SalesAgent && r.IsActive, cancellationToken);



            if (role is null)
            {
                _logger.LogError("❌ Registration failed: Role 'SalesAgent' not found or inactive.");
                return Result<RegisterResponse>.Fail("User role not found", ResultStatus.InternalError);
            }

            var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(request.Password);

            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = role.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await using var transaction = await _writeDbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                _logger.LogInformation("📥 Registering new user: {Email}", request.Email);

                await _writeDbContext.Users.AddAsync(newUser, cancellationToken);
                await _writeDbContext.SaveChangesAsync(cancellationToken);
                var userWithRole = await _writeDbContext.Users
                       .Include(u => u.Role)
                       .FirstOrDefaultAsync(u => u.Email == newUser.Email, cancellationToken);

                if (userWithRole is null)
                {

                    _logger.LogError("❗ User not found after saving. Rolling back. Email: {Email}", request.Email);
                    await transaction.RollbackAsync(cancellationToken);
                    return Result<RegisterResponse>.Fail("Unexpected error: User not found after registration", ResultStatus.InternalError);

                }


                var accessToken = _tokenService.GenerateAccessToken(userWithRole);

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("✅ User registered successfully. UserId: {UserId}", newUser.Id);

                var response = new RegisterResponse
                {
                    AccessToken = accessToken,
                    FullName = newUser.FullName,
                    Email = newUser.Email
                };

                return Result<RegisterResponse>.Ok(response, AuthMessages.RegisterSuccess);


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "🔥 Registration failed due to unexpected error. Email: {Email}", request.Email);
                return Result<RegisterResponse>.Fail("An unexpected error occurred during registration", ResultStatus.InternalError);
            }
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _readDbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null || !user.IsActive || user.IsDeleted || user.IsLocked)
                return Result<LoginResponse>.Fail(AuthMessages.InvalidCredentials, ResultStatus.Unauthorized);

            var isPasswordValid = _passwordHasher.VerifyPassword(
                request.Password, user.PasswordHash, user.PasswordSalt);

            if (!isPasswordValid)
                return Result<LoginResponse>.Fail(AuthMessages.InvalidCredentials, ResultStatus.Unauthorized);

            if (user.Role is null)
                return Result<LoginResponse>.Fail("User role mapping missing", ResultStatus.InternalError);

            var accessToken = _tokenService.GenerateAccessToken(user);

            var response = new LoginResponse
            {
                AccessToken = accessToken,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };

            return Result<LoginResponse>.Ok(response, AuthMessages.LoginSuccess);
        }

        public async Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _readDbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user is null)
                return Result<RefreshTokenResponse>.Fail(AuthMessages.UserNotFound, ResultStatus.NotFound);

            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpires < DateTime.UtcNow)
                return Result<RefreshTokenResponse>.Fail("Refresh token is invalid or expired", ResultStatus.Unauthorized);

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(3);

            await _writeDbContext.SaveChangesAsync(cancellationToken);

            var response = new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return Result<RefreshTokenResponse>.Ok(response, "Access token refreshed successfully");
        }
    }
}
