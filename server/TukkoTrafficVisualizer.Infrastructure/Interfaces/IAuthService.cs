using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IAuthService
    {
        Task<EmailVerificationResponse> SignUpAsync(SignUpRequest request);
        Task<SignInResponse> SignInAsync(SignInRequest request);
        Task VerifyEmailAsync(VerifyEmailRequest request);
        Task<SessionResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task<EmailVerificationResponse?> NewEmailVerificationAsync(NewEmailVerificationRequest request);
        Task SignOutAsync(SignOutRequest request);
    }
}
