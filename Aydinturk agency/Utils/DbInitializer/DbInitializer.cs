using Aydinturk_agency.Data;
using Aydinturk_agency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aydinturk_agency.Utils.DbInitializer
{

    public class DbInitializer : IDbInitializer
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Apply any pending migrations to the database
                if (_db.Database.GetPendingMigrations().Any())
                {
                    await _db.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while applying database migrations.", ex);
            }



            // Check if the 'Admin' role exists, and create it + customer role if it doesn't
            if (!await _roleManager.RoleExistsAsync(SD.Admin_Role))
            {
                try
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Admin_Role));
                    await _roleManager.CreateAsync(new IdentityRole(SD.Customer_Role));
                } catch (Exception ex)
                {
                    Console.WriteLine("role error" , ex.Message);
                }
            }



            // Check if the admin user exists, and create them if they don't
            if (await _userManager.FindByNameAsync(SD.AdminUsername) == null)
            {
                try
                {
                    var adminUser = new ApplicationUser
                    {
                        FullName = SD.AdminFullName,
                        UserName = SD.AdminUsername,
                        Location = SD.AdminLocation,
                        PhoneNumber = SD.AdminPhoneNumber,
                        Email = SD.AdminEmail,
                        AccountBalance = SD.AdminAccountBalance,
                        EmailConfirmed = true
                    };

                    await _userManager.CreateAsync(adminUser, SD.AdminPassword);

                    // Add the 'Admin' role to the admin user
                    await _userManager.AddToRoleAsync(adminUser, SD.Admin_Role);

                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("user error", ex.Message);
                }
            }
        }
    }

}

