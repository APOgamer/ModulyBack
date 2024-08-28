using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Domain.Model.Queries;
using ModulyBack.IAM.Domain.Repositories;
using ModulyBack.IAM.Domain.Services;

namespace ModulyBack.IAM.Application.Internal.QueryServices;

public class UserQueryService : IUserQueryServices
{
    private readonly IUserRepository _userRepository;
    

    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.Id);
    }

    public async Task<User?> Handle(GetUserByNameQuery query)
    
    {
        return await _userRepository.FindByUsernameAsync(query.Name);

    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> Handle(GetUserInformationByUsernameQuery query)
    {
        return await _userRepository.FindByUsernameAsync(query.username);
    }



}