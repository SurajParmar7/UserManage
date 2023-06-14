
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.entities.Models;
using UserManage.entities.Viewmodels;

namespace UserManage.repository.Interface
{
    public interface IUserInterface
    {

        public Userview ListUser(string search, int pageNumber, int pageSize,string sort);
        public List<State> getState();
        public List<City> getCities(int stateId);
        public void InsertUser(User u);
        public Boolean IsEmailAvailable(string email);
        public User GetDataEdit(int id);
        public void UpdateUser(UserAddview userAddview);
        public void DeletUser(long UserId);
        public Boolean IsUserNameAvailable(string username);

    }
}
