using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        public static async Task SeedUsers(UserManager<User> userManager)
        {
            IdentityResult userResult;
            const string GeneralPassword = "12345678";
            List<User> users = new List<User>();
            users.Add(new User() { UserName = "lysyshyn@gmail.com", FirstName = "Admin", LastName = "Admin", Email = "lysyshyn@gmail.com", EmailConfirmed = true, Blocked = false, RoleId = 1 });
            users.Add(new Psychologist() { UserName = "komarov@gmail.com", FirstName = "Doctor", LastName = "Komarov", Email = "komarov@gmail.com", EmailConfirmed = true, Blocked = false, RoleId = 2, Gender = Gender.Male, Position = "Psyhologist", HireDate = DateTime.Now, PhoneNumber = "097-111-2568", Address = "Lviv, Ukraine, Shevchenka st." });
            users.Add(new Psychologist() { UserName = "kushnir@gmail.com", FirstName = "Doctor", LastName = "Kushnir", Email = "kushnir@gmail.com", EmailConfirmed = true, Blocked = false, RoleId = 2, Gender = Gender.Female, Position = "Psyhologist", HireDate = DateTime.Now, PhoneNumber = "068-654-9813", Address = "Lviv, Ukraine, Svobody st." });
            users.Add(new Psychologist() { UserName = "zavadska@gmail.com", FirstName = "Doctor", LastName = "Zavadska", Email = "zavadska@gmail.com", EmailConfirmed = true, Blocked = false, RoleId = 2, Gender = Gender.Female, Position = "Psyhologist", HireDate = DateTime.Now, PhoneNumber = "050-138-0901", Address = "Lviv, Ukraine, Universytetska st." });

            foreach (var user in users)
            {
                var userExist = await userManager.FindByEmailAsync(user.Email);
                if (userExist == null)
                {
                    userResult = await userManager.CreateAsync(user, GeneralPassword);
                    var add_role = await userManager.AddToRoleAsync(user, user.Role.Name);
                }
            }
        }

        public static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            string[] roleNames = { "Admin", "Psychologist", "AuthorizedUser", "AnonymousUser" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(role));
                }
            }
        }
    }
}
