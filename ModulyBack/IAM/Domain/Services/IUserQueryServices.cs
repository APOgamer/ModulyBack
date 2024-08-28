using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Domain.Model.Queries;

namespace ModulyBack.IAM.Domain.Services;

public interface IUserQueryServices
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByNameQuery query);
    Task<User?> Handle(GetUserInformationByUsernameQuery query);


}