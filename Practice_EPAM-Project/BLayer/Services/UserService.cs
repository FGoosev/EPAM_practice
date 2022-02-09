using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DLayer.Interfaces;
using Entities;
using BLayer.Interfaces;
using BLayer.DTO;
using BLayer.Infrastructure;
using System.Linq;

namespace BLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;

        public UserService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task<OperationDetails> CreateAsync(UserDTO userDto)
        {
            var user = await _database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await _database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Name = userDto.Name, Surname = userDto.Surname, Email = userDto.Email, Age = userDto.Age };
                _database.ClientManager.Create(clientProfile);
                await _database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }



        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await _database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task EditAsync(UserDTO userDto)
        {
            
            //ApplicationUser user = 
            var user = await _database.UserManager.FindByEmailAsync(userDto.Email);
            if (user != null)
            {

                user.ClientProfile.Name = userDto.Name;
                user.ClientProfile.Surname = userDto.Surname;
                user.ClientProfile.Age = userDto.Age;
                _database.ClientManager.Update(user.ClientProfile);
                await _database.SaveAsync();
            }
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}