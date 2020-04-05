using Core.Entities.Security;
using Infrastructure.Data.DbMapping.Security;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Infrastructure.Data
{
    public sealed class WhiteLabelAPIContext : DbContext, IWhiteLabelAPIContext
    {
        public WhiteLabelAPIContext(DbContextOptions options)
            : base(options)
        {
            DataBase = Database;
        }

        //Security
        public DbSet<User> Users { get; set; }


        public DatabaseFacade DataBase { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Security
            modelBuilder.MapUser();
        }
    }
}
