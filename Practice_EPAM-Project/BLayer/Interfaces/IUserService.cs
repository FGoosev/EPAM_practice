using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BLayer.DTO;
using BLayer.Infrastructure;

namespace BLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto);
        Task EditAsync(UserDTO userDto);
    }
}
