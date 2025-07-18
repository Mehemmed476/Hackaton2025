﻿using HotelManagementSystem.Core.Entities.Common;
using HotelManagementSystem.DL.Contexts;
using HotelManagementSystem.DL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.DL.Repositories.Implementations;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<ICollection<T>> GetAllAsync(bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition, bool isTracking = true, params string[] includes)
    {
        var query = Table.Where(condition).AsQueryable();
        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> condition, int page, int size, bool isTracking = true, params string[] includes)
    {
        var query = Table.Where(condition).Skip(page * size).Take(size);
        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public async Task<T> GetByIdAsync(Guid id, bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();

        if (!isTracking)
        {
            query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        T? entity = await query.FirstOrDefaultAsync(t => t.Id == id);
        return entity;
    }

    public async Task<T> GetOneByCondition(Expression<Func<T, bool>> condition, bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
        {
            query = query.AsNoTracking();
        }

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        T? entity = await query.FirstOrDefaultAsync(condition);
        return entity;
    }

    public async Task<bool> IsExist(Guid id)
    {
        await Table.AnyAsync(x => x.Id == id);
        return true;
    }
}
