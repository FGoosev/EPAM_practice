using System;
using System.Threading.Tasks;
using DLayer.Identity;
using Entities;

namespace DLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        IRepository<File> Files { get; }
        Task SaveAsync();
    }
}
