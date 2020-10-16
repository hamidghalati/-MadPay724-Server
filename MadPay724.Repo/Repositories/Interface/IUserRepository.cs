using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MadPay724.Data.Models;
using MadPay724.Repo.Infrastructure;


namespace MadPay724.Repo.repositories.Interface
{
   public interface IUserRepository:IRepository<User>
    {
        Task<bool> UserExists(string username);
    }
}
