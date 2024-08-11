using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.Interface;
using MusicStore.Web.Data;
using MusicStore.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MusicStoreUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MusicStoreUser>();
        }

        public void Delete(MusicStoreUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public MusicStoreUser Get(string id)
        {
            return entities.SingleOrDefault(z => z.Id == id);
        }

        public List<MusicStoreUser> GetAll()
        {
            return entities.ToList();
        }

        public void Insert(MusicStoreUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MusicStoreUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    };
}
