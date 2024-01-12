﻿using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.Persistant;
using ProductService.Application.Dto;

namespace ProductService.Persistence.DatabaseContext.Repository
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;
        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAsync(Pagination pagination)
        {
            int skip = (pagination.Page - 1) * pagination.Limit; 
            List<T> list = await _context.Set<T>()
                .Skip(skip)
                .Take(pagination.Limit)
                .ToListAsync();
            
            return list;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
