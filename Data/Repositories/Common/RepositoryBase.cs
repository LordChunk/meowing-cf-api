﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Common
{
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, ApplicationContext>
           where TEntity : class, IEntity
    {
        internal RepositoryBase(ApplicationContext context) : base(context)
        {
        }
    }

    public abstract class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly DbSet<TEntity> DbSet;

        internal RepositoryBase(TContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> GetQueryBase()
        {
            return DbSet;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);

            var changed = await Context.SaveChangesAsync();
            return changed > 0 ? entity : null;
        }

        public async Task<TEntity> Get(params Expression<Func<TEntity, bool>>[] predicates)
        {
            return await predicates
                .Aggregate(
                    GetQueryBase(),
                    (query, predicate) => query.Where(predicate))
                .FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAll(params Expression<Func<TEntity, bool>>[] predicates)
        {
            return await predicates
                .Aggregate(
                    GetQueryBase(),
                    (query, predicate) => query.Where(predicate))
                .ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            var changed = await Context.SaveChangesAsync();
            return changed > 0 ? entity : null;
        }
        public virtual async Task<List<TEntity>> Remove(params Expression<Func<TEntity, bool>>[] predicates)
        {
            var entities = await GetAll(predicates);

            await using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                Context.RemoveRange(entities);
                int deleted = await Context.SaveChangesAsync();

                await transaction.CommitAsync();

                if (deleted == entities.Count)
                {
                    return entities;
                }

                await transaction.RollbackAsync();
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public async Task<TEntity> Remove(TEntity entity)
        {
            Context.Remove(entity);

            var changed = await Context.SaveChangesAsync();
            return changed > 0 ? entity : null;
        }
    }
}