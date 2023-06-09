﻿using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new() //IRepository<TEntity> - filha de Entity
    {

        protected readonly MeuDbContext Db; //protected pq tanto o repository quanto quem herdar do repository poderá ter acesso a ele
        protected readonly DbSet<TEntity> DbSet;


        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
           return await DbSet.AsNoTracking().Where(predicate).ToListAsync();  // serve para mais performance
            //await - espera o resulado acontecer
        }       

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();  
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            //Db.Set<TEntity>().Add(entity);
            DbSet.Add(entity);
            await SaveChanges();           
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {          
         //   DbSet.Remove(await DbSet.FindAsync(id));
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

       public void Dispose()
        {
            Db?.Dispose();
        }

    }
}
