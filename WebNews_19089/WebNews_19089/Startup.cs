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
            if (!roleManager.RoleExists("Jornalist")) {

                var role = new IdentityRole {

                    Name = "Jornalist"

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

            // Criar um utilizador 'Administrador'
            var user = new ApplicationUser {

                UserName = "admin@mail.pt",
                Email = "admin@mail.pt"

            };

            string userPWD = "123qwe!";
            var chkUser = userManager.Create(user, userPWD);

            // Adicionar o Utilizador à respetiva Role-Agentes-
            if (chkUser.Succeeded) {
                var result1 = userManager.AddToRole(user.Id, "Admin");
            }

        }
    }
}
