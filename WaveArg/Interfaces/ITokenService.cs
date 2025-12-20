using Data.Entities;

namespace WaveArg.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuarios user);
    }
}
