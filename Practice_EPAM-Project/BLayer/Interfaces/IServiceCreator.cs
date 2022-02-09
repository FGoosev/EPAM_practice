

namespace BLayer.Interfaces
{
    interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
        IFileService CreateFileService(string connection);
    }
}
