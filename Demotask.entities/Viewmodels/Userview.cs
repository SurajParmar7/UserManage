
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.entities.Models;

namespace UserManage.entities.Viewmodels
{
    public class Userview
    {
       // public List<UserAddview> users { get; set; }
        public List<User> users { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

    }
}
