using System;
using System.Collections.Generic;
using System.Text;
using MadPay724.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MadPay724.Data.DatabaseContext
{
    class MadPayDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=MadPay724db;Integrated Security=True;MultipleActiveResultSets=true;");
        }

        public DbSet<User> Users  { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<BankCard> BankCards { get; set; }


    }
}
