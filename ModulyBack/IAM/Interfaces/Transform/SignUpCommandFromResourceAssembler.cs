using ModulyBack.IAM.Domain.Model.Commands;
using ModulyBack.IAM.Interfaces.Resources;

namespace ModulyBack.IAM.Interfaces.Transform
{
    public class SignUpCommandFromResourceAssembler
    {
        public static SignUpCommand ToCommandFromResource(SignUpResource resource)
        {
            return new SignUpCommand(
                resource.Username,
                resource.Password,
                resource.FullName,
                resource.Age,
                resource.Dni,
                resource.PhoneNumber,
                resource.Email
            );
            
        }
    }
}