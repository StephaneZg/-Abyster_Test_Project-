
using Abyster_Test_Project.Domain.Categorys;
using Abyster_Test_Project.Domain.Roles;
using Abyster_Test_Project.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Abyster_Test_Project.Data;

public class DatabaseSeeder {

    private DatabaseContext _context;

    public DatabaseSeeder(DatabaseContext context){
        _context =  context;
    }

    public void Seed(){

        if(!_context.Categories.Any()){
            var categories = new List<Category>(){
                new Category(){
                    libelle = "Debite"
                }, 
                new Category(){
                    libelle = "Credite"
                }
            };
            _context.Categories.AddRange(categories);
        }

        if(!_context.Roles.Any()){
            var roles = new List<Role>(){
                new Role(){
                    libelle = "Admin"
                }, 
                new Role(){
                    libelle = "User"
                }
            };
            _context.Roles.AddRange(roles);
        }
        
        // var admin = _context.Users.FirstAsync(user => user.emailAddress == "admin@admin.com");
        if(!_context.Users.Any()){
            var adminUser = new User(){
                firstName = "Administrator",
                lastName = "Administrator",
                emailAddress = "admin@admin.com",
                password = BCrypt.Net.BCrypt.HashPassword("Admin*1234")
            };
            _context.Users.Add(adminUser);
        }

        _context.SaveChanges();
    }
}