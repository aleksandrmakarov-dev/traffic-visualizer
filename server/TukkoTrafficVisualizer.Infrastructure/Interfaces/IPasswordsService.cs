namespace TukkoTrafficVisualizer.Infrastructure.Interfaces
{
    public interface IPasswordsService
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
