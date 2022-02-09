using BLayer.Interfaces;
using DLayer.Repositories;

namespace BLayer.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IFileService CreateFileService(string connection)
        {
            return new FileService(new UnitOfWork(connection));
        }

        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
    }
}