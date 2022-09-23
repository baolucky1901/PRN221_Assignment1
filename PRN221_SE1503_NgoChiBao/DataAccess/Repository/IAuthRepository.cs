using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IAuthRepository 
    {
        //abstract method (khong dinh nghia body)
        public Member checkLogin(string username, string password);
    }
}
