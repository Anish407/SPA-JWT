using System;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core5Ng.Core.EF
{
   public  class DataProtectionKeyContext : DbContext, IDataProtectionKeyContext
    {
        public DataProtectionKeyContext(DbContextOptions<DataProtectionKeyContext> options)
                    : base(options) { }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
