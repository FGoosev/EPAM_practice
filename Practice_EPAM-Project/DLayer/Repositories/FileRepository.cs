using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLayer.EF;
using DLayer.Interfaces;
using Entities;

namespace DLayer.Repositories
{
    public class FileRepository : IRepository<File>
    {
        private ApplicationContext db;
        public FileRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<File> GetAll()
        {
            return db.Files;
        }

        public File Get(int id)
        {
            return db.Files.Find(id);
        }

        public void Create(File file)
        {
            db.Files.Add(file);
            db.SaveChanges();
        }

        public void Update(File file)
        {
            db.Entry(file).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<File> Find(Func<File, Boolean> predicate)
        {
            return db.Files.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            File file = db.Files.Find(id);
            if (file != null)
                db.Files.Remove(file);
            db.SaveChanges();
        }

        public File FindByName(string item)
        {
            return db.Files.Find(item);
        }
    }
}
