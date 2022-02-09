using System;
using System.Collections.Generic;
using BLayer.DTO;
using Entities;

namespace BLayer.Interfaces
{
    public interface IFileService : IDisposable
    {
        void Create(FileDTO fileDTO);
        File GetFile(int id);
        IEnumerable<File> GetFiles();
        void Update(FileDTO fileDTO);
        void Remove(int id);

    }
}
