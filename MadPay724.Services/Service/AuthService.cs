using MadPay724.Common.Helper;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Models;
using MadPay724.Repo.Infrastructure;
using MadPay724.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork<MadPayDbContext> _db;
        public AuthService(IUnitOfWork<MadPayDbContext> dbContext)
        {
            _db = dbContext;
        }

        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            Utilities.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _db.UserRepository.InsertAsync(user);
            await _db.SaveAsync();
            return user;
        }

       

    }
}
