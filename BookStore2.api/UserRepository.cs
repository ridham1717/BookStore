using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore2.Models.ViewModels;
using BookStore2.Models.Models;

namespace BookStore2.Repository
{
    public class UserRepository : BaseRepository
    {
        
       public List<User> getUser()
        {
            return context.Users.ToList();
        }

        public List<Role> GetRoles()
        {
            return context.Roles.ToList();
        }
        public User Login(LoginModel model)
        {
            return context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()) && c.Password.Equals(model.password));
        }

        public User Register(RegisterModel model)

        {
            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Roleid = model.Roleid


            };

            var entry = context.Users.Add(user);
            context.SaveChanges();
            return entry.Entity;
        }
        
    }
}
