using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, string domain)
        {
            string adminEmail = "admin@admin.com";
            string password = "123_Secret";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail, Domain = domain };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                    await userManager.AddClaimAsync(admin, new System.Security.Claims.Claim("domain", admin.Domain));
                }
            }
            if (await userManager.FindByNameAsync("BigBoss") == null)
            {
                ApplicationUser bigBoss = new ApplicationUser { UserName = "BigBoss", Domain = "PRO-SAAS" };
                var result = await userManager.CreateAsync(bigBoss, "P@ssw0rd-1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(bigBoss, "admin");
                    await userManager.AddClaimAsync(bigBoss, new System.Security.Claims.Claim("domain", bigBoss.Domain));
                }
            }
            if (await userManager.FindByNameAsync("TestUser") == null)
            {
                ApplicationUser testUser = new ApplicationUser { UserName = "TestUser", Domain = "PRO-SAAS" };
                var result = await userManager.CreateAsync(testUser, "P@ssw0rd-1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "user");
                    await userManager.AddClaimAsync(testUser, new System.Security.Claims.Claim("domain", testUser.Domain));
                }
            }
            if (await userManager.FindByNameAsync("LookAtMe") == null)
            {
                ApplicationUser lookAtMe = new ApplicationUser { UserName = "LookAtMe", Domain = "SAAS" };
                var result = await userManager.CreateAsync(lookAtMe, "P@ssw0rd-1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(lookAtMe, "admin");
                    await userManager.AddClaimAsync(lookAtMe, new System.Security.Claims.Claim("domain", lookAtMe.Domain));
                }
            }
            if (await userManager.FindByNameAsync("TestUser1") == null)
            {
                ApplicationUser testUser1 = new ApplicationUser { UserName = "TestUser1", Domain = "SAAS" };
                var result = await userManager.CreateAsync(testUser1, "P@ssw0rd-1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser1, "user");
                    await userManager.AddClaimAsync(testUser1, new System.Security.Claims.Claim("domain", testUser1.Domain));
                }
            }
        }
    }
}
