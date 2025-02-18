﻿using API.Context;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Context : MyContext
        where Entity : class
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }
        public Entity Get(Key key)
        {
            return entities.Find(key);
        }
        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }
        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
        public int Delete(Key key)
        {
            var entity = entities.Find(key);
            entities.Remove(entity);
            var result = myContext.SaveChanges();
            return result;
        }
    }

}
