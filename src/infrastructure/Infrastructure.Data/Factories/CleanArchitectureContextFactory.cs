using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Factories
{
    public class WhiteLabelAPIContextFactory : IDesignTimeDbContextFactory<WhiteLabelAPIContext>
    {
        public WhiteLabelAPIContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WhiteLabelAPIContext>();
#if DEBUG
            builder.UseSqlServer("Server=tcp:<database_server>,1433;Initial Catalog=bd_fsms_dev;Persist Security Info=False;User ID=<username>;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;",
            opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(3000).TotalSeconds));
#else
            builder.UseSqlServer("Server=tcp:<database_server>,1433;Initial Catalog=bd_fsms_dev;Persist Security Info=False;User ID=<username>;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;",
            opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(3000).TotalSeconds));
#endif
            return new WhiteLabelAPIContext(builder.Options);
        }
    }
}
