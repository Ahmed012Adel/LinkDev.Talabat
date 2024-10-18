using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructrure.Persistence.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructrure.Persistence._Identity
{
    internal class StoreIdentityDbIntializer(StoreIdentityDbContext dbContext , UserManager<ApplicationUser> userManager) : DbIntializer(dbContext) ,IStoreIdentityDbIntializer
    {
        public override async Task DataSeedAsync()
        {
            var user = new ApplicationUser()
            {
                DisplayName = "Ahmed Adel",
                UserName = "Ahmed.Adel",
                Email = "ahmedadel@gmail.com",
                PhoneNumber = "1234567890",
            };
            await userManager.CreateAsync(user , "P@ssw0rd");
        }
    }
}
