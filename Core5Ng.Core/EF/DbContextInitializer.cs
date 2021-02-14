using Core5Ng.Core.EF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core5Ng.Core.EF
{
    public class DbContextInitializer
    {
        public static async  Task InitializeDb(DataProtectionKeyContext dataProtectionKeyContext,
                                               ApplicationDbContext applicationDbContext,
                                               IFunctionalSvc functionalSvc)
        {
            await dataProtectionKeyContext.Database.EnsureCreatedAsync();
            await applicationDbContext.Database.EnsureCreatedAsync();

            // Check, if db contains any users. If db is not empty, then db has been already seeded
            if (applicationDbContext.ApplicationUsers.Any())
            {
                return;
            }

            // If empty create Admin User and App User
            await functionalSvc.CreateDefaultAdminUser();
            await functionalSvc.CreateDefaultUser();

        }
    }
}
