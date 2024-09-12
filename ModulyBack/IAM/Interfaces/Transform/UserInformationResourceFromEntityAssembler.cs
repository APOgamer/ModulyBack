using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Interfaces.Resources;

namespace ModulyBack.IAM.Interfaces.Transform;

public class UserInformationResourceFromEntityAssembler
{
    public static UserInformationResource ToResourceFromEntity(User user)
    {
        return new UserInformationResource(
            user.Id, 
            user.Username, 
            user.PasswordHash, 
            user.FullName, // Usamos FullName en lugar de FirstName y LastName
            user.Age, // Añadimos la edad
            user.Dni, // Añadimos el DNI
            user.PhoneNumber, // Añadimos el número de teléfono
            user.Email
        );
    }   
}