using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHadher;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUserRepository userRepository,
                            IPasswordHashService passwordHadher,
                            ILogger<UsersService> logger,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHadher = passwordHadher;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserViewModel> AddUser(UserModel userModel)
        {
            if (userModel == null) throw new ArgumentNullException(nameof(userModel));

            // Ensures we do not duplicate usernames in the database
            await EnsureUsernameIsNotInUse(userModel);

            var user = MapUser(userModel);
            await _userRepository.Add(user);

            _logger.LogInformation("User saved to database {@User}", user);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<IReadOnlyCollection<UserViewModel>> GetUsers()
        {
            var users = await _userRepository.FindAll();
            return _mapper.Map<IReadOnlyCollection<UserViewModel>>(users);
        }

        public async Task<UserViewModel> UpdateUser(int id, UserModel userModel)
        {
            if (userModel == null) throw new ArgumentNullException(nameof(userModel));

            var user = await _userRepository.FindById(id);
            if (user == null) throw new EntityNotFoundException();
            if (userModel.UserName != user.UserName)
            {
                _logger.LogInformation("Updating username. Checking if username is not taken");
                await EnsureUsernameIsNotInUse(userModel);
            }

            user = MapUser(userModel, user);
            await _userRepository.Update(user);

            _logger.LogInformation("User updated in database {@User}", user);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetUser(int id)
        {
            var user = await _userRepository.FindById(id);
            return _mapper.Map<UserViewModel>(user);
        }
        public async Task<UserViewModel> GetUserByUsername(string username)
        {
            var user = await _userRepository.FindByUserName(username);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> DeleteUser(int id)
        {
            var user = await _userRepository.FindById(id);
            if (user == null) throw new EntityNotFoundException();
            await _userRepository.Remove(user);
            return _mapper.Map<UserViewModel>(user);
        }

        private async Task EnsureUsernameIsNotInUse(UserModel userModel)
        {
            // Try to get user by username to determine if username has already been used before
            var user = await _userRepository.FindByUserName(userModel.UserName);
            if (user is not null)
            {
                _logger.LogInformation("Attempt to register a username that is already in use {Username}", userModel.UserName);
                throw new UsernameAlreadyTakenException($"Username '{userModel.UserName}' is already taken by another user. Try a different one");
            }
        }

        private User MapUser(UserModel userModel, User user = null)
        {
            // User will be null on creating new user, and will be already populate on updating.
            if (user == null) user = new User();

            user.FullName = userModel.FullName;
            user.Role = userModel.Role;
            user.UserName = userModel.UserName;

            if (string.IsNullOrEmpty(user.Password))
                user.Password = _passwordHadher.Hash(userModel.Password);

            return user;
        }
    }
}
