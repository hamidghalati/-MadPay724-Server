using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MadPay724.Data.Infrastructure
{
   public interface IUnitOfWork<TContext>:IDisposable where TContext:DbContext
   {
       void Save();
       Task<int> SaveAsync();
   }
}
