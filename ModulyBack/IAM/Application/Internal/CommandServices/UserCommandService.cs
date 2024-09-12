using ModulyBack.IAM.Application.Internal.OutboundServices;
using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Domain.Model.Commands;
using ModulyBack.IAM.Domain.Repositories;
using ModulyBack.IAM.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace ModulyBack.IAM.Application.Internal.CommandServices
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IHashingService _hashingService;

        public UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService, IHashingService hashingService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _hashingService = hashingService;
        }

        public async Task<(User user, string token)> Handle(SignInCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.username) || string.IsNullOrEmpty(command.password))
            {
                throw new ArgumentException("Invalid username or password.");
            }

            var user = await _userRepository.FindByUsernameAsync(command.username);
            if (user == null || !_hashingService.VerifyPassword(command.password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials.");
            }

            var token = _tokenService.GenerateToken(user);
            return (user, token);
        }

        public async Task Handle(SignUpCommand command)
        {
            command.Validate(); // Validar los datos del comando SignUpCommand
            if (await _userRepository.ExistsByUsernameAsync(command.Username))
                throw new Exception($"Username {command.Username} is already taken");

            var hashedPassword = _hashingService.HashPassword(command.Password);

            // Utiliza FullName para la creación del usuario
            var newUser = new User(
                command.Username,
                command.FullName, 
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Email,
                hashedPassword
            );

            try
            {
                await _userRepository.AddAsync(newUser);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"An error occurred while creating user: {e.Message}");
            }
        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await _userRepository.FindByUsernameAsync(user.Username);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            // Actualiza los campos en el usuario existente
            existingUser.UpdateUsername(user.Username);
            existingUser.UpdateFullName(user.FullName);
            existingUser.UpdateAge(user.Age);
            existingUser.UpdateDni(user.Dni);
            existingUser.UpdatePhoneNumber(user.PhoneNumber);
            existingUser.UpdateEmail(user.Email);

            await _userRepository.UpdateAsync(existingUser);
        }
    }
}
