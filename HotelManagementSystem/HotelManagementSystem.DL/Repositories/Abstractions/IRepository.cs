using HotelManagementSystem.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.DL.Repositories.Abstractions;

public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }
}
