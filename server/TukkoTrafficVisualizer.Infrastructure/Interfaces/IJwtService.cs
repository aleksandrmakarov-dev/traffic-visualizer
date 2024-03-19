using JwtPayload = TukkoTrafficVisualizer.Infrastructure.Models.JwtPayload;

namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string GetToken(JwtPayload payload);
        JwtPayload? ValidateToken(string token);
    }
}
