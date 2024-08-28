using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Domain.Model.Commands;

namespace ModulyBack.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<(User user, string token)> Handle(SignInCommand command);
    Task Handle(SignUpCommand command);
    //Task<(User user, string token)> AuthenticateAsync(string username, string password);
    Task UpdateUser(User command);

}