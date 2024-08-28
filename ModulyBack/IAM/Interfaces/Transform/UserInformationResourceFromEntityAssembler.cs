using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Interfaces.Resources;

namespace ModulyBack.IAM.Interfaces.Transform;

public class UserInformationResourceFromEntityAssembler
{
    public static UserInformationResource ToResourceFromEntity(User user)
    {
        return new UserInformationResource(user.Id, user.Username,user.PasswordHash,user.Role,user.FirstName,user.LastName,user.Email);
    }   
}