using System;
using Entities;

namespace DLayer.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
        void Update(ClientProfile item);
    }
}
