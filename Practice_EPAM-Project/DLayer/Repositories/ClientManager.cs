using System.Data.Entity;
using DLayer.EF;
using DLayer.Interfaces;
using Entities;

namespace DLayer.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Update(ClientProfile item)
        {
            Database.Entry(item).State = EntityState.Modified;
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
