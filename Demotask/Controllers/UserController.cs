﻿
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
        public IActionResult Userlisting(string search, int pageNumber, int pageSize)
        {
            var userList = _userInterface.ListUser(search, pageNumber, pageSize);

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
        [HttpPost]
        public IActionResult UserAdd(UserAddview userAddview)

        {
            var emailExist = _userInterface.IsEmailAvailable(userAddview.Email);
            if (emailExist)
            {
                //ModelState.AddModelError("email", "Email already exists");
                //return View("UserAdd");
                return Ok("email");

            }
            var usernameExist = _userInterface.IsUserNameAvailable(userAddview.Username);
            if (usernameExist)
            {
                //ModelState.AddModelError("username", "Username already exists");
                //return View("UserAdd");
                return Ok("username");
            }
            else
            {
                User u = new User();
                u.FirstName = userAddview.FirstName;
                u.LastName = userAddview.LastName;
                u.Email = userAddview.Email;
                u.Password = userAddview.Password;
                u.Phone = userAddview.Phone;
                u.State = userAddview.State;
                u.City = userAddview.City;
                u.StreetAddress = userAddview.StreetAddress;
                u.Username = userAddview.Username;
                _userInterface.InsertUser(u);
                return Ok("add");
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