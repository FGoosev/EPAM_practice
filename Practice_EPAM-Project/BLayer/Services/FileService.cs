using System;
using System.Collections.Generic;
using System.Web;
using BLayer.DTO;
using BLayer.Infrastructure;
using BLayer.Interfaces;
using DLayer.Interfaces;
using Entities;

namespace BLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork Database;

        public FileService(IUnitOfWork uow)
        {
            Database = uow;
        }


        public File GetFile(int id)
        {
            var file = Database.Files.Get(id);
            if (file == null)
                throw new HttpException(404, $"Файл с id: {id} не найден");

            return new File { FileName = file.FileName, Data = file.Data };
        }

        public IEnumerable<File> GetFiles() => Database.Files.GetAll();
        

        public void Create(FileDTO fileDTO)
        {          
                var file = new File {FileName = fileDTO.FileName, Data = fileDTO.Data };
                Database.Files.Create(file);
                Database.SaveAsync();
        }

        public void Update(FileDTO fileDTO)
        {
            var file = Database.Files.FindByName(fileDTO.FileName);
            if(file != null)
            {
                Database.Files.Update(file);
                Database.SaveAsync();
            }
            
        }

        public void Remove(int id)
        {
            var file = Database.Files.Get(id);
            if(file != null)
            {
                Database.Files.Delete(id);
                Database.SaveAsync();
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}