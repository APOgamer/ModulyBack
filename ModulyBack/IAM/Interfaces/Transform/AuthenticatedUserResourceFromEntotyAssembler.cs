using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Interfaces.Resources;

namespace ModulyBack.IAM.Interfaces.Transform;

public class AuthenticatedUserResourceFromEntotyAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}