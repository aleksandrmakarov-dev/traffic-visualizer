using System.Security.Cryptography;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;

namespace TukkoTrafficVisualizer.Infrastructure.Services;

public class TokensService : ITokensService
{
    public string GetToken(int count)
    {
        return RandomNumberGenerator.GetHexString(count, true);
    }
}