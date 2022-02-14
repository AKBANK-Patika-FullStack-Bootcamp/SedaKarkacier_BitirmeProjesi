using DAL.Model;
using Entities;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        List<User> userList = new List<User>();
        Result _result = new Result();
        UserService userService = new UserService();

        [HttpGet]
        public List<User> GetUser()
        {

            return userService.GetUsers();
        }

        [HttpGet("/User/GetUserPaging")]
        public IActionResult GetUserPaging([FromQuery] OwnerParameters ownerParameters)
        {
            var owners = userService.GetUsers() 
                                                
           .Skip(ownerParameters.PageNumber) 
           .Take(ownerParameters.PageSize) 
           .ToList();

            return Ok(owners);
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            List<User> userList = new List<User>();

            User? resultObject = new User();
            resultObject = userList.Find(x => x.UserId == id);
            return resultObject;

        }

        [HttpPost]
        public Result Post(User user)
        {
            User usr = userService.FindUser(user.Name, user.Email);
            //Yeni eleman listede var mı ? 
            bool userCheck = (usr != null) ? true : false;

            if (userCheck == false)
            {
                //Listeye yeni eleman ekleniyor.
                if (userService.AddModel(user) == true)
                {
                    _result.status = 1;
                    _result.Message = "Yeni eleman listeye eklendi.";
                }
                else
                {
                    _result.status = 0;
                    _result.Message = "Hata, kullanıcı eklenemedi.";
                }

            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu eleman listede zaten var.";
            }

            return _result;
        }

        [HttpPut("{UserId}")]
        public Result Update(int UserId, User newValue)
        {

            //Kullanıcı güncelleme işlemi yapılır.
            User? _oldValue = userList.Find(o => o.UserId == UserId);
            if (_oldValue != null)
            {
                userList.Add(newValue);
                userList.Remove(_oldValue);

                _result.status = 1;
                _result.Message = "Kullanıcı bilgileri başarıyla güncellendi";
                _result.UserList = userList;
            }
            else
            {
                _result.status = 0;
                _result.Message = "Bu kullanıcıyı içerde bulamadık.";
            }
            return _result;
        }

        [HttpDelete("{UserId}")]
        public Result Delete(int UserId)
        {
            if (userService.DeleteModel(UserId))
            {
                _result.status = 1;
                _result.Message = "Kullanıcı silindi";
                _result.UserList = userList;
            }
            else
            {
                _result.status = 0;
                _result.Message = "Kullanıcı zaten silinmişti.";
            }
            return _result;
        }
    }
}
