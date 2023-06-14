
using Microsoft.AspNetCore.Mvc;
using UserManage.entities.Models;
using UserManage.entities.Viewmodels;
using UserManage.repository.Interface;

namespace Demotask.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
          
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Userlist()
        {

            return View();
        }
        public IActionResult Userlisting(string search, int pageNumber, int pageSize, string sort)
        {
            var userList = _userInterface.ListUser(search, pageNumber, pageSize, sort);

            return PartialView("_UserPartial", userList);
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        public IActionResult listOfState()
        {
            IEnumerable<State> states = _userInterface.getState();
            return Json(states);
        }
        [HttpGet]
        public IActionResult GetCities(int stateId)
        {

            IEnumerable<City> cities = _userInterface.getCities(stateId);

            return Json(cities);
        }
        //[HttpPost]
        //public IActionResult UserAdd(UserAddview userAddview)

        //{
        //    var emailExist = _userInterface.IsEmailAvailable(userAddview.Email);
        //    if (emailExist)
        //    {
        //        return Ok("email");
        //    }
        //    var usernameExist = _userInterface.IsUserNameAvailable(userAddview.Username);
        //    if (usernameExist)
        //    {
        //        return Ok("username");
        //    }
        //    else
        //    {
        //        User u = new User();
        //        u.FirstName = userAddview.FirstName;
        //        u.LastName = userAddview.LastName;
        //        u.Email = userAddview.Email;
        //        u.Password = userAddview.Password;
        //        u.Phone = userAddview.Phone;
        //        u.State = userAddview.State;
        //        u.City = userAddview.City;
        //        u.StreetAddress = userAddview.StreetAddress;
        //        u.Username = userAddview.Username;
        //        _userInterface.InsertUser(u);
        //        return Ok("add");
        //    }

        //}
        [HttpPost]
        public IActionResult UserAdd(UserAddview userAddview)
        {
            try
            {
                var emailExist = _userInterface.IsEmailAvailable(userAddview.Email);
                if (emailExist)
                {
                    return Ok("email");
                }

                var usernameExist = _userInterface.IsUserNameAvailable(userAddview.Username);
                if (usernameExist)
                {
                    return Ok("username");
                }
                else
                {
                    {
                        User u = new User();
                        u.FirstName = userAddview.FirstName;
                        u.LastName = userAddview.LastName;
                        u.Email = userAddview.Email;
                        u.Phone = userAddview.Phone;
                        u.StreetAddress = userAddview.StreetAddress;
                        u.City = userAddview.City;
                        u.State = userAddview.State;
                        u.Username = userAddview.Username;
                        u.Password = userAddview.Password;
                       _userInterface.InsertUser(u);
                       return Ok("add");
                    };


                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        public IActionResult UserEdit(int id)
        {
           
            var dataedit = _userInterface.GetDataEdit(id);
            return Json(dataedit);
        }
        [HttpPost]
        public IActionResult UserEdit(UserAddview userAddview)

        {
          _userInterface.UpdateUser(userAddview);
           return Ok("edit");
        }
        public IActionResult Userdelete(long UserId)
        {
            _userInterface.DeletUser(UserId);
            
            return Ok();

        }
    }
}
