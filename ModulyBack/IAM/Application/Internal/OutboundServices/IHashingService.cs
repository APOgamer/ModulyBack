
namespace ModulyBack.IAM.Application.Internal.OutboundServices;

/**
*  <summary>
*       The hashing service interface
* </summary>
* <remarks>
*       This interface is used to hash and verify passwords
* </remarks>
*/
public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}