using System;
using System.Collections.Generic;
using System.Text;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Models;
using MadPay724.Repo.Infrastructure;

using MadPay724.Repo.repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MadPay724.Data.repositories.Repo
{
  public class UserRepository:Repository<User>,IUserRepository
  {
      private readonly DbContext _db;

      public UserRepository(DbContext dbContext) : base(dbContext)
      {
          _db = (_db ?? (MadPayDbContext) _db);
      }

  }
}
