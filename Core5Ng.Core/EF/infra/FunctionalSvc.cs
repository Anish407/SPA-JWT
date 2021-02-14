using Core5Ng.Core.EF.Core;
using Core5Ng.Models.Config;
using Core5Ng.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Core5Ng.Common.Constants.Roles;

namespace Core5Ng.Core.EF.infra
{
    public class FunctionalSvc : IFunctionalSvc
    {
        public AdminUserOptions AdminUserOptions { get;}
        public AppUserOptions AppUserOptions { get;}
        public UserManager<ApplicationUser> UserManager { get; }
        public IHostEnvironment Environment { get; }

        public FunctionalSvc(IOptions<AdminUserOptions> adminOptions, 
                             IOptions<AppUserOptions> appoptions,
                             UserManager<ApplicationUser> userManager,
                             IHostEnvironment environment)
        {
            AppUserOptions = appoptions.Value;
            AdminUserOptions = adminOptions.Value;
            UserManager = userManager;
            Environment = environment;
        }


        public async Task CreateDefaultAdminUser()
        {
            try
            {
                var adminUser = new ApplicationUser
                {
                    Email = AdminUserOptions.Email,
                    UserName = AdminUserOptions.Username,
                    EmailConfirmed = true,
                    ProfilePic = await GetDefaultProfilePic(),
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    FirstName = AdminUserOptions.Firstname,
                    LastName = AdminUserOptions.Lastname,
                    UserRole = Admin,
                    IsActive = true,
                    Address = new List<Address>
                    {
                        new Address {Country = AdminUserOptions.Country, Type = "Billing"},
                        new Address {Country = AdminUserOptions.Country, Type = "Shipping"}
                    }
                };

                // create admin user 
                var result = await UserManager.CreateAsync(adminUser, AdminUserOptions.Password);

                if (result.Succeeded)
                {
                    // assign admin role
                    await UserManager.AddToRoleAsync(adminUser, Admin);
                    //Log.Information("Admin User Created {UserName}", adminUser.UserName);
                }
                else
                {
                    //var errorString = string.Join(",", result.Errors);
                    //Log.Error("Error while creating user {Error}", errorString);
                }

            }
            catch (Exception ex)
            {
                //Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                //   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }

        private async  Task<string> GetDefaultProfilePic()
        {
            try
            {
                return string.Empty;
                // Default Profile pic path
                // Create the Profile Image Path
                //var profPicPath = Environment.ContentRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}";
                //var defaultPicPath = Environment.ContentRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}default{Path.DirectorySeparatorChar}profile.jpeg";
                //var extension = Path.GetExtension(defaultPicPath);
                //var filename = DateTime.Now.ToString("yymmssfff");
                //var path = Path.Combine(profPicPath, filename) + extension;
                //var dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}", filename) + extension;

                //await using (Stream source = new FileStream(defaultPicPath, FileMode.Open))
                //{
                //    await using Stream destination = new FileStream(path, FileMode.Create);
                //    await source.CopyToAsync(destination);
                //}

                //return dbImagePath;

            }
            catch (Exception ex)
            {
                //Log.Error("{Error}", ex.Message);
            }

            return string.Empty;

        }

        public async Task CreateDefaultUser()
        {
            try
            {
                var appUser = new ApplicationUser
                {
                    Email = AppUserOptions.Email,
                    UserName = AppUserOptions.Username,
                    EmailConfirmed = true,
                    ProfilePic = await GetDefaultProfilePic(),
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    FirstName = AppUserOptions.Firstname,
                    LastName = AppUserOptions.Lastname,
                    UserRole = Customer,
                    IsActive = true,
                    Address = new List<Address>
                    {
                        new Address {Country = AppUserOptions.Country, Type = "Billing"},
                        new Address {Country = AppUserOptions.Country, Type = "Shipping"}
                    }
                };

                var result = await UserManager.CreateAsync(appUser, AppUserOptions.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(appUser, Customer);
                   // Log.Information("App User Created {UserName}", appUser.UserName);
                }
                else
                {
                    var errorString = string.Join(",", result.Errors);
                    //Log.Error("Error while creating user {Error}", errorString);
                }

            }
            catch (Exception ex)
            {
                //Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                //   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }


    
    }
}
