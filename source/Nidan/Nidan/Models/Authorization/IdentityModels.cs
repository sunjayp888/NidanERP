using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Nidan.Models.Authorization
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public int? OrganisationId { get; set; }
        public int? PersonnelId { get; set; }
        public int? CentreId { get; set; }

    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name)
            : base(name)
        {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString(), throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        static ApplicationDbContext()
        {
            //Database.SetInitializer<ApplicationDbContext>(null);
            Database.SetInitializer(new CustomInitializer());
            Create().Database.Initialize(true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var user = modelBuilder.Entity<ApplicationUser>();

            user.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(256)
               .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true, Order = 1 }));

            user.Property(u => u.OrganisationId)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true, Order = 2 }));
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as ApplicationUser;

                if (user != null)
                {
                    if (Users.Any(u => string.Equals(u.UserName, user.UserName) && u.OrganisationId == user.OrganisationId))
                        errors.Add(new DbValidationError("User", string.Format("Username {0} is already taken for OrganisationId {1}", user.UserName, user.OrganisationId)));

                    if (Users.Any(u => string.Equals(u.Email, user.Email) && u.OrganisationId == user.OrganisationId))
                        errors.Add(new DbValidationError("User", string.Format("Email Address {0} is already taken for OrganisationId {1}", user.UserName, user.OrganisationId)));
                }
                else
                {
                    var role = entityEntry.Entity as IdentityRole;

                    if (role != null && this.Roles.Any(r => string.Equals(r.Name, role.Name)))
                        errors.Add(new DbValidationError("Role", string.Format("Role {0} already exists", role.Name)));
                }

                if (errors.Any())
                    return new DbEntityValidationResult(entityEntry, errors);
            }

            return new DbEntityValidationResult(entityEntry, new List<DbValidationError>());
        }


        public class CustomInitializer : IDatabaseInitializer<ApplicationDbContext>
        {
            public void InitializeDatabase(ApplicationDbContext context)
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context), null);
                var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

                if (!userManager.Users.Any(u => u.UserName == "superadmin@nidantech.com"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "superadmin@nidantech.com",
                        Email = "superadmin@nidantech.com",
                        EmailConfirmed = true,
                    };

                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "SuperAdmin").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });

                    userManager.Create(user, "Inland12!");
                }

                if (!userManager.Users.Any(u => u.UserName == "admin1@itsupportlimited.com"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin1@itsupportlimited.com",
                        Email = "admin1@itsupportlimited.com",
                        EmailConfirmed = true,
                        OrganisationId = 1
                    };

                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });

                    userManager.Create(user, "Inland12!");
                }

                if (!userManager.Users.Any(u => u.UserName == "admin2@itsupportlimited.com"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin2@itsupportlimited.com",
                        Email = "admin2@itsupportlimited.com",
                        EmailConfirmed = true,
                        OrganisationId = 2
                    };

                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });

                    userManager.Create(user, "Inland12!");
                }

                if (!userManager.Users.Any(u => u.UserName == "admindevuk@itsupportlimited.com"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admindevuk@itsupportlimited.com",
                        Email = "admindevuk@itsupportlimited.com",
                        EmailConfirmed = true,
                        OrganisationId = 3
                    };

                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });

                    userManager.Create(user, "Inland12!");
                }
                if (!userManager.Users.Any(u => u.UserName == "admindevmumbai@nidantech.com"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admindevmumbai@nidantech.com",
                        Email = "admindevmumbai@nidantech.com",
                        EmailConfirmed = true,
                        OrganisationId = 4
                    };

                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });

                    userManager.Create(user, "Inland12!");
                }

                if (!userManager.Users.Any(u => u.UserName == "employee@itsupportlimited.com"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "employee@itsupportlimited.com",
                        Email = "employee@itsupportlimited.com",
                        EmailConfirmed = true,
                    };
                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "User").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });
                    userManager.Create(user, "Inland12!");
                }
            }
        }
    }
}