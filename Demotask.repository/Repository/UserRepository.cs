using UserManage.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.entities.Data;
using UserManage.entities.Viewmodels;
using UserManage.entities.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace UserManage.repository.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly DemotaskContext _demotaskContext;

        public UserRepository(DemotaskContext demotaskContext) { _demotaskContext = demotaskContext; }

        //public Userview ListUser(string search, int pageNumber, int pageSize)
        //{
        //    Userview userview = new Userview();
        //    try
        //    {
        //        if (pageNumber == 0)
        //        {
        //            pageNumber = 1;
        //        }
        //        if (pageSize == 0)
        //        {
        //            pageSize = 2;
        //        }
                
        //        List<User> user;

        //        if (!string.IsNullOrEmpty(search))
        //        {
        //            user = _demotaskContext.Users.Where(x => x.FirstName.ToLower().Contains(search.ToLower()) ||
        //                x.LastName.ToLower().Contains(search.ToLower()) || x.Email.ToLower().Contains(search.ToLower()) && x.DeletedAt == null).ToList();
        //        }
        //        else
        //        {
        //            user = _demotaskContext.Users.Where(x => x.DeletedAt == null).ToList();
        //        }

        //        int totalCount = (int)Math.Ceiling((double)user.Count / pageSize);
        //        List<User> pagedUsers = user.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        //        userview.users = pagedUsers;
        //        userview.PageCount = totalCount;
        //        userview.PageSize = pageSize;
        //        userview.CurrentPage = pageNumber;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return userview;
        //}

        public Userview ListUser(string search, int pageNumber, int pageSize, string sort)

        {
            var output = new SqlParameter("@pageCount", SqlDbType.Int) { Direction = ParameterDirection.Output };

            List<UserAddview> user = _demotaskContext.Useradd.FromSqlInterpolated($"exec SPListUser @search={search},@pageNumber={pageNumber},@pageSize={pageSize},@pageCount={output} out").ToList();


            Userview userview = new Userview();
            userview.users = user;
            userview.PageCount = int.Parse(output.Value.ToString());
            userview.PageSize = pageSize;
            userview.CurrentPage = pageNumber;
            return userview;
        }

        public List<State> getState()
        {
            try
            {
                List<State> states = _demotaskContext.States.ToList();
                return states;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<City> getCities(int stateId)
        {
            try
            {

                var cities = _demotaskContext.Cities.Where(x => x.StateId == stateId).ToList();
                return cities;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        //public void InsertUser(User u)
        //{
        //    try
        //    {
        //        _demotaskContext.Add(u);
        //        _demotaskContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public void InsertUser(User u)
        {
            try
            {
                _demotaskContext.Database.ExecuteSqlInterpolated($@"EXEC SPInsertUser 
                {u.FirstName},
                {u.LastName},
                {u.Email},
                {u.Phone},
                {u.StreetAddress},
                {u.City},
                {u.State},
                {u.Username},
                {u.Password};
        ");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Boolean IsEmailAvailable(string email)
        {
            try
            {
                return _demotaskContext.Users.Any(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Boolean IsUserNameAvailable(string username)
        {
            try
            {
                return _demotaskContext.Users.Any(x => x.Username == username);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public User GetDataEdit(int id)
        //{
        //    try
        //    {

        //        User u = _demotaskContext.Users.Where(x => x.UserId == id).FirstOrDefault();

        //        return u;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        public User GetDataEdit(int id)
        {
            try
            {
                var user = _demotaskContext.Users
                    .FromSqlInterpolated($"EXEC SPGetDataEdit {id}")
                    .AsEnumerable()
                    .FirstOrDefault();

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //public void UpdateUser(UserAddview userAddview)
        //{
        //    try
        //    {
        //        User u = _demotaskContext.Users.Where(x => x.UserId == userAddview.UserId).FirstOrDefault();
        //        u.FirstName = userAddview.FirstName;
        //        u.LastName = userAddview.LastName;
        //        u.Email = userAddview.Email;
        //        u.Password = userAddview.Password;
        //        u.Phone = userAddview.Phone;
        //        u.State = userAddview.State;
        //        u.City = userAddview.City;
        //        u.StreetAddress = userAddview.StreetAddress;
        //        u.Username = userAddview.Username;
        //        _demotaskContext.Update(u);
        //        _demotaskContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}


        public void UpdateUser(UserAddview userAddview)
        {
            try
            {
                _demotaskContext.Database.ExecuteSqlInterpolated($@"
            EXEC SPUpdateUser
                {userAddview.UserId},
                {userAddview.FirstName},
                {userAddview.LastName},
                {userAddview.Email},
                {userAddview.Phone},
                {userAddview.StreetAddress},
                {userAddview.City},
                {userAddview.State},
                {userAddview.Username},
                {userAddview.Password};
        ");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeletUser(long UserId)
        {
            try
            {
                User user = _demotaskContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                if (user != null)
                {
                    user.DeletedAt = DateTime.Now;
                    _demotaskContext.Users.Update(user);
                    _demotaskContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
