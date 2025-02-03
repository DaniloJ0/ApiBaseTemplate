namespace Infrastructure.Interfacess;

public interface IJwtTokenService
{
    string GenerateToken(string userName);
}
