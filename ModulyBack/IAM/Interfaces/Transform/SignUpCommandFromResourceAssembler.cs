using ModulyBack.IAM.Domain.Model.Commands;
using ModulyBack.IAM.Interfaces.Resources;

namespace ModulyBack.IAM.Interfaces.Transform
{
    public class SignUpCommandFromResourceAssembler
    {
        public static SignUpCommand ToCommandFromResource(SignUpResource resource)
        {
            // Supongo que `SignUpResource` tiene propiedades `FullName`, `Age`, `Dni`, `PhoneNumber`, y `Email`
            return new SignUpCommand(
                resource.Username,
                resource.Password,
                resource.FullName, // Campo actualizado
                resource.Age,      // Campo actualizado
                resource.Dni,      // Campo actualizado
                resource.PhoneNumber, // Campo actualizado
                resource.Email,    // Campo actualizado
                resource.Role
            );
        }
    }
}