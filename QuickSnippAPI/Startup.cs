using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QuickSnippAPI.Models;

[assembly: OwinStartup(typeof(QuickSnippAPI.Startup))]

namespace QuickSnippAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        // Method to create default user roles and admin for user login
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In first Startup, create first Admin role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // Create default user role!
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
