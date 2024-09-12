using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Interfaces.Resources;

namespace ModulyBack.IAM.Interfaces.Transform;

public class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id,user.Username,user.FullName,user.Age,user.Dni,user.PhoneNumber,user.Email);
    }
}