using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MadPay724.Repo.repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MadPay724.Repo.Infrastructure
{
   public interface IUnitOfWork<TContext>:IDisposable where TContext:DbContext
   {
        IUserRepository UserRepository { get; }
    
       void Save();
       Task<int> SaveAsync();
   }
}
