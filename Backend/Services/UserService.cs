using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Entities;

namespace Services
{
    public class UserService
    {
        private DBContext _context = new DBContext();
        Logger logger = new Logger();

        public bool AddModel(User _user)
        {
            try
            {
                _context.User.Add(_user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }
        public bool DeleteModel(int Id)
        {
            try
            {
                _context.User.Remove(FindUser("", "", Id));
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                logger.createLog("HATA " + exc.Message);
                return false;
            }
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            users = _context.User.ToList();

            return users;
        }
        public User FindUser(string Name = "", string Email = "", int UserId = 0)
        {
            User? user = new User();
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email))
                user = _context.User.FirstOrDefault(m => m.Name == Name && m.Email == Email);
            else if (UserId > 0)
            {
                user = _context.User.FirstOrDefault(m => m.UserId == UserId);
            }
            return user;
        }

        public void CreateLogin(APIAuthority loginUser)
        {
            _context.APIAuthority.Add(loginUser);
            _context.SaveChanges();
        }

        public APIAuthority GetLogin(APIAuthority loginUser)
        {
            APIAuthority? user = new APIAuthority();
            if (!string.IsNullOrEmpty(loginUser.UserName) && !string.IsNullOrEmpty(loginUser.Password))
            {
                user = _context.APIAuthority.FirstOrDefault(m => m.UserName == loginUser.UserName && m.Password == loginUser.Password);
            }

            return user;

        }
    }
}

