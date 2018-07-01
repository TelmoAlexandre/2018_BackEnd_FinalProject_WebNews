using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebNews_19089.Models;

[assembly: OwinStartupAttribute(typeof(WebNews_19089.Startup))]
namespace WebNews_19089
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Inicializar a aplicação
            inicAplication();
        }

        private void inicAplication() {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // Criar a Role 'Jornalista'
            if (!roleManager.RoleExists("Journalist")) {

                var role = new IdentityRole {

                    Name = "Journalist"

                };

                roleManager.Create(role);
            }

            // Criar a Role 'Editor de redação'
            if (!roleManager.RoleExists("NewsEditor")) {

                var role = new IdentityRole {

                    Name = "NewsEditor"

                };

                roleManager.Create(role);
            }

            // Criar a Role 'Editor de redação'
            if (!roleManager.RoleExists("Admin")) {

                var role = new IdentityRole {

                    Name = "Admin"

                };

                roleManager.Create(role);
            }

            // Criar um administrador
            var admin = new ApplicationUser {

                UserName = "admin@mail.com",
                Email = "admin@mail.com"
            };
            
            // Adicionar o administrador ao role
            if (userManager.Create(admin, "Qwe123!").Succeeded) {

                userManager.AddToRole(admin.Id, "Admin");
            }

            // Criar um editor de redação
            var editor = new ApplicationUser {

                UserName = "editor@mail.com",
                Email = "editor@mail.com"
            };

            // Adicionar o seu role
            if(userManager.Create(editor, "Qwe123!").Succeeded) {

                userManager.AddToRole(editor.Id, "NewsEditor");
            }

            // Criar um jornalista
            var journalist1 = new ApplicationUser {

                UserName = "journalist1@mail.com",
                Email = "journalist1@mail.com"
            };

            // Adicionar o seu role
            if (userManager.Create(journalist1, "Qwe123!").Succeeded) {

                userManager.AddToRole(journalist1.Id, "Journalist");
            }

            // Criar um jornalista
            var journalist2 = new ApplicationUser {

                UserName = "journalist2@mail.com",
                Email = "journalist2@mail.com"
            };

            // Adicionar o seu role
            if (userManager.Create(journalist2, "Qwe123!").Succeeded) {

                userManager.AddToRole(journalist2.Id, "Journalist");
            }

            // Criar um jornalista
            var journalist3 = new ApplicationUser {

                UserName = "journalist3@mail.com",
                Email = "journalist3@mail.com"
            };

            // Adicionar o seu role
            if (userManager.Create(journalist3, "Qwe123!").Succeeded) {

                userManager.AddToRole(journalist3.Id, "Journalist");
            }

        }
    }
}
